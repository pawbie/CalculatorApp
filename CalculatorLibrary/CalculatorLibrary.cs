using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;

namespace CalculatorProgram
{
    public class Calculator
    {
        // Log and diagnostic fields
        private JsonWriter _writer;
        private StreamWriter _logFile;

        // Calculations list
        private List<Calculation> _completedCalculations;

        // Operations amount tracker
        public int OperationsCount { get; private set; }

        public Calculator()
        {
            InitializeDiagnosticTrace("calculator.log");
            InitializeJsonWriter("calculatorlog.json");

            _completedCalculations = new();
            OperationsCount = 0;
        }

        public IEnumerable<(string, double)> ListCompletedCalculations()
        {
            foreach (var calculation in _completedCalculations)
            {
                yield return (calculation.Equation, calculation.Result);
            }
        }

        public void ClearCompletedCalculations()
        {
            _completedCalculations.Clear();
            OperationsCount = 0;
        }

        private void InitializeDiagnosticTrace(string fileName)
        {
            StreamWriter logFile = File.CreateText(fileName);

            Trace.Listeners.Add(new TextWriterTraceListener(logFile));
            Trace.AutoFlush = true;

            Trace.WriteLine("Starting Calculator Log");
            Trace.WriteLine(String.Format("Started {0}", System.DateTime.Now.ToString()));
        }

        private void InitializeJsonWriter(string fileName)
        {
            StreamWriter logFileJson = File.CreateText(fileName);
            logFileJson.AutoFlush = true;

            _writer = new JsonTextWriter(logFileJson);
            _writer.Formatting = Formatting.Indented;
            _writer.WriteStartObject();
            _writer.WritePropertyName("Operations");
            _writer.WriteStartArray();
        }

        public void Finish()
        {
            _writer.WriteEndArray();
            _writer.WriteEndObject();
            _writer.Close();
        }

        public double DoOperation(double num1, double num2, string op)
        {
            // Default value is "not-a-number" if an operation, such as division, could result in an error.
            double result = double.NaN;
            string calculation = "";

            _writer.WriteStartObject();
            _writer.WritePropertyName("Operand1");
            _writer.WriteValue(num1);
            _writer.WritePropertyName("Operand2");
            _writer.WriteValue(num2);
            _writer.WritePropertyName("Operation");

            // Use a switch statement to do the math.
            switch (op)
            {
                case "add":
                    result = num1 + num2;
                    calculation = $"{num1} + {num2}";
                    Trace.WriteLine($"{calculation} = {result}");
                    _writer.WriteValue("Add");
                    break;
                case "substract":
                    result = num1 - num2;
                    calculation = $"{num1} - {num2}";
                    Trace.WriteLine($"{calculation} = {result}");
                    _writer.WriteValue("Subtract");
                    break;
                case "multiply":
                    result = num1 * num2;
                    calculation = $"{num1} * {num2}";
                    _writer.WriteValue("Multiply");
                    Trace.WriteLine($"{calculation} = {result}");
                    break;
                case "divide":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        calculation = $"{num1} / {num2}";
                        Trace.WriteLine($"{calculation} = {result}");
                    }
                    _writer.WriteValue("Divide");
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }
            if (double.IsNaN(result) == false) {
                _completedCalculations.Add(new Calculation(calculation, result));
                OperationsCount++;
             };

            _writer.WritePropertyName("Result");
            _writer.WriteValue(result);
            _writer.WriteEndObject();

            return result;
        }
    }
}