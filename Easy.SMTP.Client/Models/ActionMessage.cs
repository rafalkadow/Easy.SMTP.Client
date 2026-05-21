using System;

namespace Easy.SMTP.Client.Models
{
    [Serializable()]
    public class ActionMessage
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public bool ShowMessage { get; set; }
    }
}
