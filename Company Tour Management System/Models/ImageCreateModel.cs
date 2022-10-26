using System.ComponentModel.DataAnnotations;

namespace Company_Tour_Management_System.Models
{
    public class ImageCreateModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile ImagePath { get; set; }

    }
}
