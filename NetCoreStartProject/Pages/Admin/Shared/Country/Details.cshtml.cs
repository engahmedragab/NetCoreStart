using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NetCoreStartProject.Data;
using NetCoreStartProject.Domain;

namespace NetCoreStartProject.Pages.Admin.Shared.Country
{
    public class DetailsModel : PageModel
    {
        private readonly NetCoreStartProject.Data.DataContext _context;

        public DetailsModel(NetCoreStartProject.Data.DataContext context)
        {
            _context = context;
        }

        public Domain.Country Country { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Country = await _context.Countries
                .Include(c => c.CreatedByUser).FirstOrDefaultAsync(m => m.Id == id);

            if (Country == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
