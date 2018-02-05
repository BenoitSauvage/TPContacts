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
    /// Interaction logic for SearchUser.xaml
    /// </summary>
    public partial class SearchUser : Window {
        long id;
        Worker.Classes.UserWorker user;


        public SearchUser(long id_user) {
            InitializeComponent();
            id = id_user;
            user = new Worker.Classes.UserWorker();
            txt_userName.Text = "Bonjour " + user.GetLoginById(id);
        }

        private void Button_Quit(object sender, RoutedEventArgs e) {

            this.Close();

        }

        private void Button_Search(object sender, RoutedEventArgs e) {

        }

        private void Button_Nouveau(object sender, RoutedEventArgs e) {

        }
    }
}
