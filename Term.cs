//    Copyright 2017  Oliver Mead
//
//    Terms.cs is a part of PolynomialDivision

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
    class Term
    {
        public double Coefficient { get; set; }
        public int Exponent { get; set; }
        
        public static string Power(string exponentstr, bool called_by_self = false)
        {
            string power = "";
            switch (exponentstr)
            {
                case "0":
                    power = "\u2070"; // unicode ^0
                    break;
                case "1":
                    if (!called_by_self)
                    {
                        break;
                    }
                    else
                    {
                        power = "\u00B9"; // unicode ^1
                        break;
                    }
                case "2":
                    power = "\u00B2"; // unicode ^2
                    break;
                case "3":
                    power = "\u00B3"; // unicode ^3
                    break;
                case "4":
                    power = "\u2074"; // unicode ^4
                    break;
                case "5":
                    power = "\u2075"; // unicode ^5
                    break;
                case "6":
                    power = "\u2076"; // unicode ^6
                    break;
                case "7":
                    power = "\u2077"; // unicode ^7
                    break;
                case "8":
                    power = "\u2078"; // unicode ^8
                    break;
                case "9":
                    power = "\u2079"; // unicode ^9
                    break;
                case "n":
                    power = "\u207F";
                    break;
                default:
                    // the recursive part, will get the unicode superscript character for the first character, then run recursively for the others.
                    power = Power(Convert.ToString(exponentstr[0]), true) + Power(exponentstr.Remove(0, 1), true);
                    break;
            }
            return power;
        }

        public Term(double coefficient, int exponent)
        {
            Coefficient = coefficient;
            Exponent = exponent;
        }
        public static Term operator + (Term term1, Term term2)
        {
            Term result = new Term(term1.Coefficient + term2.Coefficient, term2.Exponent);
            return result;
        }
        public static Term operator - (Term term1, Term term2)
        {
            Term result = new Term(term1.Coefficient - term2.Coefficient, term2.Exponent);
            return result;
        }
        public static Term operator * (Term term, int multiplier)
        {
            Term result = new Term(term.Coefficient * multiplier, term.Exponent);
            return result;
        }
        public static Term operator * (Term term, double multiplier)
        {
            Term result = new Term(term.Coefficient * multiplier, term.Exponent);
            return result;
        }
        public static Term operator / (Term term1, Term term2)
        {
            Term result = new Term(term1.Coefficient / term2.Coefficient, term1.Exponent - term2.Exponent);
            return result;
        }
        public override string ToString()
        {
            if (Exponent == 0)
            {
                return Convert.ToString(Coefficient);
            }
            else
            {
                if (Exponent != 1)
                {
                    if (Coefficient >= 0)
                    {
                        return " " + Convert.ToString(Coefficient) + "x" + Power(Convert.ToString(Exponent));
                    }
                    else
                    {
                        return Convert.ToString(Coefficient) + "x" + Power(Convert.ToString(Exponent));
                    }
                }
                else
                {
                    if (Coefficient >= 0)
                    {
                        return " " + Convert.ToString(Coefficient) + "x ";
                    }
                    else
                    {
                        return Convert.ToString(Coefficient) + "x ";
                    }
                }
            }
        }
    }
}
