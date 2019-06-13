using Microsoft.Azure.EventHubs.Processor;

namespace Examples.HostingEventHubs.EventHubs
{
    public class ProcessorFactory : IEventProcessorFactory
    {
        private readonly IPayloadProcessor _processor;

        public ProcessorFactory(IPayloadProcessor processor)
        {
            _processor = processor;
        }

        public IEventProcessor CreateEventProcessor(PartitionContext context)
        {
            return new Processor(_processor);
        }
    }
}