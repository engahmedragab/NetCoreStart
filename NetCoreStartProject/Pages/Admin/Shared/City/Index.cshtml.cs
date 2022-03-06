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

namespace NetCoreStartProject.Pages.Admin.Shared.City
{
    [Authorize(Roles = "SuperAdmin")]
    public class IndexModel : PageModel
    {
        private readonly NetCoreStartProject.Data.DataContext _context;

        public IndexModel(NetCoreStartProject.Data.DataContext context)
        {
            _context = context;
        }

        public IList<Domain.City> City { get;set; }

        public async Task OnGetAsync()
        {
            City = await _context.Cities
                .Include(c => c.Country)
                .Include(c => c.CreatedByUser).ToListAsync();
        }
    }
}
