using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ElectronicJournalForTheInstitute.Data;
using ElectronicJournalForTheInstitute.Models;

namespace ElectronicJournalForTheInstitute.Pages.Groups
{
    public class DetailsModel : PageModel
    {
        private readonly ElectronicJournalForTheInstitute.Data.ApplicationDbContext _context;

        public DetailsModel(ElectronicJournalForTheInstitute.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Group Group { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var group = await _context.Groups.FirstOrDefaultAsync(m => m.Id == id);

            if (group is not null)
            {
                Group = group;

                return Page();
            }

            return NotFound();
        }
    }
}
