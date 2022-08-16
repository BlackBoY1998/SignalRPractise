using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestAdminLTE.MyClass;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestAdminLTE.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private ConDB conDB = new ConDB();
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get([FromHeader]string username,[FromHeader] string password)
        {
            return new string[] { "value1="+username, "value2="+password };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(string id)
        {
            return id;
        }

        // POST api/<controller>
        [HttpPost]
        public string Post([FromForm]string username,[FromForm]string password)
        {
            string result = "";
            DataTable dt = conDB.GetData($"SELECT * FROM `user` WHERE username = '{username}';");
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["password"].ToString() == EncodeString.MD5HashCryptography(password))
                {
                    //HttpContext.Session.SetString("login", "1");
                    //return View("Index");
                    result = DataTable2JSON.DataTableToJSON(dt);
                }
            }
            return result;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
