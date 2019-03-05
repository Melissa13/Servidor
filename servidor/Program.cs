using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using System.Web;
using System.Text;
using Newtonsoft.Json;
//using Website.Common.Exceptions.MoodleIntegration;
//using Website.Common.Objects.BC.Moodle;
//using Newtonsoft.Json;

namespace servidor
{

    

    class Program
    {
        static void Main(string[] args)
        {
            
            String token = "5708e1cb28191d8d50401a15e56bea81";

            string createRequest = string.Format("http://www.deltasoft.com.do/moodle/webservice/rest/server.php?wstoken={0}&wsfunction={1}&moodlewsrestformat=json&&criteria[0][key]=email&criteria[0][value]=%%", token, "core_user_get_users");

            Console.WriteLine(createRequest);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(createRequest);
            req.Method = "GET";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = 0;
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream resStream = resp.GetResponseStream();
            StreamReader reader = new StreamReader(resStream);
            string contents = reader.ReadToEnd();

            //Console.WriteLine(contents);

            /*
             *           // Deserialize
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            if (contents.Contains("exception"))
            {
                // Error
                MoodleException moodleError = serializer.Deserialize<MoodleException>(contents);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                // Good
                List<Categoria> categorias = serializer.Deserialize<List<Categoria>>(contents);
                
                Funciones.tempCategorias.AddRange(categorias);

                return View(categorias);
            }

            
        }
             */
            
            // Deserialize
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            if (contents.Contains("exception"))
            {
                // Error
                MoodleException moodleError = serializer.Deserialize<MoodleException>(contents);
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
               // RootObject root = JsonConvert.DeserializeObject<List<User>>(contents);

                RootObject root = JsonConvert.DeserializeObject<RootObject>(contents);

                List<User> users = root.users;


                if(users[0].email != null)
                    Console.WriteLine(users[0].email);


                // User user = JsonConvert.DeserializeObject<User>(contents);
                // List<User> users = serializer.Deserialize<List<User>>(contents);

                Console.WriteLine("lal");
                //Funciones.tempCategorias.AddRange(users);
                // Good
                /*List<Categoria> categorias = serializer.Deserialize<List<Categoria>>(contents);

                Funciones.tempCategorias.AddRange(categorias);

                return View(categorias);*/


            }
        }

        public class MoodleUser
        {
            public string username { get; set; }
            public string password { get; set; }
            //public string firstname { get; set; }
            //public string lastname { get; set; }
            //public string email { get; set; }
        }

        public class MoodleException
        {
            public string exception { get; set; }
            public string errorcode { get; set; }
            public string message { get; set; }
            public string debuginfo { get; set; }
        }

        public class MoodleCreateUserResponse
        {
            public string id { get; set; }
            public string username { get; set; }
        }

    }
}
