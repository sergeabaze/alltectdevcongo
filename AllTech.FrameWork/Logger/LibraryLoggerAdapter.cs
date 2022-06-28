using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Logging;


namespace AllTech.FrameWork.Logger
{
    public class LibraryLoggerAdapter : ILoggerFacade
    {
        // private static ILog logger = LogManager.GetLogger("WPF Application");
        #region ILoggerFacade Members

        public void Log(string message, Category category, Priority priority)
        {
            // Log.Write(message, category.ToString(), (int)priority);
            if (category == Category.Exception)
            {
               // Exception(new Exception(message), ExceptionPolicies.Default);
                return;
            }

            //Logger.Write(message, category.ToString(), (int)priority);
        }

        /// <summary>
        /// Logs an entry using the Enterprise Library Logging.
        /// </summary>
        /// <param name="entry">the LogEntry object used to log the 
        /// entry with Enterprise Library.</param>
        //public void Log(LogEntry entry)
        //{
        //    Logger.Write(entry);
        //}

        // Other methods if needed, i.e., a default Exception logger.
        public void Exception(Exception ex)
        { // do stuff }

        #endregion
        }
    }
}
