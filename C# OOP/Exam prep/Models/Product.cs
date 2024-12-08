using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackFriday.Models.Contracts;
using BlackFriday.Utilities.Messages;

namespace BlackFriday.Models
{
    public abstract class Product : IProduct
    {
        public string ProductName { get; private set; }
        public double BasePrice { get; private set; }
        public virtual double BlackFridayPrice => BasePrice;
        public bool IsSold { get; private set; }

        protected Product(string productName, double basePrice)
        {
            if (string.IsNullOrWhiteSpace(productName))
                throw new ArgumentException(ExceptionMessages.ProductNameRequired);
            if (basePrice <= 0)
                throw new ArgumentException(ExceptionMessages.ProductPriceConstraints);

            ProductName = productName;
            BasePrice = basePrice;
            IsSold = false;
        }

        public void UpdatePrice(double newPriceValue)
        {
            if (newPriceValue <= 0)
                throw new ArgumentException(ExceptionMessages.ProductPriceConstraints);
            BasePrice = newPriceValue;
        }

        public void ToggleStatus() => IsSold = !IsSold;

        public override string ToString()
            => $"Product: {ProductName}, Price: {BasePrice:F2}, You Save: {(BasePrice - BlackFridayPrice):F2}";
    }

}
