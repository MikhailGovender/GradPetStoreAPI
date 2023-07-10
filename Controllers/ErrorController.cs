using Microsoft.AspNetCore.Mvc;

namespace WebAPIv1.Controllers
{
    /*
     * Implementation of Global Error Handling
     */
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        [HttpGet()]
        public IActionResult Error()
        {
            return Problem();
        }
    }
}
