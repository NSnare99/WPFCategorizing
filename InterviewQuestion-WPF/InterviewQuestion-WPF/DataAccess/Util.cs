using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace InterviewQuestion_WPF.DataAccess
{
    public static class Util
    {
        //Return user based on unique UserID
        public static clsStudent GetSpecificStudent(string UserID)
        {
            List<clsStudent> currentStudents = GetStudents();
            foreach(clsStudent student in currentStudents)
            {
                if(student.getStudentID() == UserID)
                {
                    return student;
                }
            }
            return new clsStudent("", "", "", "");
        }

       
        //Retrieve all students from data
        public static List<clsStudent> GetStudents()
        {
            
            List<clsStudent> students = new List<clsStudent>();
            string[] lines = File.ReadAllLines(getDataPath());
            foreach (string line in lines)
            {
               
                students.Add(retrieveStudentFromDataLine(line));
            }
            return students;
        }
        //Based on clsStudent object, stringify for input to data file
        public static string TurnStudentObjectIntoString(clsStudent studentInput)
        {
           
            return studentInput.getStudentID() + ", " + studentInput.getStudentFirstName() + ", " +
                studentInput.getStudentLastName() + ", " + studentInput.getStudentDisplayName();

        }
        //Opposite process from above file; turn string into student object
        public static clsStudent retrieveStudentFromDataLine(string data)
        {
            string[] studentTokens = data.Split(",");
            clsStudent returnStudent = new(studentTokens[0].Trim(),
                                         studentTokens[1].Trim(),
                                         studentTokens[2].Trim(),
                                         studentTokens[3].Trim());
            return returnStudent;

        }
        //Grad reference to StudentData.txt outside of debug folder to allow writing/reading data to file
        public static string getDataPath()
        {
            string path = @".\";
            string newPath = Path.GetFullPath(Path.Combine(path, @"..\..\..\StudentData.txt"));
            return newPath;
        }
    }
}
