using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NGAPI.Data;
using NGAPI.Models;

namespace NGAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public UsersController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AppUser>> GetUsers()
        {
            var users =  _dataContext.Users.ToList();
            return users;
        }

        [HttpGet("{id}")]
        public ActionResult<AppUser> GetUser(int id)
        {
            var user = _dataContext.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

    }
}
