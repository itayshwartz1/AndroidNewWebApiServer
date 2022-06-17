
using System.ComponentModel.DataAnnotations;

namespace noam2.Model
{
    public class UserExtended
    {
        public UserExtended(string id, string name, string password, string server)
        {
            Id = id;
            Name = name;
            Password = password;
            Server = server;
        }

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
