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

        public DbSet<User> Users { get; set; }
     
        public DbSet<Role> Roles { get; set; }
      
        public DbSet<Bug> Bugs { get; set; }
    
        public DbSet<Status> Statuses { get; set; }
       
        public DbSet<Picture> Pictures { get; set; }
    
        public DbSet<BugHistory> BugHistories { get; set; }
    }
}
