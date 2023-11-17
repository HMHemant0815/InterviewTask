using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PracticalTask1
{
    public class CategoryModel
    {
    
        public int categoryId { get; set; }
        [Required]
        public   string categoryName { get; set; }

        //public required List<ProductModel> Products { get; set; }
    }
}
