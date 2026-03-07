namespace ElectronicJournalForTheInstitute.Models;

public class Grade
{
    public int Id { get; set; }
    public int Value { get; set; }
    public string Comment { get; set; } = string.Empty;

    public int StudentId { get; set; }
    public Student? Student { get; set; }

    public int LessonId { get; set; }
    public Lesson? Lesson { get; set; }

    public override string ToString() => $"Оцінка: {Value} балів";
}