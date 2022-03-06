using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NetCoreStartProject.Data;
using NetCoreStartProject.Domain;

namespace NetCoreStartProject.Pages.Invitations
{
    [Authorize(Roles = "SuperAdmin")]
    public class DetailsModel : PageModel
    {
        private readonly DataContext _context;

        public DetailsModel(DataContext context)
        {
            _context = context;
        }

        public Invitation Invitation { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Invitation = await _context.Invitations
                .Include(i => i.City)
                .Include(i => i.Country)
                .Include(i => i.User).FirstOrDefaultAsync(m => m.Id == id);

            if (Invitation == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
