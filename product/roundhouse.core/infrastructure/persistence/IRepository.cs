namespace roundhouse.infrastructure.persistence
{
    using NHibernate.Cfg;

    public interface IRepository
    {
        void start(bool using_transaction);
        void rollback();
        void finish();

        void save_or_update<T>(T item) where T : class;

        Configuration nhibernate_configuration { get; }
        //string connection_string { get; }
    }
}