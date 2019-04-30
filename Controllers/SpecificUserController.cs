using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeepThingsAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;

namespace KeepThingsAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SpecificUserController : ControllerBase
    {
        private readonly KTDBContext _context;
        private SqlConnectionController sql = new SqlConnectionController();

        public SpecificUserController(KTDBContext context)
        {
            _context = context;

            //if (_context.Users.Count() == 0)
            //{

            //    _context.SaveChanges();
            //}
        }

        
        [HttpGet("{id}")]
        public string GetSpecificUser(int id)
        {
            return sql.User_getSpecificUser(id);
        }
    }
}
