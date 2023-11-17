using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace PracticalTask1
{
    public class ViewProductDTO
    {
        public int productId { get; set; }
        public string productName { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public decimal price { get; set; }
        [Required]
        public int categoryId { get; set; }
        public string categoryName { get; set; } 

    }
}
