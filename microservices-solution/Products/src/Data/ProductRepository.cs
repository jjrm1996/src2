using System.Collections.Generic;
using System.Linq;
using Products.src.Data;

namespace Products.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> _products = [
            new Product { Id = 1, Name = "Laptop Pro 15", Price = 1299.99m, Description = "Potente laptop para desarrollo y diseño." },
            new Product { Id = 2, Name = "Mouse Inalámbrico", Price = 25.50m, Description = "Erigonómico y con batería de larga duración." },
            new Product { Id = 3, Name = "Monitor 4K 27", Price = 349.00m, Description = "Resolución Ultra HD con panel IPS." },
            new Product { Id = 4, Name = "Teclado Mecánico", Price = 89.99m, Description = "Interruptores Blue con retroiluminación RGB." },
            new Product { Id = 5, Name = "Auriculares Noise Cancelling", Price = 199.50m, Description = "Cancelación de ruido activa y sonido premium." },
            new Product { Id = 6, Name = "Disco Duro Externo 2TB", Price = 75.00m, Description = "Almacenamiento portátil USB 3.0." },
            new Product { Id = 7, Name = "Webcam Full HD", Price = 45.00m, Description = "Ideal para conferencias y streaming." },
            new Product { Id = 8, Name = "Silla Ergonómica", Price = 210.00m, Description = "Soporte lumbar ajustable para largas jornadas." },
            new Product { Id = 9, Name = "Hub USB-C", Price = 35.99m, Description = "Adaptador multi-puerto con HDMI y SD." },
            new Product { Id = 10, Name = "Micrófono de Condensador", Price = 120.00m, Description = "Calidad de estudio para podcasts y voz." }
        ];

        public IEnumerable<Product> GetAll()
        {
            return _products;
        }

        public Product GetById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Update(Product product)
        {
            var existingProduct = GetById(product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Description = product.Description;
            }
        }

        public void Delete(int id)
        {
            var product = GetById(id);
            if (product != null)
            {
                _products.Remove(product);
            }
        }
    }
}