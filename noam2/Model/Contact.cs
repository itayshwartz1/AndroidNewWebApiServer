using System.ComponentModel.DataAnnotations;

namespace noam2.Model
{
    public class Contact
    {

        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Server { get; set; }
        [Required]
        public string Last { get; set; } = "";
        [Required]
        public string Lastdate { get; set; } = "";
    }
}
