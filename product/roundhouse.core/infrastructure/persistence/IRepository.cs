namespace roundhouse.infrastructure.persistence
{
    using NHibernate.Cfg;

    public interface IRepository
    {
        Configuration nhibernate_configuration { get; }
        //string connection_string { get; }
    }
}