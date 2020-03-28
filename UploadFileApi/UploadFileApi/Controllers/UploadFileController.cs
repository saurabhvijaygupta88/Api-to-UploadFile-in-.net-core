using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
        public ActionResult<Dictionary<string, string>> Get()
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("key1", "value1");
            keyValuePairs.Add("key2", "value2");
            return keyValuePairs;
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

        [HttpPost("bulkupsert")]
        public async Task<IActionResult> BulkUpsert([FromBody]ConcurrentDictionary<string, string> json)
        {
            return Content(json.ToString());
        }


    }
    public class UploadFile
    {
        public IFormFile File { get; set; }
        public bool ForceOverwrite { get; set; }
    }
}