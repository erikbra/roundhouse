namespace roundhouse.parameters
{
    public interface IParameter<out T>
    {
        T underlying_type {get;}
        string name {get;}
        object value{get;}
    }
}