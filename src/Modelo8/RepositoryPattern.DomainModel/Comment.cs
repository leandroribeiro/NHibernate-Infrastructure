using System;
using RepositoryPattern.Infrastructure;

namespace RepositoryPattern.DomainModel
{
	public class Comment : IEntityKey<int>
	{
		public virtual int Id { get; set; }
		public virtual string Email { get; set; }
		public virtual DateTime CommentDate { get; set; }
		public virtual string Text { get; set; }
		public virtual int Rating { get; set; }
		public virtual BlogPost BlogPost { get; set; }
	}
}
