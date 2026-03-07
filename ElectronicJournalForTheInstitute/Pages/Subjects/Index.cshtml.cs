using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ElectronicJournalForTheInstitute.Data;
using ElectronicJournalForTheInstitute.Models;

namespace ElectronicJournalForTheInstitute.Pages.Subjects
{
    public class IndexModel : PageModel
    {
        private readonly ElectronicJournalForTheInstitute.Data.ApplicationDbContext _context;

        public IndexModel(ElectronicJournalForTheInstitute.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Subject> Subject { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Subject = await _context.Subjects
                .Include(s => s.Teacher).ToListAsync();
        }
    }
}
