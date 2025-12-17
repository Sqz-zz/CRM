
using CRM.Domain.Common;
using CRM.Domain.Entities;

namespace CRM.Domain.Entities
{
    public class Client : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; } 

        public string Email { get; set; }
        public string Phone { get; set; }
        public string? CompanyName { get; set; } 

        
        public int StatusId { get; set; }
        public Status Status { get; set; }

        
        public ICollection<Note> Notes { get; set; } = new List<Note>();
    }
}