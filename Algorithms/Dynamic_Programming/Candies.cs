/*
Alice is a kindergarden teacher. She wants to give some candies to the children in her class.  All the children sit in a line and each  of them  has a rating score according to his or her usual performance.  Alice wants to give at least 1 candy for each child.Children get jealous of their immediate neighbors, so if two children sit next to each other then the one with the higher rating must get more candies. Alice wants to save money, so she wants to minimize the total number of candies.

Input

The first line of the input is an integer N, the number of children in Alice’s class. Each of the following N lines contains an integer indicates the rating of each child.

Ouput

Output a single line containing the minimum number of candies Alice must give.

Sample Input

3
1
2
2

Sample Ouput

4

Explanation

The number of candies Alice must give to a child are 1 or 2 (candies).

Constraints:

N and the rating  of each child are no larger than 10^5.
*/

using System;
using System.Collections.Generic;
using System.IO;
class Solution {
    static void Main(String[] args) {
               int N = Convert.ToInt32(Console.ReadLine());
        int[] c = new int[N];
        int[] r = new int[N];
        for (int i = 0; i < N; i++)
        {
            r[i] = Convert.ToInt32(Console.ReadLine());
            c[i] = 1;
        }

        bool f = true;
        while (f)
        {
            f = false;
            for (int i = 0; i < N; i++)
            {                
                if (i == 0 && r[0] > r[1] && c[0] <= c[1])
                {
                    c[0] = c[1] + 1;
                    f = true;
                }
                else if (i == N - 1 && r[N - 1] > r[N - 2] && c[N - 1] <= c[N - 2])
                {
                    c[N - 1] = c[N - 2] + 1;
                    f = true;
                }
                else
                {
                    if (i > 0 && r[i] > r[i - 1] && c[i] <= c[i - 1]) { c[i] = c[i - 1] + 1; f = true; }
                    else if ((i + 1) < N && r[i] > r[i + 1] && c[i] <= c[i + 1]) { c[i] = c[i + 1] + 1; f = true; }
                }
            }
        }

        long t = 0;
        for (int i = 0; i < N; i++)  t += c[i];

        Console.WriteLine(t);
    }
}