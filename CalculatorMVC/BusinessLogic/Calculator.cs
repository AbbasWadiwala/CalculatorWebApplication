using System;
using System.Collections.Generic;
using System.Linq;
using CalculatorMVC.Resources;

namespace CalculatorMVC.BusinessLogic
{
    public interface ICalculator
    {
        string Calculate(string userInput);
    }

    public class Calculator : ICalculator
    {
        private List<string> ListOfNumbersAndOperators { get; set; }

        public string Calculate(string userInput)
        {
            GenerateListOfNumbersAndOperatorsFromInput(userInput);

            if (ListOfNumbersAndOperators == null || !ListOfNumbersAndOperators.Any())
                return userInput;

            var total = ProcessListOfNumbersAndOperators(ListOfNumbersAndOperators);

            return total.FirstOrDefault();
        }

        private void GenerateListOfNumbersAndOperatorsFromInput(string userInput)
        {
            String[] delimiters = { MathOperators.Multiplication, MathOperators.Division, MathOperators.Addition, MathOperators.Subtraction };

            List<string> listOfNumbers = userInput.Split(delimiters, StringSplitOptions.None).ToList();

            if (!ValidateNumberList(listOfNumbers))
                return;

            List<string> listOfOperators = new List<string>();

            foreach (var elementInString in userInput)
                if (delimiters.Any(delimiter => elementInString.ToString().Equals(delimiter)))
                    listOfOperators.Add(elementInString.ToString());

            if (listOfNumbers.Count != listOfOperators.Count + 1)
                return;           

            ListOfNumbersAndOperators = new List<string>(listOfNumbers.Count + listOfOperators.Count);

            for (int i = 0; i < listOfOperators.Count; i++)
            {
                ListOfNumbersAndOperators.Add(listOfNumbers.ElementAt(i));
                ListOfNumbersAndOperators.Add(listOfOperators.ElementAt(i));
            }
            ListOfNumbersAndOperators.Add(listOfNumbers.ElementAt(listOfNumbers.Count-1));
        }

        private static bool ValidateNumberList(List<string> listOfNumbers)
        {
            if (listOfNumbers.Count < 2)
                return false;

            return listOfNumbers.All(number => int.TryParse(number, out _));
        }

        private List<string> ProcessListOfNumbersAndOperators(List<string> listOfNumbersAndOperators)
        {
            if (listOfNumbersAndOperators.Contains(MathOperators.Multiplication) || listOfNumbersAndOperators.Contains(MathOperators.Division))
            {
                var indexOfFirstMultiplicationOperator = listOfNumbersAndOperators.IndexOf(MathOperators.Multiplication);
                indexOfFirstMultiplicationOperator = indexOfFirstMultiplicationOperator != -1 ? indexOfFirstMultiplicationOperator : int.MaxValue;

                var indexOfFirstDivisionOperator = listOfNumbersAndOperators.IndexOf(MathOperators.Division);
                indexOfFirstDivisionOperator = indexOfFirstDivisionOperator != -1 ? indexOfFirstDivisionOperator : int.MaxValue;

                if (indexOfFirstMultiplicationOperator < indexOfFirstDivisionOperator)
                    return ProcessListOfNumbersAndOperators(ProcessACalculationAndMaintainList(MathOperators.Multiplication, listOfNumbersAndOperators));
                else
                    return ProcessListOfNumbersAndOperators(ProcessACalculationAndMaintainList(MathOperators.Division, listOfNumbersAndOperators));
            }
            else if (listOfNumbersAndOperators.Contains(MathOperators.Addition) || listOfNumbersAndOperators.Contains(MathOperators.Subtraction))
            {
                var indexOfFirstAdditionOperator = listOfNumbersAndOperators.IndexOf(MathOperators.Addition);
                indexOfFirstAdditionOperator = indexOfFirstAdditionOperator != -1 ? indexOfFirstAdditionOperator : int.MaxValue;

                var indexOfFirstSubtractionOperator = listOfNumbersAndOperators.IndexOf(MathOperators.Subtraction);
                indexOfFirstSubtractionOperator = indexOfFirstSubtractionOperator != -1 ? indexOfFirstSubtractionOperator : int.MaxValue;

                if (indexOfFirstAdditionOperator < indexOfFirstSubtractionOperator)
                    return ProcessListOfNumbersAndOperators(ProcessACalculationAndMaintainList(MathOperators.Addition, listOfNumbersAndOperators));
                else
                    return ProcessListOfNumbersAndOperators(ProcessACalculationAndMaintainList(MathOperators.Subtraction, listOfNumbersAndOperators));
            }
            else
                return listOfNumbersAndOperators;
        }

        private static List<string> ProcessACalculationAndMaintainList(string operation, List<string> listOfNumbersAndOperators)
        {
            int indexOfOperator = listOfNumbersAndOperators.IndexOf(operation);
            var value1 = double.Parse(listOfNumbersAndOperators.ElementAt(indexOfOperator - 1));
            var value2 = double.Parse(listOfNumbersAndOperators.ElementAt(indexOfOperator + 1));

            var calculatedValue = CalculateBasedOnOperator(operation, value1, value2);
            listOfNumbersAndOperators[indexOfOperator - 1] = calculatedValue;
            listOfNumbersAndOperators.RemoveRange(indexOfOperator, 2);

            return listOfNumbersAndOperators;
        }

        private static string CalculateBasedOnOperator(string operation, double value1, double value2)
        {
            if (operation.Equals(MathOperators.Multiplication))
                return Multiply(value1, value2);
            else if (operation.Equals(MathOperators.Division))
                return Divide(value1, value2);
            else if (operation.Equals(MathOperators.Addition))
                return Add(value1, value2);
            else if (operation.Equals(MathOperators.Subtraction))
                return Subtract(value1, value2);
            else
                return "0";            
        }

        private static string Add(double value1, double value2)
        {
            return (value1 + value2).ToString();
        }

        private static string Subtract(double value1, double value2)
        {
            return (value1 - value2).ToString();
        }

        private static string Multiply(double value1, double value2)
        {
            return (value1 * value2).ToString();
        }

        private static string Divide(double value1, double value2)
        {
            return (value1 / value2).ToString();
        }
    }
}
