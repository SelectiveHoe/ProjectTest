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
    public partial class LogIn : Window
    {
        public User user;
        InstanceContext site;
        public LogIn()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //site = new InstanceContext(this);
            //ServerServiceClient proxy = new ServerServiceClient(site);
            user = new User();
            user.Login = Login.Text;
            user.Password = Password.Password;
            //user = proxy.Auth(Login.Text, Password.Password);
            //if(user == null)
            //{
            //    Title = "LogIn: Не правильный логин или пароль";
            //}
            //else
            //{
                this.Close();
            //}
        }
    }
}
