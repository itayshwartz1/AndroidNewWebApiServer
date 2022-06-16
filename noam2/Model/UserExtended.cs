
using System.ComponentModel.DataAnnotations;

namespace noam2.Model
{
    public class UserExtended
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string Server { get; set; }
    }
}
