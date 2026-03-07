namespace ElectronicJournalForTheInstitute.Models;

public class Teacher
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;

    public ICollection<Subject> Subjects { get; set; } = new List<Subject>();

    public override string ToString() => $"Викладач: {FullName}";
}