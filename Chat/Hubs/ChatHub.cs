using Chat.Types;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Hubs
{
    public class ChatHub : Hub
    {
        public Task SendMessage(string login, string msg)
        {
            string result = "";
            using (var client = new HttpClient())
            {
                Message cl = new Message();
                cl.login = login;
                cl.message = msg;

                var json = JsonConvert.SerializeObject(cl);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var url = "http://localhost:80/api/Message";

                var response = client.PostAsync(url, data);

                result = response.Result.Content.ReadAsStringAsync().Result;
            }
            if (int.Parse(result) == 0)
                return Clients.All.SendAsync("RecieveMessage", login, msg);
            else
                return null;
        }

        public Task GetHistory(string len)
        {
            string result = "";
            using (var client = new HttpClient())
            {

                var url = "http://localhost:80/api/Message/"+len;

                var response = client.GetAsync(url);

                result = response.Result.Content.ReadAsStringAsync().Result;
            }

            return Clients.Client(Context.ConnectionId).SendAsync("ChatHistory", result);
        }
        public Task RegAndLogin(string opId, string login, string message)
        {
            string result = "";
            using (var client = new HttpClient())
            {
                UserAction cl = new UserAction();
                cl.opId = int.Parse(opId);
                cl.login = login;
                cl.pass = message;

                var json = JsonConvert.SerializeObject(cl);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var url = "http://localhost:80/api/Account";

                var response = client.PostAsync(url, data);

                result = response.Result.Content.ReadAsStringAsync().Result;
            }
            return Clients.Client(Context.ConnectionId).SendAsync("RegAndLoginResult", result);
        }
    }
}
