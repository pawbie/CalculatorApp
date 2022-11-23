using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorProgram
{
    internal class Calculation
    {
        public readonly string Equation;
        public readonly double Result;

        public Calculation(string equation, double result)
        {
            Equation = equation;
            Result = result;
        }
    }
}
