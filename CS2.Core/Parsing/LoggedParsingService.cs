using System.IO;
using Castle.Core.Logging;
using CS2.Core.Analysis;
using CS2.Core.Logging;
using CS2.Core.Parsing;
using Lucene.Net.Documents;

namespace CS2.Core.Parsing
{
    public class LoggedParsingService : IParsingService, ILoggingService
    {
        private readonly IParsingService inner;
        private ILogger logger = NullLogger.Instance;

        public LoggedParsingService(IParsingService inner)
        {
            this.inner = inner;
        }

        #region ILoggingService Members

        public ILogger Logger
        {
            get { return logger; }
            set { logger = value; }
        }

        #endregion

        #region IParsingService Members

        public string[] Exclusions
        {
            get { return inner.Exclusions; }
            set { inner.Exclusions = value; }
        }

        public AbstractAnalyzer Analyzer
        {
            get { return inner.Analyzer; }
        }

        public bool TryParse(FileInfo file, out Document document)
        {
            bool couldParse = inner.TryParse(file, out document);

            Logger.InfoFormat(couldParse ? "Done parsing file {0}" : "Error parsing file {0}", file.FullName);

            return couldParse;
        }

        #endregion
    }
}