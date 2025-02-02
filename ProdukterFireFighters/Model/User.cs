namespace ProdukterLib.Classes
{
    public class User
    {
        // Instansfelter
        private int _id;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _phone;
        private string _password;
        private string _image;
        private bool _isAdmin;
        private bool _isEmployee;

        // Properties
        public int Id
        {
            get => _id;
            set => _id = value;
        }
        //Validering
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (value.Length < 2)
                {
                    throw new ArgumentException("Firstname must be at least 2 characters long");
                }
                _firstName = value;
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                if (value.Length < 2)
                {
                    throw new ArgumentException("Lastname must be at least 2 characters long");
                }
                _lastName = value;
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                if (!value.Contains("@"))
                {
                    throw new ArgumentException("Email must contain @");
                }
                _email = value;
            }
        }

        public string Phone
        {
            get => _phone;
            set
            {
                if (value.Length < 8)
                {
                    throw new ArgumentException("Phone number must be at least 8 characters long");
                }
                _phone = value;
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (value.Length < 6)
                {
                    throw new ArgumentException("Password must be at least 6 characters long");
                }
                _password = value;
            }
        }

        public string Image
        {
            get => _image;
            set => _image = value;
        }

        public bool IsAdmin
        {
            get => _isAdmin;
            set => _isAdmin = value;
        }

        public bool IsEmployee
        {
            get => _isEmployee;
            set => _isEmployee = value;
        }

        // Standardkonstruktør
        public User()
        {
            _id = 0;
            _firstName = string.Empty;
            _lastName = string.Empty;
            _email = string.Empty;
            _phone = string.Empty;
            _password = string.Empty;
            _image = string.Empty;
            _isAdmin = false;
            _isEmployee = false;
        }

        // Overloadet konstruktør m. alle parametre
        public User(int id, string firstName, string lastName, string email, string phone, string password, string image, bool isAdmin, bool isEmployee)
        {
            _id = id;
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _phone = phone;
            _password = password;
            _image = image;
            _isAdmin = isAdmin;
            _isEmployee = isEmployee;
        }
        public override string ToString()
        {
            return $"{_id} {_firstName} {_lastName} {_email} {_phone} {_password} {_image} {_isAdmin} {_isEmployee}";
        }
    }
}
