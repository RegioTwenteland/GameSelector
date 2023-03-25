using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSelector.Views
{
    internal interface IView
    { 
        void SendData(string key, object value);

        object GetData(string key);

        void SubscribeEvent(string eventName, Action<object> callback);
    }
}
