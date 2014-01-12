/*
The median of M numbers is defined as the middle number after sorting them in order if M is odd or the average number of the middle 2 numbers (again after sorting) if M is even. You have an empty number list at first. Then you can add or remove some number from the list. For each add or remove operation, output the median of numbers in the list.

Example : 
For a set of m = 5 numbers, { 9, 2, 8, 4, 1 } the median is the third number in sorted set { 1, 2, 4, 8, 9 } which is 4. Similarly for set of m = 4, { 5, 2, 10, 4 }, the median is the average of second and the third element in the sorted set { 2, 4, 5, 10 } which is (4+5)/2 = 4.5  

Input: 
The first line is an integer n indicates the number of operations. Each of the next n lines is either “a x” or “r x” which indicates the operation is add or remove.

Output: 
For each operation: If the operation is add output the median after adding x in a single line. If the operation is remove and the number x is not in the list, output “Wrong!” in a single line. If the operation is remove and the number x is in the list, output the median after deleting x in a single line. (if the result is an integer DO NOT output decimal point. And if the result is a real number , DO NOT output trailing 0s.)

Note
When calculating median in even case if your output is 3.0 print only 3 and if it is 3.50 print only 3.5

Constraints: 
0 < n <= 100,000 
For each “a x” or “r x”, x will always be an integer which will fit in 32 bit signed integer.

Sample Input:

7  
r 1  
a 1  
a 2  
a 1  
r 1  
r 2  
r 1  
Sample Output:

Wrong!  
1  
1.5  
1  
1.5  
1  
Wrong!
Note: As evident from the last line of the input, if after remove operation the list becomes empty you have to print “Wrong!” ( quotes are for clarity ).
*/

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
class Solution {

    public class PriorityQueue<T> : ICollection<int>
        {
            private List<int> _baseHeap;
            private IComparer<int> _comparer;
            private bool heapType = false; // false for min

            public PriorityQueue(bool hType)
                : this(Comparer<int>.Default)
            {
                heapType = hType;
            }

            public PriorityQueue(int capacity, bool hType)
                : this(capacity, Comparer<int>.Default)
            {
                heapType = hType;
            }

            public PriorityQueue(IComparer<int> comparer)
            {
                if (comparer == null)
                    throw new ArgumentNullException();

                _baseHeap = new List<int>();
                _comparer = comparer;
            }

            public PriorityQueue(int capacity, IComparer<int> comparer)
            {
                if (comparer == null)
                    throw new ArgumentNullException();

                _baseHeap = new List<int>(capacity);
                _comparer = comparer;
            }
            
            public int Peek()
            {
                if (!IsEmpty) return _baseHeap[0];
                else
                    throw new InvalidOperationException("Priority queue is empty");
            }

            public bool IsEmpty
            {
                get { return _baseHeap.Count == 0; }
            }

            private void ExchangeElements(int pos1, int pos2)
            {
                int val = _baseHeap[pos1];
                _baseHeap[pos1] = _baseHeap[pos2];
                _baseHeap[pos2] = val;
            }

            public void Add(int val)
            {
                _baseHeap.Add(val);
                HeapifyFromEndToBeginning(_baseHeap.Count - 1);
            }

            private int HeapifyFromEndToBeginning(int pos)
            {
                if (pos >= _baseHeap.Count) return -1;

                while (pos > 0)
                {
                    int parentPos = (pos - 1) / 2;
                    if ((heapType && _comparer.Compare(_baseHeap[parentPos], _baseHeap[pos]) < 0) ||
                        (!heapType && _comparer.Compare(_baseHeap[parentPos], _baseHeap[pos]) > 0))
                    {
                        ExchangeElements(parentPos, pos);
                        pos = parentPos;
                    }
                    else break;
                }
                return pos;
            }

