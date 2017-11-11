using System.Collections.Generic;
using UnityEngine;

public class EventQueue
{
    private Queue<IEvent> _queue = new Queue<IEvent>();

    public void add(IEvent eventToAdd)
    {
        _queue.Enqueue(eventToAdd);
    }
    
    /**
     * @todo threaded
     */
    public void execute()
    {
        while (0 != _queue.Count)
        {
            _queue.Dequeue().trigger();
        }
    }
}