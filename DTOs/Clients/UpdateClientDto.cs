namespace CRM.DTOs.Clients
{
    public class UpdateClientDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public string Phone { get; set; }
        public string? CompanyName { get; set; }
        public int StatusId { get; set; }
    }
}
