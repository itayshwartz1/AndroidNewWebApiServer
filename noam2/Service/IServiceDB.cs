using noam2.Data;
using noam2.Model;
using static noam2.Controllers.contactsController;

namespace noam2.Controllers
{
    public interface IServiceDB
    {
        //Contacts
        public Task<List<Contact>> GetAllContacts(string connectId, noam2Context database);
        public Task<Contact> GetContact(string connectContactId, string contactId, noam2Context database);
        public Task<int> CreateContact(string connectedId, Contact contact, noam2Context database);
        public Task<int> UpdateContact(string connectContactId, string destId, string Name, string Server, noam2Context database);
        public Task<int> DeleteContact(string connectContactId, string contactId, noam2Context database);

        // Messages
        public Task<int> CreateMessage(string connectContactId, string destContactId, string content, noam2Context database);
        public Task<Message> GetMessageById(string connectContactId, string destContactId, int messageId, noam2Context database);
        public Task<List<Message>> GetAllMessages(string connectContactId, string destContactId, noam2Context database);
        public Task<int> UpdateMessageById(string connectContactId, string destContactId, int messageId, string message, noam2Context database);
        public Task<int> DeleteMessageById(string connectContactId, string destContactId, int messageId, noam2Context database);

        public Task<int> TransferMessage(string from, string to, string content, noam2Context database);
        public Task<int> InviteContact(string from, string to, string server, noam2Context database);


        ////////////////////////////////////////

        public Task<User> GetUser(string id, noam2Context database);
        public Task<List<User>> GetAllUsers(noam2Context database);

        public Task<int> CreateUser(User user, noam2Context database);

        public Task<List<Chat>> GetChats(string id, noam2Context database);

        public Task<User> GetUserById(string id, noam2Context database);

        public Task<int> SetToken(TokenToId tokenToId, noam2Context database);


    }
}
