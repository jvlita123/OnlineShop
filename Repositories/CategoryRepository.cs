using Sklep_MVC_Projekt.Data;
using Sklep_MVC_Projekt.Models;

namespace Sklep_MVC_Projekt.Repositories
{
    public class CategoryRepository : BaseRepository<Category>
    {
        private DataContext _dataContext;

        public CategoryRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
    }
}
