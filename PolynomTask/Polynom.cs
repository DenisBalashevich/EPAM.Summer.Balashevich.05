using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace PolynomTask
{
    /// <summary>
    ///  Class Polynom for computing, representation and comparing
    /// </summary>
    public class Polynom : ICloneable, IEquatable<Polynom>, IComparable<Polynom>, IFormattable
    {
        public struct Monom
        {
            public int MonomDegree { get; }
            public double MonomCoefficient { get; }

            public Monom(int degree, double val)
            {
                MonomDegree = degree;
                MonomCoefficient = val;
            }
        }
        private readonly Monom[] coefficients = { };

        /// <summary>
        /// return Polynom's degree
        /// </summary>
        public int Degree
        {
            get { return coefficients.Length; }
        }

        #region Constructors
        /// <summary>
        /// Initialize all information of the Polynom
        /// </summary>
        /// <param name="obj"> Takes Polynom </param>

        public Polynom(Polynom obj)
        {
            if (obj == null)
                throw new ArgumentNullException();
            coefficients = new Monom[obj.coefficients.Length];
            obj.coefficients.CopyTo(coefficients, 0);
        }

        /// <summary>
        /// Initialize all information of the Polyno
        /// </summary>
        /// <param name="arr">Takes params as parameter</param>

        public Polynom(params double[] arr)
        {
            if (arr == null)
                throw new System.Exception("Polynom is empty");

            coefficients = new Monom[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != 0)
                    coefficients[i] = new Monom(i, arr[i]);
            }
        }

        private Polynom(params Monom[] mon)
        {
            if (mon == null)
                throw new System.Exception("Polynom is empty");

            coefficients = new Monom[mon.Length];
            mon.CopyTo(coefficients, 0);
        }
        #endregion


        #region Operators

        /// <summary>
        /// Override operation Sum
        /// </summary>
        /// <param name="first">Polynom</param>
        /// <param name="second">Polynom</param>
        /// <returns>Their Sum</returns>

        public static Polynom operator +(Polynom first, Polynom second)
        {
            int resultDegree = first.Degree >= second.Degree ? first.Degree : second.Degree;
            double[] result = new double[resultDegree + 1];
            first.coefficients.Select(i => i.MonomCoefficient).ToArray().CopyTo(result, 0);

            for (int i = 0; i < second.Degree; i++)
            {
                result[i] += second.coefficients[i].MonomCoefficient;
            }
            return new Polynom(result);
        }

        public static Polynom Sum(Polynom first, Polynom second)
        {
            return first + second;
        }

        /// <summary>
        ///  Override operation Sum
        /// </summary>
        /// <param name="first">Polynom</param>
        /// <param name="second"> number</param>
        /// <returns>Their Sum</returns>

        public static Polynom operator +(Polynom first, double second)
        {
            double[] result = CopyArray(first);
            result[0] += second;
            return new Polynom(result);
        }

        private static double[] CopyArray(Polynom pol)
        {
            double[] result = new double[pol.Degree];
            pol.coefficients.Select(i => i.MonomCoefficient).ToArray().CopyTo(result, 0);
            return result;
        }

        public static Polynom Sum(Polynom first, double second)
        {
            return first + second;
        }

        /// <summary>
        ///  Override operation Minus
        /// </summary>
        /// <param name="first">Polynom</param>
        /// <param name="second"> number</param>
        /// <returns>Their difference</returns>
        public static Polynom operator -(Polynom first, double second)
        {
            double[] result = CopyArray(first);
            result[0] -= second;
            return new Polynom(result);
        }

        public static Polynom Difference(Polynom first, double second)
        {
            return first - second;
        }

        /// <summary>
        ///  Override operation Minus
        /// </summary>
        /// <param name="first">Polynom</param>
        /// <param name="second"> Polynom</param>
        /// <returns>Their difference</returns>

        public static Polynom operator -(Polynom first, Polynom second)
        {
            return new Polynom(first + (-second));
        }

        public static Polynom Difference(Polynom first, Polynom second)
        {
            return first - second;
        }
        /// <summary>
        ///  Override operation Minus
        /// </summary>
        /// <param name="first"></param>
        /// <returns>-Polynom</returns>
        public static Polynom operator -(Polynom pol)
        {
            double[] result = new double[pol.Degree];
            pol.coefficients.Select(i => i.MonomCoefficient * (-1)).ToArray().CopyTo(result, 0);
            return new Polynom(result);
        }


        /// <summary>
        ///  Override operation Multiply
        /// </summary>
        /// <param name="first">Polynom</param>
        /// <param name="second"> Number</param>
        /// <returns>Their Multipying</returns>

        public static Polynom operator *(Polynom firstPolynom, double number)
        {
            var result = firstPolynom.coefficients.Select(i => number * i.MonomCoefficient).ToArray();
            return new Polynom(result);
        }

        public static Polynom Multiplication(Polynom first, Polynom second)
        {
            return first * second;
        }
        /// <summary>
        ///  Override operation Multiply
        /// </summary>
        /// <param name="first">Polynom</param>
        /// <param name="second"> Number</param>
        /// <returns>Their Multipying</returns>

        public static Polynom operator *(Polynom first, Polynom second)
        {
            int resultDegree = first.Degree + second.Degree + 1;
            double[] result = new double[resultDegree];
            for (int i = 0; i < first.Degree; i++)
            {
                for (int j = 0; j < second.Degree; j++)
                {
                    result[i + j] += first.coefficients[i].MonomCoefficient * second.coefficients[j].MonomCoefficient;
                }
            }
            return new Polynom(result);
        }

        public static Polynom Multiplication(Polynom first, double second)
        {
            return first * second;
        }

        /// <summary>
        ///  Override operation Equalating
        /// </summary>
        /// <param name="first">Polynom</param>
        /// <param name="second"> Polynom</param>
        /// <returns>True, if their eqvivalent, False differently</returns>

        public static bool operator ==(Polynom first, Polynom second)
        {
            if (ReferenceEquals(first, null) || ReferenceEquals(second, null))
            {
                return false;
            }
            return first.Equals(second);
        }

        /// <summary>
        ///  Override operation UnEqualating
        /// </summary>
        /// <param name="first">Polynom</param>
        /// <param name="second"> Polynom</param>
        /// <returns>True, if their uneqvivalent, False differently</returns>

        public static bool operator !=(Polynom first, Polynom second)
        {
            if (ReferenceEquals(first, null) || ReferenceEquals(second, null))
            {
                return false;
            }
            return first.Equals(second);
        }
        #endregion

        #region Clone
        /// <summary>
        /// Object Clone
        /// </summary>
        /// <returns></returns>

        object ICloneable.Clone()
        {
            return Clone();
        }

        /// <summary>
        /// Polynom Clone
        /// </summary>
        /// <returns></returns>

        public Polynom Clone()
        {
            return new Polynom(coefficients);
        }

        #endregion

        #region Equals 
        /// <summary>
        /// Defines first Polynom in equalent to second
        /// </summary>
        /// <param name="second"> Represents comparing polynom</param>
        /// <returns>True if their equalent, False differently</returns>

        public bool Equals(Polynom second)
        {
            if (ReferenceEquals(this, second))
                return true;

            if (ReferenceEquals(null, second) || this.Degree != second.Degree)
                return false;

            return coefficients.SequenceEqual(second.coefficients);
        }

        /// <summary>
        /// Defines first Polynom in equalent to second
        /// </summary>
        /// <param name="second"> Represents comparing object</param>
        /// <returns>True if their equalent, False differently</returns>
        /// 
        public override bool Equals(Object obj)
        {
            Polynom polynomObj = obj as Polynom;
            if (obj == null)
                return false;

            return this.Equals(polynomObj);
        }

        /// <summary>
        /// Defines first Polynom in equalent to second
        /// </summary>
        /// <param name="first">Represents comparing polynom</param>
        /// <param name="second">Represents comparing polynom</param>
        /// <returns>True if their equalent, False differently</returns>

        public static bool Equals(Polynom first, Polynom second)
        {
            if (ReferenceEquals(null, first))
                return false;

            return first.Equals(second);
        }
        #endregion

        #region ToString
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            if (Degree == 0)
                result.Append(0);

            if (coefficients[0].MonomCoefficient != 0)
                result.Append(coefficients[0].MonomCoefficient);

            for (int i = 1; i < this.Degree; i++)
            {
                if (coefficients[i].MonomCoefficient > 0)
                    result.AppendFormat("+{0}x^{1}", coefficients[i].MonomCoefficient, coefficients[i].MonomDegree);
                else if (coefficients[i].MonomCoefficient < 0)
                    result.AppendFormat("{0}x^{1}", coefficients[i].MonomCoefficient, coefficients[i].MonomDegree);
            }
            return string.Format("{0}", result);
        }

        public string ToString(string format)
        {
            return ToString(format, null);
        }

        public string ToString(IFormatProvider formatProvider)
        {
            return ToString("G", formatProvider);
        }

        public string ToString(string format, IFormatProvider provider)
        {
            if (String.IsNullOrEmpty(format))
                format = "G";

            if (provider == null)
                provider = CultureInfo.CurrentCulture;

            switch (format.ToUpperInvariant())
            {
                case "G":
                    return this.ToString();
                // Some casses can be added
                default:
                    throw new FormatException(String.Format("The {0} format string is not supported.", format));
            }
        }
        #endregion

        /// <summary>
        /// Override GetHashCode
        /// </summary>
        /// <returns>GetHashCode</returns>
        public override int GetHashCode()
        {
            int ghc = 0;
            for (int i = 0; i < coefficients.Length; i++)
            {
                ghc += (int)coefficients[i].MonomCoefficient + Degree;
            }
            return ghc;
        }
        /// <summary>
        /// Compare Polynoms
        /// </summary>
        /// <param name="second">Caomparing polynom</param>
        /// <returns>Number > 0 if first gratter, = 0 if equalent, Less 0 if first smaller </returns>
        public int CompareTo(Polynom second)
        {
            if (second == null)
                return 1;

            if (ReferenceEquals(this, second))
                return 0;

            if (this.Degree != second.Degree)
                return this.Degree - second.Degree;

            for (int i = 0; i < this.Degree; i++)
            {
                if (coefficients[i].MonomCoefficient > second.coefficients[i].MonomCoefficient)
                    return 1;
                if (coefficients[i].MonomCoefficient < second.coefficients[i].MonomCoefficient)
                    return -1;
            }
            return 0;


        }
    }
}
