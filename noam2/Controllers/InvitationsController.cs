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
    public class invitationsController : Controller
    {
        private static IServiceDB _contactsService;
        IHubContext<ChatHub> hub;
        noam2Context database;

        public invitationsController(IServiceDB contactsService, IHubContext<ChatHub> hub, noam2Context db) 

        {
            database = db;
            _contactsService = contactsService;
            this.hub = hub;
        }
        public class InvitationsMessage
        {
            public string From { get; set; }
            public string To { get; set; }
            public string Server { get; set; }

        }

        
        [HttpPost]
        public async Task<IActionResult> InviteContact( [Bind("From,To,Server")] InvitationsMessage invitationsMessage)
        {
            int isInvited = await _contactsService.InviteContact(invitationsMessage.From, invitationsMessage.To, invitationsMessage.Server, database);
            if (isInvited == 1)
            {
                await hub.Clients.All.SendAsync("ContactAdded", invitationsMessage.From, invitationsMessage.From, invitationsMessage.Server, invitationsMessage.From, invitationsMessage.To);
                return StatusCode(201);
            }
            return StatusCode(401);
        }
    } 
}



