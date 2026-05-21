namespace Easy.SMTP.Client.Models
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
