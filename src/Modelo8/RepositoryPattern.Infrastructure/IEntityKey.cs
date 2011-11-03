
namespace RepositoryPattern.Infrastructure
{
	public interface IEntityKey<TKey>
	{
		TKey Id { get; }
	}
}
