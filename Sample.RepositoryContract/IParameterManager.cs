using System.Data;

namespace Sample.RepositoryContract
{
    public interface IParameterManager
    {
        IDataParameter Get(object value);
        IDataParameter Get(string paramName, object value);
        IDataParameter Get(string paramName, object value, ParameterDirection direction);
        IDataParameter Get(string paramName, object value, ParameterDirection direction, DbType type);
        IDataParameter GetNew(string paramName, object value, ParameterDirection direction, SqlDbType type);
    }
}

