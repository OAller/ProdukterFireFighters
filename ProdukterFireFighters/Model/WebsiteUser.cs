using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdukterLib.Classes
{
    public class WebsiteUser
    {
        // Instans
        private int _id;
        private string _fornavn;
        private string _efternavn;
        private string _email;
        private string _kodeord;
        private bool _isCustomer;

        // Properties
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Fornavn
        {
            get { return _fornavn; }
            set { _fornavn = value; }
        }

        public string Efternavn
        {
            get { return _efternavn; }
            set { _efternavn = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string Kodeord
        {
            get { return _kodeord; }
            set { _kodeord = value; }
        }

        public bool IsCustomer
        {
            get { return _isCustomer; }
            set { _isCustomer = value; }
        }

        // Constructor
        public WebsiteUser()
        {
            _id = 0;
            _fornavn = string.Empty;
            _efternavn = string.Empty;
            _email = string.Empty;
            _kodeord = string.Empty;
            _isCustomer = false;
        }

        public WebsiteUser(int id, string fornavn, string efternavn, string email, string kodeord, bool isCustomer)
        {
            _id = id;
            _fornavn = fornavn;
            _efternavn = efternavn;
            _email = email;
            _kodeord = kodeord;
            _isCustomer = true;
        }
    }
}
