using ElectronicJournalForTheInstitute.Data;
using ElectronicJournalForTheInstitute.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ElectronicJournalForTheInstitute.Pages.Students;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    // Список студентів для відображення
    public IList<Student> Students { get; set; } = default!;

    // Модель для форми додавання
    [BindProperty]
    public Student NewStudent { get; set; } = default!;

    // Метод для відображення сторінки (GET)
    public async Task OnGetAsync()
    {
        // Тимчасовий код для створення групи, якщо її немає (ваша група 4КІ2)
        if (!_context.Groups.Any(g => g.Id == 1))
        {
            _context.Groups.Add(new Group { Id = 1, Name = "4КІ2", Year = 4 });
            await _context.SaveChangesAsync();
        }

        Students = await _context.Students.ToListAsync();
    }

    // Метод для обробки форми додавання (POST)
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            Students = await _context.Students.ToListAsync();
            return Page();
        }

        _context.Students.Add(NewStudent);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}