using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using InterviewQuestion_WPF.DataAccess;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using InterviewQuestion_WPF.ViewModel;

namespace InterviewQuestion_WPF
{


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        // Define variables to store initial values; validates whether changes were made in edit screen
        private string initialTxtFieldUserIDValue = "";
        private string initialTxtFieldFirstNameValue = "";
        private string initialTxtFieldLastNameValue = "";
        private string initialTxtFieldDisplayNameValue = "";

        
        public Dictionary<string, int> myDictionary { get; set; }
        public MainWindow()
        {

            DataContext = this;
            InitializeComponent();

        }

        //Initialize combox box with data values from StudentData.txt at startup
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            initializeUI();
        }


        //Properties and handlers for displaying current selected/retrieved data in stack panels

        public event PropertyChangedEventHandler? PropertyChanged;


        private string currentSelectionUserID;
        private string currentSelectionDisplayName;
        private string currentSelectionLastName;
        private string currentSelectionFirstName;

        //Property for each field to bind to individual stack panel
        public string CurrentSelectionUserID
        {
            get { return currentSelectionUserID; }
            set
            {
                currentSelectionUserID = value;
                OnPropertyChange();
            }
        }

        public string CurrentSelectionDisplayName
        {
            get { return currentSelectionDisplayName; }
            set
            {
                currentSelectionDisplayName = value;
                OnPropertyChange();
            }
        }

        public string CurrentSelectionLastName
        {
            get { return currentSelectionLastName; }
            set
            {
                currentSelectionLastName = value;
                OnPropertyChange();
            }
        }

        public string CurrentSelectionFirstName
        {
            get { return currentSelectionFirstName; }
            set
            {
                currentSelectionFirstName = value;
                OnPropertyChange();
            }
        }
        //Allows either manual update in code-behind or UI based changes from CallerMemberName
        private void OnPropertyChange([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        //Event handlers for button clicks
        //
        //
        //

        //Open window to edit current data selection
        private void EditSelection_Click(object sender, RoutedEventArgs e)
        {
            //Cannot edit null data ("none" option in combo box)
            if (DataSelector.SelectedIndex != 0)
            {
                //Open popup to edit current selection
                editPopup.IsOpen = true;
                //Set initial values to track whether selection was edited, and pre-fill edit screen
                //with current selection fields
                initialTxtFieldUserIDValue = txtFieldUserID.Text = currentSelectionUserID;
                initialTxtFieldFirstNameValue = txtFieldFirstName.Text = currentSelectionFirstName;
                initialTxtFieldLastNameValue = txtFieldLastName.Text = currentSelectionLastName;
                initialTxtFieldDisplayNameValue = txtFieldDisplayName.Text = currentSelectionDisplayName;

            }




        }

        private void DeleteSelection_Click(object sender, RoutedEventArgs e)
        {
            DataUpload.RemoveUser(CurrentSelectionUserID);
            initializeUI(DataSelector.SelectedIndex - 1);
        }

        private void AddNewUser_Click(object sender, RoutedEventArgs e)
        {
            createPopup.IsOpen = true;
           
        }

        private void ChangeWindow_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current is App app)
            {
                app.ChangeWindow(); 
                // Call the method defined in App.xaml.cs
            }
            //InterviewQuestion_WPF.DataTable window = new DataTable();
            //PersonViewModel VM = new PersonViewModel();
            //window.DataContext = VM;
          
            //window.Show();
                 

        }



        //Handler for submission of data edit screen
        private void SubmitEdits_Click(object sender, RoutedEventArgs e)
        {
            
            //Clean current data strings and ensure that ID wasn't changed
            CleanUpNonVitalChanges();
            if (initialTxtFieldUserIDValue != txtFieldUserID.Text)
            {
                idChangePopup.IsOpen = true;
            }
            else
            {

                // Perform data validation to ensure data was actually edited (not just added whitespace)
                bool hasInputChanged = (initialTxtFieldDisplayNameValue != txtFieldDisplayName.Text ||
                                        initialTxtFieldUserIDValue != txtFieldUserID.Text ||
                                            initialTxtFieldFirstNameValue != txtFieldFirstName.Text ||
                                            initialTxtFieldLastNameValue != txtFieldLastName.Text);



                if (hasInputChanged)
                {
                    //Allow user to confirm their editing choice
                    confirmationPopup.IsOpen = true;
                }

                else
                {
                    //Open window to indicate the user hasn't change data meaningfully
                    noChangesPopup.IsOpen = true;
                }
            }
        }


        //Handles cancelling of edit submission
        private void SubmitEdits_Cancel(object sender, RoutedEventArgs e)
        {
            editPopup.IsOpen = false;
        }
        //Handles closing of alert windows for unchanged data
        private void CloseNoChangesPopup_Click(object sender, RoutedEventArgs e)
        {
            noChangesPopup.IsOpen = false;
            idChangePopup.IsOpen = false;
        }
        //Handles user confirming they want to push edits to the data
        private void ConfirmEditsButton_Click(object sender, RoutedEventArgs e)
        {

            confirmationPopup.IsOpen = false;
            //Retrieve current state of data; encode same way as clsStudent constructor
            //is formed 
            Dictionary<string, string> changes = new Dictionary<string, string>()
                {
                    {"uid", txtFieldUserID.Text},
                    {"dn", txtFieldDisplayName.Text },
                    {"fn", txtFieldFirstName.Text},
                    {"ln", txtFieldLastName.Text},
                };
            //Push edits to "database" (StudentData.txt file)
            DataUpload.WriteChangesToStudentData(changes, txtFieldUserID.Text);
            //Change UI to reflect newly changed data
            initializeUI(DataSelector.SelectedIndex);
            //Close edit screen
            editPopup.IsOpen = false;

        }


        //Handle declining edits
        private void DeclineEditsButton_Click(object sender, RoutedEventArgs e)
        {
            // Close the confirmation popup
            confirmationPopup.IsOpen = false;
        }
        //Handle changes to combobox dropdown, which contains all users read from database
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox? comboBox = sender as ComboBox;
            if (comboBox?.SelectedItem != null)
            {
                List<clsStudent> students = new List<clsStudent>();
                students = Util.GetStudents();
                string? selectedValue = comboBox?.SelectedItem.ToString();
                string currentFullName = "";

                foreach (clsStudent student in students)
                {
                    //Compare each student in database to current selection
                    currentFullName = student.getStudentFirstName() + " " + student.getStudentLastName();
                    if (currentFullName == selectedValue)
                    {
                        //Update UI to reflect changed combobox
                        UpdateUIStackPanels(student);
                        return;
                    }
                }
                //If empty user ("none") is selected, populate right-hand screen with white space
                UpdateUIStackPanels(new clsStudent("        ", "        ", "        ", "        "));

            }


        }

        private void SubmitNewUser_Click(object sender, RoutedEventArgs e)
        {
            DataUpload.WriteNewStudentToStorage(null, new clsStudent(txtFieldUserID_NewUser.Text, txtFieldFirstName_NewUser.Text, txtFieldLastName_NewUser.Text, txtFieldDisplayName_NewUser.Text), Util.getDataPath());
            initializeUI(DataSelector.Items.Count);
            createPopup.IsOpen = false;
        }

        private void SubmitNewUser_Cancel(object sender, RoutedEventArgs e)
        {
            createPopup.IsOpen = false;
        }

        //Update StackPanels to reflect new data being read
        private void UpdateUIStackPanels(clsStudent student)
        {

            currentSelectionUserID = student.getStudentID();
            currentSelectionDisplayName = student.getStudentDisplayName();
            currentSelectionLastName = student.getStudentLastName();
            currentSelectionFirstName = student.getStudentFirstName();
            OnPropertyChange("CurrentSelectionUserID");
            OnPropertyChange("CurrentSelectionDisplayName");
            OnPropertyChange("CurrentSelectionLastName");
            OnPropertyChange("CurrentSelectionFirstName");



        }



     
        //Write to combo box UI element from data file
        private void initializeUI(int indexOfComboBoxToStartAt = 0)
        {
            List<clsStudent> students = Util.GetStudents();
            //Clear old values in case they have been altered
            DataSelector.Items.Clear();
            //If passed in after data edit, change index to reflect this
            DataSelector.SelectedIndex = indexOfComboBoxToStartAt;
            //Add initial empty value
            DataSelector.Items.Add("None");
            //Read in data from database
            foreach (clsStudent student in students)
            {
                if (student.getStudentFirstName() != "---")
                    DataSelector.Items.Add(student.getStudentFirstName() + " " + student.getStudentLastName());
            }


        }

        //Trim whitespace from "edited" data to ensure meaningful changes
        private void CleanUpNonVitalChanges()

        {

            txtFieldDisplayName.Text = txtFieldDisplayName.Text.Trim();
            txtFieldUserID.Text = txtFieldUserID.Text.Trim();
            txtFieldLastName.Text = txtFieldLastName.Text.Trim();
            txtFieldFirstName.Text = txtFieldFirstName.Text.Trim();



        }
    }


}
