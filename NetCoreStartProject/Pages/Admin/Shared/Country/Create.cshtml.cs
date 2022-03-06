using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NetCoreStartProject.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace NetCoreStartProject.Pages.Admin.Shared.Country
{
    [Authorize(Roles = "SuperAdmin")]
    public class CreateModel : PageModel
    {
        private readonly NetCoreStartProject.Data.DataContext _context;

        public CreateModel(NetCoreStartProject.Data.DataContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        //ViewData["CreatedBy"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Domain.Country Country { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            Country.CreationDate = DateTime.Now;
            Country.LastModifiedDate = DateTime.Now;
            Country.CreatedBy = HttpContext.GetUserGuid();
            Country.LastModifiedBy = HttpContext.GetUserGuid();
            Country.IsDeleted = false;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Countries.Add(Country);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
