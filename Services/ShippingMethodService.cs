using Sklep_MVC_Projekt.Models;
using Sklep_MVC_Projekt.Repositories;

namespace Sklep_MVC_Projekt.Services
{
    public class ShippingMethodService
    {
        private readonly ShippingMethodRepository _shippingMethodRepository;

        public ShippingMethodService(ShippingMethodRepository shippingMethodRepository)
        {
            _shippingMethodRepository = shippingMethodRepository;
        }

        public List<ShippingMethod> GetAll()
        {
            return _shippingMethodRepository.GetAll().ToList();
        }
        public ShippingMethod GetById(int id)
        {
            return _shippingMethodRepository.GetById(id);
        }
    }
}
