namespace Domain.DataTransferObjects
{
    public class CreateClientDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string PhoneNumber { get; set; }
        public string Location { get; set; }
    }
}