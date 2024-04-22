using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RazorCRUDUI.Models
{
    public class ItemModel
    {
        public int ID { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Display(Name = "Item Name")]
        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 1000)]
        [Required]
        public decimal Price { get; set; }

        public string? PictureUrl { get; set; }
    }
}
