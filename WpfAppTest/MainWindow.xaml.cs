using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAppTest.MyServiceReference;

namespace WpfAppTest
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IServerServiceCallback
    {
        InstanceContext site;
        ServerServiceClient proxy;
        public MainWindow()
        {
            InitializeComponent();
            site = new InstanceContext(this);
            proxy = new ServerServiceClient(site);

            var User =  proxy.GetUser("admin");
            int a = 1;
        }

        public void Message([MessageParameter(Name = "message")] object message1, string descr)
        {
            throw new NotImplementedException();
        }
    }
}
