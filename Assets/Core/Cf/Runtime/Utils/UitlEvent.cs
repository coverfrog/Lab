using UnityEngine;
using UnityEngine.Events;

namespace Cf
{
    public partial class Util 
    {
        public static class Event
        {
            public static int GetEventsCount(UnityEvent unityEvent, bool validOnly = true)
            {
                var eventCount = unityEvent.GetPersistentEventCount();
                
                if (validOnly)
                {
                    var validCount = 0;

                    for (var i = 0; i < eventCount; i++)
                    {
                        var target = unityEvent.GetPersistentTarget(i);
                        var methodName = unityEvent.GetPersistentMethodName(i);

                        if (target != null && !string.IsNullOrEmpty(methodName))
                        {
                            validCount++;
                        }
                    }

                    return validCount;
                }

                else
                {
                    return eventCount;
                }
            }
        }
    }
}
