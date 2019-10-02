using System.ServiceModel;

namespace Server
{
    public interface IClientCallback
    {
        [OperationContract(IsOneWay = true)]
        void Message(dynamic message,string descr);
    }
}