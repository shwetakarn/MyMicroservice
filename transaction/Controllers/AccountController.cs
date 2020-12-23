using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace transaction.Controllers
{

    [Route("api/account")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        [HttpGet("balance")]
        public ActionResult Balance()
        {
           return Ok(2000);
        }
    }
}