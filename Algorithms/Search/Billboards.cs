/*
ADZEN is a very popular advertising firm in your city. In every road you can see their advertising billboards. Recently they are facing a serious challenge , MG Road the most used and beautiful road in your city has been almost filled by the billboards and this is having a negative effect on the natural view.

On people’s demand ADZEN has decided to remove some of the billboards in such a way that there are no more than K billboards standing together in any part of the road.

 

You may assume the MG Road to be a straight line with N billboards.Initially there is no gap between any two adjecent billboards.

 None of the billboards can be reordered, they can just be removed.

ADZEN’s primary income comes from these billboards so the billboard removing process has to be done in such a way that the billboards remaining at end should give maximum possible profit among all possible final configurations.Total profit of a configuration is the sum of the profit values of all billboards present in that configuration.

 

Given N,K and the profit value of each of the N billboards, output the maximum profit that can be obtained from the remaining billboards under the conditions given.

 

Input description 

First line contain two space seperated integers N and K. Then follow N lines describing the profit value of each billboard i.e ith line contains the profit value of ith billboard.

Sample Input

6 2   
1  
2  
3  
1  
6  
10 
Sample Output

21
Explanation

In given input there are 6 billboards and after the process no more than 2 should be together.
So remove 1st and 4th billboards giving a configuration _ 2 3 _ 6 10 having a profit of 21. No other configuration has a profit more than 21.So the answer is 21.

Constraints
1 <= N <= 100,000(10^5)
1 <= K <= N
0 <= profit value of any billboard <= 2,000,000,000(2*10^9)
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.IO;
class Solution {
        public class Deque : ICollection, IEnumerable, ICloneable
        {
            #region Deque Members

            #region Fields

            // The node at the front of the deque.
            private Node front = null;

            // The node at the back of the deque.
            private Node back = null;

            // The number of elements in the deque.
            private int count = 0;

            // The version of the deque.
            private long version = 0;

            #endregion

            #region Construction

            /// <summary>
            /// Initializes a new instance of the Deque class.
            /// </summary>
            public Deque()
            {
            }

            public Deque(ICollection col)
            {
                #region Require

                if (col == null)
                {
                    throw new ArgumentNullException("col");
                }

                #endregion

                foreach (object obj in col)
                {
                    PushBack(obj);
                }
            }

            #endregion

            #region Methods

            public virtual void Clear()
            {
                count = 0;

                front = back = null;

                version++;
            }
              
            public virtual bool Contains(object obj)
            {
                foreach (object o in this)
                {
                    if (o == null && obj == null)
                    {
                        return true;
                    }
                    else if (o.Equals(obj))
                    {
                        return true;
                    }
                }

                return false;
            }

            public virtual void PushFront(object obj)
            {
                // The new node to add to the front of the deque.
                Node newNode = new Node(obj);

                // Link the new node to the front node. The current front node at 
                // the front of the deque is now the second node in the deque.
                newNode.Next = front;

                // If the deque isn't empty.
                if (Count > 0)
                {
                    // Link the current front to the new node.
                    front.Previous = newNode;
                }

                // Make the new node the front of the deque.
                front = newNode;

                // Keep track of the number of elements in the deque.
                count++;

                // If this is the first element in the deque.
                if (Count == 1)
                {
                    // The front and back nodes are the same.
                    back = front;
                }

                version++;
            }

            public virtual void PushBack(object obj)
            {
                // The new node to add to the back of the deque.
                Node newNode = new Node(obj);

                // Link the new node to the back node. The current back node at 
                // the back of the deque is now the second to the last node in the
                // deque.
                newNode.Previous = back;

                // If the deque is not empty.
                if (Count > 0)
                {
                    // Link the current back node to the new node.
                    back.Next = newNode;
                }

                // Make the new node the back of the deque.
                back = newNode;

                // Keep track of the number of elements in the deque.
                count++;

                // If this is the first element in the deque.
                if (Count == 1)
                {
                    // The front and back nodes are the same.
                    front = back;
                }

                version++;
            }

            public virtual object PopFront()
            {
                #region Require

                if (Count == 0)
                {
                    throw new InvalidOperationException("Deque is empty.");
                }

                #endregion

                // Get the object at the front of the deque.
                object obj = front.Value;

                // Move the front back one node.
                front = front.Next;

                // Keep track of the number of nodes in the deque.
                count--;

                // If the deque is not empty.
                if (Count > 0)
                {
                    // Tie off the previous link in the front node.
                    front.Previous = null;
                }
                // Else the deque is empty.
                else
                {
                    // Indicate that there is no back node.
                    back = null;
                }

                version++;

                return obj;
            }
            
            public virtual object PopBack()
            {
                #region Require

                if (Count == 0)
                {
                    throw new InvalidOperationException("Deque is empty.");
                }

                #endregion

                // Get the object at the back of the deque.
                object obj = back.Value;

                // Move back node forward one node.
                back = back.Previous;

                // Keep track of the number of nodes in the deque.
                count--;

                // If the deque is not empty.
                if (Count > 0)
                {
                    // Tie off the next link in the back node.
                    back.Next = null;
                }
                // Else the deque is empty.
                else
                {
                    // Indicate that there is no front node.
                    front = null;
                }

                version++;

                return obj;
            }

            public virtual object PeekFront()
            {
                #region Require

                if (Count == 0)
                {
                    throw new InvalidOperationException("Deque is empty.");
                }

                #endregion

                return front.Value;
            }

            public virtual object PeekBack()
            {
                #region Require

                if (Count == 0)
                {
                    throw new InvalidOperationException("Deque is empty.");
                }

                #endregion

                return back.Value;
            }

            public virtual object[] ToArray()
            {
                object[] array = new object[Count];
                int index = 0;

                foreach (object obj in this)
                {
                    array[index] = obj;
                    index++;
                }

                return array;
            }

            public static Deque Synchronized(Deque deque)
            {
                #region Require

                if (deque == null)
                {
                    throw new ArgumentNullException("deque");
                }

                #endregion

                return new SynchronizedDeque(deque);
            }
            #endregion

            #region Node Class

            // Represents a node in the deque.
            [Serializable()]
            private class Node
            {
                private object value;

                private Node previous = null;

                private Node next = null;

                public Node(object value)
                {
                    this.value = value;
                }

                public object Value
                {
                    get
                    {
                        return value;
                    }
                }

                public Node Previous
                {
                    get
                    {
                        return previous;
                    }
                    set
                    {
                        previous = value;
                    }
                }

                public Node Next
                {
                    get
                    {
                        return next;
                    }
                    set
                    {
                        next = value;
                    }
                }
            }

            #endregion

            #region DequeEnumerator Class

            [Serializable()]
            private class DequeEnumerator : IEnumerator
            {
                private Deque owner;

                private Node currentNode;

                private object current = null;

                private bool moveResult = false;

                private long version;

                public DequeEnumerator(Deque owner)
                {
                    this.owner = owner;
                    currentNode = owner.front;
                    this.version = owner.version;
                }

                #region IEnumerator Members

                public void Reset()
                {
                    #region Require

                    if (version != owner.version)
                    {
                        throw new InvalidOperationException(
                            "The Deque was modified after the enumerator was created.");
                    }

                    #endregion

                    currentNode = owner.front;
                    moveResult = false;
                }

                public object Current
                {
                    get
                    {
                        #region Require

                        if (!moveResult)
                        {
                            throw new InvalidOperationException(
                                "The enumerator is positioned before the first " +
                                "element of the Deque or after the last element.");
                        }

                        #endregion

                        return current;
                    }
                }

                public bool MoveNext()
                {
                    #region Require

                    if (version != owner.version)
                    {
                        throw new InvalidOperationException(
                            "The Deque was modified after the enumerator was created.");
                    }

                    #endregion

                    if (currentNode != null)
                    {
                        current = currentNode.Value;
                        currentNode = currentNode.Next;

                        moveResult = true;
                    }
                    else
                    {
                        moveResult = false;
                    }

                    return moveResult;
                }

                #endregion
            }

            #endregion

            #region SynchronizedDeque Class

            // Implements a synchronization wrapper around a deque.
            [Serializable()]
            private class SynchronizedDeque : Deque
            {
                #region SynchronziedDeque Members

                #region Fields

                // The wrapped deque.
                private Deque deque;

                // The object to lock on.
                private object root;

                #endregion

                #region Construction

                public SynchronizedDeque(Deque deque)
                {
                    #region Require

                    if (deque == null)
                    {
                        throw new ArgumentNullException("deque");
                    }

                    #endregion

                    this.deque = deque;
                    this.root = deque.SyncRoot;
                }

                #endregion

                #region Methods

                public override void Clear()
                {
                    lock (root)
                    {
                        deque.Clear();
                    }
                }

                public override bool Contains(object obj)
                {
                    bool result;

                    lock (root)
                    {
                        result = deque.Contains(obj);
                    }

                    return result;
                }

                public override void PushFront(object obj)
                {
                    lock (root)
                    {
                        deque.PushFront(obj);
                    }
                }

                public override void PushBack(object obj)
                {
                    lock (root)
                    {
                        deque.PushBack(obj);
                    }
                }

                public override object PopFront()
                {
                    object obj;

                    lock (root)
                    {
                        obj = deque.PopFront();
                    }

                    return obj;
                }

                public override object PopBack()
                {
                    object obj;

                    lock (root)
                    {
                        obj = deque.PopBack();
                    }

                    return obj;
                }

                public override object PeekFront()
                {
                    object obj;

                    lock (root)
                    {
                        obj = deque.PeekFront();
                    }

                    return obj;
                }

                public override object PeekBack()
                {
                    object obj;

                    lock (root)
                    {
                        obj = deque.PeekBack();
                    }

                    return obj;
                }

                public override object[] ToArray()
                {
                    object[] array;

                    lock (root)
                    {
                        array = deque.ToArray();
                    }

                    return array;
                }

                public override object Clone()
                {
                    object clone;

                    lock (root)
                    {
                        clone = deque.Clone();
                    }

                    return clone;
                }

                public override void CopyTo(Array array, int index)
                {
                    lock (root)
                    {
                        deque.CopyTo(array, index);
                    }
                }

                public override IEnumerator GetEnumerator()
                {
                    IEnumerator e;

                    lock (root)
                    {
                        e = deque.GetEnumerator();
                    }

                    return e;
                }

                #endregion

                #region Properties

                public override int Count
                {
                    get
                    {
                        lock (root)
                        {
                            return deque.Count;
                        }
                    }
                }

                public override bool IsSynchronized
                {
                    get
                    {
                        return true;
                    }
                }

                #endregion

                #endregion
            }

            #endregion

            #endregion

            #region ICollection Members
            public virtual bool IsSynchronized
            {
                get
                {
                    return false;
                }
            }

            public virtual int Count
            {
                get
                {
                    return count;
                }
            }

            public virtual void CopyTo(Array array, int index)
            {
                #region Require

                if (array == null)
                {
                    throw new ArgumentNullException("array");
                }
                else if (index < 0)
                {
                    throw new ArgumentOutOfRangeException("index", index,
                        "Index is less than zero.");
                }
                else if (array.Rank > 1)
                {
                    throw new ArgumentException("Array is multidimensional.");
                }
                else if (index >= array.Length)
                {
                    throw new ArgumentException("Index is equal to or greater " +
                        "than the length of array.");
                }
                else if (Count > array.Length - index)
                {
                    throw new ArgumentException(
                        "The number of elements in the source Deque is greater " +
                        "than the available space from index to the end of the " +
                        "destination array.");
                }

                #endregion

                int i = index;

                foreach (object obj in this)
                {
                    array.SetValue(obj, i);
                    i++;
                }
            }

            public virtual object SyncRoot
            {
                get
                {
                    return this;
                }
            }

            #endregion

            #region IEnumerable Members
            public virtual IEnumerator GetEnumerator()
            {
                return new DequeEnumerator(this);
            }
            #endregion

            #region ICloneable Members
            public virtual object Clone()
            {
                Deque clone = new Deque(this);

                clone.version = this.version;

                return clone;
            }

            #endregion
        }

    static void Main(String[] args) {
			string[] split = Console.ReadLine().Split(new Char[] { ' ', '\t', '\n' });
            int n = Convert.ToInt32(split[0]), k = Convert.ToInt32(split[1]);
            int[] c = new int[n+1];
            long sum = 0;
            for (int i = 0; i < n; i++)
            {
                c[i] = Convert.ToInt32(Console.ReadLine());
                sum += c[i];
            }
            
            if (k >= n)
                Console.Write(sum);
            else
            {
                c[n] = 0;
                Deque q = new Deque();
                long[] d = new long[n + 1];
                for (int i = 0; i < k + 1; i++)
                {
                    d[i] = c[i];
                    while (q.Count > 0 && d[i] <= d[Convert.ToInt32(q.PeekBack())])
                        q.PopBack();
                    q.PushBack(i);
                }
                
                for (int i = k + 1; i < n; i++)
                {
                    d[i] = d[Convert.ToInt32(q.PeekFront())] + c[i];
                    while (q.Count > 0 && d[i] <= d[Convert.ToInt32(q.PeekBack())])
                        q.PopBack();
                    while (q.Count > 0 && Convert.ToInt32(q.PeekFront()) <= i - k - 1)
                        q.PopFront();
                    q.PushBack(i);
                }
                d[n] = d[Convert.ToInt32(q.PeekFront())];
                Console.Write(sum - d[n]);
            }
    }
}