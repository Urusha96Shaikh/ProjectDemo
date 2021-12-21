using Microsoft.AspNetCore.Http;
using online_shopping_app.Data;
using online_shopping_app.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace online_shopping_app.ViewModels
{
    public class ProductViewModel
    {
        private ApplicationDbContext _DB_Context;
        public ProductViewModel(ApplicationDbContext DB_Context)
        {
            _DB_Context = DB_Context;
        }
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Image")]
        public IFormFile Image { get; set; }

        [Display(Name = "Available")]
        public bool IsAvailable { get; set; }
        [Display(Name = "Status")]
        public string ActiveStatus { get; set; }

        [Display(Name = "ProductType Id")]
        public int ProductTypeId { get; set; }
        [ForeignKey("ProductTypeId")]
        public ProductTypes ProductTypes { get; set; }

        [Display(Name = "Tag Name Id")]
        public int TagNameId { get; set; }
        [ForeignKey("TagNameId")]
        public TagNames TagNames { get; set; }

        public ProductViewModel() { }

        public ProductViewModel(int productId)
        {
            Products product = _DB_Context.Products.Where(item => item.Id == productId).ToList().FirstOrDefault();
            SetProperties(product);
        }

        public ProductViewModel(Products product)
        {
            SetProperties(product);
        }

        private void SetProperties(Products product)
        {
            this.Id = product.Id;
            this.Name = product.Name;
            this.Price = product.Price;
            this.ProductTypeId = product.ProductTypeId;
            this.TagNameId = product.TagNameId;

            //this.Image = product.Image;
            this.IsAvailable = product.IsAvailable;
            if (this.IsAvailable == true)
            {
                this.ActiveStatus = "Active";
            }
            else
            {
                this.ActiveStatus = "Inactive";
            }
        }

        public static List<ProductViewModel> ModelsToViewModels(List<Products> products)
        {
            List<ProductViewModel> ProductsVMs = new List<ProductViewModel> { };

            if (products != null)
            {
                foreach (Products item in products)
                {
                    ProductsVMs.Add(new ProductViewModel(item));
                }
            }
            return ProductsVMs;
        }
    }
}
