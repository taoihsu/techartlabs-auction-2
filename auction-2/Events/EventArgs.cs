using System;

namespace auction_2.Events
{
    public class EventArgs<T>: EventArgs
    {
        public T EventInfo { get; private set; }

        public EventArgs(T eventInfo)
        {
            EventInfo = eventInfo;
        }
    }
}
