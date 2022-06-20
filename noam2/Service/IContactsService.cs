using noam2.Model;
using static noam2.Controllers.contactsController;

namespace noam2.Controllers
{
    public interface IContactsService
    {
        //Contacts
        public List<Contact> GetAllContacts(string connectId);
        public Contact GetContact(string connectContactId, string contactId);
        public int CreateContact(string connectedId, Contact contact);
        public int UpdateContact(string connectContactId, string destId, string Name, string Server);
        public int DeleteContact(string connectContactId, string contactId);

        // Messages
        public int CreateMessage(string connectContactId, string destContactId, string content);
        public Message GetMessageById(string connectContactId, string destContactId, int messageId);
        public List<Message> GetAllMessages(string connectContactId, string destContactId);
        public int UpdateMessageById(string connectContactId,  string destContactId, int messageId, string message);
        public int DeleteMessageById(string connectContactId, string destContactId, int messageId);

        public Task<int> TransferMessage(string from, string to, string content);
        public Task<int> InviteContact(string from, string to, string server);


        ////////////////////////////////////////

        public User GetUser(string id);
        public List<User> GetAllUsers();

        public int CreateUser(User user);

        public List<Chat> GetChats(string id);

        public User GetUserById(string id);

        public int SetToken(TokenToId tokenToId);


    }
}
