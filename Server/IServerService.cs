using ConsoleApp_DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Server
{
    [ServiceContract(CallbackContract = typeof(IClientCallback))]
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IServerService" в коде и файле конфигурации.
    public interface IServerService
    {
        [OperationContract(IsOneWay = true)]
        void Registration(User user);
        [OperationContract]
        User Auth(string login, string password);

        [OperationContract]
        User GetUser(string login);

        [OperationContract]
        List<BugHistory> GetBugHistory();
        [OperationContract]
        List<BugHistory> GetBugHistoryToNameUser(string nameUser = "");
        [OperationContract]
        List<BugHistory> GetBugHistoryToDecriptionBug(string DescriptionBug);
        [OperationContract(IsOneWay = true)]
        void EditStatusBugs(string Login, string DescriptionBugHistory, string status);
        [OperationContract(IsOneWay = true)]
        void DropBug(string DescriptionBug, int UserId);

        [OperationContract(IsOneWay = true)]
        void AddBug(User user, string Decription, List<List<byte>> ImgBytes);
    }
}
