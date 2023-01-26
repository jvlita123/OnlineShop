using Sklep_MVC_Projekt.Models;
using Sklep_MVC_Projekt.Repositories;

namespace Sklep_MVC_Projekt.Services
{
    public class PaymentMethodService
    {
        private readonly PaymentMethodRepository _paymentMethodRepository;

        public PaymentMethodService(PaymentMethodRepository paymentMethodRepository)
        {
            _paymentMethodRepository = paymentMethodRepository;
        }

        public List<PaymentMethod> GetAll()
        {
            return _paymentMethodRepository.GetAll().ToList();
        }
    }
}
