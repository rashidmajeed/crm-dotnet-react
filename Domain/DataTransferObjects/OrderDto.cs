using System;
using System.Collections.Generic;

namespace Domain.DataTransferObjects
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalDue { get; set; }
        public string Status { get; set; }
        public int ClientId { get; set; }
        public virtual ClientDto Client { get; set; }
        public List<ProductDto> Product { get; set; }

    }
}