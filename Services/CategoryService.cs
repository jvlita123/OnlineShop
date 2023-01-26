using Sklep_MVC_Projekt.Models;
using Sklep_MVC_Projekt.Repositories;

namespace Sklep_MVC_Projekt.Services
{
    public class CategoryService
    {
        private readonly CategoryRepository _categoryRepository;

        public CategoryService(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public List<Category> GetAll()
        {
            return _categoryRepository.GetAll().ToList();
        }
    }
}
