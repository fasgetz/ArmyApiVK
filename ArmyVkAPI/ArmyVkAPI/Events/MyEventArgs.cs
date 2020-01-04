using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;

namespace ArmyVkAPI.Events
{
    class MyEventArgs : EventArgs
    {
        VkApi token;

        public MyEventArgs(VkApi token)
        {
            this.token = token;
        }

        public VkApi GetToken()
        {
            return token;
        }
    }
}
