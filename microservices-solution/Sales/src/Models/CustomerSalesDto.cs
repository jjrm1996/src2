using System.Collections.Generic;

namespace Sales.Models
{
    public class CustomerSalesDto
    {
        public Customer Customer { get; set; }
        public IEnumerable<Sale> Sales { get; set; }
    }
}
