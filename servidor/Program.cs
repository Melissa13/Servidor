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
//using Website.Common.Exceptions.MoodleIntegration;
//using Website.Common.Objects.BC.Moodle;
//using Newtonsoft.Json;

namespace servidor
{
    class Program
    {
        static void Main(string[] args)
        {
            String token = "70b758fc3218debfbbe861679b9a6cff";

            MoodleUser user = new MoodleUser();
            user.username = HttpUtility.UrlEncode("judadocu");
            user.password = HttpUtility.UrlEncode("Silver1");

            List<MoodleUser> userList = new List<MoodleUser>();
            userList.Add(user);

            Array arrUsers = userList.ToArray();

            String postData = String.Format("users[0][username]={0}&users[0][password]={1}", user.username, user.password);

            string createRequest = string.Format("http://www.deltasoft.com.do/moodle/webservice/rest/server.php?wstoken={0}&wsfunction={1}&moodlewsrestformat=json", token, "core_course_get_courses");
            Console.WriteLine(createRequest);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(createRequest);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = 0;
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream resStream = resp.GetResponseStream();
            StreamReader reader = new StreamReader(resStream);
            string contents = reader.ReadToEnd();
            System.IO.File.WriteAllText(@"C:\Users\Public\WriteText.txt", contents);


            // Deserialize
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            /*if (contents.Contains("exception"))
            {
                // Error
                MoodleException moodleError = serializer.Deserialize<MoodleException>(contents);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                // Good
                List<Curso> cursos = serializer.Deserialize<List<Curso>>(contents);
                foreach (Curso c in cursos)
                    c.progreso = 0;

                return View(cursos);
            }*/

            Console.ReadKey();
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
