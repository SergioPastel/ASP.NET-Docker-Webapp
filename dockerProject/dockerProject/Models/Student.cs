namespace dockerProject.Models
{
    public class Student
    {
        // Atributes
        private int _id;
        private string _name;
        private string _email;
        private int _nif;
        private DateOnly _dateOfBirth;

        // Properties
        public int Id { get { return _id; } set { _id = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public string Email { get { return _email; } set { _email = value; } }
        public int Nif { get { return _nif; } set { _nif = value; } }
        public DateOnly DateOfBirth { get { return _dateOfBirth; } set { _dateOfBirth = value; } }

        // Constructor
        public Student() { 
            _id = 0;
            _name = string.Empty;
            _email = string.Empty;
            _nif = 0;
            _dateOfBirth = DateOnly.MinValue;
        }
        public Student(int id, string name, string email, int nif, DateOnly dateOfBirth)
        {
            _id = id;
            _name = name;
            _email = email;
            _nif = nif;
            _dateOfBirth = dateOfBirth;
        }
    }
}
