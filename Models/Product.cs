using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Sklep_MVC_Projekt.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "ProductName is required")]
        [Display(Name = "Product name")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        private int _price;
        [Required(ErrorMessage = "Price is required")]
        [Display(Name = "Price")]
        public int Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public bool IsNew { get; set; }

        public bool IsOnSale { get; set; }

        [Required]
        [Display(Name = "AvailableAmmount")]
        public int AvailableAmmount { get; set; }

        [Required]
        [Display(Name = "CategoryID")]
        public int CategoryID { get; set; }

        [Display(Name = "CategoryID")]
        [ForeignKey("CategoryID")]
        public Category Category { get; set; }

        public virtual ICollection<Photo> Photo { get; set; }
        public virtual ICollection<ProductOrder> ProductOrders { get; set; }
        public virtual ICollection<CustomerProduct> CustomerProducts { get; set; }

    }
}
