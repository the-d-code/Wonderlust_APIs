namespace WONDERLUST_PROJECT_APIs.Controllers
{
    using WONDERLUST_PROJECT_APIs.Models.DbModels;
    using WONDERLUST_PROJECT_APIs.Models.InputModels;
    using WONDERLUST_PROJECT_APIs.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly ILogger logger;
        private readonly AuthService authService;

        public UserController(ILogger logger, AuthService authService)
        {
            this.authService = authService;
            this.logger = logger;
        }

        
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult<User[]> GetAll()
        {
            try
            {
                var users = this.authService.GetAll();
                return Ok(users);
            }
            catch (Exception error)
            {
                logger.LogError(error.Message);
                return StatusCode(500);
            }
        }

        [HttpPost("ChangeRole")]
        [Authorize(Roles = "Administrator")]
        public ActionResult<User> ChangeRole(UserChangeRoleInputModel model)
        {
            try
            {
                var user = this.authService.ChangeRole(model.Email, model.Role);
                return Ok(user);
            }
            catch (Exception error)
            {
                logger.LogError(error.Message);
                return StatusCode(500);
            }
        }

        //[HttpPost("ChangePassword")]
      
        //public ActionResult<User> ChangePassword(ChangePasswordModel model)
        //{
        //    try
        //    {
        //        var user = this.authService.ChangePassword(model.Password, model.New_Password);
        //        return Ok(user);
        //    }
        //    catch (Exception error)
        //    {
        //        logger.LogError(error.Message);
        //        return StatusCode(500);
        //    }
        //}


    }
}
