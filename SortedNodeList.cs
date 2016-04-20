using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarPathing
{
    class SortedNodeList<T> : LinkedList<T> where T : IComparable
    {
    //SortedNodeList sorts nodes by their FCost from least to most
    //Push puts the value into the list according to it's FCost
    //Pop will return the node with the least FCost
    public void Push(T pNode)
    {
        //check if it is most efficient
        if(Count == 0  || pNode.CompareTo(First.Value) < 0)
        {
            AddFirst(pNode);
            return;
        }

        //check if it is least efficient
        if(pNode.CompareTo(Last.Value) > 0)
        {
            AddLast(pNode);
            return;
        }

        //cycle through nodes until we find where it fits
        LinkedListNode<T> linkedNode = First;
        while(pNode.CompareTo(linkedNode.Value) > 0)
        {
            linkedNode = linkedNode.Next;
        }

        AddBefore(linkedNode, pNode);
    }

    public T Pop() 
    {
        //First node has the lowest FCost
        T node = First.Value;
        Remove(node);
        return node;
    }

    }
}
