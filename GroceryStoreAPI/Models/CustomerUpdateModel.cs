using System.ComponentModel.DataAnnotations;

namespace GroceryStoreAPI.Models
{
    public class CustomerUpdateModel
    {
        [Required]
        [MaxLength(64)]
        public string Name { get; set; }
    }
}
