﻿using Microsoft.EntityFrameworkCore;
using Sample.DataContract;
using System.Collections.Generic;

namespace Sample.RepositoryContract
{
    public interface IDbContext
    {
        
        DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;

        IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters)
            where TEntity : BaseEntity, new();

        int ExecuteStoredProcedureNonQuery(string commandText, params object[] parameters);

        #region Output Parametes
        /// <summary>
        /// Execute StoredProcedure List with Output parameters
        /// </summary>
        /// <typeparam name="TEntity">TEntity</typeparam>
        /// <param name="commandText">Command Text or store procedure name</param>
        /// <param name="totalOutputParams">Total number of Output parameters</param>
        /// <param name="output">params object[] DBParameters</param>
        /// <param name="parameters"></param>
        /// <returns>>IList TEntity</returns>
        IList<TEntity> ExecuteStoredProcedureListWithOutput<TEntity>(string commandText, int totalOutputParams, out object[] output, params object[] parameters)
            where TEntity : BaseEntity, new();

        /// <summary>
        /// Execute Stored Procedure Non Query with Output parameters
        /// </summary>
        /// <param name="commandText">Command Text or store procedure name</param>
        /// <param name="totalOutputParams">Total number of Output parameters</param>
        /// <param name="outputs">out object[] outputs</param>
        /// <param name="parameters">params object[] parameters</param>
        /// <returns>Integer value</returns>
        int ExecuteStoredProcedureNonQueryWithOutput(string commandText, int totalOutputParams, out object[] outputs, params object[] parameters);
        #endregion
    }
}
