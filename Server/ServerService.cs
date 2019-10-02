using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ConsoleApp_DB;

namespace Server
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "ServerService" в коде и файле конфигурации.
    public class ServerService : IServerService
    {
        Dictionary<User, OperationContext> operationContexts = new Dictionary<User, OperationContext>();
        public void AddBug(User user, string Decription, List<List<byte>> ImgBytes)
        {
            DatabaseContext db = new DatabaseContext();
            List<Picture> pic = new List<Picture>();
            for (int i = 0; i < pic.Count; i++)
                pic.Add(new Picture() { Image = ImgBytes[i].ToArray() });

            Bug bug = new Bug() { Description = Decription, Pictures = pic };

            db.Bugs.Add(bug);
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

        public User Auth(string login, string password)
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
                return user;
            }            
            return null;
        }

        public void DropBug(string DescriptionBug, int UserId)
        {
            DatabaseContext db = new DatabaseContext();
            var bug = db.Bugs.FirstOrDefault(x => x.Description == DescriptionBug);
            var status = db.Statuses.FirstOrDefault(x => x.Id == 5);
            var user = db.Users.FirstOrDefault(x => x.Id == UserId);
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

        public List<BugHistory> GetBugHistory()
        {
            DatabaseContext db = new DatabaseContext();
            return db.BugHistories.ToList();
        }

        public List<BugHistory> GetBugHistoryToDecriptionBug(string DecriptionBug)
        {
            DatabaseContext db = new DatabaseContext();
            return db.BugHistories.Where(x => x.Bug.Description == DecriptionBug).ToList();
        }

        public List<BugHistory> GetBugHistoryToNameUser(string loginUser = "")
        {
            DatabaseContext db = new DatabaseContext();
            return db.BugHistories.Where(x => x.User.Login == loginUser).ToList();
        }

        public User GetUser(string login)
        {
            DatabaseContext db = new DatabaseContext();
            var user = db.Users.FirstOrDefault(x => x.Login == login);

            if (user != null)
                return user;

            return null;
        }

        public void Registration(User user)
        {
            DatabaseContext db = new DatabaseContext();
            db.Users.Add(user);

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
