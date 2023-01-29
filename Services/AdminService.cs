using Sklep_MVC_Projekt.Models;
using Sklep_MVC_Projekt.Repositories;

namespace Sklep_MVC_Projekt.Services
{
    public class AdminService
    {
        private readonly ShopAttributeRepository _shopAttributeRepository;
        public AdminService(ShopAttributeRepository shopAttributeRepository) 
        {
            _shopAttributeRepository = shopAttributeRepository;
        }

        public string GetVisitCounter()
        {
            return _shopAttributeRepository.GetVisitCounter().Value;
        }

        public void IncrementVisitCounter()
        {
            _shopAttributeRepository.IncrementVisitCounter();
        }
    }
}
