using Microsoft.AspNetCore.Mvc;

namespace Server.Models
{
    public class VersionsModel
    {
        [BindProperty]
        public string Version { get; set; }
    }
}
