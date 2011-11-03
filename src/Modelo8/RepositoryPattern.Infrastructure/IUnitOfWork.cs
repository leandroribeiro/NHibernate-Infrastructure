using System;

namespace RepositoryPattern.Infrastructure
{
	public interface IUnitOfWork : IDisposable
	{
		void Commit();
		void Rollback();
	}
}
