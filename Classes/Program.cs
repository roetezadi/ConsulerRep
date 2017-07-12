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

namespace TelegramBOT_Right
{
    class Program
    {
        private static string Token = "436848122:AAED6oVgCAAkGOXYW5lusmOm8d3MkjVni6w";
        //creating a client
        static HttpClient client = new HttpClient();


        static void Main(string[] args)
        {
            RunBotAsync();
        }
        public static async Task RunBotAsync()
        {
            //making the bot
            var Bot = new TelegramBot(Token);
            var me = Bot.MakeRequestAsync(new GetMe()).Result;


            //creating for each user a specific id to have it's own state
            var UserId = new Dictionary<long, int>();
            int cnt = 0;


            //making the user's properties
            Person[] persons = new Person[1000000];
            for (int i = 0; i < 1000000; i++)
            {
                persons[i] = new Person();
            }

            //making the keyborads for future
            MyKeyboards mykeyboard = new MyKeyboards();

            //creating instance for API
            API api = new API();

            long offset = 0;

            while (true)
            {
                var updates = Bot.MakeRequestAsync(new GetUpdates() { Offset = offset }).Result;

                try
                {
                    foreach (var update in updates)
                    {
                        //Console.WriteLine("ID {0}",cnt);
                        //Console.WriteLine("offset:{0}",offset);
                        var ChatID = update.Message.Chat.Id;
                        if (!UserId.ContainsKey(ChatID))
                        {
                            Console.WriteLine("new :{0}", ChatID);
                            UserId.Add(ChatID, cnt);
                            persons[UserId[ChatID]].state = "start_state";
                            cnt++;
                        }



                        offset = update.UpdateId + 1;
                        var text = update.Message.Text;
                        Console.WriteLine("OUT", text);
                        persons[UserId[ChatID]].text = text;



                        if (persons[UserId[ChatID]].text == "/start")
                        {
                            string message = " 📌به بات مشاوره خوش آمدید\n❓با نتخاب گزینه پرسش و پاسخ میتوانید از لیست سوالات متداول سوال خود را بیابید و پاسخ آن را مشاهده کنید.\n📝با انتخاب گزینه آزمون روانشناسی میتوانید خود را مورد سنجش قرار دهید. ";
                            var reg = new SendMessage(ChatID, message) { ReplyMarkup = mykeyboard.Main_Menu };
                            persons[UserId[ChatID]].state = "start_state";
                            Bot.MakeRequestAsync(reg);
                        }
                        else if (persons[UserId[ChatID]].text == "❓پرسش و پاسخ" && persons[UserId[ChatID]].state == "start_state")
                        {
                            persons[UserId[ChatID]].state = "Question";
                            RootQuestions RQ = api.GetAllActiveQuestionsByParentId(3);
                            for(int i=0;i< RQ.Value.Questions.Capacity; i++)
                            {
                                persons[UserId[ChatID]].questions.Add(RQ.Value.Questions[i]);
                            }
                            persons[UserId[ChatID]].last_parent_id.Push(0);
                            var reg = new SendMessage(ChatID, "لطفا گروهه مورد نطر خود را وارد کنید:");
                            Bot.MakeRequestAsync(reg);
                        }
                        else if (persons[UserId[ChatID]].text == "📝آزمون روانشناسی" && persons[UserId[ChatID]].state == "start_state")
                        {
                            persons[UserId[ChatID]].state = "Test";
                            List<TestGroup> ltg = api.GetAllActiveTestGroup();
                            persons[UserId[ChatID]].last_parent_id.Push(-1);
                            string message = "لطفا گروه مورد نظر خود را جهت آزمون انتخاب کنید:";
                            var reg = new SendMessage(ChatID, message) {ReplyMarkup = mykeyboard.BuildKey(ltg)};
                            
                            Bot.MakeRequestAsync(reg);
                        }
                        else if (persons[UserId[ChatID]].state == "Test" && persons[UserId[ChatID]].text != "انصراف")
                        {
                            string gid_find = "";
                            for(int i = 0;i< persons[UserId[ChatID]].text.Length; i++)
                            {
                                if(persons[UserId[ChatID]].text[i] == '.')
                                {
                                    break;
                                }
                                gid_find += persons[UserId[ChatID]].text[i];
                            }
                            int pid = persons[UserId[ChatID]].last_parent_id.Peek();
                            string gid = gid_find;

                            if(pid != -1)
                            {
                                int target = 0;
                                for (int i = 0; i < persons[UserId[ChatID]].testing.Count; i++)
                                {
                                    if (persons[UserId[ChatID]].testing[i].Id == gid)
                                    {
                                        target = i;
                                        break;
                                    }
                                }
                                if (persons[UserId[ChatID]].testing[target].IsLeaf == "true")
                                {
                                    List<TestQuestion> tq = api.GetAllQuestionOfTestByTestId(persons[UserId[ChatID]].testing[target].Id);
                                    persons[UserId[ChatID]].state = "Test_Questioning";

                                }
                                else
                                {
                                    List<Test> t = api.getAllActiveQuestionsByParentId(pid, gid);
                                    persons[UserId[ChatID]].testing = t;
                                    string message = "لطفا آزمون مورد نظر خود را انتخاب کنید:";
                                    var reg = new SendMessage(ChatID, message) { ReplyMarkup = mykeyboard.BuildKeyForTest(t) };
                                    Bot.MakeRequestAsync(reg);
                                }
                            }
                            else
                            {
                                List<Test> t = api.getAllActiveQuestionsByParentId(pid, gid);
                                persons[UserId[ChatID]].testing = t;
                                string message = "لطفا آزمون مورد نظر خود را انتخاب کنید:";
                                var reg = new SendMessage(ChatID, message) { ReplyMarkup = mykeyboard.BuildKeyForTest(t) };
                                Bot.MakeRequestAsync(reg);
                            }
                            

                            Console.WriteLine("end");
                            persons[UserId[ChatID]].last_parent_id.Push(persons[UserId[ChatID]].text[0]);
                        }
                        else if (persons[UserId[ChatID]].text == "انصراف")
                        {
                            string message = "منو اصلی:";
                            var reg = new SendMessage(ChatID, message);
                            Bot.MakeRequestAsync(reg);
                        }
                        
                    }
                }
                catch
                {

                }
            }
        }


        //set the base url
        
        



    }
}
