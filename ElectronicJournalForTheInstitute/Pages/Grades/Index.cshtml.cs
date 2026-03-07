using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ElectronicJournalForTheInstitute.Data;
using ElectronicJournalForTheInstitute.Models;

namespace ElectronicJournalForTheInstitute.Pages.Grades
{
    public class IndexModel : PageModel
    {
        private readonly ElectronicJournalForTheInstitute.Data.ApplicationDbContext _context;

        public IndexModel(ElectronicJournalForTheInstitute.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Grade> Grade { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Grade = await _context.Grades
                .Include(g => g.Lesson)
                .Include(g => g.Student).ToListAsync();
        }
    }
}
