/*
Given N integers, count the total pairs of integers that have a difference of K.

Input Format 
1st line contains N & K (integers).
2nd line contains N numbers of the set. All the N numbers are assured to be distinct.

Output Format
One integer saying the number of pairs of numbers that have a diff K.

Constraints:
N <= 10^5
0 < K < 10^9
Each integer will be greater than 0 and at least K away from 2^31-1
*/

using System;
using System.Collections.Generic;
using System.IO;
class Solution {
    static void Main(String[] args) {
            string[] split = Console.ReadLine().Split(new Char[] { ' ', '\t', '\n' });
            int N = Convert.ToInt32(split[0]), i=0;
            int K = Convert.ToInt32(split[1]);
            split = Console.ReadLine().Split(new Char[] { ' ', '\t', '\n' });
            HashSet<int> v = new HashSet<int>();
            for (i = 0; i < split.Length; i++)
                v.Add(Convert.ToInt32(split[i]));

            int t = 0;
            HashSet<int>.Enumerator e = v.GetEnumerator();
            while (e.MoveNext())
                if (v.Contains(e.Current + K)) t++;          
            Console.Write(t);
    }
}