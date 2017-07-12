using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBOT_Right
{
    class Questions
    {
        public int Id { get; set; }
        public int PId { get; set; }
        public string PIdName { get; set; }
        public string Text { get; set; }
        public string Answer { get; set; }
        public int LevelNumber { get; set; }
        public bool IsLeaf { get; set; }
        public bool IsActive { get; set; }
    }
}
