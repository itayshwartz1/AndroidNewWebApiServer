using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using noam2.Data;
using noam2.Model;
using noam2.Service;

namespace noam2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class contactsController : Controller
    {
        private static IServiceDB _contactsService;
        private noam2Context database;
        public contactsController(IServiceDB contactsService, noam2Context db)
        {
            _contactsService = contactsService;
            database = db;
        }
   
        //************************************** Contacts *****************************in*************//

        // Post: Contact/
        [HttpPost]
        public async Task<IActionResult> CreateContactAsync(string connectedId ,[Bind("Id,Name,Server")] ContactCreate contactCreate)
        {
            Contact contact = new Contact() { Id = contactCreate.Id, Name = contactCreate.Name, Server = contactCreate.Server, Last = "", Lastdate = "" };
            int isCreates= await _contactsService.CreateContact(connectedId, contact, database);
            if (isCreates == 1)
            {
                return StatusCode(201);
            }
            return StatusCode(401);

        }

        // Get: Contact?connectedId={connectedId}
        [HttpGet]
        public async Task<IActionResult> GetAllContacts(string connectedId)
        {
            
            List<Contact> contacts= await _contactsService.GetAllContacts(connectedId, database);
            if(contacts == null)
            {
                return StatusCode(401);
            }
            return Json(contacts);
        }

        // Get: Contact/{id}?connectedId={connectedId}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContact(string id, string connectedId)
        {
            Contact contact = await _contactsService.GetContact(connectedId, id, database);
            if (contact == null)
            {
                return NotFound();
            }
            return Json(contact);
        }

        // Put: Contact/{id}?connectedId={connectedId}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(string id,string connectedId,[Bind("Name,Server")] ContactUpdateData contactUpdateData)
        {
            int isUpdate= await _contactsService.UpdateContact(connectedId, id, contactUpdateData.Name, contactUpdateData.Server, database);
           if (isUpdate == 1)
            {
                return NoContent();
            }
            return StatusCode(401);
        }

        // Delete: Contact/{id}?connectedId={connectedId}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(string id,string connectedId)
        {
            int isDelete= await _contactsService.DeleteContact(connectedId, id, database);
            if (isDelete == 1)
            {
                return StatusCode(204);
            }
            return StatusCode(401);
        }

        //************************************** Messages ******************************************//



        // Post: Contact/{id}/messages?connectedId={connectedId}
        [HttpPost("{id}/messages")]
        public async Task<IActionResult> CreateMessage(string id,string connectedId, [Bind("Content")] MessageData createMessage)
        {
            int isCreated= await _contactsService.CreateMessage(connectedId, id, createMessage.Content, database);
            if (isCreated == 1)
            {
                return StatusCode(201);
            }
            return StatusCode(401);
        }

        // Get: Contact/{id}/messages/{id2}?connectedId={connectedId}
        [HttpGet("{id}/messages/{id2}")]
        public async Task<IActionResult> GetMessageById(string id, int id2, string connectedId)
        {
            Message message= await _contactsService.GetMessageById(connectedId, id, id2, database);
            if (message != null)
            {
                return Json(message);
            }
            return NotFound();
        }


        // Get: Contact/{id}/messages?connectedId={connectedId}
        [HttpGet("{id}/messages")]
        public async Task<IActionResult> GetAllMessages(string id, string connectedId)
        {
            List<Message> messages= await _contactsService.GetAllMessages(connectedId, id, database);
            if (messages != null)
            {
                return Json(messages);
            }
            return StatusCode(401);
        }


        [HttpPut("{id}/messages/{id2}")]
        public async Task<IActionResult> UpdateMessageById(string id, int id2,string connectedId, [Bind("Content")] MessageData message)
        {
            int isUpdate= await _contactsService.UpdateMessageById(connectedId, id, id2, message.Content, database);
            if (isUpdate == 1)
            {
                return StatusCode(204);
            }
            return NotFound();
        }

        [HttpDelete("{id}/messages/{id2}")]
        public async Task<IActionResult> DeleteMessageById(string connectedId,string id, int id2)
        {
            int isDelete= await _contactsService.DeleteMessageById(connectedId, id, id2, database);
            if (isDelete == 1)
            {
                return NoContent();
            }
            return StatusCode(401);
        }


        //************************************************Our Functions*****************************************

        // Get: Contact/User?connectedId={connectedId}
        [HttpGet("User")]
        public async Task<IActionResult> GetUser(string connectedId)
        {
            User user = await _contactsService.GetUser(connectedId, database);
            if (user != null)
            {
                return Json(user);
            }
            return NotFound();
        } 

        // Get: contacts/AllUsers
        [HttpGet("AllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var res = Json(_contactsService.GetAllUsers(database));
            return res;
            

        }



        // Get: Contact/User?connectedId={connectedId}
        [HttpGet("Chats")]
        public async Task<IActionResult> GetChats(string connectedId)
        {
            return Json( _contactsService.GetChats(connectedId, database));
           
        }


        // Post: contacts/User
        [HttpPost("User")]
        public async Task<IActionResult> CreateUser([Bind("Id,Name,Password, Server, Contacts")] User user)
        {
            int isCreates = await _contactsService.CreateUser(user, database);
            if (isCreates == 1)
            {
                return NoContent();
            }
            return StatusCode(401);
        }


        // Post: contacts/SetToken
        [HttpPost("SetToken")]
        public async Task<IActionResult> SetToken([Bind("Id,Token")] TokenToId tokenToId)
        {
            int isAdded = await _contactsService.SetToken(tokenToId, database);
            if (isAdded == 1)
            {
                return NoContent();
            }
            return StatusCode(401);
        }
    }
}