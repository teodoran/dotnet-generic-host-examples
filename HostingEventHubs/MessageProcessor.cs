using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Examples.HostingEventHubs.EventHubs;
using Hafslund.Telemetry;

namespace Examples.HostingEventHubs
{
    public class MessageProcessor : IPayloadProcessor
    {
        private readonly ITelemetryInsightsLogger _logger;

        public MessageProcessor(ITelemetryInsightsLogger logger)
        {
            _logger = logger;
        }

        public async Task<ProcessorStatus> OpenAsync(string partitionId)
        {
            _logger.TrackTrace("MessageProcessor opening", new
            {
                Partition = partitionId,
            });

            return await Task.FromResult(ProcessorStatus.Open);
        }

        public async Task<ProcessorStatus> CloseAsync(string partitionId, string closeReason)
        {
            _logger.TrackTrace("MessageProcessor closing", new
            {
                Partition = partitionId,
                Reason = closeReason,
            });

            return await Task.FromResult(ProcessorStatus.Closed);
        }

        public async Task<ProcessorStatus> ProcessEventsAsync(string partitionId, IEnumerable<byte[]> eventPayloads)
        {
            var receivedMessages = eventPayloads.Select(bytes => Encoding.UTF8.GetString(bytes));

            _logger.TrackTrace("MessageProcessor sending mottatte hendelser", new
            {
                NoOfMessagesReceived = eventPayloads.Count(),
                FirstPayload = receivedMessages.FirstOrDefault(),
            });

            return await Task.FromResult(ProcessorStatus.EventsProcessed);
        }

        public async Task<ProcessorStatus> ProcessErrorAsync(string partitionId, Exception error)
        {
            _logger.TrackException(error, new
            {
                Partition = partitionId,
            });

            return await Task.FromResult(ProcessorStatus.ErrorHandled);
        }
    }
}