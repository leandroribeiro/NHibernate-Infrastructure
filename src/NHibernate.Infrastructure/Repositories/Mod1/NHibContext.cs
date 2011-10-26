

namespace NHibernate.Infrastructure.Repositories.Mod1 {
    /// <summary>
    /// A NHibernate-context object which operates on a single-database.
    /// Offers functionality for creating UnitOfWorks for use in e.g
    /// Repositories that uses NHibernate.
    /// To create a custom Context implementation, inherit from this
    /// class and ensure that SessionFactory is assigned in cTor.
    /// </summary>
    public abstract class NHibContext {
        protected ISessionFactory SessionFactory { get; set; }

        /// <summary>
        /// Creates an new UnitOfWork.
        /// </summary>
        /// <returns></returns>
        public NHibUnitOfWork CreateUnitOfWork() {
            return new NHibUnitOfWork(SessionFactory.OpenSession());
        }
    }

}
