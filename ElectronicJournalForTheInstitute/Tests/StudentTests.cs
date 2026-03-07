using ElectronicJournalForTheInstitute.Data;
using ElectronicJournalForTheInstitute.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ElectronicJournalForTheInstitute.Tests;

public class StudentTests
{
    private ApplicationDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        
        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task AddStudent_ShouldAddStudentToDatabase()
    {
        using var context = GetDbContext();
        var student = new Student { FirstName = "Іван", LastName = "Іванов", DateOfBirth = new DateTime(2005, 1, 1) };

        context.Students.Add(student);
        await context.SaveChangesAsync();

        var savedStudent = await context.Students.FirstOrDefaultAsync(s => s.FirstName == "Іван");
        Assert.NotNull(savedStudent);
        Assert.Equal("Іванов", savedStudent.LastName);
    }

    [Fact]
    public async Task ReadStudent_ShouldReturnCorrectStudent()
    {
        using var context = GetDbContext();
        var student = new Student { FirstName = "Петро", LastName = "Петров" };
        context.Students.Add(student);
        await context.SaveChangesAsync();

        var retrievedStudent = await context.Students.FindAsync(student.Id);

        Assert.NotNull(retrievedStudent);
        Assert.Equal("Петро", retrievedStudent.FirstName);
    }

    [Fact]
    public async Task UpdateStudent_ShouldChangeStudentData()
    {
        using var context = GetDbContext();
        var student = new Student { FirstName = "Анна", LastName = "Шевченко" };
        context.Students.Add(student);
        await context.SaveChangesAsync();

        student.FirstName = "Марія";
        context.Students.Update(student);
        await context.SaveChangesAsync();

        var updatedStudent = await context.Students.FindAsync(student.Id);
        Assert.Equal("Марія", updatedStudent?.FirstName);
    }

    [Fact]
    public async Task DeleteStudent_ShouldRemoveStudentFromDatabase()
    {
        using var context = GetDbContext();
        var student = new Student { FirstName = "Олег", LastName = "Олегов" };
        context.Students.Add(student);
        await context.SaveChangesAsync();

        context.Students.Remove(student);
        await context.SaveChangesAsync();

        var deletedStudent = await context.Students.FindAsync(student.Id);
        Assert.Null(deletedStudent); 
    }
}