using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackFriday.Models.Contracts;
using BlackFriday.Utilities.Messages;

namespace BlackFriday.Models
{
    public abstract class User : IUser
    {
        public string UserName { get; private set; }
        public abstract bool HasDataAccess { get; }
        private string email;

        public string Email
        {
            get => HasDataAccess ? "hidden" : email;
            private set
            {
                if (!HasDataAccess && string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(ExceptionMessages.EmailRequired);
                email = value;
            }
        }

        protected User(string userName, string email)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException(ExceptionMessages.UserNameRequired);

            UserName = userName;
            Email = email;
        }

        public override string ToString()
            => $"{UserName} - Status: {(HasDataAccess ? "Admin" : "Client")}, Contact Info: {Email}";
    }

}
