using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRApp.Models
{
    public class MessageModel
    {
        public string Text { get; set; }
        public bool IsOutgoing { get; set; }
    }
}
