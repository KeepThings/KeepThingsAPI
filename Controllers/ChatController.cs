using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeepThingsAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace KeepThingsAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : Controller
    {
        private readonly KTDBContext _context;
        private SqlConnectionController sql = new SqlConnectionController();
        public ChatController(KTDBContext context)
        {
            _context = context;

            //if (_context.Chat.Count() == 0)
            //{
            //    _context.SaveChanges();
            //}
        }

        [HttpGet("{id}")]
        public string GetChat(int id)
        {
            return sql.Chat_getChats(id);
        }
        [HttpPost]
        public string PostChat(Chat chat)
        {
            return sql.Chat_postChat(chat);
        }
        [HttpDelete("{id}")]
        public string DeleteChat(int id)
        {
            return sql.Chat_deleteChat(id);
        }
        // GET: api/<controller>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<controller>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<controller>
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
