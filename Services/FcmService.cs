using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Services
{
    public class FcmService
    {
        static string serverKey = "AAAAc-Z-YyU:APA91bEvE0PWfHAJqYSbj1nU-qBlEtKXwv1SrP5mmBhuxSAIjyxQp6g3FlXRfDK9eM5JDNsFWdrBpLdJQQphqWijOYwSN_6L4EO08-qhCSHXEevSU28iRZRbZQHlM8uCf52PHe9OJfpu";
        static string SenderId = "497788281637";

        public static string SendNotificationFromFirebaseCloud(string title, string description, string topicName = "default")
        {
            try
            {
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "POST";
                tRequest.ContentType = "application/json";
                var objNotification = new
                {
                    to = "/topics/" + topicName,
                    notification = new
                    {
                        title = title,
                        body = description,
                        sound = "default",
                        priority = "high",
                        click_action = "FCM_PLUGIN_ACTIVITY",
                    },
                    data = new
                    {
                        title = title,
                        body = description,
                        customData = new
                        {
                            Timeout = 30,
                            OrderId = 91
                        }
                    }
                };
             
                string jsonNotificationFormat = JsonConvert.SerializeObject(objNotification);

                Byte[] byteArray = Encoding.UTF8.GetBytes(jsonNotificationFormat);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", serverKey));
                tRequest.Headers.Add(string.Format("Sender: id={0}", SenderId));

                tRequest.ContentLength = byteArray.Length;
                tRequest.ContentType = "application/json";
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String responseFromFirebaseServer = tReader.ReadToEnd();

                                return responseFromFirebaseServer;

                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string SendNotificationFromFirebaseCloud( string title, string description, List<string> deviceTokens)
        {
            try
            {
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "POST";
                tRequest.ContentType = "application/json";
                if (deviceTokens == null || deviceTokens.Count == 0)
                    throw new Exception("The value should be a list of registration tokens to which to send the multicast message. The array must contain at least 1 and at most 1000 registration tokens. To send a message to a single device, use the to parameter.");

                var objNotification = new
                {
                    registration_ids = deviceTokens,
                    notification = new
                    {
                        title = title,
                        body = description,
                        sound = "default",
                        priority = "high",
                        click_action = "FCM_PLUGIN_ACTIVITY",
                    },
                    data = new
                    {
                        title = title,
                        body = description,
                        customData = new
                        {
                            url = "http://google.com"
                        }
                    }
                };

                string jsonNotificationFormat = Newtonsoft.Json.JsonConvert.SerializeObject(objNotification);

                Byte[] byteArray = Encoding.UTF8.GetBytes(jsonNotificationFormat);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", serverKey));
                tRequest.Headers.Add(string.Format("Sender: id={0}", SenderId));

                tRequest.ContentLength = byteArray.Length;
                tRequest.ContentType = "application/json";
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String responseFromFirebaseServer = tReader.ReadToEnd();

                                return responseFromFirebaseServer;

                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
