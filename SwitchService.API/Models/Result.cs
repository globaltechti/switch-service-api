using System.Net;

namespace SwitchService.API.Models
{
    public class Result<T> where T : class
    {

        public T Content { get; set; }
        public List<string> Messages { get; set; } = new List<string>();
        public HttpStatusCode StatusCode { get; set; }

        public void AddMessage(string message) { 

          Messages.Add(message);    
        } 

        public void AddMessages(IList<string> messages)
        {
            Messages.AddRange(messages);
        }
    }
}
