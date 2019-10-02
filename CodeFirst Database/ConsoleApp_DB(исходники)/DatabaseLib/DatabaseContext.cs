using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_DB
{
    [DataContract]
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("DbConnection")
        {
            Database.SetInitializer<DatabaseContext>
                (new MyContextInitializer());
            Database.Initialize(true);
        }
        [DataMember]
        public DbSet<User> Users { get; set; }
        [DataMember]
        public DbSet<Role> Roles { get; set; }
        [DataMember]
        public DbSet<Bug> Bugs { get; set; }
        [DataMember]
        public DbSet<Status> Statuses { get; set; }
        [DataMember]
        public DbSet<Picture> Pictures { get; set; }
        [DataMember]
        public DbSet<BugHistory> BugHistories { get; set; }
    }
}
