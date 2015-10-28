using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using System.Threading.Tasks;

namespace Receiver
{
    class Program
    {
        static void Main(string[] args)
        {
          string eventHubConnectionString = "{Endpoint=sb://infotron-ns.servicebus.windows.net/;SharedAccessKeyName=ReceiveRule;SharedAccessKey=qHLYZvpuX2aBLijgkLJew4CpdUEPHlt6BSLQ/8ZjELU=}";
          string eventHubName = "{infotron}";
          string storageAccountName = "{infotroneventhubstorage}";
          string storageAccountKey = "{+QJ2u4jJNKaU9EUcFGlHa2yWQOTZjrRHF3DS4ZmpoJYfPIydqvAKj7MouytLnvR/1qdaUSc1rrNBeHtQbBeaYQ==}";
          string storageConnectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}",
              storageAccountName, storageAccountKey);

          string eventProcessorHostName = Guid.NewGuid().ToString();
          EventProcessorHost eventProcessorHost = new EventProcessorHost(eventProcessorHostName, eventHubName, EventHubConsumerGroup.DefaultGroupName, eventHubConnectionString, storageConnectionString);
          Console.WriteLine("Registering EventProcessor...");
          eventProcessorHost.RegisterEventProcessorAsync<SimpleEventProcessor>().Wait();

          Console.WriteLine("Receiving. Press enter key to stop worker.");
          Console.ReadLine();
          eventProcessorHost.UnregisterEventProcessorAsync().Wait();
        }
    }
}
