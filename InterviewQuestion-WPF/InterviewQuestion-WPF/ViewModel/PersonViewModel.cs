using InterviewQuestion_WPF.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewQuestion_WPF.ViewModel
{
   

  public class PersonViewModel 
    {
        public void AddPerson(string firstName, string lastName, string city, string state, string country)
        {
            int newUserId = _PeopleList.Count + 1; // Assuming UserId is auto-incremented

            
        }
        public List<Person> _PeopleList;

      

        

        public PersonViewModel()
        {
            _PeopleList = new List<Person>
            {
            
        };
        }

        public List<Person> People
        {
            get { return _PeopleList; }
            set { _PeopleList = value; }
        }

       




        public void UpdateList(Person inputPerson)
        {
            _PeopleList.Add(inputPerson);
            
            
            
           
        }

        public void InitializeListWithStudents() 
        {
            List<clsStudent> students = Util.GetStudents().Skip(1).ToList();
            foreach(var student in students)
            {

                _PeopleList.Add(new Person { UserId = student.getStudentID(), FirstName = student.getStudentFirstName(), LastName = student.getStudentLastName(), DisplayName = student.getStudentDisplayName(), });



            }
        }

       
    }
}
