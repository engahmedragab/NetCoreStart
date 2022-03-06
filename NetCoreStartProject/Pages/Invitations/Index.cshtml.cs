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
    public class IndexModel : PageModel
    {
        private readonly NetCoreStartProject.Data.DataContext _context;

        public IndexModel(NetCoreStartProject.Data.DataContext context)
        {
            _context = context;
        }

        public IList<Invitation> Invitation { get;set; }

        public async Task OnGetAsync()
        {
            Invitation = await _context.Invitations
                .Include(i => i.City)
                .Include(i => i.Country)
                .Include(i => i.User).ToListAsync();
        }
    }
}
