using System.ComponentModel.DataAnnotations;

namespace Company_Tour_Management_System.Models
{
    public class Participant
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public string Email { get; set; }
        public int State { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
