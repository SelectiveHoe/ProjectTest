using ConsoleApp_DB;
using Server.Model;
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
        void Registration(string login, string password, List<string> rolesName);

        [OperationContract]
        UserDTO Auth(string login, string password);

        [OperationContract]
        UserDTO GetUser(string login);

        [OperationContract]
        List<BugHistoriesDTO> GetBugHistory();

        [OperationContract]
        List<BugHistoriesDTO> GetBugHistoryToNameUser(string nameUser = "");

        [OperationContract]
        List<BugHistoriesDTO> GetBugHistoryToDecriptionBug(string DescriptionBug);

        [OperationContract(IsOneWay = true)]
        void EditStatusBugs(string Login, string DescriptionBugHistory, string status);

        [OperationContract(IsOneWay = true)]
        void DropBug(string DescriptionBug, string LoginUser);

        [OperationContract(IsOneWay = true)]
        void AddBug(string Login, string Decription, List<List<byte>> ImgBytes);
    }
}
