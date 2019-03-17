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
using Newtonsoft.Json.Linq;
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
            string descarga;

            string createRequest = string.Format("http://www.deltasoft.com.do/moodle/webservice/rest/server.php?wstoken={0}&wsfunction={1}&courseid={2}&moodlewsrestformat=json", token, "core_course_get_contents", "10");

           // www.deltasoft.com.do/ moodle / webservice / rest / server.php ? wstoken = 5708e1cb28191d8d50401a15e56bea81 & wsfunction = core_course_get_contents & courseid = 10 & moodlewsrestformat = json

            Console.WriteLine(createRequest);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(createRequest);
            req.Method = "GET";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = 0;
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream resStream = resp.GetResponseStream();
            StreamReader reader = new StreamReader(resStream);
            string contents = reader.ReadToEnd();
            
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
                
                List<Course> courses = JsonConvert.DeserializeObject<List<Course>>(contents);
                Course root = courses[0];
                Module mod = root.Modules[0];
                Content cont = mod.Contents[0];
                Console.WriteLine(cont.Fileurl);
                descarga = cont.Fileurl + "&token=" + token;

                using (var client = new WebClient())
                {
                    client.DownloadFile(descarga, "kek.pdf");
                }
                
            }
        }

        //Validar login
        public void ValidateLogin()
        {
            /*Ese es un placeholder de prueba*/
            String pass = "123456Em";
            /*Ese es un placeholder de prueba*/
            String user = "proyecto2018em";
            /* Este string es constante, no se puede cambiar porque es el
             * que da acceso a la pva a ver si valida o no el usuario que
             * se manda*/
            String service = "moodle_mobile_app";

            string createRequest = string.Format("http://www.deltasoft.com.do/moodle/login/token.php?username=" + user + "&password=" + pass + "&service=" + service);

            Console.WriteLine(createRequest);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(createRequest);
            req.Method = "GET";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = 0;
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream resStream = resp.GetResponseStream();
            StreamReader reader = new StreamReader(resStream);
            string contents = reader.ReadToEnd();

            Console.WriteLine(contents);



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

                Token root = JsonConvert.DeserializeObject<Token>(contents);

                String tok = root.token;
                bool passa = false;

                if (tok != null)
                {
                    //Login
                    passa = true;
                }
                else
                    //no dejar pasar
                    passa = false;

                //Puesto para breakpoint y parar la consola para chequear
                Console.WriteLine("lal");

            }
        }

        //Conseguir usuario
        public void GetUsers()
        {
            String token = "5708e1cb28191d8d50401a15e56bea81";

            /*placeholder de prueba para conseguir datos de usuario
             Si se quieren todos los usuario de moodle se pone: %%*/
            string email = "juandanieljoa@gmail.com";
            string createRequest = string.Format("http://www.deltasoft.com.do/moodle/webservice/rest/server.php?wstoken={0}&wsfunction={1}&moodlewsrestformat=json&&criteria[0][key]=email&criteria[0][value]="+email, token, "core_user_get_users");

            Console.WriteLine(createRequest);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(createRequest);
            req.Method = "GET";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = 0;
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream resStream = resp.GetResponseStream();
            StreamReader reader = new StreamReader(resStream);
            string contents = reader.ReadToEnd();
            

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

                //La lista de todos los usuarios desearalizada de Json osea una lista normal
                List<User> users = root.users;

                //Probando que sirve
                if (users[0].email != null)
                    Console.WriteLine(users[0].fullname);

                //Puesto para breakpoint y parar la consola para chequear
                Console.WriteLine("lal");
               
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