            private void HeapifyFromBeginningToEnd(int pos)
            {
                if (pos >= _baseHeap.Count) return;

                while (true)
                {
                    int biggest = pos;
                    int left = 2 * pos + 1;
                    int right = 2 * pos + 2;
                    if ((heapType && left < _baseHeap.Count && _comparer.Compare(_baseHeap[biggest], _baseHeap[left]) < 0) ||
                        (!heapType && left < _baseHeap.Count && _comparer.Compare(_baseHeap[biggest], _baseHeap[left]) > 0))
                        biggest = left;
                    if ((heapType && right < _baseHeap.Count && _comparer.Compare(_baseHeap[biggest], _baseHeap[right]) < 0) ||
                        (!heapType && right < _baseHeap.Count && _comparer.Compare(_baseHeap[biggest], _baseHeap[right]) > 0))
                        biggest = right;

                    if (biggest != pos)
                    {
                        ExchangeElements(biggest, pos);
                        pos = biggest;
                    }
                    else break;
                }
            }

            public void Clear()
            {
                _baseHeap.Clear();
            }

            public bool Contains(int val)
            {
                return _baseHeap.Contains(val);
            }

            public int Count
            {
                get { return _baseHeap.Count; }
            }

            public void CopyTo(int[] array, int arrayIndex)
            {
                _baseHeap.CopyTo(array, arrayIndex);
            }

            public bool IsReadOnly
            {
                get { return false; }
            }

            public bool Remove(int val)
            {
                if (_baseHeap.Count == 0) return false;
                int elementIdx = _baseHeap.IndexOf(val);
                if (elementIdx < 0) return false;

                _baseHeap[elementIdx] = _baseHeap[_baseHeap.Count - 1];
                _baseHeap.RemoveAt(_baseHeap.Count - 1);

                int newPos = HeapifyFromEndToBeginning(elementIdx);
                if (newPos == elementIdx)
                    HeapifyFromBeginningToEnd(elementIdx);

                return true;
            }

            public IEnumerator<int> GetEnumerator()
            {
                return _baseHeap.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
        }
    
   static void Main(String[] args) {
            int N = Convert.ToInt32(Console.ReadLine()), i;
            BitArray o = new BitArray(N, false);
            int[] q = new int[N];
            for (i = 0; i < N; i++)
            {
                string[] split = Console.ReadLine().Split(new Char[] { ' ', '\t', '\n' });
                o[i] = split[0].Trim().Equals("a") ? true : false;
                q[i] = Convert.ToInt32(split[1].Trim());
            }
       
            PriorityQueue<int> s = new PriorityQueue<int>((N/2), true);
            PriorityQueue<int> b = new PriorityQueue<int>((N/2)+1, false);
       		bool w;
       
                for (i = 0; i < N; i++)
                {
                    w = false;
                    if (o[i])
                    {
                        if (s.Count == 0)
                            s.Add(q[i]);
                        else
                        {
                            int max = s.Peek();
                            if (q[i] > max)
                                b.Add(q[i]);
                            else
                                s.Add(q[i]);
                        }
                    }
                    else
                    {
                        if (!s.Remove(q[i]))
                        {
                            if (!b.Remove(q[i]))
                                w = true;
                        }
                    }

                    int t;
                    if (b.Count > s.Count)
                    {
                        t = b.Peek();
                        s.Add(t);
                        b.Remove(t);
                    }
                    if (s.Count > (b.Count + 1))
                    {
                        t = s.Peek();
                        b.Add(t);
                        s.Remove(t);
                    }

                    if (w || b.Count == 0 && s.Count == 0)
                        w = true;
                    else if (s.Count != b.Count)
                    {
                        if (s.Count > 0)
                            Console.WriteLine(s.Peek());
                        else if (b.Count > 0)
                            Console.WriteLine(b.Peek());
                    }
                    else if (s.Count == b.Count)
                    {
                        double m = (s.Peek()&b.Peek())+((s.Peek()^b.Peek())/2.0);
                        Console.WriteLine(m.ToString("0.#"));
                    }
                    else
                        w = true;

                    if (w) Console.WriteLine("Wrong!");
                }
    }
}