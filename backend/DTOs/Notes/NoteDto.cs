namespace CRM.DTOs.Notes
{
    public class NoteDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
