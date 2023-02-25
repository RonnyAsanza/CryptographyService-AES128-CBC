using Microsoft.AspNetCore.Mvc;

namespace Encrypted_and_Decrypted.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/cryptography")]
    public class CryptographyController : Controller
    {
        private readonly ICryptographyService _cryptographyService;
        public CryptographyController(ICryptographyService cryptographyService)
        {
            _cryptographyService = cryptographyService;
        }

        [HttpPost]
        public ActionResult Encrypt([FromBody] object obj)
        {
            return new JsonResult(_cryptographyService.Encrypt(obj));
        }

        [HttpGet]
        public ActionResult Decrypt([FromHeader] string obj)
        {
            return new JsonResult(_cryptographyService.Decrypt(obj));
        }

    }
}
