namespace dockerProject.Models
{
    public class Student
    {
        // Atributes
        private int _id;
        private string _name;
        private string _email;
        private string _password;
        private DateTime _dateOfBirth;

        // Properties
        public int Id { get { return _id; } set { _id = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public string Email { get { return _email; } set { _email = value; } }
        public string Password { get { return _password; } set { _password = value; } }
        public DateTime DateOfBirth { get { return _dateOfBirth; } set { _dateOfBirth = value; } }

        // Constructor
        public Student() { 
            _id = 0;
            _name = string.Empty;
            _email = string.Empty;
            _password = string.Empty;
            _dateOfBirth = DateTime.MinValue;
        }
        public Student(int id, string name, string email, string password, DateTime dateOfBirth)
        {
            _id = id;
            _name = name;
            _email = email;
            _password = password;
            _dateOfBirth = dateOfBirth;
        }
    }
}
