using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EagleDigital.CodeFirst.TenantMusic.Models;
using EagleDigital.Service.TenantMusic.IServices;
using EagleDigital.CodeFirst.TenantMusic.Repositories;

namespace EagleDigital.Service.TenantMusic.Services
{
    public class AuthorService : IAuthorService
    {

        private readonly IEntityRepository<Author> _authorRepository;
        public AuthorService(IEntityRepository<Author> authorRepository)
        {
            this._authorRepository = authorRepository;
        }

        public IQueryable<Author> List()
        {
            var authorList = _authorRepository.GetAll().OrderBy(p => p.Name);
            return authorList;
        }

        public Author Details(int id)
        {
            var authorDetails = _authorRepository.Get(id);
            return authorDetails;
        }

        public Author Insert(Author author)
        {
            var authorDetails = new Author();
            authorDetails.Name = author.Name;

            authorDetails = _authorRepository.InsertOnCommit(authorDetails);
            _authorRepository.CommitChanges();
            return authorDetails;
        }

        public Author Update(Author author)
        {
            var authorDetails = _authorRepository.Get(author.Id);
            authorDetails.Name = author.Name;
            _authorRepository.CommitChanges();
            return authorDetails;
        }
    }
}
