﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Logging;


namespace AllTech.FrameWork.Logger
{
    public class Log4NetLogger : ILoggerFacade
    {

       // private static ILog logger = LogManager.GetLogger("WPF Application");
        //private readonly ILog m_Logger =
        //       LogManager.GetLogger(typeof(Log4NetLogger));

        #region ILoggerFacade Members

        /// <summary>
        /// Writes a log message.
        /// </summary>
        /// <param name="message">The message to write.</param>
        /// <param name="category">The message category.</param>
        /// <param name="priority">Not used by Log4Net; pass Priority.None.</param>
        public void Log(string message, Category category, Priority priority)
        {
            //switch (category)
            //{
            //    case Category.Debug:
            //        m_Logger.Debug(message);
            //        break;
            //    case Category.Warn:
            //        m_Logger.Warn(message);
            //        break;
            //    case Category.Exception:
            //        m_Logger.Error(message);
            //        break;
            //    case Category.Info:
            //        m_Logger.Info(message);
            //        break;
            //}
        }

        #endregion
 
    }
}
