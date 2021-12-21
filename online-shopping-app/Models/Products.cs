using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace online_shopping_app.Models
{
    public class Products
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Image")]
        public string Image { get; set; }

        [Display(Name = "Available")]
        public bool IsAvailable  { get; set; }

        [Display(Name = "ProductType Id")]
        public int ProductTypeId { get; set; }
        [ForeignKey("ProductTypeId")]
        public ProductTypes ProductTypes { get; set; }

        [Display(Name = "Tag Name Id")]
        public int TagNameId { get; set; }
        [ForeignKey("TagNameId")]
        public TagNames TagNames { get; set; }
    }
}
