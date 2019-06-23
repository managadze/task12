using System;

namespace task12
{
    class Program
    {
        public static void Color(string txt)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(txt);
            Console.ResetColor();
        }

        public static void Red(string txt)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(txt);
            Console.ResetColor();
        }
        static Random rnd = new Random();
        public static int max = 0;
        static public Tree addToTree(Tree root, int new_value, ref int c)
        {
            if (root == null)
            {
                root = new Tree();
                root.value = new_value;
                root.left = null;
                root.right = null;
                return root;
            }
            if (root.value < new_value)  // добавление ветви
            {
                c++;
                root.right = addToTree(root.right, new_value, ref c);
            }
            else
            {
                c++;
                root.left = addToTree(root.left, new_value, ref c);
            }
            return root;
        }

        static public void treeToArray(Tree root, int[] a)
        {
            if (root == null) return;  // условие окончания - нет сыновей
            treeToArray(root.left, a);
            a[max++] = root.value;
            treeToArray(root.right, a);
        }

        static public void sortTree(ref int[] a, ref int comp, ref int rev)
        {
            Tree root = null;
            for (int i = 0; i < a.Length; i++)
            {
                rev++;
                root = addToTree(root, a[i], ref comp);
            }
            treeToArray(root, a);
            max = 0;
        }

        public class Tree
        {
            public int value;
            public Tree left;
            public Tree right;
        }


        static void Show(int[] arr)
        {
            foreach (int x in arr)
                Console.Write(x + " ");
        }

        static int[] BinAfterSort(int[] arr, ref int c, ref int r)
        {
            int[] m = new int[arr.Length];
            m = Copy(arr);
            Console.WriteLine();
            Color("\nСортировка двоичным деревом:");
            Console.WriteLine("Массив после сортировки:");
            c = 0;
            r = 0;
            sortTree(ref m, ref c, ref r);
            Show(m);
            Console.WriteLine($"\nКоличество сравнений = {c}, количество перестановок = {r}");
            return m;
        }

        static int[] CountingSort(int[] arr, ref int com, ref int rev)
        {
            int max = -1;
            for (int i = 0; i < arr.Length; i++)
            {
                com++;
                if (arr[i] > max)
                {
                    max = arr[i];
                }
            }

            int[] mas = new int[max+1];
            for (int i=0; i<arr.Length; i++)
            {
                int j = arr[i];
                mas[j]++;
            }
            int m = 0;
            for (int j=0; j<mas.Length;j++)
            {
                if (mas[j] != 0)
                {
                    for (int i=0; i< mas[j]; i++)
                    {
                        arr[m] = j;
                        m++;
                        
                    }
                }
                com++;
            }
            return arr;
        }
        static int[] Copy(int[] a)
        {
            int[] m = new int[a.Length];
            int i = 0;
            foreach (int x in a)
            {
                m[i] = x;
                i++;
            }
            return m;
        }
        static int[] CountAfterSort(int[] arr)
        {
            int[] m = new int[arr.Length];
            m = Copy(arr);
            int c = 0, r =0;
            Console.WriteLine();
            Color("\nСортировка подсчетом:");
            Console.WriteLine("Массив после сортировки:");
            c = 0;
            r = 0;
            m=CountingSort(m, ref c, ref r);
            Show(m);
            Console.WriteLine($"\nКоличество сравнений = {c}, количество перестановок = {r}");
            return m;
        }

        static void Main(string[] args)
        {
            int compare = 0, reversal = 0;
            bool ok = true;
            int size = 0;
            do
            {
               Console.WriteLine("Введите размер:");
                try
                {
                    size = Int32.Parse(Console.ReadLine());
                    ok = true;
                    if ((size < 1)||(size>200))
                    {
                        Red("Ошибка, введите число от 1 до 200");
                        ok = false;
                    }
                }
                catch
                {
                    ok = false;
                    Red("Ошибка, введите целое число");
                }
            } while (!ok);

            int[] arr = new int [size];
            for (int i=0; i<size; i++)
            {
                arr[i] = rnd.Next(0, 100);
            }

            int[] sort = new int[size];
            Red("Неотсортированный массив:");
            Show(arr);
            sort=BinAfterSort(arr, ref compare, ref reversal);
            sort=CountAfterSort(arr);

            Red("\nОтсортированный по возрастанию массив:");
            Array.Sort(arr);
            Show(arr);
            sort=BinAfterSort(arr, ref compare, ref reversal);
            sort=CountAfterSort(arr);

            Red("\nОтсортированный по убыванию массив:");
            Array.Reverse(arr);
            Show(arr);
            sort=BinAfterSort(arr, ref compare, ref reversal);
            sort=CountAfterSort(arr);

            Console.ReadKey();
        }
    }
}
