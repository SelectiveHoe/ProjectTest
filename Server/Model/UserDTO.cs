using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model
{
    [DataContract]
    public class UserDTO
    {
        [DataMember]
        public string Login { get; set; }

        [DataMember]
        public List<string> Roles { get; set; }

        public UserDTO()
        {
            Roles = new List<string>();
        }
    }
}
