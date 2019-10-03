using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_DB
{
    [DataContract]
    public class Picture
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int BugId { get; set; }
        [DataMember]
        public byte[] Image { get; set; }

        public virtual Bug Bug { get; set; }
    }
}
