using System.Collections.Generic;
using UnityEngine;

public class EventList
{
    private List<IEvent> _list = new List<IEvent>();

    public void add(IEvent eventToAdd)
    {
        _list.Add(eventToAdd);
    }
    
    /**
     * @todo threaded
     */
    public void execute()
    {
        foreach (IEvent currentEvent in _list)
        {
            currentEvent.trigger();
        }
    }
}
