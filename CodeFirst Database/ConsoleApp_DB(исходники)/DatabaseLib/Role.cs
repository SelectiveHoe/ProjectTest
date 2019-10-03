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
    public class Role
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }


        public virtual ICollection<User> Users { get; set; }
        public Role()
        {
            Users = new List<User>();
        }
    }
}
