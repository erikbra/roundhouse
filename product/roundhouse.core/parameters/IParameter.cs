using System.Data;

namespace roundhouse.parameters
{
    public interface IParameter
    {
        IDbDataParameter underlying_type {get;}
        string name {get;}
        object value{get;}
    }
}