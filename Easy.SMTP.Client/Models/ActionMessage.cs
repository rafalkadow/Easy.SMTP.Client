using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.SMTP.Models
{
    [Serializable()]
    public class ActionMessage
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public bool ShowMessage { get; set; }
    }
}
