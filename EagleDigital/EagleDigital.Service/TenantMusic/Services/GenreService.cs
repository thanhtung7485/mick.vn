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
    public class GenreService : IGenreService
    {
       private readonly IEntityRepository<Genre> _genreRepository;
       public GenreService(IEntityRepository<Genre> genreRepository)
        {
            this._genreRepository = genreRepository;
        }

        public IQueryable<Genre> List()
        {
            var genreList = _genreRepository.GetAll().OrderBy(p=>p.Name);
            return genreList;
        }

        public Genre Details(int id)
        {
            var genreDetails = _genreRepository.Get(id);
            return genreDetails;
        }

        public Genre Insert(Genre genre)
        {
           
            var genreDetails = new Genre();
            genreDetails.Name = genre.Name;
           
            genreDetails = _genreRepository.InsertOnCommit(genreDetails);
            _genreRepository.CommitChanges();
            return genreDetails;
        }

        public Genre Update(Genre genre)
        {
             var genreDetails = _genreRepository.Get(genre.Id);
            genreDetails.Name = genre.Name;
            _genreRepository.CommitChanges();
            return genreDetails;
        }
    }
}
