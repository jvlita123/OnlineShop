using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Sklep_MVC_Projekt.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "CategoryName is required")]
        [Display(Name = "CategoryName")]
        public string CategoryName { get; set; }

        public int ParentCategoryID { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
