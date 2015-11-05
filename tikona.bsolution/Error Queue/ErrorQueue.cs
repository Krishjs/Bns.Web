using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bns.Framework.Common.Errors
{ 
    public class ErrorQueue
    {
        private static readonly Lazy<ErrorQueue> errorQueueResolver = 
            new Lazy<ErrorQueue>(GetErrorQueueInstance);

        private static ErrorQueue errorQueue;

        private static string errors = "";

        private static ErrorQueue Current
        {
            get
            {
                if (errorQueue == null)
                {
                    errorQueue = errorQueueResolver.Value;
                }

                return errorQueue;
            }
        }

        public static ErrorQueue Enqueue(string txt)
        {
            errors += "\n"  + txt;
            return Current;
        }

        public static string GetErrors()
        {
            return errors;
        }

        private static ErrorQueue GetErrorQueueInstance()
        {
            return new ErrorQueue();
        }
    }
}