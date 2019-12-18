using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CupcakeShop.Models
{
    public class CupcakeModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MMM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Created On")]
        public DateTime? CreatedOn { get; set; }
        [DisplayName("Last Modified")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MMM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? LastModified { get; set; }
        [Required]
        public string Ingredients { get; set; }
        public int NumberOfIngredients { get; set; }
        [DisplayName("Image")]
        public string ImageFileName { get; set; }
        public bool Featured { get; set; }
    }
}