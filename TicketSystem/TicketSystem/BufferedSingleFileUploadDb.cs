using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Http;

namespace TicketSystem
{
    public class BufferedSingleFileUploadDb
    {
        [Display(Name = "File")]
        public IFormFile FormFile { get; set; }
    }
}
