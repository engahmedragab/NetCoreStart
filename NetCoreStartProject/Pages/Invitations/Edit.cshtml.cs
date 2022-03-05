using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NetCoreStartProject.Data;
using NetCoreStartProject.Domain;

namespace NetCoreStartProject.Pages.Invitations
{
    public class EditModel : PageModel
    {
        private readonly NetCoreStartProject.Data.DataContext _context;

        public EditModel(NetCoreStartProject.Data.DataContext context)
        {
            _context = context;
        }

        [BindProperty]
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
           ViewData["CityId"] = new SelectList(_context.Cities, "Id", "NameAr");
           ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "NameAr");
           ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Invitation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvitationExists(Invitation.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool InvitationExists(int id)
        {
            return _context.Invitations.Any(e => e.Id == id);
        }
    }
}
