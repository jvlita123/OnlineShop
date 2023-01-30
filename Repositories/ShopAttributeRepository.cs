using Sklep_MVC_Projekt.Data;
using Sklep_MVC_Projekt.Models;

namespace Sklep_MVC_Projekt.Repositories
{
    public class ShopAttributeRepository : BaseRepository<ShopAttribute>
    {
        private DataContext _dataContext;

        public ShopAttributeRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }

        public ShopAttribute GetVisitCounter()
        {
            ShopAttribute? shopAttribute = _dataContext.ShopAttributes.Where(p => p.Attribute == "Visits").Select(p => p).FirstOrDefault();
            if (shopAttribute == null)
            {
                shopAttribute = new ShopAttribute
                {
                    Attribute = "Visits",
                    Value = "0"
                };

                _dataContext.Add(shopAttribute);
                _dataContext.SaveChanges();
            }
            return shopAttribute;
        }

        public void IncrementVisitCounter()
        {
            ShopAttribute? shopAttribute = GetVisitCounter();
            if (shopAttribute != null)
            {
                int value = int.Parse(shopAttribute.Value) + 1;
                shopAttribute.Value =  value.ToString();

                _dataContext.Update(shopAttribute);
            }

            _dataContext.SaveChanges();
        }
    }
}
