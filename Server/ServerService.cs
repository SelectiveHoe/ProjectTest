using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ConsoleApp_DB;
using Server.Model;

namespace Server
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "ServerService" в коде и файле конфигурации.
    public class ServerService : IServerService
    {
        Dictionary<User, OperationContext> operationContexts = new Dictionary<User, OperationContext>();


        public void AddBug(string Login, string Decription, List<List<byte>> ImgBytes)
        {
            DatabaseContext db = new DatabaseContext();
            List<Picture> pic = new List<Picture>();
            for (int i = 0; i < pic.Count; i++)
                pic.Add(new Picture() { Image = ImgBytes[i].ToArray() });

            Bug bug = new Bug() { Description = Decription, Pictures = pic };

            db.Bugs.Add(bug);
            db.SaveChanges();

            try
            {
                foreach (var item in operationContexts.ToList())
                {
                    if (item.Key.Login != Login)
                    {
                        if (item.Key.Roles.FirstOrDefault(x => x.Name == "Разработчик" ||
                        x.Name == "Старший разработчик" || x.Name == "Владелец проекта" ||
                        x.Name == "Старший тестировщик") != null)
                        {
                            item.Value.GetCallbackChannel<IClientCallback>().Message(bug, "Новый баг");
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UserDTO Auth(string login, string password)
        {
            DatabaseContext db = new DatabaseContext();
            var user = db.Users.FirstOrDefault(x => x.Login == login && x.Password == password);

            if (user != null)
            {
                try
                {
                    var temp = operationContexts[user];
                    temp = OperationContext.Current;
                }
                catch (Exception)
                {
                    operationContexts.Add(user, OperationContext.Current);
                }

                List<string> roles = new List<string>();
                foreach (var role in user.Roles)
                    roles.Add(role.Name);

                return new UserDTO() { Login = user.Login, Roles = roles };
            }            

            return null;
        }

        public void DropBug(string DescriptionBug, string LoginUser)
        {
            DatabaseContext db = new DatabaseContext();
            var bug = db.Bugs.FirstOrDefault(x => x.Description == DescriptionBug);
            var status = db.Statuses.FirstOrDefault(x => x.Id == 5);
            var user = db.Users.FirstOrDefault(x => x.Login == LoginUser);

            db.BugHistories.Add(new BugHistory()
            {
                BugId = bug.Id,
                Bug = bug,
                Date = DateTime.Now,
                Status = status,
                StatusId = status.Id,
                User = user,
                UserId = user.Id
            });

            db.SaveChanges();

            try
            {
                foreach (var item in operationContexts.ToList())
                {
                    if (item.Key.Login != user.Login)
                    {
                        if (item.Key.Roles.FirstOrDefault(x => x.Name == "Разработчик" ||
                        x.Name == "Старший разработчик" || x.Name == "Владелец проекта" ||
                        x.Name == "Старший тестировщик") != null)
                        {
                            item.Value.GetCallbackChannel<IClientCallback>().Message(bug, "Новый баг");
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void EditStatusBugs(string Login, string DescriptionBugHistory, string status)
        {
            DatabaseContext db = new DatabaseContext();
            var bug = db.BugHistories.LastOrDefault(x => x.Bug.Description == DescriptionBugHistory);
            var stat = db.Statuses.FirstOrDefault(x => x.Name == status);

            bug.Status = stat;
            db.BugHistories.Add(bug);
            db.SaveChanges();

            try
            {
                foreach (var item in operationContexts.ToList())
                {
                    if (item.Key.Login != Login)
                    {
                        if (item.Key.Roles.FirstOrDefault(x => x.Name == "Разработчик" ||
                        x.Name == "Старший разработчик" || x.Name == "Владелец проекта" ||
                        x.Name == "Старший тестировщик") != null)
                        {
                            item.Value.GetCallbackChannel<IClientCallback>().Message(bug, "Новый баг");
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<BugHistoriesDTO> GetBugHistory()
        {
            DatabaseContext db = new DatabaseContext();
            var tempData = db.BugHistories.ToList();
            List<BugHistoriesDTO> bugHistories = new List<BugHistoriesDTO>();

            foreach(var Item in tempData)
            {

                List<string> roles = new List<string>();
                foreach (var role in Item.User.Roles)
                    roles.Add(role.Name);

                var tempUser = new UserDTO
                {
                    Login = Item.User.Login,
                    Roles = roles
                };

                bugHistories.Add(new BugHistoriesDTO()
                {
                    Description = Item.Bug.Description,
                    StatusName = Item.Status.Name,
                    User = tempUser,
                    Time = Item.Date
                    
                });
            }

            return bugHistories;
        }

        public List<BugHistoriesDTO> GetBugHistoryToDecriptionBug(string DecriptionBug)
        {
            DatabaseContext db = new DatabaseContext();
            var tempData = db.BugHistories.Where(x => x.Bug.Description == DecriptionBug).ToList();
            List<BugHistoriesDTO> bugHistories = new List<BugHistoriesDTO>();

            foreach (var Item in tempData)
            {
                List<string> roles = new List<string>();
                foreach (var role in Item.User.Roles)
                    roles.Add(role.Name);

                var tempUser = new UserDTO
                {
                    Login = Item.User.Login,
                    Roles = roles
                };

                bugHistories.Add(new BugHistoriesDTO()
                {
                    Description = Item.Bug.Description,
                    StatusName = Item.Status.Name,
                    User = tempUser,
                    Time = Item.Date

                });
            }

            return bugHistories;
        }

        public List<BugHistoriesDTO> GetBugHistoryToNameUser(string loginUser = "")
        {
            DatabaseContext db = new DatabaseContext();
            var tempData = db.BugHistories.Where(x => x.User.Login == loginUser).ToList();
            List<BugHistoriesDTO> bugHistories = new List<BugHistoriesDTO>();

            foreach (var Item in tempData)
            {
                List<string> roles = new List<string>();
                foreach (var role in Item.User.Roles)
                    roles.Add(role.Name);

                var tempUser = new UserDTO
                {
                    Login = Item.User.Login,
                    Roles = roles
                };

                bugHistories.Add(new BugHistoriesDTO()
                {
                    Description = Item.Bug.Description,
                    StatusName = Item.Status.Name,
                    User = tempUser,
                    Time = Item.Date

                });
            }

            return bugHistories;
        }

        public UserDTO GetUser(string login)
        {
            DatabaseContext db = new DatabaseContext();
            var user = db.Users.FirstOrDefault(x => x.Login == login);

            if (user != null)
            {
                List<string> roles = new List<string>();
                foreach (var role in user.Roles)
                    roles.Add(role.Name);

                return new UserDTO
                {
                    Login = user.Login,
                    Roles = roles
                };
            }

            return null;
        }

        public void Registration(string login, string password, List<string> rolesName)
        {
            DatabaseContext db = new DatabaseContext();
            var filteredOrders = db.Roles.Where(o => rolesName.Contains(o.Name));
            var user = new User() { Login = login, Password = password, Roles = filteredOrders.ToList() };
            db.Users.Add(user);
            db.SaveChanges();

            if (user.Roles.FirstOrDefault(x => x.Name == "Разработчик") != null)
            {
                foreach (var item in operationContexts.ToList())
                {
                    if (item.Key.Login != user.Login)
                    {
                        if (item.Key.Roles.FirstOrDefault(x => x.Name == "Разработчик") != null)
                        {
                            item.Value.GetCallbackChannel<IClientCallback>().Message(user, "Новый разработчик");
                        }
                    }
                }
            }
        }
    }
}
