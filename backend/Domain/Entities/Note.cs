
using CRM.Domain.Common;

namespace CRM.Domain.Entities
{
    public class Note : BaseEntity
    {
        public string Text { get; set; }

        
        public int ClientId { get; set; }
        public Client Client { get; set; }

        
        public int AuthorId { get; set; } 
        public User Author { get; set; }
    }
}