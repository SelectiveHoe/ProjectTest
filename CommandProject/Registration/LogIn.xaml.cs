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
        public User user;
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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Login.Text == "" || Password.Password == "")
            {
                MessageBox.Show("Введите логин и пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                user = new User();
                user.Login = Login.Text;
                user.Password = Password.Password;
                user = await proxy.AuthAsync(user.Login, user.Password);
                if (user == null)
                {
                    MessageBox.Show("Не правильный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
