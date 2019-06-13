using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;

namespace Examples.HostingEventHubs.EventHubs
{
    public class Processor : IEventProcessor
    {
        private readonly IPayloadProcessor _processor;

        public Processor(IPayloadProcessor processor)
        {
            _processor = processor;
        }

        public async Task OpenAsync(PartitionContext context)
        {
            await _processor.OpenAsync(context.PartitionId);
        }

        public async Task CloseAsync(PartitionContext context, CloseReason reason)
        {
            await _processor.CloseAsync(context.PartitionId, reason.ToString());
        }

        public async Task ProcessEventsAsync(PartitionContext context, IEnumerable<EventData> events)
        {
            var eventPayloads = events.Select(data => data.Body.Array);
            await _processor.ProcessEventsAsync(context.PartitionId, eventPayloads);
            await context.CheckpointAsync();
        }

        public async Task ProcessErrorAsync(PartitionContext context, Exception error)
        {
            await _processor.ProcessErrorAsync(context.PartitionId, error);
        }
    }
}