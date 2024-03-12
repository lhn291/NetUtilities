using NetUtilities.Domain.Enums;
using System;

namespace NetUtilities.Models
{
    public class Log
    {
        public DateTime? DateLog { get; private set; }
        public string TimeZone { get; private set; }
        public LevelEnum Level { get; private set; }
        public string Content { get; private set; }

        public Log()
        {

        }

        public Log(DateTime? dateLog, string timeZone, LevelEnum level, string content)
        {
            DateLog = dateLog;
            TimeZone = timeZone;
            Level = level;
            Content = content;
        }

        public void AppendContent(string content)
        {
            Content = string.Concat(Content, "\r\n", content);
        }
    }
}
