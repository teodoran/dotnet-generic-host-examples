namespace Examples.HostingEventHubs.EventHubs
{
    public enum ProcessorStatus
    {
        Open,
        Closed,
        EventsProcessed,
        FailedProcessingEvents,
        ErrorHandled,
    }
}