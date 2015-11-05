using Bns.Framework.Common.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bns.Framework.Common.Messaging
{
    public class RelayRider
    {        
        private static IRider raider = new EmailRider();

        public static void Set(IRider r)
        {
            raider = r;
        }

        public static void Send(RecoveryDetails details)
        {
            try
            {
                raider.Send(details);
            }
            catch (Exception ex)
            {

                ErrorQueue.Enqueue(ex.Message);
            }            
        }
    }
}
