using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Security.Principal;
using System.Xml.Linq;

namespace Sklep_MVC_Projekt.Models
{
    public class Customer
    {
        [Required]
        [Key]   
        public int CustomerID { get; set; }

        [Display(Name = "Imie")]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [Display(Name = "AdressFlat")]
        public string AdressFlat { get; set; }

        [Display(Name = "AdressBuilding")]
        public string AdressBuilding { get; set; }

        [Display(Name = "AdressStreet")]
        public string AdressStreet { get; set; }

        [Display(Name = "AdressCity")]
        public string AdressCity { get; set; }

        [Display(Name = "AdressCountry")]
        public string AdressCountry { get; set; }

        [StringLength(4, MinimumLength = 4, ErrorMessage = "Invalid postcode")]
        public string Postcode { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [ForeignKey("Id")]
        public virtual IdentityUser? IdentityUser { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<CustomerProduct> CustomerProducts { get; set; }
    }
}
