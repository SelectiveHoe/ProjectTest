using CommandProject.MyServiceReference;
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
using System.Windows.Shapes;

namespace CommandProject.Registration
{
    public partial class LogIn : Window, IServerServiceCallback
    {
        public UserDTO user;
        InstanceContext site;
        ServerServiceClient proxy;
        public LogIn()
        {
            InitializeComponent();
            site = new InstanceContext(this);
            proxy = new ServerServiceClient(site);
        }

        public void Message([MessageParameter(Name = "message")] object message1, string descr)
        {
            throw new NotImplementedException();
        }

        private  void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Login.Text == "admin" || Password.Password == "x61Ey612Kl2gpFL56FT9weDnpSo4AV8j8+qx2AuTHdRyY036xxzTTrw10Wq3+4qQyB+XURPWx1ONxp3Y3pB37A==")
            {
                MessageBox.Show("Введите логин и пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                user = proxy.Auth("admin", "x61Ey612Kl2gpFL56FT9weDnpSo4AV8j8+qx2AuTHdRyY036xxzTTrw10Wq3+4qQyB+XURPWx1ONxp3Y3pB37A==");

                if (user == null)
                {
                    Title = "LogIn: Не правильный логин или пароль";
                }
                else
                {
                    this.Close();
                }
            }
        }
    }
}
