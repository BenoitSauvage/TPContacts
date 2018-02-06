using System;
using System.Collections.Generic;
using System.Linq;
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

namespace UI {
    /// <summary>
    /// Interaction logic for Sign_in.xaml
    /// </summary>
    public partial class Sign_in : Window {

        private Worker.Classes.UserWorker userWorker;

        public Sign_in() {
            InitializeComponent();
            userWorker = new Worker.Classes.UserWorker();
        }

        private void Button_New_User(object sender, RoutedEventArgs e) {
            CreateUser createUserWindow = new CreateUser();
            createUserWindow.Show();
            this.Close();

        }

        private void Button_Signin(object sender, RoutedEventArgs e) {

            switch(userWorker.CheckConnect(txtBox_login.Text, txtBox_password.Text)){
                case 2:
                    // SearchUser main = new SearchUser(userWorker.GetCurrentUser());
                    MainWindow main = new MainWindow(userWorker.GetCurrentUser());
                    main.Show();
                    this.Close();
                    break;
                case 1:
                    MessageBox.Show("Error: Incorect Password");
                    break;
                case 0:
                    MessageBox.Show("Error: Unknow loggin");
                    break;

            }


        }

    }
}
