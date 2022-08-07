using System;
using System.Text;
using System.Linq;
using System.Numerics;
using System.Collections.Generic;

#region Exercise
/*
The purpose of this kata is to write a program that can do some algebra. Write 
a function expand that takes in an expression with a single, one character 
variable, and expands it. The expression is in the form (ax+b)^n where a and b 
are integers which may be positive or negative, x is any single character variable, 
and n is a natural number. If a = 1, no coefficient will be placed in front of 
the variable. If a = -1, a "-" will be placed in front of the variable.

The expanded form should be returned as a string in the form ax^b+cx^d+ex^f... 
where a, c, and e are the coefficients of the term, x is the original one 
character variable that was passed in the original expression and b, d, and f,
are the powers that x is being raised to in each term and are in decreasing order. 
If the coefficient of a term is zero, the term should not be included. 
If the coefficient of a term is one, the coefficient should not be included. 
If the coefficient of a term is -1, only the "-" should be included. 
If the power of the term is 0, only the coefficient should be included. 
If the power of the term is 1, the caret and power should be excluded.
*/
#endregion Exercise

namespace z22
{
    public class KataSolution
    {
        private static double GetFactorial(int n)
        {
            double result = 1;
            for (int i = 2; i <= n; ++i)
                result *= i;
            return result;
        }

        private static double GetBinomialCoefficient(int n, int k) => GetFactorial(n) / (GetFactorial(k) * GetFactorial(n - k));

        public static string Expand(string expr)  
        {
            if (!int.TryParse(expr.Split('^').Last(), out int degree)) throw new ArgumentException();
            
            string expression = expr.Split("^").First().Split("()".ToCharArray(), StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).First();
            
            if (degree == 0) return "1";
            if (degree == 1) return expression;

            string first = expression.Split("+-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).First(),
                unknownCoefficient = string.Join("", first.TakeLast(1));
            int firstArgument = Convert.ToInt32(first.Length == 1 ? 1 : string.Join("", first.SkipLast(1))),
                secondArgument = Convert.ToInt32(expression.Split("+-".ToCharArray()).Last());

            if (expression.First() == '-') firstArgument = -firstArgument;
            if (expression.LastIndexOf('-') > 0) secondArgument = -secondArgument;
            
            StringBuilder result = new StringBuilder();
            for (int currentDegree = 0; currentDegree <= degree; ++currentDegree)
            {
                double binomialCoefficient = GetBinomialCoefficient(degree, currentDegree) * Math.Pow(firstArgument, degree - currentDegree) * Math.Pow(secondArgument, currentDegree);
                int coefficientDegree = degree - currentDegree;
                string binomial = string.Empty;
                bool last = binomialCoefficient == 1 && coefficientDegree == 0;
                
                if (binomialCoefficient > 1 || last) binomial = $"+{binomialCoefficient}";
                else if (binomialCoefficient == 1) binomial = string.Empty;
                else if (binomialCoefficient == -1 && coefficientDegree != 0) binomial = "-";
                else if (binomialCoefficient < 1) binomial = binomialCoefficient.ToString();

                if (binomialCoefficient != 0) 
                {
                    string unkown = coefficientDegree > 1 ? $"{unknownCoefficient}^{coefficientDegree}" : coefficientDegree == 1 ? unknownCoefficient : string.Empty;
                    result.Append(binomial);
                    result.Append(unkown);
                }
            }

            return result[0] == '+' ? result.ToString()[1..^0] : result.ToString();
        }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            // Console.WriteLine(KataSolution.Expand("(x+1)^2"));
            // Console.WriteLine(KataSolution.Expand("(p-1)^3"));
            // Console.WriteLine(KataSolution.Expand("(2f+4)^6"));
            // Console.WriteLine(KataSolution.Expand("(-2a-4)^0"));
            // Console.WriteLine(KataSolution.Expand("(-12t+43)^2"));
            Console.WriteLine(KataSolution.Expand("(-o+23)^3"));
            // Console.WriteLine(KataSolution.Expand("(-x-1)^2"));
        }
    }
}