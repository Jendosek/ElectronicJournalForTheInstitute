namespace ElectronicJournalForTheInstitute.Models;

public class Subject
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Credits { get; set; }

    public int TeacherId { get; set; }
    public Teacher? Teacher { get; set; }

    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    public override string ToString() => $"{Title} ({Credits} кр.)";
}