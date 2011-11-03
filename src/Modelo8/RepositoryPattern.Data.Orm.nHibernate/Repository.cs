using System;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using RepositoryPattern.Infrastructure;

namespace RepositoryPattern.Data.Orm.nHibernate
{
	public class Repository<TKey, T> : IPersistRepository<T>,
		IReadOnlyRepository<TKey, T> where T : class, IEntityKey<TKey>
	{
		private readonly ISession _session;
		public Repository(ISession session)
		{
			_session = session;
		}

		public bool Add(T entity)
		{
			_session.Save(entity);
			return true;
		}

		public bool Add(System.Collections.Generic.IEnumerable<T> items)
		{
			foreach (T item in items)
			{
				_session.Save(item);
			}
			return true;
		}

		public bool Update(T entity)
		{
			_session.Update(entity);
			return true;
		}

		public bool Delete(T entity)
		{
			_session.Delete(entity);
			return true;
		}

		public bool Delete(System.Collections.Generic.IEnumerable<T> entities)
		{
			foreach (T entity in entities)
			{
				_session.Delete(entity);
			}
			return true;
		}

		public IQueryable<T> All()
		{
			return _session.Query<T>();
		}

		public T FindBy(System.Linq.Expressions.Expression<Func<T, bool>> expression)
		{
			return FilterBy(expression).SingleOrDefault();
		}

		public IQueryable<T> FilterBy(System.Linq.Expressions.Expression<Func<T, bool>> expression)
		{
			return All().Where(expression).AsQueryable();
		}

		public T FindBy(TKey id)
		{
			return _session.Get<T>(id);
		}
	}
}
