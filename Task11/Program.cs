using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InputHelper;

namespace Task11
{
    class Program
    {
        private static string EncryptString(string text)
        {
            int size = (int)Math.Ceiling(Math.Sqrt(text.Length));
            char[,] matrix = new char[size, size];
            int c = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (c < text.Length)
                        matrix[i, j] = text[c++];
                    else
                        break;
                }
            }

            //char[] textArray = text.ToCharArray();
            //Array.Reverse(textArray);
            //text = new string(textArray);

            //int textLength = text.Length;
            //for (int i = 0; i < size * size - textLength; i++)
            //    text += '\0';

            StringBuilder encryptedString = new StringBuilder();

            int Line = 0;
            int Column = 0;
            int beginLines = 0;
            int beginColumns = 0;
            int endLines = size;
            int endColumns = size;
            int index = 0;
            while (encryptedString.Length != size * size)
            {
                for (int i = Column; i < endColumns; i++)
                {
                    encryptedString.Append(matrix[Line, i]);
                    Column = i;
                }

                if (encryptedString.Length > size * size)
                    break;
                Line++;
                beginLines++;
                for (int i = Line; i < endLines; i++)
                {
                    encryptedString.Append(matrix[i, Column]);
                    Line = i;
                }

                if (encryptedString.Length > size * size)
                    break;
                Column--;
                endColumns--;
                for (int i = Column; i >= beginColumns; i--)
                {
                    encryptedString.Append(matrix[Line, i]);
                    Column = i;
                }

                if (encryptedString.Length > size * size)
                    break;
                Line--;
                endLines--;
                for (int i = Line; i >= beginLines; i--)
                {
                    encryptedString.Append(matrix[i, Column]);
                    Line = i;
                }

                if (encryptedString.Length > size * size)
                    break;
                Column++;
                beginColumns++;
            }

            char[] textArray = encryptedString.ToString().ToCharArray();
            Array.Reverse(textArray);
            return new string(textArray);
        }

        static string DecryptString(string encryptedString)
        {
            int size = (int)Math.Ceiling(Math.Sqrt(encryptedString.Length));
            char[,] matrix = new char[size, size];

            char[] textArray = encryptedString.ToCharArray();
            Array.Reverse(textArray);
            encryptedString = new string(textArray);
            encryptedString = encryptedString.Replace('%', '\0');

            int Line = 0;
            int Column = 0;
            int beginLines = 0;
            int beginColumns = 0;
            int endLines = size;
            int endColumns = size;
            int index = 0;
            while (index < encryptedString.Length)
            {
                for (int i = Column; i < endColumns; i++)
                {
                    matrix[Line, i] = encryptedString[index++];
                    Column = i;
                }

                if (index > size * size)
                    break;
                Line++;
                beginLines++;
                for (int i = Line; i < endLines; i++)
                {
                    matrix[i, Column] = encryptedString[index++];
                    Line = i;
                }

                if (index > size * size)
                    break;
                Column--;
                endColumns--;
                for (int i = Column; i >= beginColumns; i--)
                {
                    matrix[Line, i] = encryptedString[index++];
                    Column = i;
                }

                if (index > size * size)
                    break;
                Line--;
                endLines--;
                for (int i = Line; i >= beginLines; i--)
                {
                    matrix[i, Column] = encryptedString[index++];
                    Line = i;
                }

                if (index > size * size)
                    break;
                Column++;
                beginColumns++;
            }


            StringBuilder decryptedString = new StringBuilder();
            int c = 0;
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    decryptedString.Append(matrix[i, j]);

            return decryptedString.ToString();
        }
        static void Main()
        {
            Console.Clear();
            Console.WriteLine("Задача 11\n=================");
            Console.WriteLine("Условие задачи:\nЧтобы зашифровать текст из 121 буквы, его можно записать в квадратную матрицу порядка 11 по строкам,\n" +
                              "а затем прочитать по спирали, начиная с центра (т. е. с элемента, имеющего индексы 6, 6).\n" +
                              "а) Зашифровать данный текст.\nб) Расшифровать данный текст. \n" +
                              "=================");

            Console.WriteLine("1. Зашифровать строку");
            Console.WriteLine("2. Расшифровать строку");
            int input = Input.ReadInt("Выберите действие:", 1, 2);
            switch (input)
            {
                case 1:
                    EncryptionMenu();
                    break;
                case 2:
                    DecryptionMenu();
                    break;
            }
        }

        static void EncryptionMenu()
        {
            Console.Clear();
            Console.Write("Введите строку, которую хотите зашифровать: ");
            string text = Console.ReadLine();
            string encryptedString = EncryptString(text);

            encryptedString = encryptedString.Replace('\0', '%');
            Console.WriteLine($"Зашифрованная строка (% - символ пустого знака): {encryptedString}");
            Console.ReadLine();
            Main();
        }

        static void DecryptionMenu()
        {
            Console.Clear();
            Console.Write("Введите строку, которую хотите расшифровать (используйте знак % для обозначения пустого символа): ");
            string text = Console.ReadLine();
            string decryptedString = DecryptString(text);
            Console.WriteLine($"Расшифрованная строка: {decryptedString}");
            Console.ReadLine();
            Main();
        }
    }
}
