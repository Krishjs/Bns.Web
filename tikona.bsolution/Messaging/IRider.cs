using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bns.Framework.Common.Messaging
{
    public interface IRider
    {
        void Send(RecoveryDetails details);
    }
}
