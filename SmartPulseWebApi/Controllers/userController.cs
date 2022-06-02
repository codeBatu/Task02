using Helper;
using Microsoft.AspNetCore.Mvc;
using Model;
using Repository;
using ServiceLayer.Service;
using System.Threading.Tasks;

namespace SmartPulseWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class userController : ControllerBase
    {
        
        private readonly UserService _userService;

        public userController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Login")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }
     


       [HttpPut("updateUser")]
        public IActionResult UpdateUser(int id ,[FromBody] UserTable model)
        {
            var response = _userService.Update(id,model);

            return Ok(response);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _userService.GetById(id));
        }


        [HttpPost("Register")]
        public async Task<IActionResult> SingUp([FromBody] UserTable userTableEntity)
        {
            return Ok(await _userService.SignUp(userTableEntity));
        }

        
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
       
    }
}