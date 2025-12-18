using FluentAssertions;
using FractionLib;

namespace xUnitTest
{
    public class FractionLibTest
    {
        [Fact]
        public void FractionDesigner_WithZeroDeniminator_ReturnDivideByZeroException()
        {
            int numerator = 1, denominator = 0;
            Assert.Throws<DivideByZeroException>(() =>
            {
                Fraction fraction = new(numerator, denominator);
            });
        }

        [Fact]
        public void FractionDesigner_WithNegativeDeniminator_ReturnValues()
        {
            int numerator = 2, denominator = -4;

            Fraction fraction = new(numerator, denominator);

            fraction.Numerator.Should().Be(-1);
            fraction.Denominator.Should().Be(2);
        }

        [Fact]
        public void FractionDesigner_WithPositiveDeniminator_ReturnValues()
        {
            int numerator = 2, denominator = 8;

            Fraction fraction = new(numerator, denominator);

            fraction.Numerator.Should().Be(1);
            fraction.Denominator.Should().Be(4);
        }

        [Theory]
        [InlineData(-1, 2, -0.5f)]
        [InlineData(1, 4, 0.25f)]
        [InlineData(4, 2, 2f)]
        public void ToDouble_WithCorrectValues_BeDouble_ReturnCorrectValues(int numerator, int denumerator, double expected)
        {
            Fraction fraction = new(numerator, denumerator);
            fraction.ToDouble().Should().BeOfType(typeof(double));
            fraction.ToDouble().Should().Be(expected);
        }

        [Fact]
        public void ToString_WithCorrectValues_ReturnCorrectString()
        {
            Fraction fraction = new(3, 5);
            fraction.ToString().Should().Be("3/5");
        }

        [Fact]
        public void Equals_WithEqualData_ReturnTrue()
        {
            Fraction fraction1 = new(1, 2);
            Fraction fraction2 = new(2, 4);
            fraction1.Equals(fraction2).Should().BeTrue();
        }

        [Fact]
        public void Equals_WithUnequalData_ReturnFalse()
        {
            Fraction fraction1 = new(3, 4);
            Fraction fraction2 = new(2, 4);
            fraction1.Equals(fraction2).Should().BeFalse();
        }

        [Fact]
        public void OverridenEquals_WithEqualData_ReturnTrue()
        {
            Fraction fraction1 = new(1, 2);
            object fraction2 = fraction1;

            fraction1.Equals(fraction2).Should().BeTrue();
        }

        [Fact]
        public void OverridenEquals_WithUnequalData_ReturnFalse()
        {
            Fraction fraction1 = new(1, 2);
            object fraction2 = new Fraction(3, 5);

            fraction1.Equals(fraction2).Should().BeFalse();
        }

        [Fact]
        public void OverridenEquals_WithNullData_ReturnFalse()
        {
            Fraction fraction1 = new(1, 2);
            object fraction2 = null!;

            fraction1.Equals(fraction2).Should().BeFalse();
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(-3, 7)]
        public void OverridenGetHashCode_WithCorrectValues_ReturnCorrectValue(int numerator, int denumerator)
        {
            Fraction fraction = new(numerator, denumerator);
            var expected = HashCode.Combine(numerator, denumerator);

            fraction.GetHashCode().Should().Be(expected);
        }

        [Theory]
        [InlineData(1, 2, 3, 5, 11, 10)]
        [InlineData(-4, 3, 2, 5, -14, 15)]
        [InlineData(-2, -3, 1, 3, 1, 1)]
        public void PlusOperator_WithCorrectValues_ReturnCorrectValues(int firstNum, int firstDenum, 
            int secondEnum, int secondDenum, int expNum, int expDenum)
        {
            Fraction fraction1 = new(firstNum, firstDenum);
            Fraction fraction2 = new(secondEnum, secondDenum);

            Fraction expFraction = new(expNum, expDenum);

            var fraction3 = fraction1 + fraction2;

            fraction3.Should().Be(expFraction);
        }

        [Theory]
        [InlineData(1, 2, 3, 5, -1, 10)]
        [InlineData(-4, 3, 2, 5, -26, 15)]
        [InlineData(-2, -3, 1, 3, 1, 3)]
        public void MinusOperator_WithCorrectValues_ReturnCorrectValues(int firstNum, int firstDenum,
            int secondEnum, int secondDenum, int expNum, int expDenum)
        {
            Fraction fraction1 = new(firstNum, firstDenum);
            Fraction fraction2 = new(secondEnum, secondDenum);

            Fraction expFraction = new(expNum, expDenum);
            var fraction3 = fraction1 - fraction2;
            fraction3.Should().Be(expFraction);
        }

        [Theory]
        [InlineData(1, 2, 3, 5, 3, 10)]
        [InlineData(-4, 3, 2, 5, -8, 15)]
        [InlineData(-2, -3, 1, 3, 2, 9)]
        public void MultiplyOperator_WithCorrectValues_ReturnCorrectValues(int firstNum, int firstDenum,
            int secondEnum, int secondDenum, int expNum, int expDenum)
        {
            Fraction fraction1 = new(firstNum, firstDenum);
            Fraction fraction2 = new(secondEnum, secondDenum);

            Fraction expFraction = new(expNum, expDenum);
            var fraction3 = fraction1 * fraction2;
            fraction3.Should().Be(expFraction);
        }

        [Theory]
        [InlineData(1, 2, 3, 5, 5, 6)]
        [InlineData(-4, 3, 2, 5, -20, 6)]
        [InlineData(-2, -3, 1, 3, 2, 1)]
        public void DividedOperator_WithCorrectValues_ReturnCorrectValues(int firstNum, int firstDenum,
            int secondEnum, int secondDenum, int expNum, int expDenum)
        {
            Fraction fraction1 = new(firstNum, firstDenum);
            Fraction fraction2 = new(secondEnum, secondDenum);

            Fraction expFraction = new(expNum, expDenum);
            var fraction3 = fraction1 / fraction2;
            fraction3.Should().Be(expFraction);
        }

        [Theory]
        [InlineData(3, 2, 0, 7)]
        public void DividedOperator_WithUncorrectValues_ThrowException(int firstNum, int firstDenum,
            int secondEnum, int secondDenum)
        {
            Assert.Throws<DivideByZeroException>(() =>
            {
                Fraction fraction1 = new(firstNum, firstDenum);
                Fraction fraction2 = new(secondEnum, secondDenum);
                var fraction3 = fraction1 / fraction2;
            });
        }
    }
}