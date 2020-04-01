namespace Domain.DataTransferObjects
{
    public class ClientDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string PhoneNumber { get; set; }
        public string Location { get; set; }
        public int OrderId { get; set; }
        public OrderDto Orders { get; set; }
    }
}