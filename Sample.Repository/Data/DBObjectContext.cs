using Microsoft.EntityFrameworkCore;
using Sample.DataContract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Sample.RepositoryContract;

namespace Sample.Repository.Data
{
    public class DBObjectContext : DbContext,IDbContext
    {
        //private readonly string _connectionString;
        //protected DBObjectContext(string connectionString)
        //{
        //    _connectionString = connectionString;
        //}
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer(_connectionString);
        //    }
        //}

        public DBObjectContext(DbContextOptions<DBObjectContext> options)
       : base(options)
        { }
        public DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetInterfaces()
             .Any(gi => gi.IsGenericType && gi.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))).ToList();

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);
        }
        protected virtual TEntity AttachEntityToContext<TEntity>(TEntity entity) where TEntity : BaseEntity, new()
        {
            if (entity.Id > 0)
            {
                var alreadyAttached = Set<TEntity>().Local.Where(x => x.Id == entity.Id).FirstOrDefault();
                if (alreadyAttached == null)
                {
                    //attach new entity
                    Set<TEntity>().Attach(entity);
                    return entity;
                }
                else
                {
                    //entity is already loaded.
                    return alreadyAttached;
                }
            }
            else
            {
                return entity;
            }
        }
        public new DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }
        public IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters) where TEntity : BaseEntity, new()
        {
            var connection = Database.GetDbConnection();

            //open the connection for use
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            //create a command object
            using (var cmd = connection.CreateCommand())
            {
                //command to execute
                cmd.CommandText = commandText;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 1000;

                // move parameters to command object
                if (parameters != null)
                    foreach (var p in parameters)
                    {
                        if (p != null)
                            cmd.Parameters.Add(p);
                    }

                IList<TEntity> result;
                //database call
                using (var reader = cmd.ExecuteReader())
                {
                    result = reader.Translate<TEntity>().ToList();
                    for (int i = 0; i < result.Count; i++)
                        result[i] = AttachEntityToContext(result[i]);
                }
                //close connection finally if open.(DS)
                if (connection.State == ConnectionState.Open) { connection.Close(); }
                return result;
            }
        }
        public int ExecuteStoredProcedureNonQuery(string commandText, params object[] parameters)
        {
            var connection = Database.GetDbConnection();
            //Don't close the connection after command execution

            //open the connection for use
            if (connection.State == ConnectionState.Closed)
                connection.Open();

            //create a command object
            using (var cmd = connection.CreateCommand())
            {
                //command to execute
                cmd.CommandText = commandText;
                cmd.CommandType = CommandType.StoredProcedure;

                // move parameters to command object
                if (parameters != null)
                    foreach (var p in parameters)
                    {
                        if (p != null)
                            cmd.Parameters.Add(p);
                    }

                var rowseffected = cmd.ExecuteNonQuery();
                //close connection finally if open.(DS)
                if (connection.State == ConnectionState.Open) { connection.Close(); }
                return rowseffected;
            }
        }
        public IList<TEntity> ExecuteStoredProcedureSingleList<TEntity>(string commandText, params object[] parameters) where TEntity : new()
        {
            IList<TEntity> entity;
            var connection = Database.GetDbConnection();
            //Don't close the connection after command execution

            //open the connection for use
            if (connection.State == ConnectionState.Closed) { connection.Open(); }

            //create a command object
            using (var cmd = connection.CreateCommand())
            {
                //command to execute
                cmd.CommandText = commandText;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 1000;

                if (parameters != null)
                {
                    int i = 0;
                    // move parameters to command object
                    foreach (var p in parameters)
                    {
                        if (p != null)
                        {
                            cmd.Parameters.Add(p);
                        }
                    }
                }
                using (var reader = cmd.ExecuteReader())
                {
                    entity = reader.Translate<TEntity>();
                }
                //close connection finally if open.(DS)
                if (connection.State == ConnectionState.Open) { connection.Close(); }

            }
            return entity;
        }
        public TEntity ExecuteStoredProcedureMultipleList<TEntity>(string commandText, params object[] parameters) where TEntity : new()
        {
            TEntity entity;
            var connection = Database.GetDbConnection();
            //Don't close the connection after command execution

            //open the connection for use
            if (connection.State == ConnectionState.Closed) { connection.Open(); }

            //create a command object
            using (var cmd = connection.CreateCommand())
            {
                //command to execute
                cmd.CommandText = commandText;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 1000;

                if (parameters != null)
                {
                    int i = 0;
                    // move parameters to command object
                    foreach (var p in parameters)
                    {
                        if (p != null)
                        {
                            cmd.Parameters.Add(p);
                        }
                    }
                }
                using (var reader = cmd.ExecuteReader())
                {
                    entity = reader.TranslateMulti<TEntity>();
                }
                //close connection finally if open.(DS)
                if (connection.State == ConnectionState.Open) { connection.Close(); }

            }
            return entity;
        }

        #region Output Parametes

        public IList<TEntity> ExecuteStoredProcedureListWithOutput<TEntity>(string commandText, int totalOutputParams, out object[] output, params object[] parameters) where TEntity : BaseEntity, new()
        {
            totalOutputParams = totalOutputParams == 0 ? 1 : totalOutputParams;
            var connection = Database.GetDbConnection();
            //Don't close the connection after command execution

            //open the connection for use
            if (connection.State == ConnectionState.Closed) { connection.Open(); }

            //create a command object
            using (var cmd = connection.CreateCommand())
            {
                //command to execute
                cmd.CommandText = commandText;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 1000;

                bool hasOutputParameters = false;
                DbParameter[] OutputParam = new DbParameter[totalOutputParams];
                if (parameters != null)
                {
                    int i = 0;
                    // move parameters to command object
                    foreach (var p in parameters)
                    {
                        if (p != null)
                        {
                            cmd.Parameters.Add(p);
                            var outputP = p as DbParameter;
                            if (outputP == null) { continue; }

                            if (outputP.Direction == ParameterDirection.InputOutput || outputP.Direction == ParameterDirection.Output) { hasOutputParameters = true; OutputParam[i] = outputP; i++; }
                        }
                    }
                }
                IList<TEntity> result;
                using (var reader = cmd.ExecuteReader())
                {
                    result = reader.Translate<TEntity>().ToList();

                    for (int i = 0; i < result.Count; i++)
                        result[i] = AttachEntityToContext(result[i]);

                    reader.NextResult();
                    output = new object[totalOutputParams]; output[0] = 0;
                    if (hasOutputParameters)
                    {
                        //Access output variable
                        for (int res = 0; res < totalOutputParams; res++) { output[res] = (OutputParam[res].Value == null) ? 0 : OutputParam[res].Value; }
                    }
                }
                //close connection finally if open.(DS)
                if (connection.State == ConnectionState.Open) { connection.Close(); }
                return result;
            }
        }

        public int ExecuteStoredProcedureNonQueryWithOutput(string commandText, int totalOutputParams, out object[] outputs, params object[] parameters)
        {
            totalOutputParams = totalOutputParams == 0 ? 1 : totalOutputParams;

            var connection = Database.GetDbConnection();
            //Don't close the connection after command execution

            //open the connection for use
            if (connection.State == ConnectionState.Closed)
                connection.Open();

            //create a command object
            using (var cmd = connection.CreateCommand())
            {
                //command to execute
                cmd.CommandText = commandText;
                cmd.CommandType = CommandType.StoredProcedure;

                bool hasOutputParameters = false;
                DbParameter[] OutputParam = new DbParameter[totalOutputParams];
                if (parameters != null)
                {
                    int i = 0;
                    // move parameters to command object
                    foreach (var p in parameters)
                    {
                        if (p != null)
                        {
                            cmd.Parameters.Add(p);
                            var outputP = p as DbParameter;
                            if (outputP == null) { continue; }

                            if (outputP.Direction == ParameterDirection.InputOutput || outputP.Direction == ParameterDirection.Output) { hasOutputParameters = true; OutputParam[i] = outputP; i++; }
                        }
                    }
                }

                var rowseffected = cmd.ExecuteNonQuery();
                outputs = new object[totalOutputParams]; outputs[0] = 0;
                if (hasOutputParameters)
                {
                    //Access output variable
                    for (int res = 0; res < totalOutputParams; res++) { outputs[res] = (OutputParam[res].Value == null) ? 0 : OutputParam[res].Value; }
                }
                //close connection finally if open.(DS)
                if (connection.State == ConnectionState.Open) { connection.Close(); }
                return rowseffected;
            }
        }

        #endregion

    }
}