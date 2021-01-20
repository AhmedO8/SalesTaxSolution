using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesTax.Models
{
    public enum ProductCategories
    {
        Books,
        Food,
        Medical,
        Beauty,
        Technology
    }
    public class Product
    {

        public int Id { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        [Display(Name = "Name")]
        public string ProductName { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Display(Name = "Is this an imported product?")]
        public bool Imported { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Price")]

        public decimal Price { get; set; }
        [Display(Name = "Category")]
        [Required]
        public ProductCategories ProductCategory { get; set; }
        [Display(Name = "Tax")]
        public decimal SalesTaxAmount { get; set; }
        [Display(Name = "Total")]
        public decimal FinalProductPrice { get; set; }


    }

}
