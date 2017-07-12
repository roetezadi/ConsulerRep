using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBOT_Right
{
    class TestQuestion
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public int MaxAnswer { get; set; }
        public string TestName { get; set; }
        public string Text { get; set; }
        public List<TestOption> TestOptionses { get; set; }
    }
}
