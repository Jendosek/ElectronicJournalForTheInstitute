using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ElectronicJournalForTheInstitute.Data;
using ElectronicJournalForTheInstitute.Models;

namespace ElectronicJournalForTheInstitute.Pages.Grades
{
    public class CreateModel : PageModel
    {
        private readonly ElectronicJournalForTheInstitute.Data.ApplicationDbContext _context;

        public CreateModel(ElectronicJournalForTheInstitute.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["LessonId"] = new SelectList(_context.Lessons, "Id", "Id");
        ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Grade Grade { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Grades.Add(Grade);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
