using System.ComponentModel.DataAnnotations;

namespace noam2.Model
{
    public class MessageExtanded
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Created { get; set; }
        [Required]
        public bool Sent { get; set; }

        [Required]
        public string User1 { get; set; }
        [Required]
        public string User2 { get; set; }

    }
}
