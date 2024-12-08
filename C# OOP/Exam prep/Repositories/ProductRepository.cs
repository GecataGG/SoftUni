using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackFriday.Models.Contracts;
using BlackFriday.Repositories.Contracts;

namespace BlackFriday.Repositories
{
    public class ProductRepository : IRepository<IProduct>
    {
        private readonly List<IProduct> models;

        public ProductRepository() => models = new List<IProduct>();

        public IReadOnlyCollection<IProduct> Models => models.AsReadOnly();

        public void AddNew(IProduct product) => models.Add(product);

        public IProduct GetByName(string name) => models.FirstOrDefault(p => p.ProductName == name);

        public bool Exists(string name) => models.Any(p => p.ProductName == name);
    }

}
