//    PolynomialDivision, performs polynomial division and shows the method.
//    
//    Copyright 2017  Oliver Mead
//
//    PolynomialDivision is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    PolynomialDivision is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program. If not, see<https://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolynomialDivision
{
    class Program
    {
        static double a;
        static double p;
        static double x;
        static double remainder;
        static Expression expression;
        static Expression quotientExpr;

        public static double TakeInputDouble(bool nonzero = false)
        {
            string input = Console.ReadLine();
            double output;
            if (!double.TryParse(input, out output))
            {
                string stroutput = "";
                foreach(char character in input)
                {
                    stroutput += Convert.ToString((int)character);
                }
                if(stroutput == "")
                {
                    stroutput = "0";
                }
                output = Convert.ToDouble(stroutput);
            }
            while (output == 0 && nonzero)
            {
                Console.Write("This value cannot be 0!!\n>> ");
                input = Console.ReadLine();
                if (!double.TryParse(input, out output))
                {
                    string stroutput = "";
                    foreach (char character in input)
                    {
                        stroutput += Convert.ToString((int)character);
                    }
                    if (stroutput == "")
                    {
                        stroutput = "0";
                    }
                    output = Convert.ToDouble(stroutput);
                }
            }
            return output;
        }

        public static int TakeInputInt()
        {
            string input = Console.ReadLine();
            int output;
            if (!int.TryParse(input, out output))
            {
                Console.Write("Enter an integer please, no decimals, strings or complex numbers\n>> ");
                input = Console.ReadLine();
            }
            return output;
        }

        public static int TakeInputIntPos()
        {
            string input = Console.ReadLine();
            int output;
            while (!int.TryParse(input, out output) || input.Contains("-"))
            {
                Console.Write("Enter a natural number please, no decimals, negatives strings or complex numbers\n>> ");
                input = Console.ReadLine();
            }
            return output;
        }

        public static void TakeExpressionInput()
        {
            List<int> exponents = new List<int> { };
            List<double> coefficients = new List<double> { };
            Console.Write("What is the highest power of x in the expression?\n>> ");
            exponents.Add(TakeInputIntPos());
            Console.Write("Please enter the coefficient of x{0}\n>> ", Term.Power(exponents[0].ToString()));
            coefficients.Add(TakeInputDouble(true));

            for (int expon = exponents[0] - 1; expon >= 0; expon--)
            {
                Console.Write("Please enter the coefficient of x{0}\n>> ", Term.Power(expon.ToString()));
                coefficients.Add(TakeInputDouble());
                exponents.Add(expon);
            }

            List<Term> terms = new List<Term> { };
            for (int i = 0; i < exponents.Count(); i++)
            {
                terms.Add(new Term(coefficients[i], exponents[i]));
            }
            expression = new Expression(terms);
            Console.WriteLine("You Entered: {0}", Convert.ToString(expression));
        }

        public static void TakeDivisorInput()
        {
            Console.Write("In the form (ax + p)\nPlease enter the value of a\n>> ");
            a = TakeInputDouble(true);
            Console.Write("Please enter the value of p\n>> ");
            p = TakeInputDouble();
            x = (-p) / a;
        }

        public static string PolynomialDivide(Expression thisExpr, double a, double p)
        {
            Expression thisExpression = new Expression(thisExpr.Terms);
            string quotient;
            List<Term> quotientTerms = new List<Term> { };
            Term divisorXPart = new Term(a, 1);
            string working = "\n";

            string leftGap;
            string originalLeftGap;
            int shift;

            Expression divisor = new Expression(new List<Term> { new Term(a, 1), new Term(p, 0) });
            leftGap = new string(' ', divisor.ToStringVerbose().Length - 1);
            originalLeftGap = leftGap;
            string topbar = leftGap + " " + new string('_', thisExpression.ToStringVerbose().Length + 3);
            working += topbar + "\n";
            working += divisor.ToStringVerbose() + "|" + thisExpression.ToStringVerbose() +"\n";
            
            Term next;
            Term exprTerm;
            Term pByNext;
            Term carry;
            string line;
            string underline;
            string otherLine;
            for( int count = 0; count < thisExpression.Terms.Count() - 1; )
            {
                exprTerm = thisExpression.Terms[count];
                next = exprTerm / divisorXPart;
                pByNext = next * p;
                quotientTerms.Add(next);

                count ++;

                if (pByNext.Coefficient >= 0)
                {
                    line = "-(" + exprTerm.ToString() + " + " + pByNext.ToString() + ")";
                }
                else
                {
                    line = "-(" + exprTerm.ToString() + " - " + (pByNext * -1).ToString() + ")";
                }

                working += leftGap + line + "\n";
                shift = (line.Length - (" + " + pByNext.ToString() + ")").Length) + 1;

                underline = new string('\u203E', line.Length);
                working += leftGap + underline + "\n";

                leftGap += new string(' ', shift);

                thisExpression.Terms[count] = thisExpression.Terms[count] - pByNext;
                if (count < thisExpression.Terms.Count() - 1)
                {
                    carry = thisExpr.Terms[count + 1];
                    if (carry.Coefficient >= 0)
                    {
                        otherLine = "  " + thisExpression.Terms[count].ToString() + " + " + carry.ToString() + "\n";
                    }
                    else
                    {
                        otherLine = "  " + thisExpression.Terms[count].ToString() + " - " + (carry * -1).ToString() + "\n";
                    }
                }
                else
                {
                    otherLine = " " + remainder.ToString();
                }

                working += leftGap + otherLine;
            }
            quotientExpr = (new Expression(quotientTerms));
            quotient = originalLeftGap + "  " + quotientExpr.ToStringVerbose() + working;
            return quotient;
        }

        public static void Copyright()
        {
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Copyright();
            Console.WriteLine("POLYNOMIAL DIVISION");

            TakeExpressionInput();
            TakeDivisorInput();

            remainder = expression.Subx(x);
            if (remainder != 0)
            {
                if (p > 0)
                {
                    if (a != 1 && a != -1)
                    {
                        Console.WriteLine("({0}x + {1}) is not a factor of the expression you entered!", a, p);
                    }
                    else if (a == 1)
                    {
                        Console.WriteLine("(x + {0}) is not a factor of the expression you entered!", p);
                    }
                    else
                    {
                        Console.WriteLine("({0} - x) is not a factor of the expression you entered!", p);
                    }
                }
                else if (p < 0)
                {
                    if (a != 1 && a != -1)
                    {
                        Console.WriteLine("({0}x - {1}) is not a factor of the expression you entered!", a, p * -1);
                    }
                    else if (a == 1)
                    {
                        Console.WriteLine("(x - {0}) is not a factor of the expression you entered!", p * -1);
                    }
                    else
                    {
                        Console.WriteLine("-(x + {0}) is not a factor of the expression you entered!", p * -1);
                    }
                }
                else
                {
                    if (a != 1 && a != -1)
                    {
                        Console.WriteLine("{0}x is not a factor of the expression you entered!", a);
                    }
                    else if (a == 1)
                    {
                        Console.WriteLine("x is not a factor of the expression you entered!");
                    }
                    else
                    {
                        Console.WriteLine("-x is not a factor of the expression you entered!");
                    }
                }
                Console.WriteLine("The remainder is {0}", expression.Subx(x));
            }
            else
            {
                if (p > 0)
                {
                    if (a != 1 && a != -1)
                    {
                        Console.WriteLine("({0}x + {1}) is a factor of the expression you entered!", a, p);
                    } else if (a == 1)
                    {
                        Console.WriteLine("(x + {0}) is a factor of the expression you entered!", p);
                    } else
                    {
                        Console.WriteLine("({0} - x) is a factor of the expression you entered!", p);
                    }
                } else if (p < 0)
                {
                    if (a != 1 && a != -1)
                    {
                        Console.WriteLine("({0}x - {1}) is a factor of the expression you entered!", a, p * -1);
                    }
                    else if (a == 1)
                    {
                        Console.WriteLine("(x - {0}) is a factor of the expression you entered!", p * -1);
                    }
                    else
                    {
                        Console.WriteLine("-(x + {0}) is a factor of the expression you entered!", p * -1);
                    }
                } else
                {
                    if (a != 1 && a != -1)
                    {
                        Console.WriteLine("{0}x is a factor of the expression you entered!", a);
                    }
                    else if (a == 1)
                    {
                        Console.WriteLine("x is a factor of the expression you entered!");
                    }
                    else
                    {
                        Console.WriteLine("-x is a factor of the expression you entered!");
                    }
                }
            }

            string result = PolynomialDivide(expression, a, p);

            Console.WriteLine("\n{0}\n", result);

            Console.WriteLine("quotient =   {0}", quotientExpr.ToString());

            Console.ReadKey();
        }
    }
}
