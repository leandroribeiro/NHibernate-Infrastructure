using FluentNHibernate.Mapping;
using RepositoryPattern.DomainModel;

namespace RepositoryPattern.Data.Orm.nHibernate.Map
{
	public class CommentMap : ClassMap<Comment>
	{
		public CommentMap()
		{
			Id(c => c.Id);
			Map(c => c.Email);
			Map(c => c.CommentDate);
			Map(c => c.Text);
			Map(c => c.Rating);
			References<BlogPost>(c => c.BlogPost);
		}
	}
}
