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

namespace TelegramBOT_Right
{
    class MyKeyboards
    {
        public ReplyKeyboardMarkup Main_Menu;
        
        public MyKeyboards()
        {
            Main_Menu = new ReplyKeyboardMarkup();
            Main_Menu.Keyboard = new KeyboardButton[][]
            {
                new KeyboardButton[]
                {
                    new KeyboardButton("📝آزمون روانشناسی"),
                    new KeyboardButton("❓پرسش و پاسخ")
                },
                new KeyboardButton[]
                {
                    new KeyboardButton("💰خرید تمام قابلیت‌ها"),
                    new KeyboardButton("📞ارتباط با مشاوران")
                },
                new KeyboardButton[]
                {
                    new KeyboardButton("📚دانستنی‌ها"),
                    new KeyboardButton("⚠️مشکل فقهی")
                }
            };
        }
        public ReplyMarkupBase BuildKey(List<TestGroup> tg)
        {
            ReplyKeyboardMarkup TestGroupK;
            TestGroupK = new ReplyKeyboardMarkup();
            int n = tg.Count;
            TestGroupK.Keyboard = new KeyboardButton[n][];
            Console.WriteLine(n);
            int rem = n % 2;
            int i = 0;
            int j = 0;
            int cnt = 0;
            for (i = 0; i < n / 2; i++)
            {
                TestGroupK.Keyboard[i] = new KeyboardButton[2];
                for (j = 0; j < 2; j++)
                {
                    TestGroupK.Keyboard[i][j] = new KeyboardButton(tg[cnt].Id + "." + tg[cnt].Name);
                    cnt++;
                }
            }

            Console.WriteLine(rem);
            if (rem != 0 && n != 1)
            {
                TestGroupK.Keyboard[i + 1] = new KeyboardButton[1];
                TestGroupK.Keyboard[i + 1][0] = new KeyboardButton(tg[cnt].Id + "." + tg[cnt].Name);
            }
            else if (n == 1)
            {
                TestGroupK.Keyboard[0] = new KeyboardButton[1];
                TestGroupK.Keyboard[0][0] = new KeyboardButton(tg[0].Id + "." + tg[0].Name);
            }
            Console.WriteLine("kkkkk");
            return TestGroupK;
        }

        public ReplyKeyboardMarkup BuildKeyForTest(List<Test> tg)
        {
            
            ReplyKeyboardMarkup TestGroupK;
            TestGroupK = new ReplyKeyboardMarkup();
            int n = tg.Count;
            Console.WriteLine("here {0}",n);
            
            int rem = n % 2;
            int i = 0;
            int j = 0;
            TestGroupK.Keyboard = new KeyboardButton[n / 2 + rem][];
            int cnt = 0;
            for (i = 0; i < n / 2; i++)
            {
                TestGroupK.Keyboard[i] = new KeyboardButton[2];
                for (j = 0; j < 2; j++)
                {
                    Console.WriteLine(tg[i + j].Name);
                    TestGroupK.Keyboard[i][j] = new KeyboardButton(tg[cnt].Id + "." + tg[cnt].Name);
                    cnt++;
                }
            }
            Console.WriteLine(rem);
            if (rem != 0 && n != 1)
            {
                TestGroupK.Keyboard[i + 1] = new KeyboardButton[1];
                TestGroupK.Keyboard[i + 1][0] = new KeyboardButton(tg[cnt].Id + "." + tg[cnt].Name);
            }
            else if (n == 1)
            {
                TestGroupK.Keyboard[0] = new KeyboardButton[1];
                TestGroupK.Keyboard[0][0] = new KeyboardButton(tg[0].Id + "." + tg[0].Name);
            }
            Console.WriteLine("kui");
            return TestGroupK;
        }
    }
    
}
