using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Inl3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void submit_Click(object sender, EventArgs e)
        {
            resultBox.Clear();
            String ssn = ssnBox.Text;
            if (!checkValidSSN(ssn) || ssn == "")
            {
                MessageBox.Show("Fel format på personnummer", "Fel format!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
               
                Person p = new Person(firstNameBox.Text, lastNameBox.Text, ssn);
                //Print the persons information.
                resultBox.AppendText(p.ToString());
            }
            
        }

        /**
         * Kollar att det inmatade personnumret är enligt formatet i labblydelsen (ååmmddxxxx)
         */
        private Boolean checkSSNFormat(String input)
        {
            string pattern;
            // yy (1900-2009)
            pattern = @"[0-9][0-9]";
            // mm (01-12)
            pattern += @"(0[1-9]|1[0-2])";
            // dd (01-31)
            pattern += @"(0[1-9]|1[0-9]|2[0-9]|3[0-1])";
            // xxxx (0000 - 9999)
            pattern += @"[0-9]{4}$";
            Regex rgx = new Regex(pattern);
            return rgx.IsMatch(input);
        }

        /**
         * Kollar om personnumret är korrekt enligt 21Algoritmen.
         * Tar in personnumret som ååmmdd-xxxx eller utan bindestreck '-'
         */
        private Boolean checkValidSSN(String input)
        {
            input = input.Replace("-", "");
            
            if(!checkSSNFormat(input)) return false;

            int sum = 0;
            
            for(int i=0,multiplier = 0;i<input.Length;i++)
            {
                Char c = input[i];
                multiplier = i % 2 == 0 ? 2 : 1;
                int res = Convert.ToInt32(c.ToString()) * multiplier;

                sum += getNumberSum(res);
            }
            return sum % 10 == 0;
            
        }

        /**
         * Returnerar talsumman enligt 21Algoritmen. 
         * Exempel: 14 ger 1+4 = 5, 21 ger 2 + 1 = 3
         */
        private int getNumberSum(int number)
        {
            if (number < 10) return number;
            int sum = 0,m;
            while(number > 0)
            {
                m = number % 10;
                sum += m;
                number = number / 10;
            }
            return sum;
        }
    }
    
    /**
    Skräddarsydd personklass av det enklaste laget
    Håller allt som relaterar till personen
    */
    class Person
    {
        private string FName { get; set; }
        private string LName { get; set; }
        private string SSN { get; set; }
        private string Sex { get; set; }
        public Person(string fName, string lName, string ssn)
        {
            this.FName = fName;
            this.LName = lName;
            this.SSN = ssn;
            //If second to last digit is even, return female, else male.
            this.Sex = Convert.ToInt32(ssn[ssn.Length - 2].ToString()) % 2 == 0 ? "Kvinna" : "Man";
        }

        /**
         * En överskriven ToString()-metod, anpassad för uppgiftslydelsen
         */
        public override string ToString()
        {
            return "Förnamn: " + FName + "\n"
                + "Efternamn: " + LName + "\n"
                + "Personnummer: " + SSN + "\n"
                + "Kön: " + Sex;
        }
    }
}
