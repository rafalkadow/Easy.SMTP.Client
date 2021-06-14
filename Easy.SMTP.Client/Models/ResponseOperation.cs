using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.SMTP.Models
{
    public class ResponseOperation
    {
        public bool OperationStatus { get; set; }
        public string Exception { get; set; }
        public string GuidRecord { get; set; }
        public ResponseOperation()
        {
            OperationStatus = false;
        }
    }
}
