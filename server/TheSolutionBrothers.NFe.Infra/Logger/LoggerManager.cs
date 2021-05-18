using System;
using log4net;

namespace TheSolutionBrothers.Nfe.Infra.Logger
{
    public static class TraceLogManager
    {

        private static bool _hasBeenConfigurated = false;

        public static bool HasBeenConfigurated
        {
            get { return _hasBeenConfigurated; }
        }

        public static ILog CurrentClassLogger
        {
            get
            {
                if (!HasBeenConfigurated)
                    Configure();
                return LogManager.GetLogger
                    (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            }
        }

        public static void Configure()
        {
            log4net.Config.XmlConfigurator.Configure();
            _hasBeenConfigurated = true;
        }


        public static void Debug(Func<string> messageFunc)
        {
            CurrentClassLogger.Debug(messageFunc);
        }

        public static void Debug(string message)
        {
            CurrentClassLogger.Debug(message);
        }

        public static void Fatal(string message, Exception exception)
        {
            CurrentClassLogger.Debug(message, exception);
        }

        public static void Debug(string message, params object[] args)
        {
            CurrentClassLogger.DebugFormat(message, args);
        }

        #region Error

        public static void Error(Func<string> messageFunc)
        {
            CurrentClassLogger.Error(messageFunc);
        }

        public static void Error(string message)
        {
            CurrentClassLogger.Error(message);
        }

        public static void ErrorException(string message, Exception exception)
        {
            CurrentClassLogger.Error(message, exception);
        }

        public static void Error(string message, params object[] args)
        {
            CurrentClassLogger.ErrorFormat(message, args);
        }

        #endregion Error

        #region Fatal

        public static void Fatal(Func<string> messageFunc)
        {
            CurrentClassLogger.Fatal(messageFunc);
        }

        public static void Fatal(string message)
        {
            CurrentClassLogger.Fatal(message);
        }

        public static void FatalException(string message, Exception exception)
        {
            CurrentClassLogger.Fatal(message, exception);
        }

        public static void Fatal(string message, params object[] args)
        {
            CurrentClassLogger.FatalFormat(message, args);
        }

        #endregion Fatal

        #region Info

        public static void Info(string message)
        {
            CurrentClassLogger.Info(message);
        }

        public static void Info(Func<string> messageFunc)
        {
            CurrentClassLogger.Info(messageFunc);
        }

        public static void InfoException(string message, Exception exception)
        {
            CurrentClassLogger.Info(message, exception);
        }

        public static void Info(string message, params object[] args)
        {
            CurrentClassLogger.InfoFormat(message, args);
        }
        #endregion Info

        #region Trace

        public static void Trace(Func<string> messageFunc)
        {
            CurrentClassLogger.Info(messageFunc);
        }

        public static void Trace(string message)
        {
            CurrentClassLogger.Info(message);
        }

        public static void TraceException(string message, Exception exception)
        {
            CurrentClassLogger.Info(message, exception);
        }

        public static void Trace(string message, params object[] args)
        {
            CurrentClassLogger.Info(string.Format(message, args));
        }

        #endregion Trace

        #region Warn

        public static void Warn(Func<string> messageFunc)
        {
            CurrentClassLogger.Warn(messageFunc);
        }

        public static void Warn(string message)
        {
            CurrentClassLogger.Warn(message);
        }

        public static void WarnException(string message, Exception exception)
        {
            CurrentClassLogger.Warn(message, exception);
        }

        public static void Warn(string message, params object[] args)
        {
            CurrentClassLogger.WarnFormat(message, args);
        }

        #endregion Warn
    }
}
