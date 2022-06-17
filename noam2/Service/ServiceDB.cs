using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Mvc;
using noam2.Controllers;
using noam2.Model;
using Google.Apis.Auth.OAuth2;

using static noam2.Controllers.contactsController;
using noam2.Data;

namespace noam2.Service
{




    public class ServiceDB : IServiceDB
    {
        public async Task<int> CreateContact(string connectedId, Contact contact, noam2Context database)
        {
            ContactExtended contactExtended = new ContactExtended() { Id=contact.Id, Name=contact.Name, Server=contact.Server, 
                Last=contact.Last, Lastdate= contact.Lastdate, MyUser= connectedId };
            database.ContactExtended.Add(contactExtended);
            await database.SaveChangesAsync();
            return 1;

        }

        public Task<int> CreateMessage(string connectContactId, string destContactId, string content, noam2Context database)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CreateUser(User user, noam2Context database)
        {
            UserExtended userExtended = new UserExtended() {Id= user.Id,Name= user.Name,Password= user.Password,Server = user.Server };
            database.UserExtended.Add(userExtended);
            await database.SaveChangesAsync();
            return 1;
        }

        public Task<int> DeleteContact(string connectContactId, string contactId, noam2Context database)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteMessageById(string connectContactId, string destContactId, int messageId, noam2Context database)
        {
            throw new NotImplementedException();
        }

        public Task<List<Contact>> GetAllContacts(string connectId, noam2Context database)
        {
            throw new NotImplementedException();
        }

        public Task<List<Model.Message>> GetAllMessages(string connectContactId, string destContactId, noam2Context database)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> GetAllUsers(noam2Context database)
        {
            List<UserExtended> userExtendedList = database.UserExtended.ToList();
            List<ContactExtended> contactExtendedsList = database.ContactExtended.ToList();
            List<User> usersList = new List<User>();



            foreach (var userEx in userExtendedList)
            {
                List<Contact> contactsList = new List<Contact> { };
                foreach (var contactEx in contactExtendedsList)
                {
                    if (contactEx.MyUser.Equals(userEx.Id))
                    {
                        contactsList.Add(new Contact() { Id = contactEx.Id, Name = contactEx.Name,
                            Server = contactEx.Server, Last = contactEx.Last, Lastdate = contactEx.Lastdate });
                    }
                }
                usersList.Add( new User() {Id= userEx.Id,Name= userEx.Name,Password= userEx.Password,
                    Server= userEx.Server,Contacts = contactsList});
                
            }
            throw new NotImplementedException();

            //return usersList;


        }

        public Task<List<Chat>> GetChats(string id, noam2Context database)
        {
            throw new NotImplementedException();
        }

        public Task<Contact> GetContact(string connectContactId, string contactId, noam2Context database)
        {
            throw new NotImplementedException();
        }

        public Task<Model.Message> GetMessageById(string connectContactId, string destContactId, int messageId, noam2Context database)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUser(string id, noam2Context database)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserById(string id, noam2Context database)
        {
            throw new NotImplementedException();
        }

        public Task<int> InviteContact(string from, string to, string server, noam2Context database)
        {
            throw new NotImplementedException();
        }

        public Task<int> SetToken(TokenToId tokenToId, noam2Context database)
        {
            throw new NotImplementedException();
        }

        public Task<int> TransferMessage(string from, string to, string content, noam2Context database)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateContact(string connectContactId, string destId, string Name, string Server, noam2Context database)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateMessageById(string connectContactId, string destContactId, int messageId, string message, noam2Context database)
        {
            throw new NotImplementedException();
        }
    }
}
