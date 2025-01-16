using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace stockManagementClean.API.Controllers.RadiantControllerBase
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RadiantControllerBase : ControllerBase
    {
        public RadiantControllerBase()
        {
            
        }
    }
}
