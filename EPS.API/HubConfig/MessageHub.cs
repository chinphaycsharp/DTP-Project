using EPS.Data;
using EPS.Data.Entities;
using EPS.Service;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace EPS.API.HubConfig
{
    public class MessageHub : Hub
    {
        //private MessageService _messageService;
        private Data.EPSContext _context;
        //public static System.Collections.Generic.List<Message> messages;
        //public MessageHub(MessageService messageService,EPSContext context)
        //{
        //    _context = context;
        //}

        public async void SendMessage(string userName, string messageSend, int user_id, int isAdmin,int clientId)
        {
            var strDate = DateTime.Now.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
            message m = new message();
            m.username = userName;
            m.user_id = user_id;
            m.isAdmin = isAdmin;
            m.createdDate = DateTime.Now;
            m.content = messageSend;
            m.clientId = clientId;
            //await _context.message.AddAsync(m);
            //_context.SaveChanges();
            //await Clients.All.SendAsync("SendMessage", userName, messageSend, user_id, strDate, isAdmin, clientId);
            // await _messageService.SaveMessage(messageDto);
        }

        public void NewUser(string userName, string connectionId)
        {
            //var messages = from d in _context.message where d.isAdmin != 1
            //               group d by d.username into g
            //               select new { Name = g.Key, MaxDate = g.Max(s => s.createdDate) };
            //List<message> m = new List<message>();
            //var data = (from d in _context.message select d).ToList();
            ////foreach (var item in messages.ToList())
            ////{
            ////    var i = data.Where(x => x.username.ToLower() == item.Name.ToLower() && x.createdDate == item.MaxDate).FirstOrDefault();
            ////    m.Add(i);
            ////}
            
            //Clients.Client(connectionId).SendAsync("PreviousMessage", data);
            //Clients.All.SendAsync("NewUser", userName);
        }
    }

    public class Message
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Text { get; set; }
        public string createDateStr { get; set; }
        public int isAdmin { get; set; }
        public int clientId { get; set; }
    }
}
