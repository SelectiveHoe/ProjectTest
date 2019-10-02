using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_DB
{
    public class MyContextInitializer : DropCreateDatabaseIfModelChanges<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            var role = context.Roles.Add(new Role { Name = "Админ" });
            context.Roles.Add(new Role { Name = "Тестировщик" });
            context.Roles.Add(new Role { Name = "Старший тестировщик" });
            context.Roles.Add(new Role { Name = "Разработчик" });
            context.Roles.Add(new Role { Name = "Старший разработчик" });
            context.Roles.Add(new Role { Name = "Владелец проекта" });
            context.SaveChanges();
            using (SHA512 shaM = new SHA512Managed())
            {
                byte[] data = Encoding.UTF8.GetBytes("admin");
                byte[] hash = shaM.ComputeHash(data);
                User adminUser = new User { Login = "admin", Password = Convert.ToBase64String(hash), Roles = new List<Role> { role } };
                context.Users.Add(adminUser);
            }

            context.Statuses.Add(new Status { Name = "Новый" });
            context.Statuses.Add(new Status { Name = "Пофиксен" });
            context.Statuses.Add(new Status { Name = "Закрыт" });
            context.Statuses.Add(new Status { Name = "Не воспроизводится" });
            context.Statuses.Add(new Status { Name = "В процессе устранения" });
            context.Statuses.Add(new Status { Name = "Открыт повторно" });

            context.SaveChanges();
        }
    }
}
