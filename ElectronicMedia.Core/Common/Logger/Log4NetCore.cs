/*********************************************************************
 * 
 * PROPRIETARY and CONFIDENTIAL
 * 
 * This is licensed from, and is trade secret of:
 * 
 *          Group 10 - PRN231 - SU23
 *          FPT University, Education and Training zone
 *          Hoa Lac Hi-tech Park, Km29, Thang Long Highway
 *          Ha Noi, Viet Nam
 *          
 * Refer to your License Agreement for restrictions on use,
 * duplication, or disclosure
 * 
 * RESTRICTED RIGHTS LEGEND
 * 
 * Use, duplication or disclosure is the
 * subject to restriction in Articles 736 and 738 of the 
 * 2005 Civil Code, the Intellectual Property Law and Decree 
 * No. 85/2011/ND-CP amending and supplementing a number of 
 * articles of Decree 100/ND-CP/2006 of the Government of Viet Nam
 * 
 * 
 * Copy right 2023 © PRN231 - SU23 - Group 10 ®. All Rights Reserved
 * 
 * Unpublished - All rights reserved under the copyright laws 
 * of the Government of Viet Nam
*********************************************************************/

using log4net;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Common.Logger
{
    public class Log4NetCore : ILogger
    {
        private static ILog logger = LogManager.GetLogger(typeof(Log4NetCore));

        public IDisposable BeginScope<TState>(TState state)
        {
            try
            {
                if (state is Array)
                {
                    var fileds = state as Array;
                    foreach (var filed in fileds)
                    {
                        if (filed is KeyValuePair<string, string>)
                        {
                            var prop = (KeyValuePair<string, string>)filed;
                            LogicalThreadContext.Properties[prop.Key] = prop.Value;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error(string.Format("BeginScope error {0}", e.Message));
            }
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            string message = null;
            if (null != formatter)
            {
                message = formatter(state, exception);
            }
            if (!string.IsNullOrEmpty(message) || exception != null)
            {
                switch (logLevel)
                {
                    case LogLevel.Critical:
                        logger.Error(message);
                        break;
                    case LogLevel.Debug:
                    case LogLevel.Trace:
                        logger.Debug(message);
                        break;
                    case LogLevel.Error:
                        logger.Error(message);
                        break;
                    case LogLevel.Information:
                        logger.Info(message);
                        break;
                    case LogLevel.Warning:
                        logger.Warn(message);
                        break;
                    default:
                        logger.Warn($"Encountered unknown log level {logLevel}, writing out as Info.");
                        logger.Info(message);
                        break;
                }
            }
        }
    }
}
