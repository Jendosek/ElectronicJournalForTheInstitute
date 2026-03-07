namespace ElectronicJournalForTheInstitute.Models;

public class Student
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    
    public int GroupId { get; set; }
    public Group? Group { get; set; }

    public ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public override string ToString() => $"{LastName} {FirstName}";
}