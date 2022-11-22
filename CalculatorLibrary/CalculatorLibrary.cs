using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;

namespace CalculatorProgram
{
    public class Calculator
    {
        // Log and diagnostic fields
        JsonWriter writer;
        StreamWriter logFile;

        // Operations amount tracker
        public int OperationsCount { get; private set; }

        public Calculator()
        {
            InitializeDiagnosticTrace("calculator.log");
            InitializeJsonWriter("calculatorlog.json");

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

            writer = new JsonTextWriter(logFileJson);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
        }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }

        public double DoOperation(double num1, double num2, int op)
        {
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");

            // Use a switch statement to do the math.
            switch (op)
            {
                case 0:
                    result = num1 + num2;
                    Trace.WriteLine(String.Format("{0} + {1} = {2}", num1, num2, result));
                    writer.WriteValue("Add");
                    break;
                case 1:
                    result = num1 - num2;
                    Trace.WriteLine(String.Format("{0} + {1} = {2}", num1, num2, result));
                    writer.WriteValue("Subtract");
                    break;
                case 2:
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    Trace.WriteLine(String.Format("{0} + {1} = {2}", num1, num2, result));
                    break;
                case 3:
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        Trace.WriteLine(String.Format("{0} + {1} = {2}", num1, num2, result));
                    }
                    writer.WriteValue("Divide");
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }
            if (double.IsNaN(result) == false) OperationsCount++;

            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            return result;
        }
    }
}