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
        public static List<IRider> Riders = new List<IRider>();
        
        public static void Send(Dictionary<string,string> details)
        {
            try
            {
                foreach (IRider rider in Riders)
                {
                    rider.Send(details);
                }
            }
            catch (Exception ex)
            {

                ErrorQueue.Enqueue(ex.Message);
            }            
        }
    }
}
