using System;

namespace Auction.Events
{
    public class ActionEventArgs<T>: EventArgs
    {
        public T EventInfo { get; private set; }

        public ActionEventArgs(T eventInfo)
        {
            EventInfo = eventInfo;
        }
    }
}
