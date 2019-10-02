using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_DB
{
    [DataContract]
    class User
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        [StringLength(450)]
        [Index(IsUnique = true)]
        public string Login { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public virtual ICollection<Role> Roles { get; set; }
        public User()
        {
            Roles = new List<Role>();
        }
    }
}
