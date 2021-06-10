using Microsoft.AspNetCore.Mvc;

namespace Server.Models
{
    public class MessageModel {
        [BindProperty]
        public int Id {get; set;}
        [BindProperty]
        public string Name {get; set;}
        [BindProperty]
        public string Text {get; set;}
        [BindProperty]
        public string Time {get; set;}
        [BindProperty]
        public int IsEncrypted {get; set;}
    }
}