namespace ElectronicJournalForTheInstitute.Models;

public class Lesson
{
    public int Id { get; set; }
    public string Topic { get; set; } = string.Empty;
    public DateTime Date { get; set; }

    public int SubjectId { get; set; }
    public Subject? Subject { get; set; }

    public ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public override string ToString() => $"Заняття: {Topic} ({Date:dd.MM.yyyy})";
}