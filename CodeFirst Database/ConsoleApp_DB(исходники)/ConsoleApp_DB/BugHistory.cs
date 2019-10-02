using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_DB
{
    [DataContract]
    class BugHistory
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int BugId { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public int StatusId { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public virtual Bug Bug { get; set; }
        [DataMember]
        public virtual User User { get; set; }
        [DataMember]
        public virtual Status Status { get; set; }
    }
}
