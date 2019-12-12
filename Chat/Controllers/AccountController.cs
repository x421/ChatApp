using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data.SQLite;
using Chat.Types;

namespace Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        // POST: api/Account
        [HttpPost] // 0 - reg 1 - auth
        public ActionResult Post([FromBody] UserAction value) // int opId, string login, string pass
        {
            SQLiteConnection conn = new SQLiteConnection("Data Source=/base.db;Version=3;");
            conn.Open();
            SQLiteCommand cmd = conn.CreateCommand();

            int retCode = -1;

            if (value.opId == 0)
                retCode = Reg(cmd, value.login, value.pass);
            else if (value.opId == 1)
                retCode = Auth(cmd, value.login, value.pass);

            return Ok(retCode);
        }

        private int Reg(SQLiteCommand cmd, string login, string pass)
        {
            if (CheckUser(cmd, login) == 1)
                return -1;// пользователь существует

            cmd.CommandText = "INSERT INTO Users(login, password) VALUES ('" + login+"', '"+pass+"')";

            return cmd.ExecuteNonQuery();
        }

        private int Auth(SQLiteCommand cmd, string login, string pass)
        {
            cmd.CommandText = "SELECT id FROM Users WHERE login='" + login + "' AND password='" + pass + "'";
            int ret = -2;
            try
            {
                ret = int.Parse(cmd.ExecuteScalar().ToString());
            }catch(NullReferenceException e)
            {
                ret = -1;
            }
            return ret;
        }

        private int CheckUser(SQLiteCommand cmd, string login)
        {
            cmd.CommandText = "SELECT id FROM Users WHERE login='"+login+"'";

            int ret = 0;
            try
            {
                ret = int.Parse(cmd.ExecuteScalar().ToString());
            }
            catch (NullReferenceException e)
            {
                ret = 0;
            }
            return ret;

        }

        // PUT: api/Account/5
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
