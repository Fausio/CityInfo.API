using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CityInfo.API.Controllers
{
    [Route("api/Files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider;

        public FilesController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
        {
            _fileExtensionContentTypeProvider = fileExtensionContentTypeProvider ?? throw new ArgumentException(nameof(fileExtensionContentTypeProvider));
        }

        [HttpGet("{Id}")]
        public ActionResult Read(int Id)
        {
            var filePath = @"Files/CSI Pictorial Version-beta.pdf";

            if (! System.IO.File.Exists(filePath))
            {
                return NotFound("File NotFound");
            }

            if (!_fileExtensionContentTypeProvider.TryGetContentType(filePath, out var contenttype))
            {
                contenttype = "application/octet-stream";
            }

            var bytes = System.IO.File.ReadAllBytes(filePath);  
            return File(bytes, contenttype, Path.GetFileName(filePath));
        }
    }
}
