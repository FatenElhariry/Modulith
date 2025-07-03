using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using Serilog.Parsing;

namespace API
{
    public static class MySinkExtensions
    {
        public static LoggerConfiguration MySink(
                  this LoggerSinkConfiguration loggerConfiguration,
                  IFormatProvider innerSink = null)
        {
            return loggerConfiguration.Sink(new MessageOverrideSink(innerSink));
        }
    }
    public class MessageOverrideSink : ILogEventSink
    {
        private readonly IFormatProvider _innerSink;

        public MessageOverrideSink(IFormatProvider innerSink)
        {
            _innerSink = innerSink;
        }
        public void Emit(LogEvent logEvent)
        {
            // Example: Always set the message to "Overridden!"
            var newLogEvent = new LogEvent(
                logEvent.Timestamp,
                logEvent.Level,
                logEvent.Exception,
                new MessageTemplate("Overridden!", new List<MessageTemplateToken>()),
                logEvent.Properties.Select(p => new LogEventProperty(p.Key, p.Value))
            );

            Console.WriteLine(newLogEvent.RenderMessage(_innerSink));
        }
    }
}
