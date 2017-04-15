using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyContacts.Models
{
    [Table("contact")]
    public class Contact
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(250)]
        public string FirstName { get; set; }

        [MaxLength(250)]
        public string LastName { get; set; }

        [MaxLength(16), Unique]
        public String Phone { get; set; }

        [MaxLength(250), Unique]
        public string Email { get; set; }
        
        public DateTime Birthday { get; set; }

        public int Gender { get; set; }

        public bool Favorite { get; set; }

        public string img { get; set; }
    }
}
