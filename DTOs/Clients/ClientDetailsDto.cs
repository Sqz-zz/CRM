using CRM.Domain.Entities;
using CRM.DTOs.Notes;

namespace CRM.DTOs.Clients
{
    public class ClientDetailsDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? CompanyName { get; set; }
        public string Status { get; set; }
        public List<NoteDto> Notes { get; set; }

        public static ClientDetailsDto FromEntity(Client client)
        {
            return new ClientDetailsDto
            {
                Id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName,
                MiddleName = client.MiddleName,
                Email = client.Email,
                Phone = client.Phone,
                CompanyName = client.CompanyName,
                Status = client.Status.Title,
                Notes = client.Notes
                    .OrderByDescending(n => n.CreatedAt)
                    .Select(n => new NoteDto
                    {
                        Id = n.Id,
                        Text = n.Text,
                        Author = n.Author.Username,
                        CreatedAt = n.CreatedAt
                    })
                    .ToList()
            };
        }
    }
}