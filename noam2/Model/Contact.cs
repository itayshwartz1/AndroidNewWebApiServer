using System.ComponentModel.DataAnnotations;

namespace noam2.Model
{
    public class Contact
    {
        public Contact(string id, string name, string server, string last, string lastdate)
        {
            Id = id;
            Name = name;
            Server = server;
            Last = last;
            Lastdate = lastdate;
        }

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
