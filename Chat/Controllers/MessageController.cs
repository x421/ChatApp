using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using Chat.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {

        // GET: api/Message
        [HttpGet("{id}")] //msg cnt
        public ActionResult<IEnumerable<Message>> Get(int id)
        {
            SQLiteConnection conn = new SQLiteConnection("Data Source=/base/base.db;Version=3;");
            conn.Open();
            SQLiteCommand cmd = conn.CreateCommand();

            cmd.CommandText = "SELECT Users.login, Messages.message FROM Users INNER JOIN Messages ON Users.id=Messages.userId ORDER BY Messages.id LIMIT "+id+"";

            SQLiteDataReader rd = cmd.ExecuteReader();

            List<Message> msgs = new List<Message>(id);
            int i = 0;

            while (rd.Read())
            {
                Message tmp = new Message();

                tmp.login = rd.GetString(0);
                tmp.message = rd.GetString(1);

                msgs.Add(tmp);
                i++;
            }

            rd.Close();

            return msgs;
        }

        // POST: api/Message
        [HttpPost]
        public ActionResult Post([FromBody] Message msg)
        {
            SQLiteConnection conn = new SQLiteConnection("Data Source=D:/base.db;Version=3;");
            conn.Open();
            SQLiteCommand cmd = conn.CreateCommand();

            cmd.CommandText = "SELECT id FROM Users WHERE login='" + msg.login + "'";
            var reader = cmd.ExecuteReader();

            int id = 0;
            if(reader.Read())
                id = reader.GetInt32(0);
            reader.Close();

            if (id != 0)
            {
                cmd.CommandText = "INSERT INTO Messages(userId, message) VALUES (" + id + ", '" + msg.message + "')";
                cmd.ExecuteNonQuery();
                return Ok(0);
            }
            return Ok(-1);
        }

        // PUT: api/Message/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
