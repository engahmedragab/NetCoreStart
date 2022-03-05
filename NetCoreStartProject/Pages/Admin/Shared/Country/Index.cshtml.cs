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
    public class IndexModel : PageModel
    {
        private readonly NetCoreStartProject.Data.DataContext _context;

        public IndexModel(NetCoreStartProject.Data.DataContext context)
        {
            _context = context;
        }

        public IList<Domain.Country> Country { get;set; }

        public async Task OnGetAsync()
        {
            Country = await _context.Countries
                .Include(c => c.CreatedByUser).ToListAsync();
        }
    }
}
