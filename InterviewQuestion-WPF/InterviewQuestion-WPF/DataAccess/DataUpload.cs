using System.Collections.Generic;
using System.Data.Common;
using System.IO;


namespace InterviewQuestion_WPF.DataAccess
{
    public static class DataUpload
    {
       
        //Input dictionary of new edits to existing student data
        public static void RemoveUser(string UserID)
        {
            string[] studentDataDump = File.ReadAllLines(Util.getDataPath());
            List<string> extendableStudentDataDump = new List<string>();
           
            foreach (var item in studentDataDump)
            {
                if(item.Substring(0, 3) != UserID)
                {
                    extendableStudentDataDump.Add(item);
                }
                
            }
            
            studentDataDump = extendableStudentDataDump.ToArray();
            File.WriteAllLines(Util.getDataPath(), studentDataDump);

        }
        public static void WriteChangesToStudentData(Dictionary<string, string> dataChanges, string? UserID)
        {
            clsStudent editedStudent = new clsStudent("", "", "", "");
            clsStudent originalStudent = new clsStudent("", "", "", "");
            //Separate between incoming changes and student being edited
            if(UserID != null)
            { 
                editedStudent = Util.GetSpecificStudent(UserID);
                originalStudent = Util.GetSpecificStudent(UserID);

            }
            
            
            
            //Set each new value in editedStudent object
            foreach (var item in dataChanges)
            {
                setNewValue(item.Key, item.Value, editedStudent);
            }

            if(UserID != null)
            {
                //Take changes and upload to StudentData.txt
                WriteNewStudentToStorage(originalStudent, editedStudent, Util.getDataPath());

            }
            else
            {
                WriteNewStudentToStorage(null, editedStudent, Util.getDataPath());

            }
            

        }
        //Upload new student attributes to .txt file
        public static void WriteNewStudentToStorage(clsStudent? originalStudent, clsStudent editedStudent, string dataSource)
        {
            string[] studentDataDump = File.ReadAllLines(dataSource);
            List<string> extendableStudentDataDump = new List<string>();
            int lineIndex = 0;
            if(originalStudent != null)
            {
                foreach (var item in studentDataDump)
                {

                    //Match ids of incoming student info and edited info
                    if (Util.retrieveStudentFromDataLine(item).getStudentID() == originalStudent.getStudentID())
                    {

                        studentDataDump[lineIndex] = Util.TurnStudentObjectIntoString(editedStudent);
                    }
                    lineIndex++;
                }

            }
            else
            {
               foreach(var item in studentDataDump)
                {
                    extendableStudentDataDump.Add(item);
                }
                extendableStudentDataDump.Add(Util.TurnStudentObjectIntoString(editedStudent));
                studentDataDump = extendableStudentDataDump.ToArray();
            }
            

            //Write to file
            File.WriteAllLines(dataSource, studentDataDump);


        }

        //Set values in edited copy of clsStudent object
        public static void setNewValue(string key, string value, clsStudent editedStudent)
        {
            if(key == "uid")
            {
                editedStudent.setStudentID(value);

            }
            else if(key == "dn")
            {
                editedStudent.setStudentDisplayName(value);

            }
            else if (key == "fn")
            {
                editedStudent.setStudentFirstName(value);
            }
            else if (key == "ln")
            {
                editedStudent.setStudentLastName(value);
            }
            else
            {

                ///Throw error

            }
        }
        
    }
}
