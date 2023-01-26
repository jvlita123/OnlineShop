using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml.Linq;

namespace Sklep_MVC_Projekt.Models
{
    public class Order
    {
        [Key]
        [Display(Name = "OrderID")]
        public int OrderID { get; set; }

        [DefaultValue("In Progress")]
        [Display(Name = "Status")]
        public string Status { get; set; }

        // [Required]
        [Display(Name = "CustomerID")]
        public int CustomerID { get; set; }

        // [Required]
        [Display(Name = "ShippingMethod")]
        public int ShippingMethodID { get; set; }

        // [Required]
        [Display(Name = "PaymentMetod")]
        public int PaymentMethodID { get; set; }

        //  [Required]
        [Column(TypeName = "decimal")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Flat")]
        public string DeliveryAdressFlat { get; set; }

        [Display(Name = "Building")]
        public string DeliveryAdressBuilding { get; set; }

        [Display(Name = "Street")]
        public string DeliveryAdressStreet { get; set; }

        [Display(Name = "City")]
        public string DeliveryAdressCity { get; set; }

        [Display(Name = "Country")]
        public string DeliveryAdressCountry { get; set; }

        [StringLength(5, MinimumLength = 5, ErrorMessage = "Invalid postcode")]
        public string Postcode { get; set; }

        [Display(Name = "Phone number")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Phone must be 10 digits")]
        public string PhoneNumber { get; set; }

        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; }

        [Display(Name = "Dostawa")]
        [ForeignKey("ShippingMethodID")]
        public virtual ShippingMethod ShippingMethod { get; set; }

        [Display(Name = "Płatność")]
        [ForeignKey("PaymentMethodID")]
        public virtual PaymentMethod PaymentMethod { get; set; }

        public virtual ICollection<ProductOrder> ProductOrders { get; set; }

    }
}
