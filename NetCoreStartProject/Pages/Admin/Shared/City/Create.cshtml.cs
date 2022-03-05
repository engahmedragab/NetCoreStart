using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NetCoreStartProject.Data;
using NetCoreStartProject.Domain;
using NetCoreStartProject.Extensions;

namespace NetCoreStartProject.Pages.Admin.Shared.City
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
        ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "NameAr");
        ViewData["CreatedBy"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Domain.City City { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            City.CreationDate = DateTime.Now;
            City.LastModifiedDate = DateTime.Now;
            City.CreatedBy = HttpContext.GetUserGuid();
            City.LastModifiedBy = HttpContext.GetUserGuid();
            City.IsDeleted = false;
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Cities.Add(City);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
