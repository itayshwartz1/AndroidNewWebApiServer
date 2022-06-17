using System.ComponentModel.DataAnnotations;

namespace noam2.Model
{
    public class ContactExtended
    {

        public ContactExtended(string id, string name, string server, string last, string lastdate, string myUser)
        {
            Id = id;
            Name = name;
            Server = server;
            Last = last;
            Lastdate = lastdate;
            MyUser = myUser;
        }

        [Key]
        public string Id { get; set; }

        public string MyUser { get; set; }

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
