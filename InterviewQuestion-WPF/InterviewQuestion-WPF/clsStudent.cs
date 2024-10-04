namespace InterviewQuestion_WPF
{
    public class clsStudent
    {
        public string UserId;
        private string FirstName;
        private string LastName;
        private string DisplayName;

        public clsStudent(string uid,
                          string fn,
                          string ln,
                          string dn)
        {
            UserId = uid;
            FirstName = fn;
            LastName = ln;
            DisplayName = dn;
        }
        //Getters and Setters for private data fields
        public void setStudentID(string uid)
        {
            this.UserId = uid;
        }
        public string getStudentID()
        {
            return this.UserId;
        }
        public void setStudentDisplayName(string dn)
        {
            this.DisplayName = dn;
        }
        public string getStudentDisplayName()
        {
            return this.DisplayName;
        }
        public void setStudentFirstName(string fn)
        {
            this.FirstName = fn;
        }
        public string getStudentFirstName()
        {
            return this.FirstName;
        }
        public void setStudentLastName(string ln)
        {
            this.LastName = ln;
        }
        public string getStudentLastName()
        {
            return this.LastName;
        }
    }
}
