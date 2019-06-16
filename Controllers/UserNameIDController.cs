using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeepThingsAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace KeepThingsAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserNameIDController : ControllerBase
    {
        private readonly KTDBContext _context;
        //private SqlConnectionController sql = new SqlConnectionController();

        public UserNameIDController(KTDBContext context)
        {
            _context = context;

            //if (_context.Users.Count() == 0)
            //{

            //    _context.SaveChanges();
            //}
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserNameID>>> GetUsers()
        {
            List<UserNameID> userNameId = new List<UserNameID>();
            List<User> users = _context.Users.ToList();
            UserNameID temp;
            foreach (User user in users)
            {
                temp = new UserNameID();
                temp.id = user.id;
                temp.username = user.username;
                userNameId.Add(temp);
            }
            return userNameId;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            //var user = await _context.Users.FindAsync(Auth_id);
            List<User> user = _context.Users.ToList();
            if (user == null)
            {
                return NotFound();
            }

            return user.Find(item => item.id == id);
        }
    }
}
