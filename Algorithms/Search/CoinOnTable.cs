/*
You have a rectangular board consisting of n rows, numbered from 1 to n and m columns, numbered from 1 to m. The top left is (1,1) and bottom right is (n,m). Initially - at time 0 - there is a coin on the top-left cell of your board. Each cell of your board contains one of these letters:
    *, exactly one of your cells has letter ‘*’.
    U, If at time t, the coin is on the cell(i,j) and cell(i,j) has letter ‘U’, the coin will be on the cell(i-1,j) in time t+1 if i > 1. otherwise there is no coin on your board at time t+1.
    L, If at time t, the coin is on the cell(i,j) and cell(i,j) has letter ‘L’, the coin will be on the cell(i,j-1) in time t+1 if j > 1. otherwise there is no coin on your board at time t+1.
    D, If at time t, the coin is on the cell(i,j) and cell(i,j) has letter ‘D’, the coin will be on the cell(i+1,j) in time t+1 if i < n. otherwise there is no coin on your board at time t+1.
    R, If at time t, the coin is on the cell(i,j) and cell(i,j) has letter ‘R’, the coin will be on the cell(i,j+1) in time t+1 if j < m. otherwise there is no coin on your board at time t+1.

When the coin reaches the cell that has letter ‘*’ it will be there permanently. When you punch on your board, your timer starts and the coin moves between cells. before doing that you want to do some operation so that you could be sure that at time k the coin will be on the cell having letter ‘*’. In each operation you can select a cell with some letter other than ‘*’ and change the letter to ‘U’, ‘L’, ‘R’ or ‘D’. You want to do as few operations as possible in order to achieve your goal. Your task is to find the minimum number of operations.

Input:
The first line of input contains three integers n and m and k.
Next n lines contains m letters which describe you board.
n and m are integers less than 51.
k is less than 1001.

Output:
on the only line of the output print an integer being the answer to the test.
If you cannot achieve your goal, output -1 please.

Sample input :

2 2 3
RD
*L

Sample output :

0

Sample input :

2 2 1
RD
*L

Sample output :

1

Explanation :

In the first example you don’t have to change any letters, but in the second example you should change the letter of cell (1,1) to ‘D’.
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Collections;
class Solution
{
    public class PriorityQueue<T> where T : IComparable<T>
    {
        private List<T> data;
        public PriorityQueue()
        {
            this.data = new List<T>();
        }

        public void Enqueue(T item)
        {
            data.Add(item);
            int ci = data.Count - 1; 
            while (ci > 0)
            {
                int pi = (ci - 1) / 2; 
                if (data[ci].CompareTo(data[pi]) >= 0) break;
                T tmp = data[ci]; data[ci] = data[pi]; data[pi] = tmp;
                ci = pi;
            }
        }

        public T Dequeue()
        {
            int li = data.Count - 1;
            T frontItem = data[0];
            data[0] = data[li];
            data.RemoveAt(li);

            --li;
            int pi = 0;
            while (true)
            {
                int ci = pi * 2 + 1; 
                if (ci > li) break;
                int rc = ci + 1;
                if (rc <= li && data[rc].CompareTo(data[ci]) < 0) 
                    ci = rc;
                if (data[pi].CompareTo(data[ci]) <= 0) break; 
                T tmp = data[pi]; data[pi] = data[ci]; data[ci] = tmp;
                pi = ci;
            }
            return frontItem;
        }

        public T Peek()
        {
            T frontItem = data[0];
            return frontItem;
        }

        public int Count()
        {
            return data.Count;
        }
    }

    public class Point : IComparable<Point>
    {
        public int x { get; set; }
        public int y { get; set; }
        public int k { get; set; }
        public int c { get; set; }
        public Point(int r, int ca, int t, int m)
        {
            x = r;
            y = ca;
            c = m;
            k = t;
        }
        public int CompareTo(Point n)
        {
            return this.c - n.c;
        }
    }

    public static int n, m;
    public static Point end = null;
    public static char[,] board;
    public static int[,] cost;
    public static char[] pm = new char[] { 'D', 'L', 'U', 'R' };
    public static int[] dx = { 1, 0, -1, 0 };
    public static int[] dy = { 0, -1, 0, 1 };

    public static int Solve(int time)
    {
        PriorityQueue<Point> q = new PriorityQueue<Point>();
        q.Enqueue(new Point(0, 0, time, 0));
        while (q.Count() > 0)
        {
            Point n = q.Dequeue();
            int x = n.x;
            int y = n.y;
            int k = n.k;
            int c = n.c;

            cost[x, y] = Math.Min(cost[x, y], c);
            if (board[x, y] == '*' && k >= 0)
                return cost[x, y];
            else if (k < 0)
                continue;

            for (int i = 0; i < 4; i++)
            {
                int xx = x + dx[i];
                int yy = y + dy[i];
                if (inBoard(xx, yy))
                {
                    int currC = board[x, y] == pm[i] ? 0 : 1;
                    if (currC + c < cost[xx, yy])
                        q.Enqueue(new Point(xx, yy, k - 1, currC + c));
                }
            }
        }
        return -1;
    }

    private static bool inBoard(int x, int y)
    {
        return (x >= 0 && x < n) && (y >= 0 && y < m);
    }

    public static void Main(string[] args)
    {
        string[] s = Console.ReadLine().Split(new Char[] { ' ', '\t', '\n' });
        n = Convert.ToInt32(s[0]);
        m = Convert.ToInt32(s[1]);
        int tk = Convert.ToInt32(s[2]);
        board = new char[n, m];
        cost = new int[n, m];
        string l;

        for (int r = 0; r < n; r++)
        {
            l = Console.ReadLine();
            for (int c = 0; c < m; c++)
            {
                board[r, c] = l[c];
                cost[r, c] = int.MaxValue;
            }
        }

        int ans = Solve(tk);
        Console.WriteLine((ans == int.MaxValue ? -1 : ans));

        //Console.WriteLine();
    }
}
