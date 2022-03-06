using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NetCoreStartProject.Domain;
using System.Threading.Tasks;

namespace NetCoreStartProject.Pages.Invitations
{
    [AllowAnonymous]
    public class InvitationModel : PageModel
    {
        private readonly Data.DataContext _context;

        public InvitationModel(Data.DataContext context)
        {
            _context = context;
        }

        public Invitation Invitation { get; set; }

        public async Task<IActionResult> OnGetAsync(string? slug)
        {
            if (slug == null)
            {
                return NotFound();
            }

            Invitation = await _context.Invitations
                .Include(i => i.City)
                .Include(i => i.Country)
                .Include(i => i.User).FirstOrDefaultAsync(m => m.Slug == slug);

            if (Invitation == null)
            {
                return NotFound();
            }
            return Page();
        }
       
    }
}
