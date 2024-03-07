using empty2802core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace empty2802core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public DBContext DBContext { get; set; }
        public UsersController(DBContext context)
        {
            DBContext = context;
        }

        [HttpPost(Name = "UserRegister")]
        public async Task Register(Users user)
        {
            if (!DBContext.Users.Any(x => x.Name == user.Name || x.Email == user.Email))
            {
                using var insertInfo = new DBContext();
                await insertInfo.Users.AddAsync(user);
                await insertInfo.SaveChangesAsync();
            }
        }
    }
}
