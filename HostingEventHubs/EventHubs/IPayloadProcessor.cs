using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Examples.HostingEventHubs.EventHubs
{
    public interface IPayloadProcessor
    {
        Task<ProcessorStatus> OpenAsync(string partitionId);

        Task<ProcessorStatus> CloseAsync(string partitionId, string closeReason);

        Task<ProcessorStatus> ProcessEventsAsync(string partitionId, IEnumerable<byte[]> eventPayloads);

        Task<ProcessorStatus> ProcessErrorAsync(string partitionId, Exception error);
    }
}