using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Sklep_MVC_Projekt.Models
{
    public class Photo
    {
        [Key]
        public int PhotoID { get; set; }

        //[Required(ErrorMessage = "Path is required")]
        [Display(Name = "Path")]
        public string Path { get; set; }

        [Required]
        [Display(Name = "ProductID")]
        public int ProductID { get; set; }


        [Display(Name = "ProductID")]
        [ForeignKey("ProductID")]
        public Product Product { get; set; }


        [Display(Name = "Description")]
        public string Description { get; set; }

        [Column("IsProfilePicture")]
        public bool IsProfilePicture { get; set; }
    }
}
