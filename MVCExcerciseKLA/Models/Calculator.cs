using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCExcerciseKLA.Models
{
    public class Calculator
    {

        public Calculator()
        {
            Instructions= "0";
            History = String.Empty;
        }
        //contains the instructions that are being set
        [Required(ErrorMessage ="Please enter instructions and/or calculations to process")]
//need to properly fix this regex.        [RegularExpression(@"(((\.?\d*)|(\d*\.\d*))[\+\-\/\*])*((\.?\d*)|(\d*\.\d*))")]
        public string Instructions { get; set; }
        //contains the result of the operation.
        public string Result { get; set; }
        /// <summary>
        /// Contains the history of operations that have been performed.
        /// </summary>
        public string History { get; set; }

        //contains the error string in case that something went wrong.
        public string Error { get; set;}
        /// <summary>
        /// This is the exposed function that will call the recursive function.
        /// </summary>
        public void  Calculate()
        {
           try
            {
                Error = string.Empty;
                this.Result = InvokeCalculation(Instructions).ToString();
                if(String.IsNullOrEmpty(History))
                {
                    History = String.Empty;
                }
                History = String.Concat(History, "\n",Instructions, "=", Result);
            }
            catch(Exception ex)
            {
                Error = string.Concat("Error on the calculation: ", ex.Message);
            }
        }
        /// <summary>
        /// Recursively resolves the calculation strings.
        /// 
        /// It processes the operators from left to right, just like a simple calculator.
        /// 
        /// Initially it receives the entire instruction set, then it tries to resolve by calling itself recursively.
        /// </summary>
        /// <param name="part">A part or all of the calculation string</param>
        /// <returns></returns>
        private double InvokeCalculation(string part)
        {
            double leftPartDouble = 0;
            double rightPartDouble = 0;
            string rightPartString = string.Empty;
            string leftPartString = string.Empty;
            bool rightPartValidDouble = false;
            bool leftPartValidDouble = false;
            int closestOperatorToTheRIght = 0;
            char operatorToApply = 'N';

            if (double.TryParse(part, out leftPartDouble))
            {
                return leftPartDouble;
            }
            else
            {
                if(part.LastIndexOf('*')>0 && part.LastIndexOf('*')>closestOperatorToTheRIght)
                {
                    operatorToApply = '*';
                    closestOperatorToTheRIght = part.LastIndexOf('*');
                }
                if (part.LastIndexOf('/') > 0 && part.LastIndexOf('/') > closestOperatorToTheRIght)
                {
                    operatorToApply = '/';
                    closestOperatorToTheRIght = part.LastIndexOf('/');
                }
                if (part.LastIndexOf('+') > 0 && part.LastIndexOf('+') > closestOperatorToTheRIght)
                {
                    operatorToApply = '+';
                    closestOperatorToTheRIght = part.LastIndexOf('+');
                }
                if (part.LastIndexOf('-') > 0 && part.LastIndexOf('-') > closestOperatorToTheRIght)
                {
                    operatorToApply = '-';
                    closestOperatorToTheRIght = part.LastIndexOf('-');
                }

                FigureLeftAndRightPart(part, operatorToApply,  out rightPartString,out leftPartString, out rightPartDouble, out leftPartDouble,  out rightPartValidDouble, out leftPartValidDouble);
                if (!leftPartValidDouble)
                {
                    ///Call recursively this method to strip out the rest of the operations.
                    leftPartDouble = InvokeCalculation(leftPartString);
                }
                switch(operatorToApply)
                {
                    case '*': return leftPartDouble * rightPartDouble;
                    case '/': return leftPartDouble / rightPartDouble;
                    case '+': return leftPartDouble + rightPartDouble;
                    case '-': return leftPartDouble - rightPartDouble;
                    default: throw new Exception(string.Concat("Not a valid operator: ", operatorToApply));
                }
            }
        }


        /// <summary>
        /// Internal helper method that assists on spliting a  calculation in left and right part of the first ocurrence of an operator
        /// </summary>
        /// <param name="source">The initial calculation string</param>
        /// <param name="operatorCharacter">either * or / or + or - </param>
        /// <param name="leftPart">the left part of the operation</param>
        /// <param name="rightPart">the right part of the operation.</param>
        private void FigureLeftAndRightPart(string source, char operatorCharacter, out string rightPart, out string leftPart, out double rightPartDouble,  out double leftPartDouble, out bool rightPartValidDouble, out bool leftPartValidDouble)
        {
            leftPart = source.Substring(0, source.LastIndexOf(operatorCharacter));
            rightPart = source.Substring(source.LastIndexOf(operatorCharacter) + 1);

            rightPartValidDouble = Double.TryParse(rightPart, out rightPartDouble);
            if (! rightPartValidDouble)
            {
                throw new Exception(string.Concat("Not a valid number: ", rightPart));
            }
            leftPartValidDouble = Double.TryParse(leftPart, out leftPartDouble);
        }
    }
}