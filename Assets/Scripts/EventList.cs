using System.Collections.Generic;
using UnityEngine;

public class EventList
{
    private List<IEvent> _list = new List<IEvent>();

    public void Add(IEvent eventToAdd)
    {
        _list.Add(eventToAdd);
    }
    
    /**
     * @todo threaded
     */
    public void Execute()
    {
        foreach (IEvent currentEvent in _list)
        {
            currentEvent.Trigger();
        }
    }
}
