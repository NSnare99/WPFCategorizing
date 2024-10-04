using InterviewQuestion_WPF.DataAccess;
using InterviewQuestion_WPF.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace InterviewQuestion_WPF
{ 
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        
       public void UpdateValues()
        {
            PersonViewModel VM = new PersonViewModel();
            this.MainWindow.DataContext = VM;

            VM.InitializeListWithStudents();

        }

        public void ChangeWindow()
        {
            if(this.MainWindow is MainWindow)
            {
              
                InterviewQuestion_WPF.DataTable window = new DataTable();
                PersonViewModel VM = new PersonViewModel();
                window.DataContext = VM;
              
                VM.InitializeListWithStudents();
                this.MainWindow.Close();
                this.MainWindow = window;
                window.Show();

            }
            else
            {
                
                InterviewQuestion_WPF.MainWindow window = new MainWindow();
                this.MainWindow.Close();
                this.MainWindow = window;
                window.Show();
             


       

            }
        }


    }
}
