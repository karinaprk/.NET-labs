using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

namespace CustomLinkedList;

public class CustomLinkedList<T> : IEnumerable<T>
{
    public Node<T> Head { get; set; }
    public Node<T> Tail { get; set; }
    
    public int Count { get; private set; }

    public CustomLinkedList() { }

    public CustomLinkedList(T data)
    {
        _ = data ?? throw new ArgumentNullException(nameof(data));
        
        var element = new Node<T> { Data = data };
        Head = element;
        Tail = element;
        Count = 1;
    }
    
    public CustomLinkedList(params T[] collection)
    {
        _ = collection ?? throw new ArgumentNullException(nameof(collection));
        
        foreach (var element in collection)
        {
            Add(element);
        }
    }

    public CustomLinkedList(IEnumerable<T> collection)
    {
        _ = collection ?? throw new ArgumentNullException(nameof(collection));
        
        foreach (var element in collection)
        {
            Add(element);
        }
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index > Count - 1)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            
            var result = default(T);
            foreach (var current in this)
            {
                if (index == 0)
                {
                    result = current;
                    break;
                }

                index--;
            }

            return result;
        }
        set
        {
            if (index < 0 || index > Count - 1)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            
            var current = Head;
            while (current is not null)
            {
                if (index == 0)
                {
                    current.Data = value;
                }

                current = current.Next;
                index--;
            }
        }
    }

    public void Add(T data)
    {
        _ = data ?? throw new ArgumentNullException(nameof(data));
        
        var element = new Node<T> { Data = data };

        if (Count == 0)
        {
            Head = element;
            Tail = element;
            Count = 1;
            return;
        }
        
        Tail.Next = element;
        element.Previous = Tail;
        Tail = element;
        Count++;
    }

    public void Remove(T data)
    {
        _ = data ?? throw new ArgumentNullException(nameof(data));
        
        var current = Head;

        while (current is not null)
        {
            if (current.Data.Equals(data))
            {
                break;
            }

            current = current.Next;
        }

        if (current is null)
        {
            return;
        }
        
        if (current.Next != null)
        {
            current.Next.Previous = current.Previous;
        }
        else
        {
            Tail = current.Previous;
        }

        if (current.Previous is not null)
        {
            current.Previous.Next = current.Next;
        }
        else
        {
            Head = current.Next;
        }

        Count--;
    }

    public bool Contains(T data)
    {
        var current = Head;

        while(current is not null)
        {
            if(current.Data.Equals(data))
            {
                return true;
            }

            current = current.Next;
        }

        return false;
    }

    public int IndexOf(T data)
    {
        int index = 0;
        foreach (var current in this)
        {
            if (current.Equals(data))
            {
                return index;
            }

            index++;
        }

        return -1;
    }
    
    public void Clear()
    {
        Head = null;
        Tail = null;
        Count = 0;
    }
    
    public IEnumerator<T> GetEnumerator()
    {
        var current = Head;
        while (current != null)
        {
            yield return current.Data;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}