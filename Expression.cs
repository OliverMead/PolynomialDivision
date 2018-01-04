//    Copyright 2017  Oliver Mead
//
//    Expression.cs is a part of PolynomialDivision
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
    class Expression
    {
        public List<Term> Terms { get; set; }

        public double Subx(double x)
        {
            double result = 0;
            double next = 0;
            foreach(Term term in Terms)
            {
                next = Math.Pow(x, term.Exponent) * term.Coefficient;
                result += next;
            }
            return result;
        }

        public Expression(List<Term> terms)
        {
            Terms = terms;
        }

        public string ToStringVerbose()
        {
            string result = "";
            int count = 0;
            foreach(Term term in Terms)
            {
                if (count == 0)
                {
                    result += term.ToString();
                }
                else if (term.Coefficient < 0)
                {
                    result += " - " + (term * -1).ToString();
                }
                else
                {
                    result += " + " + term.ToString();
                }
                count++;
            }
            return result;
        }

        public override string ToString()
        {
            string result = "";
            Term term; // to store the next term for adding to the string output
            for (int count = 0; count < Terms.Count(); count++)
            {
                term = Terms[count];
                //result += term.Coefficient + "x" + Term.Power(Convert.ToString(term.Exponent)) + " + ";
                if (count != 0)
                {
                    if (term.Coefficient == 1)
                    {
                        if (term.Exponent == 1)
                        {
                            result += " + x";
                        }
                        else if (term.Exponent == 0)
                        {
                            result += " + 1";
                        }
                        else if (term.Exponent > 1)
                        {
                            result += " + " + "x" + Term.Power(Convert.ToString(term.Exponent));
                        }
                    }
                    else if (term.Coefficient == -1)
                    {
                        if (term.Exponent == 1)
                        {
                            result += " - x";
                        }
                        else if (term.Exponent == 0)
                        {
                            result += " - 1";
                        }
                        else if (term.Exponent > 1)
                        {
                            result += " - " + "x" + Term.Power(Convert.ToString(term.Exponent));
                        }
                    }
                    else if (term.Coefficient > 0)
                    {
                        if (term.Exponent == 1)
                        {
                            result += " + " + Convert.ToString(term.Coefficient) + "x";
                        }
                        else if (term.Exponent == 0)
                        {
                            result += " + " + Convert.ToString(term.Coefficient);
                        }
                        else if (term.Exponent > 1)
                        {
                            result += " + " + Convert.ToString(term.Coefficient) + "x" + Term.Power(Convert.ToString(term.Exponent));
                        }
                    }
                    else if (term.Coefficient < 0)
                    {
                        if (term.Exponent == 1)
                        {
                            result += " - " + Convert.ToString(term.Coefficient * -1) + "x";
                        }
                        else if (term.Exponent == 0)
                        {
                            result += " - " + Convert.ToString(term.Coefficient * -1);
                        }
                        else if (term.Exponent > 1)
                        {
                            result += " - " + Convert.ToString(term.Coefficient * -1) + "x" + Term.Power(Convert.ToString(term.Exponent));
                        }
                    }
                }
                else
                {
                    if (term.Coefficient == 1)
                    {
                        if (term.Exponent == 1)
                        {
                            result += "x";
                        }
                        else if (term.Exponent == 0)
                        {
                            result += "1";
                        }
                        else if (term.Exponent > 1)
                        {
                            result += "x" + Term.Power(Convert.ToString(term.Exponent));
                        }
                    }
                    else if (term.Coefficient == -1)
                    {
                        if (term.Exponent == 1)
                        {
                            result += "-x";
                        }
                        else if (term.Exponent == 0)
                        {
                            result += "-1";
                        }
                        else if (term.Exponent > 1)
                        {
                            result += "-" + "x" + Term.Power(Convert.ToString(term.Exponent));
                        }
                    }
                    else if (term.Coefficient > 0)
                    {
                        if (term.Exponent == 1)
                        {
                            result += Convert.ToString(term.Coefficient) + "x";
                        }
                        else if (term.Exponent == 0)
                        {
                            result += Convert.ToString(term.Coefficient);
                        }
                        else if (term.Exponent > 1)
                        {
                            result += Convert.ToString(term.Coefficient) + "x" + Term.Power(Convert.ToString(term.Exponent));
                        }
                    }
                    else if (term.Coefficient < 0)
                    {
                        if (term.Exponent == 1)
                        {
                            result += "-" + Convert.ToString(term.Coefficient * -1) + "x";
                        }
                        else if (term.Exponent == 0)
                        {
                            result += "-" + Convert.ToString(term.Coefficient * -1);
                        }
                        else if (term.Exponent > 1)
                        {
                            result += "-" + Convert.ToString(term.Coefficient * -1) + "x" + Term.Power(Convert.ToString(term.Exponent));
                        }
                    }
                }
            } // format the string correctly so it looks natural
            return result;
        }
    }
}
