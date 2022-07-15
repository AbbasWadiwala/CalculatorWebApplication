using CalculatorMVC.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CalculatorMVC.UnitTests.Systems.BusinessLogic
{
    public class TestCalculator
    {

        [Theory]
        [InlineData("14", "4+5*2")]
        [InlineData("6.5", "4+5/2")]
        [InlineData("5.5", "4+5/2-1")]
        [InlineData("20", "1*6/3-2+10*2")]
        public void Calculate_GivenValidInputString_ReturnCorrectCalculatedValue(string expected, string input)
        {
            Calculator calculator = new();

            string result = calculator.Calculate(input);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("","")]
        [InlineData("2", "2")]
        [InlineData("1+", "1+")]
        public void Calculate_GivenInvalidInputString_ReturnInputString(string expected, string input)
        {
            Calculator calculator = new();

            string result = calculator.Calculate(input);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("∞", "1/0")]
        [InlineData("∞", "10/0")]
        public void Calculate_GivenInputThatWouldDivideBy0_ReturnInfinity(string expected, string input)
        {
            Calculator calculator = new();

            string result = calculator.Calculate(input);

            Assert.Equal(expected, result);
        }        
    }
}
