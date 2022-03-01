using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NetCoreStartProject.Pages.Invitations
{
    public class InvitationModel : PageModel
    {
        public string Name { get; set; }
        public SelectList Categories { get; set; }
        public int CategoryId { get; set; }
        public void OnGet()
        {
        }
        public void OnPost()
        {
            Name = "Post used";
        }
    }
}
