using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PolynomTask;

namespace PolynomTask.Tests
{
    [TestFixture]
    public class PolynomTests
    {
        [TestCaseSource("SumData")]
        public string Op_Sum_Tests(Polynom a, Polynom b)
        {
            Polynom p1 = new Polynom();
            p1 = a + b;
            return p1.ToString();
        }

        [TestCaseSource("SubstractionData")]
        public string Op_Substraction_Tests(Polynom a, Polynom b)
        {
            Polynom p1 = new Polynom();
            p1 = a - b;
            return p1.ToString();
        }

        [TestCaseSource("SubstractionDataNumber")]
        public string Op_SubstractionData_Tests(Polynom a, double b)
        {
            Polynom p1 = new Polynom();
            p1 = a - b;
            return p1.ToString();
        }

        [TestCaseSource("SumDataNumber")]
        public string Op_SumData_Tests(Polynom a, double b)
        {
            Polynom p1 = new Polynom();
            p1 = a + b;
            return p1.ToString();
        }

        [TestCaseSource("MinusPolynom")]
        public string Op_MinPolynom_Tests(Polynom a)
        {
            Polynom p1 = new Polynom();
            p1 = -a ;
            return p1.ToString();
        }

        [TestCaseSource("MultiplayData")]
        public string Op_Multiplay_Tests(Polynom a, Polynom b)
        {
            Polynom p1 = new Polynom();
            p1 = a * b;
            return p1.ToString();
        }

        [TestCaseSource("EqualsData")]
        public bool Equals_Tests(Polynom a, Polynom b)
        {
            return a==b;
        }

        public IEnumerable<TestCaseData> EqualsData()
        {
            yield return new TestCaseData(new Polynom(1, 1), new Polynom(1, 1)).Returns(true);
            yield return new TestCaseData(new Polynom(-1), new Polynom(1, 2, 3, 4)).Returns(false);
            yield return new TestCaseData(new Polynom(0, 0, 0, 0, 0, 0), new Polynom(0,0,0,0,0)).Returns(false);
            yield return new TestCaseData(new Polynom(-5, -6), new Polynom(-5,-6)).Returns(true);
        }

        public IEnumerable<TestCaseData> MultiplayData()
        {
            yield return new TestCaseData(new Polynom(1, 1), new Polynom(1, 1)).Returns("1+2x^1+1x^2");
            yield return new TestCaseData(new Polynom(-1), new Polynom(1, 2, 3, 4)).Returns("-1-2x^1-3x^2-4x^3");
            yield return new TestCaseData(new Polynom(0, 0, 0, 0, 0, 0), new Polynom(1, 2, 3, 4)).Returns(string.Empty);
            yield return new TestCaseData(new Polynom(0, 0, 0, 0), new Polynom(0, 0)).Returns(string.Empty);
            yield return new TestCaseData(new Polynom(-5, -6), new Polynom(-1, -9, -4)).Returns("5+51x^1+74x^2+24x^3");
        }

        public IEnumerable<TestCaseData> MinusPolynom()
        {
            yield return new TestCaseData(new Polynom(1, 2, 3, 4)).Returns("-1-2x^1-3x^2-4x^3");
        }

        public IEnumerable<TestCaseData> SumDataNumber()
        {
            yield return new TestCaseData(new Polynom(1, 2, 3, 4), 5).Returns("6+2x^1+3x^2+4x^3");
            yield return new TestCaseData(new Polynom(1, 2, 3, 4), 0).Returns("1+2x^1+3x^2+4x^3");
            yield return new TestCaseData(new Polynom(1, 2, 3, 4), -5).Returns("-4+2x^1+3x^2+4x^3");
        }

        public IEnumerable<TestCaseData> SubstractionDataNumber()
        {
            yield return new TestCaseData(new Polynom(1, 2, 3, 4), 5).Returns("-4+2x^1+3x^2+4x^3");
            yield return new TestCaseData(new Polynom(1, 2, 3, 4), 0).Returns("1+2x^1+3x^2+4x^3");
            yield return new TestCaseData(new Polynom(1, 2, 3, 4), -5).Returns("6+2x^1+3x^2+4x^3");
        }

        public IEnumerable<TestCaseData> SubstractionData()
        {
            yield return new TestCaseData(new Polynom(1, 2, 3, 4), new Polynom(1, 2, 3, 4)).Returns(string.Empty);
            yield return new TestCaseData(new Polynom(1), new Polynom(1, 2, 3, 4)).Returns("-2x^1-3x^2-4x^3");
            yield return new TestCaseData(new Polynom(0, 0, 0, 0, 0, 0), new Polynom(1, 2, 3, 4)).Returns("-1-2x^1-3x^2-4x^3");
            yield return new TestCaseData(new Polynom(0, 0, 0, 0), new Polynom(0, 0)).Returns(string.Empty);
            yield return new TestCaseData(new Polynom(-5, -6), new Polynom(-1, -9, -4)).Returns("-4+3x^1+4x^2");
        }

        public IEnumerable<TestCaseData> SumData()
        {
            yield return new TestCaseData(new Polynom(1, 2, 3, 4), new Polynom(1, 2, 3, 4)).Returns("2+4x^1+6x^2+8x^3");
            yield return new TestCaseData(new Polynom(1), new Polynom(1, 2, 3, 4)).Returns("2+2x^1+3x^2+4x^3");
            yield return new TestCaseData(new Polynom(0, 0, 0, 0, 0, 0), new Polynom(1, 2, 3, 4)).Returns("1+2x^1+3x^2+4x^3");
            yield return new TestCaseData(new Polynom(0, 0, 0, 0), new Polynom(0, 0)).Returns(string.Empty);
            yield return new TestCaseData(new Polynom(-5, -6), new Polynom(-1, -9, -4)).Returns("-6-15x^1-4x^2");
        }
    }
}
