using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackFriday.Models
{
    public class Client : User
    {
        public override bool HasDataAccess => false;
        public IReadOnlyDictionary<string, bool> Purchases { get; private set; }

        private readonly Dictionary<string, bool> purchases;

        public Client(string userName, string email) : base(userName, email)
        {
            purchases = new Dictionary<string, bool>();
            Purchases = purchases;
        }

        public void PurchaseProduct(string productName, bool blackFridayFlag)
            => purchases[productName] = blackFridayFlag;
    }

}
