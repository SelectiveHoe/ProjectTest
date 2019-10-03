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
    /// <summary>
    /// Логика взаимодействия для RegWin.xaml
    /// </summary>
    public partial class RegWin : Window, IServerServiceCallback
    {
        InstanceContext site;
        ServerServiceClient proxy;
        public RegWin()
        {
            InitializeComponent();
            site = new InstanceContext(this);
            proxy = new ServerServiceClient(site);
        }

        public void Message([MessageParameter(Name = "message")] object message1, string descr)
        {
            throw new NotImplementedException();
        }

        private void RegButt_Click(object sender, RoutedEventArgs e)
        {
            if (Login.Text == "" || FirstPass.Password == "" || LastPass.Password == "")
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (FirstPass.Password == LastPass.Password)
                {
                    proxy.Registration(Login.Text, FirstPass.Password, new string[] {"Админ"});
                }
                else
                {
                    MessageBox.Show("Введеные пароли не совпадают", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
