using Microsoft.AspNetCore.Identity;

namespace ElectronicJournalForTheInstitute.Models;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
}