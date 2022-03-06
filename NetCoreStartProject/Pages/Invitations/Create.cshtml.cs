using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NetCoreStartProject.Data;
using NetCoreStartProject.Domain;
using NetCoreStartProject.Extensions;

namespace NetCoreStartProject.Pages.Invitations
{
    [Authorize]
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
        //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Invitation Invitation { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            Invitation.CreationDate = DateTime.Now;
            Invitation.LastModifiedDate = DateTime.Now;
            Invitation.UserId = HttpContext.GetUserGuid();
            Invitation.ResponseFrom = Enums.ResponseFrom.Web;
            Invitation.IsDeleted = false;
            Invitation.IsDone = false;
            Invitation.Slug = (Invitation.Groom + " " + Invitation.Bride + " " + DateTime.Now.ToLongDateString()).GenrateSlug();
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Invitations.Add(Invitation);
            await _context.SaveChangesAsync();
            HttpContext.Session.SetString("InvitaionSlug", Invitation.Slug);
            return RedirectToPage("./Invitation", new { slug = Invitation.Slug });
            //return RedirectToPage("./Index");
        }
    }
}
