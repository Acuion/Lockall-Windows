using System.Security.Cryptography.X509Certificates;

namespace Lockall_Windows.Messages.Password
{
    public class MessageWithPassword
    {
        public string resourceid;
        public string password;

        public MessageWithPassword(string resourceid, string password)
        {
            this.resourceid = resourceid;
            this.password = password;
        }
    }
}