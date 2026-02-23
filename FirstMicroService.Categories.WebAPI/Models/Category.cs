using System.ComponentModel.DataAnnotations;

namespace FirstMicroService.Categories.WebAPI.Models
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
