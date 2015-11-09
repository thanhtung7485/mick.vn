using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EagleDigital.CodeFirst.TenantMusic.Models;
using EagleDigital.Service.TenantMusic.IServices;
using EagleDigital.CodeFirst.TenantMusic.Repositories;

namespace EagleDigital.Service.TenantMusic.Services
{
    public class SongService : ISongService
    {

       private readonly IEntityRepository<Song> _songRepository;
       public SongService(IEntityRepository<Song> songRepository)
        {
            this._songRepository = songRepository;
        }

        public List<Song> GetListSongProcedure(int id, string name)
        {
            return (List<Song>)_songRepository.ExecWithStoreProcedure("Song_GetAll @Id,@Name",
                   new SqlParameter("Id", SqlDbType.Int) { Value = id },
                   new SqlParameter("Name", SqlDbType.NVarChar) { Value = name }
               );
        }

        public IQueryable<Song> List()
        {
            var songList = _songRepository.GetAll().OrderBy(p => p.Name);
            return songList;
        }

        public Song Details(int id)
        {
            var songDetails = _songRepository.Get(id);
            return songDetails;
        }

        public Song Insert(Song song)
        {
            var songDetails = new Song();
            songDetails.Name = song.Name;
            songDetails.AuthorId = song.AuthorId;
            songDetails.GenreId = song.GenreId;
            songDetails = _songRepository.InsertOnCommit(songDetails);
            _songRepository.CommitChanges();
            return songDetails;
        }

        public Song Update(Song song)
        {
            var songDetails = _songRepository.Get(song.Id);
            songDetails.Name = song.Name;
            songDetails.GenreId = song.GenreId;
            songDetails.AuthorId = song.AuthorId;
            _songRepository.CommitChanges();
            return songDetails;
        }
    }
}
