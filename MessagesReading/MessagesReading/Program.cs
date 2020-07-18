using System;
using System.Linq;
using System.Text;
using AE.Net.Mail;

namespace MessagesReading
{
    internal static class Program
    {
        public static void Main()
        {
            const string host = "imap.gmail.com";
            const string username = "email";
            const string password = "password email";
            const short port = 993;
            const bool secure = true;
            
            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                
                using var client = new ImapClient(host, username, password, AuthMethods.Login, port,
                    secure);

                var messages = client.SearchMessages(SearchCondition.Unseen());
                
                var messagesList = messages.Select(s => s.Value.Uid)
                    .Select(messageId => client.GetMessage(messageId, false, true))
                    .Select(dummy => dummy).ToList();

                foreach (var message in messagesList)
                {
                    Console.WriteLine(message.Subject);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}