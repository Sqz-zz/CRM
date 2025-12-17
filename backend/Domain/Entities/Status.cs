
using CRM.Domain.Common;
using CRM.Domain.Entities;

namespace CRM.Domain.Entities
{
    public class Status : BaseEntity
    {
        public string Title { get; set; }

        // Порядок  1 - Новый, 2 - В работе, 3 - Закрыт
        public int OrderIndex { get; set; }

        
        public ICollection<Client> Clients { get; set; } = new List<Client>();
    }
}