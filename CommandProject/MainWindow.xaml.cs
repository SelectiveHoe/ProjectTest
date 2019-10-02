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
using CommandProject.MyServiceReference;

namespace CommandProject
{
    public partial class MainWindow : Window, IServerServiceCallback
    {
        ServerServiceClient proxy;
        InstanceContext site;
        public MainWindow()
        {
            Registration.LogIn login = new Registration.LogIn();
            login.ShowDialog();
            User user = login.user;
            MessageBox.Show(user.Login + " = " + user.Password);
            InitializeComponent();
            site = new InstanceContext(this);
            proxy = new ServerServiceClient(site);
            proxy.Auth(user.Login, user.Password);
        }

        public void Message([MessageParameter(Name = "message")] dynamic message1, string descr)
        {
            
        }
    }
}
