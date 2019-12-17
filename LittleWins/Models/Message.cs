using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;

namespace LittleWins.Models
{
    public class Message
    {
        public Message(){}

        public Message(string formQuery)
        {
            var formData = FormData(formQuery);

            Memory = formData["Memory"];

            JObject json = JObject.Parse(Memory);

            DateAnswer = json["twilio"]["collected_data"]["collect_comments"]["answers"]["date"]["answer"].ToString();
            DetailAnwser = json["twilio"]["collected_data"]["collect_comments"]["answers"]["details"]["answer"].ToString();
        }
        public string Memory { get; set; }
        public string DateAnswer { get; set; }
        public string DetailAnwser { get; set; }

        private Dictionary<string, string> FormData(string formQuery)
        {
            var formDictionary = new Dictionary<string, string>();
            var formArray = formQuery.Split('&');
            foreach (var item in formArray)
            {
                var pair = item.Split('=');
                var key = WebUtility.UrlDecode(pair[0]);
                var value = WebUtility.UrlDecode(pair[1]);
                formDictionary.Add(key, value);
            }

            return formDictionary;
        }

    }
}


