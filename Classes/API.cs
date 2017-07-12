using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using NetTelegramBotApi;
using NetTelegramBotApi.Requests;
using NetTelegramBotApi.Types;
using Newtonsoft.Json;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;

namespace TelegramBOT_Right
{
    class API
    {
        /************************ConsultationTest*******************************
         *******************************************************************/
        public List<Test> getAllActiveQuestionsByParentId(int pId, string gId)
        {
            string path = "http://95.211.188.247:8020/api/ConsultationTest/GetAllActiveTestNameByPidAndGroupId?pId=" + pId + "&groupId=" + gId;
            if(pId == -1)
                path = "http://95.211.188.247:8020/api/ConsultationTest/GetAllActiveTestNameByPidAndGroupId?pId=null&groupId=" + gId;
            RootTest mytest = null;
            List<Test> WantTest = new List<Test>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(path);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    HttpResponseMessage response = client.GetAsync(path).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        //getting the json string
                        string jsonString = response.Content.ReadAsStringAsync().Result;

                        //converting th json string to objects
                        mytest = JsonConvert.DeserializeObject<RootTest>(jsonString);

                        string notgood = "{";
                        for (int i = 43; i < jsonString.Length - 1; i++)
                        {
                            notgood += jsonString[i];
                        }
                        Console.WriteLine(notgood);
                        var serializer = new JavaScriptSerializer();
                        serializer.RegisterConverters(new[] { new DynamicJsonConverter() });

                        dynamic obj = serializer.Deserialize(notgood, typeof(object));
                        dynamic data = serializer.Deserialize(notgood, typeof(object));
                        Console.WriteLine("hello {0}", data.MyData.Count);
                        
                        for (int i = 0; i < data.MyData.Count; i++)
                        {
                            Test tg = new Test();
                            tg.Discription = data.MyData[i].Discription;
                            tg.GroupId = data.MyData[i].GroupId;
                            tg.GroupName = data.MyData[i].GroupName;
                            tg.Id = data.MyData[i].Id;
                            tg.IsActive = data.MyData[i].IsActive;
                            tg.IsLeaf = data.MyData[i].IsLeaf;
                            tg.LevelNumber = data.MyData[i].LevelNumber;
                            tg.Name = data.MyData[i].Name;
                            tg.PIdName = data.MyData[i].PIdName;
                            tg.QuestionNumber = data.MyData[i].QuestionNumber;
                            
                            WantTest.Add(tg);
                        }
                        Console.WriteLine("want {0}", WantTest.Count);
                    }
                }
                catch
                {

                }
            }
            return WantTest;
        }


        /*************************ConsultationTest****************************
         *******************************************************************/
        public List<TestGroup> GetAllActiveTestGroup()
        {
            string path = "http://95.211.188.247:8020/api/ConsultationTest/GetAllActiveTestGroup";
            RootTestGroup mytest = null;
            TestGroupValue rgv = null;
            List<TestGroup> WantTest = new List<TestGroup>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(path);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    HttpResponseMessage response = client.GetAsync(path).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        //getting the json string
                        string jsonString = response.Content.ReadAsStringAsync().Result;
                        Console.WriteLine(jsonString);
                        //converting th json string to objects
                        mytest = JsonConvert.DeserializeObject<RootTestGroup>(jsonString);
                      
                      
                        string notgood = "{";
                        for(int i = 43; i < jsonString.Length-1; i++)
                        {
                            notgood += jsonString[i];
                        }
                        Console.WriteLine(notgood);
                        var serializer = new JavaScriptSerializer();
                        serializer.RegisterConverters(new[] { new DynamicJsonConverter() });

                        dynamic obj = serializer.Deserialize(notgood, typeof(object));
                        dynamic data = serializer.Deserialize(notgood, typeof(object));
                        Console.WriteLine("hello {0}",data.MyData.Count);
                        for(int i = 0; i < data.MyData.Count; i++)
                        {
                            TestGroup tg = new TestGroup();
                            tg.Id = data.MyData[i].Id;
                            tg.Name = data.MyData[i].Name;
                            WantTest.Add(tg);
                        }
                        Console.WriteLine(WantTest.Count);
                    }
                }
                catch
                {

                }
            }
            return WantTest;
        }
        /***********************ConsultationTest*******************************
         *******************************************************************/
        public List<TestQuestion> GetAllQuestionOfTestByTestId(string id)
        {
            string path = "http://95.211.188.247:8020/api/ConsultationTest/GetAllQuestionOfTestByTestId?id=" + id;
            RootTestQuestion mytest = null;
            List<TestQuestion> WantTest = new List<TestQuestion>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(path);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    HttpResponseMessage response = client.GetAsync(path).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        //getting the json string
                        string jsonString = response.Content.ReadAsStringAsync().Result;
                        Console.WriteLine(jsonString);
                        //converting th json string to objects
                        mytest = JsonConvert.DeserializeObject<RootTestQuestion>(jsonString);

                        string notgood = "{";
                        for (int i = 43; i < jsonString.Length - 1; i++)
                        {
                            notgood += jsonString[i];
                        }
                        Console.WriteLine(notgood);
                        var serializer = new JavaScriptSerializer();
                        serializer.RegisterConverters(new[] { new DynamicJsonConverter() });

                        dynamic obj = serializer.Deserialize(notgood, typeof(object));
                        dynamic data = serializer.Deserialize(notgood, typeof(object));
                        Console.WriteLine("hello {0}", data.MyData.Count);
                        for (int i = 0; i < data.MyData.Count; i++)
                        {
                            TestQuestion tg = new TestQuestion();
                            tg.Id = data.MyData[i].Id;
                            tg.MaxAnswer = data.MyData[i].MaxAnswer;
                            tg.TestId = data.MyData[i].TestId;
                            tg.TestName = data.MyData[i].TestName;
                            tg.TestOptionses = data.MyData[i].TestOptionses;
                            tg.Text = data.MyData[i].Text;

                            WantTest.Add(tg);
                        }
                        Console.WriteLine("questins {0}",WantTest.Count);
                    }
                }
                catch
                {

                }
            }
            return WantTest;
        }

        /***********************Question*******************************
         *******************************************************************/
         public RootQuestions GetAllActiveQuestionsByParentId(int id)
        {
            string path = "http://95.211.188.247:8020/api/Question/GetAllActiveQuestionsByParentId?pId=" + id;
            RootQuestions mytest = null;
            Questions WantTest = null;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(path);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    HttpResponseMessage response = client.GetAsync(path).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        //getting the json string
                        string jsonString = response.Content.ReadAsStringAsync().Result;
                        Console.WriteLine(jsonString);
                        //converting th json string to objects
                        mytest = JsonConvert.DeserializeObject<RootQuestions>(jsonString);

                        //initializing the return test that the user wants
                        
                    }
                }
                catch
                {

                }
            }
            return mytest;
        }
    }
}
