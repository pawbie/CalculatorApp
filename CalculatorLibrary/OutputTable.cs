﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorProgram

{
    internal class OutputTable
    {
        private static int _tableWidth = 73;

        static void PrintLine()
        {
            Console.WriteLine(new string('-', _tableWidth));
        }

        static void PrintRow(params string[] columns)
        {
            int width = (_tableWidth - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            Console.WriteLine(row);
        }

        static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }

        static void PrintHeaders(params string[] values)
        {
            PrintLine();
            PrintRow(values);
            PrintLine();
        }

        public static void PrintTable(string[] headers, string[] values)
        {
            PrintHeaders(headers);
            foreach (var value in values)
            {
                PrintRow(value.Split(','));
            }

            PrintLine();
            Console.WriteLine();
        }
    }
}
