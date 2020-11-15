namespace roundhouse.infrastructure.persistence
{
    using System;
    using NHibernate;
    using NHibernate.Cfg;

    public sealed class Repository : IRepository
    {
        private bool running_in_a_transaction;

        public ISessionFactory session_factory { get; private set; }
        public Configuration nhibernate_configuration { get; private set; }
        public ITransaction transaction { get; private set; }
        private ISession session
        {
            get;
            set;
        }

        public Repository(ISessionFactory session_factory, Configuration cfg)
        {
            this.session_factory = session_factory;
            this.nhibernate_configuration = cfg;
            if (session_factory == null)
            {
                throw new ApplicationException("Repository cannot do any with a null session factory. Please provide a session factory.");
            }
        }

        public void start(bool using_transaction)
        {
            running_in_a_transaction = using_transaction;
            session = session_factory.OpenSession();
            if (using_transaction)
            {
                transaction = session.BeginTransaction();
            }
        }

        public void rollback()
        {
            if (running_in_a_transaction)
            {
                transaction.Rollback();
            }
            running_in_a_transaction = false;

            finish();
        }

        public void finish()
        {
            if (session != null && session.IsOpen)
            {
                if (running_in_a_transaction)
                {
                    transaction.Commit();
                }

                if (session == null) return;

                session.Close();
                session.Dispose();
                
            }
            session = null;
        }
    }
}