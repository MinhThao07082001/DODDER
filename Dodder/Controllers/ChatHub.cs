using Dodder.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Linq;
namespace Dodder.Controllers
{
    public class ChatHub : Hub
    {
        PRN211Context db = new PRN211Context();
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message + Context.ConnectionId);
        }
        public Task SendPrivateMessage(string user, string message)
        {
            return Clients.User(user).SendAsync("ReceiveMessage", message);
        }
        public async Task AddToGroup(int groupName, int userId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName.ToString());
        }
       

        public async Task SendMessageToGroup(int groupName, string message, int userId)
        {
            Conversation conversation = db.Conversations.Where(c => c.UserAccountId2 == userId || c.UserAccountIdCreator == userId).FirstOrDefault();
            int? receiveUser = conversation.UserAccountIdCreator == userId ? conversation.UserAccountId2 : conversation.UserAccountIdCreator;
            db.Messages.Add(new Message() {ConversationId = groupName, Content = message, UserAccountIdReceiver = receiveUser, UserAccountIdSender = userId });
            db.SaveChanges();
            await Clients.Groups(groupName.ToString()).SendAsync("ReceiveMessage", userId, message);
        }
    }
}