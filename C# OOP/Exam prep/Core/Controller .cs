using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackFriday.Core.Contracts;
using BlackFriday.Models.Contracts;
using BlackFriday.Models;
using BlackFriday.Utilities.Messages;

namespace BlackFriday.Core
{
        public class Controller : IController
        {
            private readonly IApplication application;

            public Controller()
            {
                application = new Application();
            }

            public string RegisterUser(string userName, string email, bool hasDataAccess)
            {
                if (application.Users.Exists(userName))
                    return $"{userName} is already registered.";

                if (application.Users.Models.Any(u => u.Email == email))
                    return $"{email} is already used by another user.";

                if (hasDataAccess && application.Users.Models.Count(u => u.HasDataAccess) >= 2)
                    return "The number of application administrators is limited.";

                IUser user = hasDataAccess
                    ? new Admin(userName, email)
                    : new Client(userName, email);

                application.Users.AddNew(user);
                return hasDataAccess
                    ? $"Admin {userName} is successfully registered with data access."
                    : $"Client {userName} is successfully registered.";
            }

            public string AddProduct(string productType, string productName, string userName, double basePrice)
            {
                if (!application.Users.Exists(userName) || !application.Users.GetByName(userName).HasDataAccess)
                    return $"{userName} has no data access.";

                if (application.Products.Exists(productName))
                    return $"{productName} already exists in the application.";

                IProduct product = productType switch
                {
                    "Item" => new Item(productName, basePrice),
                    "Service" => new Service(productName, basePrice),
                    _ => null
                };

                if (product == null)
                    return $"{productType} is not a valid type for the application.";

                application.Products.AddNew(product);
                return $"{productType}: {productName} is added in the application. Price: {basePrice:F2}";
            }

            public string UpdateProductPrice(string productName, string userName, double newPriceValue)
            {
                if (!application.Products.Exists(productName))
                    return $"{productName} does not exist in the application.";

                if (!application.Users.Exists(userName) || !application.Users.GetByName(userName).HasDataAccess)
                    return $"{userName} has no data access.";

                var product = application.Products.GetByName(productName);
                var oldPrice = product.BasePrice;
                product.UpdatePrice(newPriceValue);
                return $"{productName} -> Price is updated: {oldPrice:F2} -> {newPriceValue:F2}";
            }

            public string PurchaseProduct(string userName, string productName, bool blackFridayFlag)
            {
                if (!application.Users.Exists(userName) || application.Users.GetByName(userName).HasDataAccess)
                    return $"{userName} has no authorization for this functionality.";

                if (!application.Products.Exists(productName))
                    return $"{productName} does not exist in the application.";

                var product = application.Products.GetByName(productName);
                if (product.IsSold)
                    return $"{productName} is out of stock.";

                var client = (Client)application.Users.GetByName(userName);
                product.ToggleStatus();
                client.PurchaseProduct(productName, blackFridayFlag);

                var price = blackFridayFlag ? product.BlackFridayPrice : product.BasePrice;
                return $"{userName} purchased {productName}. Price: {price:F2}";
            }

            public string RefreshSalesList(string userName)
            {
                if (!application.Users.Exists(userName) || !application.Users.GetByName(userName).HasDataAccess)
                    return $"{userName} has no data access.";

                var soldProducts = application.Products.Models.Where(p => p.IsSold).ToList();
                soldProducts.ForEach(p => p.ToggleStatus());
                return $"{soldProducts.Count} products are listed again.";
            }

            public string ApplicationReport()
            {
                var report = new StringBuilder();

                report.AppendLine("Application administration:");
                application.Users.Models
                    .Where(u => u.HasDataAccess)
                    .OrderBy(u => u.UserName)
                    .ToList()
                    .ForEach(admin => report.AppendLine($"{admin.UserName} - Status: Admin, Contact Info: hidden"));

                report.AppendLine("Clients:");
                application.Users.Models
                    .Where(u => !u.HasDataAccess)
                    .OrderBy(u => u.UserName)
                    .Cast<Client>()
                    .ToList()
                    .ForEach(client =>
                    {
                        report.AppendLine($"{client.UserName} - Status: Client, Contact Info: {client.Email}");

                        var blackFridayPurchases = client.Purchases
                            .Where(p => p.Value)
                            .Select(p => p.Key)
                            .ToList();

                        if (blackFridayPurchases.Any())
                        {
                            report.AppendLine($"-Black Friday Purchases: {blackFridayPurchases.Count}");
                            blackFridayPurchases.ForEach(product => report.AppendLine($"--{product}"));
                        }
                    });

                return report.ToString().TrimEnd();
            }
        }
    }



