using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (ServiceHost host = new ServiceHost(typeof(ServerService)))
                {
                    host.Open();
                    if (host.State == CommunicationState.Opened)
                    {
                        Console.WriteLine("Server started on {0}",
                            host.Description.Endpoints.FirstOrDefault().Address.ToString());
                        Console.WriteLine("To stop server press any key...");
                        Console.ReadKey(true);
                        Console.WriteLine("Server stopped");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
