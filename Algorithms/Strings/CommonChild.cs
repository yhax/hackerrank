/*
Given two strings a and b of equal length, what’s the longest string (S) that can be constructed such that S is a child to both a and b.

String x is said to be a child of string y if x can be formed by deleting 0 or more characters from y

Input format

Two strings a and b with a newline separating them

Constraints

All characters are upper-cased and lie between ascii values 65-90 The maximum length of the strings is 5000

Output format

Length of the string S

Sample Input #0

HARRY
SALLY
Sample Output #0

2
The longest possible subset of characters that is possible by deleting zero or more characters from HARRY and SALLY is AY, whose length is 2.

Sample Input #1

AA
BB
Sample Output #1

0
AA and BB has no characters in common and hence the output 0

Sample Input #2

SHINCHAN
NOHARAAA
Sample Output #2

3
The largest set of characters, in order, between SHINCHAN and NOHARAAA is NHA.

Sample Input #3

ABCDEF
FBDAMN
Sample Output #3

2
BD will be optimal substring.
*/

using System;
using System.Collections.Generic;
using System.IO;
class Solution {
    static void Main(String[] args) {
        string a = Console.ReadLine();
        string b = Console.ReadLine();
        int[,] l = new int[a.Length+1, b.Length+1];
        int M = a.Length, N = b.Length;

        for (int i = 1; i <= M; i++) l[i,0] = 0;
        for (int j = 0; j <= N; j++) l[0,j] = 0;

        //printArr(l);

        for (int i = 1; i <= M; i++)
        {
            for (int j = 1; j <= N; j++)
            {
                if (a[i - 1] == b[j - 1]) l[i, j] = l[i - 1, j - 1] + 1;
                else if (l[i - 1, j] >= l[i, j - 1]) l[i, j] = l[i - 1, j];
                else l[i, j] = l[i, j - 1];
               // printArr(l);
            }
            //Console.WriteLine("---------------\n");
        }

        Console.WriteLine(l[M, N]);     
    }
}