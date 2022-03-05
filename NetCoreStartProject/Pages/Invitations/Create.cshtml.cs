using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NetCoreStartProject.Data;
using NetCoreStartProject.Domain;

namespace NetCoreStartProject.Pages.Invitations
{
    public class CreateModel : PageModel
    {
        private readonly NetCoreStartProject.Data.DataContext _context;

        public CreateModel(NetCoreStartProject.Data.DataContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CityId"] = new SelectList(_context.Cities, "Id", "NameAr");
        ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "NameAr");
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Invitation Invitation { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Invitations.Add(Invitation);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
