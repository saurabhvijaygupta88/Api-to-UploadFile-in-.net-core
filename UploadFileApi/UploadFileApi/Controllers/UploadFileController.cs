using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace UploadFileApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadFileController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "value";
        }

        [HttpPost("uploads")]
        public async Task<IActionResult> FileUpload([FromForm]UploadFile data)
        {
            string result = string.Empty;
            if (data.File != null)
            {
                using (var reader = new StreamReader(data.File.OpenReadStream()))
                {
                    result = await reader.ReadToEndAsync();
                }
            }
            return Content(result);
        }

    }
    public class UploadFile
    {
        public IFormFile File { get; set; }
        public bool ForceOverwrite { get; set; }
    }
}