using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            //int k = 8;
            //for (int i = 1; i < k; i++)
            //{
            //    foreach (var item in new int[15] { 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15})
            //    {
            //        if (item == 8)
            //        {
            //            Console.Write("#");
            //        }
            //        else if (item)
            //        {

            //        }
            //        else
            //        {
            //            Console.Write(" ");
            //        }
            //    }
            //    Console.WriteLine(" ");
            //}

            int line = 8;
            for (int i = 0; i < line; i++)
            {
                //输出空格
                for (int x = 0; x < line - i; x++)
                {
                    Console.Write(" ");
                }
                //输出星号
                for (int y = 0; y < 2 * i + 1; y++)
                {
                    Console.Write("#");
                }
                Console.WriteLine("");
            }
            //Console.WriteLine("Hello World!");
        }
    }
}
