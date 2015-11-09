using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EagleDigital.CodeFirst.TenantMusic.Models;

namespace EagleDigital.Service.TenantMusic.IServices
{
    public interface ISongService
    {

        List<Song> GetListSongProcedure(int id, string name);
        IQueryable<Song> List();
        Song Details(int id);
        Song Insert(Song song);
        Song Update(Song song);
    }
}
