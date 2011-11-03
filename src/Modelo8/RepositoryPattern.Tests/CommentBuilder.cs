using System;
using RepositoryPattern.DomainModel;

namespace RepositoryPattern.Tests
{
	public class CommentBuilder
	{
		int _id;
		string _email;
		DateTime _commentDate;
		string _text;
		int _rating;
		BlogPost _blogPost;

		
	  
		public CommentBuilder Id(int id)
		{
			_id= id;
			return this;
		}     
 
		
		public CommentBuilder Email(string email)
		{
			_email= email;
			return this;
		}     
		
		public CommentBuilder CommentDate(DateTime commentDate)
		{
			_commentDate= commentDate;
			return this;
		}     
		
		public CommentBuilder Text(string text)
		{
			_text= text;
			return this;
		}     
		
		public CommentBuilder Rating(int rating)
		{
			_rating= rating;
			return this;
		}     
		
		public CommentBuilder BlogPost(BlogPost blogPost)
		{
			_blogPost= blogPost;
			return this;
		}

		public Comment Build()
		{
			return new Comment
			{
				Id = _id,
				Email = _email,
				CommentDate = _commentDate,
				Text = _text,
				Rating = _rating,
				BlogPost = _blogPost
			};
		}

	}
}
