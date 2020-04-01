using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Company { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string Location { get; set; }
        [Required]
        public ICollection<Order> Order { get; set; }

    }
}