using NetCoreStartProject.Enums;

namespace NetCoreStartProject.Contracts.V1.Requests
{
    public class ExternalLinkedInCallbackRequest
    {
        public string Code { get; set; }
        public string State { get; set; }

    }
}