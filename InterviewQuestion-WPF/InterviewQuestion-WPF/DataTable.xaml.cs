using InterviewQuestion_WPF.DataAccess;
using InterviewQuestion_WPF.ViewModel;
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

namespace InterviewQuestion_WPF
{
    /// <summary>
    /// Interaction logic for DataTable.xaml
    /// </summary>
    public partial class DataTable : Window
    {
        public DataTable()
        {
            InitializeComponent();
        }

        private void btnUpdateList_Click(object sender, RoutedEventArgs e)
        {
            DataUpload.WriteChangesToStudentData(new Dictionary<string, string> { { "uid", "123" }, { "ln", "Doe" }, { "fn", "John" }, { "dn", "Johnny Boy" } }, null);
            // PersonViewModel VM = (PersonViewModel) Application.Current.MainWindow.DataContext;
            ((App)App.Current).UpdateValues();
            
           

        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            
            ((App)App.Current).ChangeWindow();



        }
    }
}
