using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model
{
    [DataContract]
    public class BugHistoriesDTO
    {
        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public UserDTO User { get; set; }

        [DataMember]
        public string StatusName { get; set; }

        [DataMember]
        public DateTime Time { get; set; }
    }
}
