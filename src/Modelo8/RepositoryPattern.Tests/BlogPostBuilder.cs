using System;
using System.Collections.Generic;
using RepositoryPattern.DomainModel;

namespace RepositoryPattern.Tests
{
	public class BlogPostBuilder
	{
		int _id;
		string _title;
		string _subTitle;
		string _text;
		DateTime _publicationDate;
		string _authorName;
		IList<Comment> _comments;

		public BlogPostBuilder Id(int id)
		{
			_id = id;
			return this;
		}

		public BlogPostBuilder Title(string title)
		{
			_title = title;
			return this;
		}

		
		public BlogPostBuilder SubTitle(string subTitle)
		{
			_subTitle= subTitle;
			return this;
		}     

		
		public BlogPostBuilder Text(string text)
		{
			_text= text;
			return this;
		}     
		
		public BlogPostBuilder PublicationDate(DateTime publicationDate)
		{
			_publicationDate= publicationDate;
			return this;
		}     
		
		public BlogPostBuilder AuthorName(string authorName)
		{
			_authorName= authorName;
			return this;
		}     

		
		public BlogPostBuilder Comments(IList<Comment> comments)
		{
			_comments= comments;
			return this;
		}

		public BlogPost Build()
		{
			var post = new BlogPost
			{
				Id = _id,
				Title = _title,
				SubTitle = _subTitle,
				Text = _text,
				PublicationDate = _publicationDate,
				AuthorName = _authorName
			};
			if (_comments!= null)
				foreach (var comment in _comments)
				{
					post.AddCommant(comment);
				}
			return post;
		}
	}
}
