namespace Core.Workflow
{
    public class WorkflowContext
    {
        public string CorrelationId { get; set; }
        public object Data { get; set; }

        public WorkflowContext(string correlationId, object data)
        {
            CorrelationId = correlationId;
            Data = data;
        }
    }
} 