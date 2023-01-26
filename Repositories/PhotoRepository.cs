using Sklep_MVC_Projekt.Data;
using Sklep_MVC_Projekt.Models;

namespace Sklep_MVC_Projekt.Repositories
{
    public class PhotoRepository : BaseRepository<Photo>
    {
        private DataContext _dataContext;

        public PhotoRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
    }
}
