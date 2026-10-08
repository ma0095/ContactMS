using Serilog.Core;
using Serilog.Events;
using System;

namespace ContactMS.Logging
{
    public class UtcTimestampEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty("DateTimeUtc", DateTime.UtcNow));
        }
    }
}
