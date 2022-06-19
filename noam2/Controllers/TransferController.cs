using Chatty.Api.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using noam2.Data;
using noam2.Model;
using noam2.Service;

namespace noam2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class transferController : Controller
    {
        private static IServiceDB _contactsService;
        IHubContext<ChatHub> hub;
        noam2Context database;
        public transferController(IServiceDB contactsService, IHubContext<ChatHub> hub, noam2Context db)
        {
            database = db;
            _contactsService = contactsService;
            this.hub = hub;
        }

        public class TransferMessageObject
        {
            public string From { get; set; }
            public string To { get; set; }
            public string Content { get; set; }

        }

        // Post:transfer/
        [HttpPost]
        public async Task<IActionResult> TransferMessage([Bind("From,To,Content")] TransferMessageObject transferMessageObject)
        {
            int isTransfered =await _contactsService.TransferMessage(transferMessageObject.From, transferMessageObject.To, transferMessageObject.Content, database);
            if (isTransfered == 1)
            {
                await hub.Clients.All.SendAsync("ReceiveMessage", transferMessageObject.Content, transferMessageObject.From, transferMessageObject.To);

                return StatusCode(201);
            }
            return StatusCode(401);
        }
    } }

    
