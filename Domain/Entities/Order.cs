using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalDue { get; set; }
        public string Status { get; set; }
        public int ClientId { get; set; }
        [JsonIgnore]
        public virtual Client Client { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
