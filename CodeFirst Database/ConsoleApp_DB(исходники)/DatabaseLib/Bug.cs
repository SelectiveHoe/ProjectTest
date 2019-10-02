using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_DB
{
    [DataContract]
   public class Bug
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public virtual ICollection<Picture> Pictures { get; set; }

        public Bug()
        {
            Pictures = new List<Picture>();
        }
    }
}

