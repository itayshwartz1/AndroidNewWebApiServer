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
        List<TokenToId> tokenToIds = new List<TokenToId>() { };
        public async Task<int> CreateContact(string connectedId, Contact contact, noam2Context database)
        {
            ContactExtended contactExtended = new ContactExtended() { Id=contact.Id, Name=contact.Name, Server=contact.Server, 
                Last=contact.Last, Lastdate= contact.Lastdate, MyUser= connectedId };
            database.ContactExtended.Add(contactExtended);
            await database.SaveChangesAsync();
            return 1;

        }

        public async Task<Contact> GetContact(string connectContactId, string contactId, noam2Context database)
        {
            
            List<ContactExtended> contactExtendedsList = database.ContactExtended.ToList();
            foreach(var contact in contactExtendedsList)
            {
                if(contact.Id.Equals(contactId) && contact.MyUser.Equals(connectContactId)){
                    return new Contact() { Id = contact.Id, Name = contact.Name, Server = contact.Server,
                                                Last = contact.Last, Lastdate = contact.Lastdate};
                }
            }
            return null;
        }

        public async Task<List<Contact>> GetAllContacts(string connectId, noam2Context database)
        {
            List<ContactExtended> contactExtendedsList = database.ContactExtended.ToList();
            List<Contact> contactList = new List<Contact>() { };
            foreach (var contact in contactExtendedsList)
            {
                if (contact.MyUser == connectId)
                {
                    contactList.Add(new Contact()
                    {
                        Id = contact.Id,
                        Name = contact.Name,
                        Server = contact.Server,
                        Last = contact.Last,
                        Lastdate = contact.Last
                    });
                }
            }

            return contactList;
        }

        public async Task<int> DeleteContact(string connectContactId, string contactId, noam2Context database)
        {
            var toRemove = database.ContactExtended.Where(c => c.Id == contactId && c.MyUser == connectContactId);
            if(toRemove == null)
            {
                return 0;
            }
            database.ContactExtended.RemoveRange(toRemove);
            await database.SaveChangesAsync();

            return 1;
        }

        public async Task<int> UpdateContact(string connectContactId, string destId, string Name, string Server, noam2Context database)
        {
            ContactExtended contact = database.ContactExtended.ToList().FirstOrDefault(contact => contact.MyUser == connectContactId && contact.Id == destId); 
            var toEdit = database.ContactExtended.Where(c => c.Id == destId && c.MyUser == connectContactId);
            if (toEdit == null)
            {
                return 0;
            }
            database.ContactExtended.RemoveRange(toEdit);
            await database.SaveChangesAsync();
            database.ContactExtended.Add(new ContactExtended() { Id = destId,MyUser = contact.MyUser, Name = Name, Server = Server, Last = contact.Last, Lastdate = contact.Lastdate });
            await database.SaveChangesAsync();
            return 1;
        }

        public async Task<int> CreateMessage(string connectContactId, string destContactId, string content, noam2Context database)
        {
            List<MessageExtanded> messages = database.MessageExtanded.ToList();
            int count = messages.Count();
           
            MessageExtanded newMessage = new MessageExtanded()
            {
                User1 = connectContactId,
                User2 = destContactId,
                Content = content,
                Sent = true,
                Created = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffff")
            };


            MessageExtanded firstMessage = messages.FirstOrDefault(m => m.User1 == connectContactId && m.User2 == destContactId ||
                                                                        m.User2 == connectContactId && m.User1 == destContactId);
            if(firstMessage != null)
            {
                newMessage.User1 = firstMessage.User1;
                newMessage.User2 = firstMessage.User2;
            }
            database.MessageExtanded.Add(newMessage);
            await database.SaveChangesAsync();
            int i = 0;
            return 1;
        }

        public async Task<int> CreateUser(User user, noam2Context database)
        {
            UserExtended userExtended = new UserExtended() {Id= user.Id,Name= user.Name,Password= user.Password,Server = user.Server };
            database.UserExtended.Add(userExtended);
            await database.SaveChangesAsync();
            return 1;
        }

        

        public async Task<int> DeleteMessageById(string connectContactId, string destContactId, int messageId, noam2Context database)
        {

            var toRemove = database.MessageExtanded.Where(c => c.Id == messageId);
            if (toRemove == null)
            {
                return 0;
            }
            database.MessageExtanded.RemoveRange(toRemove);
            await database.SaveChangesAsync();

            return 1;
        }

        

        public async Task<List<Model.Message>> GetAllMessages(string connectContactId, string destContactId, noam2Context database)
        {
            List< MessageExtanded > messageExtandeds  = database.MessageExtanded.ToList();
            List<Model.Message> messages = new List<Model.Message>() { };
            foreach (var message in messageExtandeds)
            {
                if(message.User1.Equals(connectContactId) && message.User2.Equals(destContactId) ||
                    message.User2.Equals(connectContactId) && message.User1.Equals(destContactId))
                {
                    messages.Add(new Model.Message()
                    {
                        Id = message.Id,
                        Content = message.Content,
                        Created = message.Created,
                        Sent = message.Sent
                    });
                }
            }
            return messages;
            
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
            //throw new NotImplementedException();

            return usersList;


        }

        public Task<List<Chat>> GetChats(string id, noam2Context database)
        {
            throw new NotImplementedException();
        }

        

        public async Task<Model.Message> GetMessageById(string connectContactId, string destContactId, int messageId, noam2Context database)
        {
            List<MessageExtanded> messages = database.MessageExtanded.ToList();
            foreach (var message in messages)
            {
                if (message.Id.Equals(messageId))
                {
                    return new Model.Message() { Id = message.Id, Content = message.Content, Created = message.Created, Sent = message.Sent };
                }
            }
            return null;
            
        }

        public async Task<User> GetUser(string id, noam2Context database)
        {
            User user = null;
            List<User> list =await GetAllUsers(database);
            user = list.FirstOrDefault(user => user.Id == id);
            return user;
        }

        public async Task<User> GetUserById(string id, noam2Context database)
        {
            return await GetUser(id, database);
        }

        public Task<int> InviteContact(string from, string to, string server, noam2Context database)
        {
            throw new NotImplementedException();
        }

        public async Task<int> SetToken(TokenToId tokenToId, noam2Context database)
        {
            TokenToId isTokenExist = null;
            isTokenExist = tokenToIds.FirstOrDefault(t => t.Id == tokenToId.Id && t.Token == tokenToId.Token);
            if (isTokenExist == null)
            {
                tokenToIds.Add(tokenToId);
            }
            return 1;
        }

        public Task<int> TransferMessage(string from, string to, string content, noam2Context database)
        {
            throw new NotImplementedException();
        }

        

        public async Task<int> UpdateMessageById(string connectContactId, string destContactId, int messageId, string content, noam2Context database)
        {
            MessageExtanded message = database.MessageExtanded.ToList().FirstOrDefault(m => m.Id == messageId);
            var toEdit = database.MessageExtanded.Where(m => m.Id == messageId);
            if (toEdit == null)
            {
                return 0;
            }
            database.MessageExtanded.RemoveRange(toEdit);
            await database.SaveChangesAsync();
            database.MessageExtanded.Add(new MessageExtanded() { Id = message.Id, Content= content, Created = message.Created,
                                                User1 = message.User1, User2 = message.User2, Sent = message.Sent});
            await database.SaveChangesAsync();
            return 1;
        }

        public async Task notifyTransferToAndroidDevicesAsync(String id, String Content)
        {

            if (FirebaseApp.DefaultInstance == null)
            {
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile("private_key.json")
                });

            }


            TokenToId isTokenExist = null;
            isTokenExist = tokenToIds.FirstOrDefault(t => t.Id == id);
            if (isTokenExist == null)
            {
                return;
            }

            var registrationToken = isTokenExist.Token;

            // See documentation on defining a message payload.
            var message = new FirebaseAdmin.Messaging.Message()
            {
                Data = new Dictionary<string, string>() { { "ITAY", "NOAM" } },
                Token = registrationToken,
                Notification = new Notification() { Title = "this is the title!!", Body = " this is the body!!!" }
            };




            // Send a message to the device corresponding to the provided
            // registration token.
            string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
            // Response is a message ID string.
            Console.WriteLine("Successfully sent message: " + response);
        }
    }
}
