using System;
using System.Linq;
using FluentNHibernate;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NUnit.Framework;
using RepositoryPattern.Data.Orm.nHibernate;
using RepositoryPattern.Data.Orm.nHibernate.Map;
using RepositoryPattern.DomainModel;

namespace RepositoryPattern.Tests
{
	[TestFixture]
	public class BlogPostTest
	{
		static ISessionFactory _sessionFactory = null;
		UnitOfWork _unitOfWork = null;
		BlogPost _post;
		Repository<int, BlogPost> _repository;


		[TestFixtureSetUp]
		public void Setup()
		{
			_sessionFactory = CreateConfiguration();
		}

		[SetUp]
		public void Init()
		{
			_unitOfWork = new UnitOfWork(_sessionFactory);
			_repository = new Repository<int, BlogPost>(_unitOfWork.Session);
			_post = new BlogPostBuilder()
						.Title("Repository Pattern")
						.SubTitle("agilefreak.workpress.com")
						.Text("Unit of work and repository pattern rocks")
						.PublicationDate(DateTime.Now)
						.AuthorName("Naz Ali")
						.Build();
		}

		private ISessionFactory CreateConfiguration()
		{
			IPersistenceConfigurer persistenceConfigurer =
				MsSqlConfiguration.MsSql2008.ConnectionString
						(c => c.FromConnectionStringWithKey("RepositoryPatternDemo"));

			Configuration cfg = persistenceConfigurer.ConfigureProperties(new Configuration());

			var persistenceModel = new PersistenceModel();
			var assembly = typeof(BlogPostMap).Assembly;
			persistenceModel.AddMappingsFromAssembly(assembly);
			persistenceModel.Configure(cfg);
			return cfg.BuildSessionFactory();
		}

		private static Comment GetComment()
		{
			return new CommentBuilder()
						.Email("n.ali@blog.com")
						.CommentDate(DateTime.Now)
						.Rating(9)
						.Text("blog comment")
						.Build();
		}

		[TearDown]
		public void RollbackUnitOfWork()
		{
			_unitOfWork.Rollback();
			_unitOfWork.Dispose();
		}


		[TestFixtureTearDown]
		public void CleanUp()
		{
			_sessionFactory.Dispose();
		}

		[Test]
		public void Can_add_blogpost_to_repository()
		{
			_repository.Add(_post);

			var savedPost = _repository.FindBy(_post.Id);
			Assert.That(savedPost, Is.Not.EqualTo(null), "Repository must return valid post for given Id");
			Assert.That(savedPost.Id, Is.EqualTo(_post.Id), "Repository returned invalid post for given Id");
		}

		[Test]
		public void Can_add_blogpost_With_Comments_to_repository()
		{
			var comment = GetComment();
			_post.AddCommant(comment);
			_repository.Add(_post);

			var savedPost = _repository.FindBy(_post.Id);
			Assert.That(savedPost.Comments.Count(), Is.EqualTo(1), "Repository must return post with one comment");
			Assert.That(savedPost.Comments.First().Id, Is.EqualTo(comment.Id));
		}

		[Test]
		public void Can_retrieve_blogpost_from_repository_using_linq_expression()
		{
			var comment = GetComment();
			_post.AddCommant(comment);
			_repository.Add(_post);

			var savedPost = _repository.FilterBy(p => p.AuthorName == "Naz Ali"
											&& p.Comments.Where(c => c.Rating > 5).Count() > 0
										);
			Assert.That(savedPost.Count(), Is.EqualTo(1), "Repository must return a post for given criteria");
			Assert.That(savedPost.First().Id, Is.EqualTo(_post.Id), "Repository returned invalid post for given criteria");
			Assert.That(savedPost.First().Comments.Count(), Is.EqualTo(1), "BlogPost must contain one comment");
		}

		[Test]
		public void Can_Delete_Blogpost_from_repository()
		{
			_repository.Add(_post);
			var savedPost = _repository.FindBy(_post.Id);
			_repository.Delete(savedPost);

			savedPost = _repository.FindBy(_post.Id);
			Assert.That(savedPost, Is.EqualTo(null), "Blog post should be removed from repository");
		}

	}
}
