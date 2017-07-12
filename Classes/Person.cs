using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBOT_Right
{
    class Person
    {
        public int id;
        public string text;
        public string state;

        public Stack<int> last_parent_id;
        public List<Questions> questions;
        public List<TestGroup> test_group;
        public List<Test> testing;
        public Person()
        {
            last_parent_id = new Stack<int>();
            questions = new List<Questions>();
            test_group = new List<TestGroup>();
            testing = new List<Test>();
    }
    }
}
