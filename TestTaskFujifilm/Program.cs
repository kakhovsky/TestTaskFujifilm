//Objective:
//By starting at the top of the triangle below and moving to adjacent numbers on the row below, the maximum total from top to bottom is 23. 
//3 
//7 4 
//2 4 6 
//8 5 9 3 
//That is: 3 + 7 + 4 + 9 = 23 
//Write a runable program that obtains the maximum total for any triangle of that type(the faster it runs the better).
//
//Dmitry Kakhovsky for Fujifilm, 2016

using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskFujifilm
{
    class Program
    {

        public static void Main(string[] args)
        {

            bool caseA = Palindrome.IsPalindrome("abnbam");
            bool caseB = Palindrome.IsPalindrome("abcba");

            SaveToFile(GenerateTriangle(35));

            var sum1 = MaxValue_v2(ReadFromFile());
            Console.WriteLine("\nSum is: " + sum1);

            Console.ReadLine();

        }


        /// <summary>
        /// Path and sum calculation
        /// </summary>
        /// <param name="triangle"></param>
        /// <returns></returns>
        static int MaxValue_v2(List<List<int>> triangle)
        {
            int sum = 0, startingIndex = 0;

            sum += triangle.Last()[startingIndex];

            for (var triangleIndex = 0; triangleIndex < triangle.Count() - 1; triangleIndex++)
            {

                int[] abc = new int[3];

                abc[0] = GetValueByIndex(triangle[triangleIndex], startingIndex + 1);
                abc[1] = GetValueByIndex(triangle[triangleIndex], startingIndex);
                abc[2] = GetValueByIndex(triangle[triangleIndex], startingIndex - 1);

                if (abc[0] > abc[1] && abc[0] >= abc[2])
                {
                    startingIndex++;
                }
                else
                {
                    if (abc[2] > abc[1] && abc[2] >= abc[0]) startingIndex--;
                }

                sum += triangle[triangleIndex][startingIndex];

                PrintRow(triangle[triangleIndex], startingIndex);

            }

            return sum;

        }

        #region helpers

        /// <summary>
        /// Generates triangle filled with numbers
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        static List<List<int>> GenerateTriangle(int height)
        {

            List<List<int>> triangle = new List<List<int>>();
            Random rnd = new Random();

            int iteration = 0, lineCount = 0;

            do
            {
                var line = new List<int>();

                for (int i = 0; i < iteration; i++)
                {
                    line.Add(rnd.Next(0, 9));
                }

                triangle.Add(line);
                iteration++;

            } while (iteration < height);


#if DEBUG
            foreach (var line in triangle)
            {
                foreach (var number in line)
                {
                    Console.Write($"{number} ");
                }
                Console.WriteLine();
            }
#endif
            Console.WriteLine();
            return triangle;
        }

        static int GetValueByIndex(List<int> row, int index)
        {
            if (index >= 0 && index <= (row.Count() - 1)) return row[index];
            return -1;

        }

        /// <summary>
        /// Print row to console
        /// </summary>
        /// <param name="row">Row</param>
        /// <param name="selectedIndex">Selected index</param>
        static void PrintRow(List<int> row, int selectedIndex)
        {
            int i = 0;
            foreach (var number in row)
            {
                if (selectedIndex == i)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{number} ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write($"{number} ");
                }
                i++;
            }
            Console.WriteLine();

        }

        /// <summary>
        /// read array from file
        /// </summary>
        /// <returns></returns>
        static List<List<int>> ReadFromFile()
        {
            List<List<int>> triangle = new List<List<int>>();
            using (StreamReader reader = File.OpenText("triangle.txt"))
            {
                while (reader.Peek() > 0)
                {
                    try
                    {

                        var line = reader.ReadLine().Split(',');

                        if (line[0].StartsWith(" ") || line[0].StartsWith(","))
                        {
                            continue;

                        }

                        List<int> intLine = new List<int>();
                        foreach (var integer in line)
                        {
                            int value;
                            bool ok = int.TryParse(integer, out value);
                            if (ok) intLine.Add(value);

                        }  //  line.Select(int.TryParse()).ToList();

                        if (intLine.Any())
                            triangle.Add(intLine);
                    }
                    catch (Exception)
                    {

                        //throw;
                    }
                }

            }

            return triangle;

        }


        /// <summary>
        /// Save generated array to file
        /// </summary>
        /// <param name="array"></param>
        static void SaveToFile(List<List<int>> array)
        {
            using (StreamWriter writer = System.IO.File.CreateText("triangle.txt"))
            {
                foreach (var line in array)
                {
                    foreach (var integer in line)
                    {
                        writer.Write($"{integer},");
                    }
                    writer.WriteLine();

                }

            }


        }
        #endregion


    }

}