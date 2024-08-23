using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Collections;
using System.Net;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace WdS.ElioPlus.Lib.Utils
{
    public class Validations
    {
        public const int MIN_SMS_VALIDITY = 30;
        public const int MAX_SMS_VALIDITY = 4320;

        //IP validation method 1
        public static bool IsValidIP(string value)
        {
            var quads = value.Split('.');

            // if we do not have 4 quads, return false
            if (!(quads.Length == 4)) return false;

            // for each quad
            foreach (var quad in quads)
            {
                int q;
                // if parse fails 
                // or length of parsed int != length of quad string (i.e.; '1' vs '001')
                // or parsed int < 0
                // or parsed int > 255
                // return false
                if (!Int32.TryParse(quad, out q)
                    || !q.ToString().Length.Equals(quad.Length)
                    || q < 0
                    || q > 255) { return false; }

            }

            return true;
        }

        public static bool IsValidUrl(string urlString)
        {
            Uri uri;
            return Uri.TryCreate(urlString, UriKind.Absolute, out uri)
                && (uri.Scheme == Uri.UriSchemeHttp
                 || uri.Scheme == Uri.UriSchemeHttps
                 || uri.Scheme == Uri.UriSchemeFtp
                 || uri.Scheme == Uri.UriSchemeMailto
                /*...*/);
        }

        public static bool IsValidWebLink(string link)
        {
            bool isValid = true;

            if (!link.StartsWith("https://www.") || !link.StartsWith("http://www.") || !link.StartsWith("https://") || !link.StartsWith("http://") || !link.StartsWith("www."))
            {
                isValid = false;
            }

            return isValid;
        }

        //IP validation method 2
        public static bool IsIpValid(string addr)
        {
            IPAddress ip;
            bool valid = !string.IsNullOrEmpty(addr) && IPAddress.TryParse(addr, out ip);
            return valid;
        }

        public static string NextIpAddress(string input)
        {
            string[] tokens = input.Split(".".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            for (int i = tokens.Length - 1; i >= 0; i--)
            {
                int item = Convert.ToInt32(tokens[i]);
                if (item < 255)
                {
                    tokens[i] = (item + 1).ToString();
                    for (int j = i + 1; j < 4; j++)
                    {
                        tokens[j] = "0";
                    }
                    break;
                }
            }
            return tokens[0] + "." + tokens[1] + "." + tokens[2] + "." + tokens[3];
        }

        //check for Leap year
        public static bool CheckLeapYear(int year)
        {
            if ((year % 4 == 0) && (year % 100 != 0) || (year % 400 == 0))

                return true;

            else return false;
        }

        //get only the right days per month in dropDownList
        public static ArrayList GetDays(int year, int month)
        {
            int i;
            ArrayList aday = new ArrayList();
            switch (month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    for (i = 1; i <= 31; i++)
                    {

                        aday.Add(i);
                    }
                    break;
                case 2:
                    if (CheckLeapYear(year))
                    {
                        for (i = 1; i <= 29; i++)
                            aday.Add(i);
                    }
                    else
                    {
                        for (i = 1; i <= 28; i++)

                            aday.Add(i);
                    }
                    break;
                case 4:
                case 6:
                case 9:
                case 11:
                    for (i = 1; i <= 30; i++)
                        aday.Add(i);
                    break;
            }
            return aday;
        }

        public static string ClearStringForBarcode(string s)
        {
            if (string.IsNullOrEmpty(s)) return "";
            string finalString = "";

            for (int i = 0; i < s.Length; i++)
            {
                string c = s.Substring(i, 1);
                switch (c)
                {
                    case "[":
                    case "]":
                    case "-":
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    case "8":
                    case "9":
                    case "0":
                    case "a":
                    case "b":
                    case "c":
                    case "d":
                    case "e":
                    case "f":
                    case "g":
                    case "h":
                    case "i":
                    case "j":
                    case "k":
                    case "l":
                    case "m":
                    case "n":
                    case "o":
                    case "p":
                    case "q":
                    case "r":
                    case "s":
                    case "t":
                    case "u":
                    case "v":
                    case "w":
                    case "x":
                    case "y":
                    case "z":
                    case "A":
                    case "B":
                    case "C":
                    case "D":
                    case "E":
                    case "F":
                    case "G":
                    case "H":
                    case "I":
                    case "J":
                    case "K":
                    case "L":
                    case "M":
                    case "N":
                    case "O":
                    case "P":
                    case "Q":
                    case "R":
                    case "S":
                    case "T":
                    case "U":
                    case "V":
                    case "W":
                    case "X":
                    case "Y":
                    case "Z":
                        finalString += c;
                        break;
                }
            }

            return finalString;
        }

        static public string ClearString_KeepNumbersEnglishChars(string s)
        {
            if (string.IsNullOrEmpty(s)) return "";
            string smssentin = "";

            for (int i = 0; i < s.Length; i++)
            {
                string c = s.Substring(i, 1);
                switch (c)
                {
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    case "8":
                    case "9":
                    case "0":
                    case "a":
                    case "b":
                    case "c":
                    case "d":
                    case "e":
                    case "f":
                    case "g":
                    case "h":
                    case "i":
                    case "j":
                    case "k":
                    case "l":
                    case "m":
                    case "n":
                    case "o":
                    case "p":
                    case "q":
                    case "r":
                    case "s":
                    case "t":
                    case "u":
                    case "v":
                    case "w":
                    case "x":
                    case "y":
                    case "z":
                    case "A":
                    case "B":
                    case "C":
                    case "D":
                    case "E":
                    case "F":
                    case "G":
                    case "H":
                    case "I":
                    case "J":
                    case "K":
                    case "L":
                    case "M":
                    case "N":
                    case "O":
                    case "P":
                    case "Q":
                    case "R":
                    case "S":
                    case "T":
                    case "U":
                    case "V":
                    case "W":
                    case "X":
                    case "Y":
                    case "Z":
                    case "_":
                    case ".":
                        smssentin += c;
                        break;
                }
            }

            return smssentin;
        }

        static public string ClearString_KeepNumbersEnglishCharsAndSymbols(string s)
        {
            if (string.IsNullOrEmpty(s)) return "";
            string smssentin = "";

            for (int i = 0; i < s.Length; i++)
            {
                string c = s.Substring(i, 1);
                switch (c)
                {
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    case "8":
                    case "9":
                    case "0":
                    case "a":
                    case "b":
                    case "c":
                    case "d":
                    case "e":
                    case "f":
                    case "g":
                    case "h":
                    case "i":
                    case "j":
                    case "k":
                    case "l":
                    case "m":
                    case "n":
                    case "o":
                    case "p":
                    case "q":
                    case "r":
                    case "s":
                    case "t":
                    case "u":
                    case "v":
                    case "w":
                    case "x":
                    case "y":
                    case "z":
                    case "A":
                    case "B":
                    case "C":
                    case "D":
                    case "E":
                    case "F":
                    case "G":
                    case "H":
                    case "I":
                    case "J":
                    case "K":
                    case "L":
                    case "M":
                    case "N":
                    case "O":
                    case "P":
                    case "Q":
                    case "R":
                    case "S":
                    case "T":
                    case "U":
                    case "V":
                    case "W":
                    case "X":
                    case "Y":
                    case "Z":
                    case "@":
                    case "£":
                    case "_":
                    case "!":
                    case "$":
                    case "#":
                    case "?":
                    case "(":
                    case ")":
                    case "*":
                    case ":":
                    case "+":
                    case ";":
                    case ",":
                    case "<":
                    case "-":
                    case "=":
                    case ".":
                    case ">":
                    case "/":
                    case "~":
                        smssentin += c;
                        break;
                }
            }

            return smssentin;
        }

        static public string ClearString_ConvertCommonCapitalLettersToEnglish(string text)
        {
            StringBuilder sb = new StringBuilder();

            for (int p = 0; p < text.Length; p++)
            {
                string c = text.Substring(p, 1);

                switch (c)
                {
                    //h prvth stili ellhnika kai h deyterh mix
                    case "Α":
                        sb.Append("A");
                        break;
                    case "Β":
                        sb.Append("B");
                        break;
                    case "Γ":
                        sb.Append("Γ");
                        break;
                    case "Δ":
                        sb.Append("Δ");
                        break;
                    case "Ε":
                        sb.Append("E");
                        break;
                    case "Ζ":
                        sb.Append("Z");
                        break;
                    case "Η":
                        sb.Append("H");
                        break;
                    case "Θ":
                        sb.Append("Θ");
                        break;
                    case "Ι":
                        sb.Append("I");
                        break;
                    case "Κ":
                        sb.Append("K");
                        break;
                    case "Λ":
                        sb.Append("Λ");
                        break;
                    case "Μ":
                        sb.Append("M");
                        break;
                    case "Ν":
                        sb.Append("N");
                        break;
                    case "Ξ":
                        sb.Append("Ξ");
                        break;
                    case "Ο":
                        sb.Append("O");
                        break;
                    case "Π":
                        sb.Append("Π");
                        break;
                    case "Ρ":
                        sb.Append("P");
                        break;
                    case "Σ":
                        sb.Append("Σ");
                        break;
                    case "Τ":
                        sb.Append("T");
                        break;
                    case "Υ":
                        sb.Append("Y");
                        break;
                    case "Φ":
                        sb.Append("Φ");
                        break;
                    case "Χ":
                        sb.Append("X");
                        break;
                    case "Ψ":
                        sb.Append("Ψ");
                        break;
                    case "Ω":
                        sb.Append("Ω");
                        break;
                    default:
                        sb.Append(text.Substring(p, 1));
                        break;
                }
            }

            return sb.ToString();

        }

        static public string ConvertLettersToEnglish(string text)
        {
            StringBuilder sb = new StringBuilder();

            text = text.Replace(" ", "_");

            for (int p = 0; p < text.Length; p++)
            {
                string c = text.Substring(p, 1);

                if (c == " ")
                    c = "_";

                switch (c)
                {
                    //h prvth stili ellhnika kai h deyterh mix
                    case "α":
                        sb.Append("a");
                        break;
                    case "β":
                        sb.Append("b");
                        break;
                    case "γ":
                        sb.Append("g");
                        break;
                    case "δ":
                        sb.Append("d");
                        break;
                    case "ε":
                        sb.Append("e");
                        break;
                    case "ζ":
                        sb.Append("z");
                        break;
                    case "η":
                        sb.Append("h");
                        break;
                    case "θ":
                        sb.Append("th");
                        break;
                    case "ι":
                        sb.Append("i");
                        break;
                    case "κ":
                        sb.Append("k");
                        break;
                    case "λ":
                        sb.Append("l");
                        break;
                    case "μ":
                        sb.Append("m");
                        break;
                    case "ν":
                        sb.Append("n");
                        break;
                    case "ξ":
                        sb.Append("ks");
                        break;
                    case "ο":
                        sb.Append("o");
                        break;
                    case "π":
                        sb.Append("p");
                        break;
                    case "ρ":
                        sb.Append("r");
                        break;
                    case "σ":
                        sb.Append("s");
                        break;
                    case "τ":
                        sb.Append("t");
                        break;
                    case "υ":
                        sb.Append("u");
                        break;
                    case "φ":
                        sb.Append("f");
                        break;
                    case "χ":
                        sb.Append("x");
                        break;
                    case "ψ":
                        sb.Append("ps");
                        break;
                    case "ω":
                        sb.Append("o");
                        break;
                    case "Α":
                        sb.Append("A");
                        break;
                    case "Β":
                        sb.Append("B");
                        break;
                    case "Γ":
                        sb.Append("C");
                        break;
                    case "Δ":
                        sb.Append("D");
                        break;
                    case "Ε":
                        sb.Append("E");
                        break;
                    case "Ζ":
                        sb.Append("Z");
                        break;
                    case "Η":
                        sb.Append("H");
                        break;
                    case "Θ":
                        sb.Append("TH");
                        break;
                    case "Ι":
                        sb.Append("I");
                        break;
                    case "Κ":
                        sb.Append("K");
                        break;
                    case "Λ":
                        sb.Append("L");
                        break;
                    case "Μ":
                        sb.Append("M");
                        break;
                    case "Ν":
                        sb.Append("N");
                        break;
                    case "Ξ":
                        sb.Append("KS");
                        break;
                    case "Ο":
                        sb.Append("O");
                        break;
                    case "Π":
                        sb.Append("P");
                        break;
                    case "Ρ":
                        sb.Append("R");
                        break;
                    case "Σ":
                        sb.Append("S");
                        break;
                    case "Τ":
                        sb.Append("T");
                        break;
                    case "Υ":
                        sb.Append("Y");
                        break;
                    case "Φ":
                        sb.Append("F");
                        break;
                    case "Χ":
                        sb.Append("X");
                        break;                   
                    case "Ψ":
                        sb.Append("PS");
                        break;
                    case "Ω":
                        sb.Append("O");
                        break;                   
                    default:
                        sb.Append(text.Substring(p, 1));
                        break;
                }
            }

            return sb.ToString();

        }

        static public string ParseText7bit(string sms, bool longsms, ref int SmsNumber)
        {
            if (string.IsNullOrEmpty(sms)) return "";

            int SmsLength = 0;
            string sms_text = "";

            for (int p = 0; p < sms.Length; p++)
            {
                string c = sms.Substring(p, 1);
                switch (c)
                {
                    case "€":
                    case "\\":
                    case "[":
                    case "]":
                    case "{":
                    case "}":
                    case "~":
                    case "^":
                    case "|":
                        SmsLength += 2;
                        break;
                    default:
                        SmsLength += 1;
                        break;
                }

                if (!longsms)
                {
                    if (SmsLength > 160)
                        continue;
                    else
                        sms_text += sms.Substring(p, 1);
                }
                else
                {
                    sms_text += sms.Substring(p, 1);
                }
            }

            if (!longsms)
            {
                SmsNumber = 1;
            }
            else
            {
                if (SmsLength > 160)
                {
                    if ((SmsLength % 153) == 0)
                        SmsNumber = (int)Math.Floor((decimal)SmsLength / 153);
                    else
                        SmsNumber = (int)Math.Floor((decimal)SmsLength / 153) + 1;

                }
                else
                {
                    SmsNumber = 1;
                }
            }

            return sms_text;

        }

        static public string filter_wapurl(string s)
        {
            string smssentin = "";

            for (int p = 0; p < s.Length; p++)
            {
                string c = s.Substring(p, 1);
                switch (c)
                {
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    case "8":
                    case "9":
                    case "0":
                    case "a":
                    case "b":
                    case "c":
                    case "d":
                    case "e":
                    case "f":
                    case "g":
                    case "h":
                    case "i":
                    case "j":
                    case "k":
                    case "l":
                    case "m":
                    case "n":
                    case "o":
                    case "p":
                    case "q":
                    case "r":
                    case "s":
                    case "t":
                    case "u":
                    case "v":
                    case "w":
                    case "x":
                    case "y":
                    case "z":
                    case "A":
                    case "B":
                    case "C":
                    case "D":
                    case "E":
                    case "F":
                    case "G":
                    case "H":
                    case "I":
                    case "J":
                    case "K":
                    case "L":
                    case "M":
                    case "N":
                    case "O":
                    case "P":
                    case "Q":
                    case "R":
                    case "S":
                    case "T":
                    case "U":
                    case "V":
                    case "W":
                    case "X":
                    case "Y":
                    case "Z":
                    case "@":
                    case "_":
                    case "!":
                    case "$":
                    case "#":
                    case "?":
                    case "%":
                    case "&":
                    case "'":
                    case "(":
                    case ")":
                    case "*":
                    case ":":
                    case "+":
                    case ";":
                    case ",":
                    case "<":
                    case "-":
                    case "=":
                    case ".":
                    case ">":
                    case "/":
                    case "\\":
                    case "[":
                    case "]":
                    case "{":
                    case "}":
                    case "~":
                    case "^":
                    case "\"|":
                        smssentin += c;
                        break;
                    default:
                        break;
                }
            }
            if (smssentin.Length > 160)
                return smssentin.Substring(0, 160);
            else
                return smssentin;
        }

        static public string FilterSmsValidity(string s)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;
            else
            {
                int value;
                if (!Int32.TryParse(s, out value))
                {
                    throw new Exception("Err600:Please enter a valid value in 'Smsvalidity' field");
                }
                else if (value < MIN_SMS_VALIDITY || value > MAX_SMS_VALIDITY)
                {
                    throw new Exception("Err601:SmsValidity Out Of Range");
                }
                return s;
            }
        }

        static public string FilterSmsValidityApiBridge(string s)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;
            else
            {
                int value;
                if (!Int32.TryParse(s, out value))
                {
                    return MAX_SMS_VALIDITY.ToString();
                }
                else if (value < MIN_SMS_VALIDITY || value > MAX_SMS_VALIDITY)
                {
                    return MAX_SMS_VALIDITY.ToString();
                }
                return s;
            }
        }

        static public bool isSmsSenderValid(string s)
        {
            for (int p = 0; p < s.Length; p++)
            {
                string c = s.Substring(p, 1);
                switch (c)
                {
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    case "8":
                    case "9":
                    case "0":
                    case "a":
                    case "b":
                    case "c":
                    case "d":
                    case "e":
                    case "f":
                    case "g":
                    case "h":
                    case "i":
                    case "j":
                    case "k":
                    case "l":
                    case "m":
                    case "n":
                    case "o":
                    case "p":
                    case "q":
                    case "r":
                    case "s":
                    case "t":
                    case "u":
                    case "v":
                    case "w":
                    case "x":
                    case "y":
                    case "z":
                    case "A":
                    case "B":
                    case "C":
                    case "D":
                    case "E":
                    case "F":
                    case "G":
                    case "H":
                    case "I":
                    case "J":
                    case "K":
                    case "L":
                    case "M":
                    case "N":
                    case "O":
                    case "P":
                    case "Q":
                    case "R":
                    case "S":
                    case "T":
                    case "U":
                    case "V":
                    case "W":
                    case "X":
                    case "Y":
                    case "Z":
                    case ".":
                    case " ":
                        break;
                    default:
                        return false;
                }
            }
            return true;
        }

        static public string FilterSmsSender(string s)
        {
            string smssentin = "";

            for (int p = 0; p < s.Length; p++)
            {
                string c = s.Substring(p, 1);
                switch (c)
                {
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    case "8":
                    case "9":
                    case "0":
                    case "a":
                    case "b":
                    case "c":
                    case "d":
                    case "e":
                    case "f":
                    case "g":
                    case "h":
                    case "i":
                    case "j":
                    case "k":
                    case "l":
                    case "m":
                    case "n":
                    case "o":
                    case "p":
                    case "q":
                    case "r":
                    case "s":
                    case "t":
                    case "u":
                    case "v":
                    case "w":
                    case "x":
                    case "y":
                    case "z":
                    case "A":
                    case "B":
                    case "C":
                    case "D":
                    case "E":
                    case "F":
                    case "G":
                    case "H":
                    case "I":
                    case "J":
                    case "K":
                    case "L":
                    case "M":
                    case "N":
                    case "O":
                    case "P":
                    case "Q":
                    case "R":
                    case "S":
                    case "T":
                    case "U":
                    case "V":
                    case "W":
                    case "X":
                    case "Y":
                    case "Z":
                    case ".":
                    case " ":
                        smssentin += c;
                        break;
                    default:
                        break;
                }
            }
            return smssentin;
        }

        static public bool IsNumeric(string s)
        {
            decimal d;
            bool ok = Decimal.TryParse(s, out d);
            return ok;
        }

        static public readonly string UsernameValidSymbols = "0123456789abcdefghijklmnopqrstuqyxvzwABCDEFGHIJKLMNOPQRSTUQYXVZW_-.@!$";
        static public bool IsUsernameCharsValid(string username)
        {
            foreach (char c in username)
            {
                if (!UsernameValidSymbols.Contains(c.ToString()))
                    return false;
            }
            return true;
        }

        static public readonly string PasswordValidSymbols = "0123456789abcdefghijklmnopqrstuqyxvzwABCDEFGHIJKLMNOPQRSTUQYXVZW!@#$%^&*()_.";
        static public bool IsPasswordCharsValid(string password)
        {
            foreach (char c in password)
            {
                if (!PasswordValidSymbols.Contains(c.ToString()))
                    return false;
            }
            return true;
        }


        static public bool NotVlddCharsForUserName(string s)
        {
            bool numberexist = false;
            bool alphaexist = false;
            bool acceptchar = false;
            bool notacceptchar = false;
            //int lengthcounter = 0;
            foreach (char c in s)
            {
                switch (c)
                {
                    case '0':
                        numberexist = true;
                        break;
                    case '1':
                        numberexist = true;
                        break;
                    case '2':
                        numberexist = true;
                        break;
                    case '3':
                        numberexist = true;
                        break;
                    case '4':
                        numberexist = true;
                        break;
                    case '5':
                        numberexist = true;
                        break;
                    case '6':
                        numberexist = true;
                        break;
                    case '7':
                        numberexist = true;
                        break;
                    case '8':
                        numberexist = true;
                        break;
                    case '9':
                        numberexist = true;
                        break;
                    case '_':
                        acceptchar = true;
                        break;
                    case '.':
                        acceptchar = true;
                        break;
                    case 'a':
                        alphaexist = true;
                        break;
                    case 'b':
                        alphaexist = true;
                        break;
                    case 'c':
                        alphaexist = true;
                        break;
                    case 'd':
                        alphaexist = true;
                        break;
                    case 'e':
                        alphaexist = true;
                        break;
                    case 'f':
                        alphaexist = true;
                        break;
                    case 'g':
                        alphaexist = true;
                        break;
                    case 'h':
                        alphaexist = true;
                        break;
                    case 'j':
                        alphaexist = true;
                        break;
                    case 'k':
                        alphaexist = true;
                        break;
                    case 'l':
                        alphaexist = true;
                        break;
                    case 'm':
                        alphaexist = true;
                        break;
                    case 'n':
                        alphaexist = true;
                        break;
                    case 'o':
                        alphaexist = true;
                        break;
                    case 'p':
                        alphaexist = true;
                        break;
                    case 'q':
                        alphaexist = true;
                        break;
                    case 'r':
                        alphaexist = true;
                        break;
                    case 's':
                        alphaexist = true;
                        break;
                    case 'w':
                        alphaexist = true;
                        break;
                    case 't':
                        alphaexist = true;
                        break;
                    case 'y':
                        alphaexist = true;
                        break;
                    case 'u':
                        alphaexist = true;
                        break;
                    case 'i':
                        alphaexist = true;
                        break;
                    case 'x':
                        alphaexist = true;
                        break;
                    case 'z':
                        alphaexist = true;
                        break;
                    case 'v':
                        alphaexist = true;
                        break;
                    case '`':
                        notacceptchar = true;
                        break;
                    case '~':
                        notacceptchar = true;
                        break;
                    case '!':
                        notacceptchar = true;
                        break;
                    case '@':
                        notacceptchar = true;
                        break;
                    case '#':
                        notacceptchar = true;
                        break;
                    case '%':
                        notacceptchar = true;
                        break;
                    case '^':
                        notacceptchar = true;
                        break;
                    case '&':
                        notacceptchar = true;
                        break;
                    case '*':
                        notacceptchar = true;
                        break;
                    case '(':
                        notacceptchar = true;
                        break;
                    case ')':
                        notacceptchar = true;
                        break;
                    case '-':
                        notacceptchar = true;
                        break;
                    case '+':
                        notacceptchar = true;
                        break;
                    case '=':
                        notacceptchar = true;
                        break;
                    case ']':
                        notacceptchar = true;
                        break;
                    case '[':
                        notacceptchar = true;
                        break;
                    case '}':
                        notacceptchar = true;
                        break;
                    case '{':
                        notacceptchar = true;
                        break;
                    case '|':
                        notacceptchar = true;
                        break;
                    case '"':
                        notacceptchar = true;
                        break;
                    case ':':
                        notacceptchar = true;
                        break;
                    case ';':
                        notacceptchar = true;
                        break;
                    case '/':
                        notacceptchar = true;
                        break;
                    case '?':
                        notacceptchar = true;
                        break;
                    case ',':
                        notacceptchar = true;
                        break;
                    case '<':
                        notacceptchar = true;
                        break;
                    case '>':
                        notacceptchar = true;
                        break;
                }
            }
            if ((numberexist && alphaexist || acceptchar) && !notacceptchar)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        static public bool NotValidCharsForUserName(string s)
        {
            foreach (char c in s)
            {
                switch (c)
                {
                    case '`':
                    case '~':
                    case '!':
                    case '@':
                    case '#':
                    case '%':
                    case '^':
                    case '&':
                    case '*':
                    case '(':
                    case ')':
                    case '-':
                    case '+':
                    case '=':
                    case ']':
                    case '[':
                    case '}':
                    case '{':
                    case '|':
                    case '"':
                    case ':':
                    case ';':
                    case '/':
                    case '?':
                    case ',':
                    case '.':
                    case '<':
                    case '>':
                        return false;
                }
            }
            return true;
        }

        static public bool IsNegativeNumber(string s)
        {
            if (s.StartsWith("-"))
                return true;
            else
                return false;
        }

        static public bool IsPositiveOrNegativeNumber(string s)
        {
            foreach (char c in s)
            {
                switch (c)
                {
                    case '-':
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        break;
                    default:
                        return false;
                }
            }
            return true;
        }

        static public bool IsNumber(string s)
        {
            if (string.IsNullOrEmpty(s)) return false;

            foreach (char c in s)
            {
                switch (c)
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    case '.':
                        break;
                    default:
                        return false;
                }
            }
            return true;
        }

        static public bool IsNumberOnly(string s)
        {
            if (string.IsNullOrEmpty(s)) return false;

            foreach (char c in s)
            {
                switch (c)
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        break;
                    default:
                        return false;
                }
            }
            return true;
        }

        static public bool IsAlpha(string s)
        {
            foreach (char c in s)
            {
                switch (c)
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        return false;
                }
            }
            return true;
        }

        static public string FixInvalidSpecialCharForSqlSearch(string s)
        {
            string valisString = "";
            int i = 0;
            foreach (char c in s)
            {
                bool isInvalidChar = false;

                switch (c)
                {
                    case '\'':

                        isInvalidChar = true;
                        break;
                }

                if (isInvalidChar)
                {
                    valisString += "'";
                }

                valisString += c;
                i++;
            }
            return valisString;
        }

        static public bool ContainsSpecialCharForRegProducts(string s)
        {
            foreach (char c in s)
            {
                switch (c)
                {
                    case '!':
                    case '@':
                    case '#':
                    case '$':
                    case '%':
                    case '^':
                    //case '&':
                    case '*':
                    case '(':
                    case ')':
                    case '+':
                    //case '.':
                    case ',':
                    case '_':
                    case '`':
                    case '~':
                    case '=':
                    case ']':
                    case '[':
                    case '}':
                    case '{':
                    case '|':
                    case '"':
                    case ':':
                    case ';':
                    case '/':
                    case '?':
                    case '<':
                    case '>':
                        return true;
                }
            }
            return false;
        }

        static public bool ContainsSpecialChar(string s)
        {
            foreach (char c in s)
            {
                switch (c)
                {
                    case '!':
                    case '@':
                    case '#':
                    case '$':
                    case '%':
                    case '^':
                    case '&':
                    case '*':
                    case '(':
                    case ')':
                    case '+':
                    case '.':
                    case ',':
                    case '_':
                    case '`':
                    case '~':
                    case '=':
                    case ']':
                    case '[':
                    case '}':
                    case '{':
                    case '|':
                    case '"':
                    case ':':
                    case ';':
                    case '/':
                    case '?':
                    case '<':
                    case '>':
                        return true;
                }
            }
            return false;
        }
        static public bool ContainsCharFromOneToZero(string s)
        {
            foreach (char c in s)
            {
                switch (c)
                {
                    case '!':
                    case '@':
                    case '#':
                    case '$':
                    case '%':
                    case '^':
                    case '&':
                    case '*':
                    case '(':
                    case ')':
                    case '+':
                    case '.':
                    case ',':
                    case '_':
                        return false;
                }
            }
            return true;
        }

        static public bool IsInteger(string s)
        {
            int d;
            bool ok = Int32.TryParse(s, out d);
            return ok;
        }

        static public int ConvertToInt32(decimal number)
        {
            int newNumber;
            if (number <= Int32.MaxValue)
                newNumber = Convert.ToInt32(number);
            else
                newNumber = 2147483647;

            return newNumber;
        }

        static public bool IsDateTime(string s)
        {
            DateTime d;
            bool ok = DateTime.TryParse(s, out d);
            return ok;
        }

        static public string ValidateMmsFile(string file)
        {
            if (!string.IsNullOrEmpty(file))
            {
                string s = file.ToLower();
                if (s.Length < 8)
                {
                    throw new Exception("Err602:Error in Mms file");
                }
                else
                {
                    if (s.Substring(0, 7) != "http://")
                        throw new Exception("Err603:Error in mms file");
                    else
                        return Validations.filter_wapurl(s);
                }
            }
            else
                return file;
        }

        static public string fix_sms(string sms)
        {
            sms = sms.Replace("α", "A");
            sms = sms.Replace("ά", "A");
            sms = sms.Replace("β", "B");
            sms = sms.Replace("γ", "Γ");
            sms = sms.Replace("δ", "Δ");
            sms = sms.Replace("ε", "E");
            sms = sms.Replace("έ", "E");
            sms = sms.Replace("ζ", "Z");
            sms = sms.Replace("η", "H");
            sms = sms.Replace("ή", "H");
            sms = sms.Replace("θ", "Θ");
            sms = sms.Replace("ι", "I");
            sms = sms.Replace("ί", "I");
            sms = sms.Replace("ϊ", "I");
            sms = sms.Replace("ΐ", "I");
            sms = sms.Replace("κ", "K");
            sms = sms.Replace("λ", "Λ");
            sms = sms.Replace("μ", "M");
            sms = sms.Replace("ν", "N");
            sms = sms.Replace("ξ", "Ξ");
            sms = sms.Replace("ο", "O");
            sms = sms.Replace("ό", "O");
            sms = sms.Replace("π", "Π");
            sms = sms.Replace("ρ", "P");
            sms = sms.Replace("σ", "Σ");
            sms = sms.Replace("ς", "Σ");
            sms = sms.Replace("τ", "T");
            sms = sms.Replace("υ", "Y");
            sms = sms.Replace("ύ", "Y");
            sms = sms.Replace("ϋ", "Y");
            sms = sms.Replace("ΰ", "Y");
            sms = sms.Replace("φ", "Φ");
            sms = sms.Replace("χ", "X");
            sms = sms.Replace("ψ", "Ψ");
            sms = sms.Replace("ω", "Ω");
            sms = sms.Replace("ώ", "Ω");
            sms = sms.Replace("΄", " ");
            sms = sms.Replace("¨", " ");
            sms = sms.Replace("΅", " ");
            sms = sms.Replace("Ά", "A");
            sms = sms.Replace("Έ", "E");
            sms = sms.Replace("Ή", "H");
            sms = sms.Replace("Ί", "I");
            sms = sms.Replace("Ϊ", "I");
            sms = sms.Replace("Ό", "O");
            sms = sms.Replace("Ύ", "Y");
            sms = sms.Replace("Ϋ", "Y");
            sms = sms.Replace("Ώ", "Ω");
            return sms;


        }

        static public string support_7bit(string sms)
        {
            string value = "";
            for (int i = 0; i < sms.Length; i++)
            {
                string c = sms.Substring(i, 1);
                switch (c)
                {
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    case "8":
                    case "9":
                    case "0":
                    case "a":
                    case "b":
                    case "c":
                    case "d":
                    case "e":
                    case "f":
                    case "g":
                    case "h":
                    case "i":
                    case "j":
                    case "k":
                    case "l":
                    case "m":
                    case "n":
                    case "o":
                    case "p":
                    case "q":
                    case "r":
                    case "s":
                    case "t":
                    case "u":
                    case "v":
                    case "w":
                    case "x":
                    case "y":
                    case "z":
                    case "A":
                    case "B":
                    case "C":
                    case "D":
                    case "E":
                    case "F":
                    case "G":
                    case "H":
                    case "I":
                    case "J":
                    case "K":
                    case "L":
                    case "M":
                    case "N":
                    case "O":
                    case "P":
                    case "Q":
                    case "R":
                    case "S":
                    case "T":
                    case "U":
                    case "V":
                    case "W":
                    case "X":
                    case "Y":
                    case "Z":
                    case "Γ":
                    case "Δ":
                    case "Θ":
                    case "Λ":
                    case "Ξ":
                    case "Π":
                    case "Σ":
                    case "Φ":
                    case "Ψ":
                    case "Ω":
                    case " ":
                    case "@":
                    case "£":
                    case "_":
                    case "!":
                    case "$":
                    case "":
                    case "¥":
                    case "#":
                    case "?":
                    case "%":
                    case "&":
                    case "'":
                    case "(":
                    case ")":
                    case "*":
                    case ":":
                    case "+":
                    case ";":
                    case ",":
                    case "<":
                    case "-":
                    case "=":
                    case ".":
                    case ">":
                    case "/":
                    case "¤":
                    case "§":
                    case "¡":
                    case "¿":
                    case "è":
                    case "ù":
                    case "ì":
                    case "ò":
                    case "Ç":
                    case "Ø":
                    case "Ä":
                    case "ä":
                    case "ø":
                    case "Æ":
                    case "Ö":
                    case "ö":
                    case "æ":
                    case "Ñ":
                    case "ñ":
                    case "Å":
                    case "ß":
                    case "Ü":
                    case "ü":
                    case "å":
                    case "É":
                    case "à":
                    case "€":
                    case "\\":
                    case "[":
                    case "]":
                    case "{":
                    case "}":
                    case "~":
                    case "^":
                    case "|":
                    case "\"":
                    case "\n":
                    case "\r":
                        value += c;
                        break;
                    default:
                        break;
                }
            }
            return value;
        }

        static public Queue<string> get7bitSmsParts(string sms)
        {
            sms = Validations.ClearString_ConvertCommonCapitalLettersToEnglish(sms);
            sms = Validations.fix_sms(sms);
            sms = Validations.support_7bit(sms);

            Queue<string> smsParts = new Queue<string>();

            int sms_len = 0;
            int char_len = 0;
            StringBuilder builder = new StringBuilder();

            for (int p = 0; p < sms.Length; p++)
            {
                string c = sms.Substring(p, 1);
                switch (c)
                {
                    case "€":
                    case "\\":
                    case "[":
                    case "]":
                    case "{":
                    case "}":
                    case "~":
                    case "^":
                    case "|":
                        char_len = 2;
                        break;
                    default:
                        char_len = 1;
                        break;
                }

                if (sms_len + char_len > 153)
                {
                    smsParts.Enqueue(builder.ToString());
                    builder = new StringBuilder();
                    sms_len = 0;
                }

                builder.Append(c);
                sms_len += char_len;
            }

            if (sms_len > 0)
            {
                smsParts.Enqueue(builder.ToString());
            }

            return smsParts;
        }

        static public bool IsUnicodeSms(string sms)
        {
            sms = ClearString_ConvertCommonCapitalLettersToEnglish(sms);

            for (int i = 0; i < sms.Length; i++)
            {
                string c = sms.Substring(i, 1);
                switch (c)
                {
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    case "8":
                    case "9":
                    case "0":
                    case "a":
                    case "b":
                    case "c":
                    case "d":
                    case "e":
                    case "f":
                    case "g":
                    case "h":
                    case "i":
                    case "j":
                    case "k":
                    case "l":
                    case "m":
                    case "n":
                    case "o":
                    case "p":
                    case "q":
                    case "r":
                    case "s":
                    case "t":
                    case "u":
                    case "v":
                    case "w":
                    case "x":
                    case "y":
                    case "z":
                    case "A":
                    case "B":
                    case "C":
                    case "D":
                    case "E":
                    case "F":
                    case "G":
                    case "H":
                    case "I":
                    case "J":
                    case "K":
                    case "L":
                    case "M":
                    case "N":
                    case "O":
                    case "P":
                    case "Q":
                    case "R":
                    case "S":
                    case "T":
                    case "U":
                    case "V":
                    case "W":
                    case "X":
                    case "Y":
                    case "Z":
                    case "Γ":
                    case "Δ":
                    case "Θ":
                    case "Λ":
                    case "Ξ":
                    case "Π":
                    case "Σ":
                    case "Φ":
                    case "Ψ":
                    case "Ω":
                    case " ":
                    case "@":
                    case "£":
                    case "_":
                    case "!":
                    case "$":
                    case "":
                    case "¥":
                    case "#":
                    case "?":
                    case "%":
                    case "&":
                    case "'":
                    case "(":
                    case ")":
                    case "*":
                    case ":":
                    case "+":
                    case ";":
                    case ",":
                    case "<":
                    case "-":
                    case "=":
                    case ".":
                    case ">":
                    case "/":
                    case "¤":
                    case "§":
                    case "¡":
                    case "¿":
                    case "è":
                    case "ù":
                    case "ì":
                    case "ò":
                    case "Ç":
                    case "Ø":
                    case "Ä":
                    case "ä":
                    case "ø":
                    case "Æ":
                    case "Ö":
                    case "ö":
                    case "æ":
                    case "Ñ":
                    case "ñ":
                    case "Å":
                    case "ß":
                    case "Ü":
                    case "ü":
                    case "å":
                    case "É":
                    case "à":
                    case "€":
                    case "\\":
                    case "[":
                    case "]":
                    case "{":
                    case "}":
                    case "~":
                    case "^":
                    case "|":
                    case "\"":
                    case "\n":
                    case "\r":
                        break;
                    default:
                        return true;
                }
            }
            return false;
        }

        static public Queue<string> getUnicodeSmsParts(string sms)
        {
            if (Validations.IsUnicodeSms(sms))
            {
                Queue<string> smsParts = new Queue<string>();
                while (sms.Length > 67)
                {
                    smsParts.Enqueue(sms.Substring(0, 67));
                    sms = sms.Substring(67);
                }

                if (!string.IsNullOrEmpty(sms))
                {
                    smsParts.Enqueue(sms);
                }

                return smsParts;
            }
            else
                return get7bitSmsParts(sms);
        }

        static public DateTime? DateFromDateString(string s)
        {
            if (string.IsNullOrEmpty((string)s))
                return null;
            try
            {
                int year = Convert.ToInt32(s.Substring(0, 4));
                int month = Convert.ToInt32(s.Substring(4, 2));
                int day = Convert.ToInt32(s.Substring(6, 2));
                return new DateTime(year, month, day);
            }
            catch { return null; }
        }

        static public DateTime? DateFromDateAndTimeString(string s, bool withSeconds)
        {
            if (string.IsNullOrEmpty((string)s))
                return null;
            try
            {
                int year = Convert.ToInt32(s.Substring(0, 4));
                int month = Convert.ToInt32(s.Substring(4, 2));
                int day = Convert.ToInt32(s.Substring(6, 2));
                int hour = Convert.ToInt32(s.Substring(8, 2));
                int minute = Convert.ToInt32(s.Substring(10, 2));

                int second = 0;

                if (withSeconds)
                    second = Convert.ToInt32(s.Substring(12, 2));

                return new DateTime(year, month, day, hour, minute, second);
            }
            catch { return null; }
        }

        static public DateTime? DateFromDateIn(int? dt)
        {
            if (!dt.HasValue) return null;
            try
            {
                string s = dt.ToString();
                int year = Convert.ToInt32(s.Substring(0, 4));
                int month = Convert.ToInt32(s.Substring(4, 2));
                int day = Convert.ToInt32(s.Substring(6, 2));
                return new DateTime(year, month, day);
            }
            catch { return null; }
        }

        static public DateTime DateTimeFromDateAndIntTime(DateTime dt, int time)
        {
            string stime = time.ToString().PadLeft(4, '0');
            string sdate = dt.ToString("yyyy-MM-dd " + stime.Substring(0, 2) + ":" + stime.Substring(2));
            return Convert.ToDateTime(sdate);
        }

        static public DateTime? DateTimeFromTimeIn(int? time)
        {
            if (!time.HasValue) return null;
            try
            {
                string stime = time.ToString().PadLeft(6, '0');

                return new DateTime(0, 0, 0, Int32.Parse(stime.Substring(0, 2)), Int32.Parse(stime.Substring(2, 2)), Int32.Parse(stime.Substring(4, 2)));
            }
            catch { return null; }
        }

        static public TimeSpan? TimeSpanFromTimeIn(int? time)
        {
            if (!time.HasValue) return null;
            try
            {
                string stime = time.ToString().PadLeft(6, '0');
                return new TimeSpan(Int32.Parse(stime.Substring(0, 2)), Int32.Parse(stime.Substring(2, 2)), Int32.Parse(stime.Substring(4, 2)));
            }
            catch { return null; }
        }

        static public TimeSpan? TimeSpanFromTimeInPad4(int? time)
        {
            if (!time.HasValue) return null;
            try
            {
                string stime = time.ToString().PadLeft(4, '0');
                return new TimeSpan(Int32.Parse(stime.Substring(0, 2)), Int32.Parse(stime.Substring(2, 2)), 0);
            }
            catch { return null; }
        }

        static public TimeSpan TimeSpanFromTimeString(string time, bool withSeconds)
        {

            try
            {
                //string stime = time.ToString().PadLeft(6, '0');
                if (withSeconds)
                {
                    return new TimeSpan(Int32.Parse(time.Substring(0, 2)), Int32.Parse(time.Substring(2, 2)), Int32.Parse(time.Substring(4, 2)));
                }
                else
                {
                    return new TimeSpan(Int32.Parse(time.Substring(0, 2)), Int32.Parse(time.Substring(2, 2)), 0);
                }
            }
            catch { return new TimeSpan(0, 0, 0); }
        }

        // Accepts a nullable date and returns null int in case of null date 
        // or the converted date
        static public int? DateIn(DateTime? dt)
        {
            if (dt.HasValue)
            {
                string s = dt.Value.Year.ToString() + dt.Value.Month.ToString().PadLeft(2, '0') + dt.Value.Day.ToString().PadLeft(2, '0');
                return Convert.ToInt32(s);
            }
            else
                return null;
        }

        static public int? TimeIn(TimeSpan? time)
        {
            if (time.HasValue)
            {
                string s = time.Value.Hours.ToString().PadLeft(2, '0') + time.Value.Minutes.ToString().PadLeft(2, '0') + time.Value.Seconds.ToString().PadLeft(2, '0');
                return Convert.ToInt32(s);
            }
            else
                return null;
        }

        static public int? TimeIn(DateTime? dt)
        {
            if (dt.HasValue)
            {
                string s = dt.Value.Hour.ToString().PadLeft(2, '0') + dt.Value.Minute.ToString().PadLeft(2, '0');
                return Convert.ToInt32(s);
            }
            else
                return null;
        }

        static public string DateToString(DateTime? dt)
        {
            if (dt.HasValue)
            {
                string s = dt.Value.Year.ToString() + dt.Value.Month.ToString().PadLeft(2, '0') + dt.Value.Day.ToString().PadLeft(2, '0');
                return s;
            }
            else
                return "";
        }

        static public string TimeToString(TimeSpan? time, bool withSeconds)
        {
            if (time.HasValue)
            {
                string s = time.Value.Hours.ToString().PadLeft(2, '0') + time.Value.Minutes.ToString().PadLeft(2, '0') + (withSeconds ? time.Value.Seconds.ToString().PadLeft(2, '0') : "");
                return s;
                //return Convert.ToInt32(s);
            }
            else
                return "0000" + (withSeconds ? "00" : "");
        }

        static public string TimeToString(DateTime? dt, bool withSeconds)
        {
            if (dt.HasValue)
            {
                string s = dt.Value.Hour.ToString().PadLeft(2, '0') + dt.Value.Minute.ToString().PadLeft(2, '0') + (withSeconds ? dt.Value.Second.ToString().PadLeft(2, '0') : "");
                return s;
            }
            else
            {
                return "";
            }
            //return Convert.ToInt32(s);
        }

        // accepts a nullable date and returns the provided default int if the date is null
        // or the converted int
        static public int DateIn(DateTime? dt, int defValue)
        {
            int? value = Validations.DateIn(dt);
            if (value.HasValue)
                return value.Value;
            else
                return defValue;
        }

        static public int TimeIn(DateTime? dt, int defValue)
        {
            int? value = Validations.TimeIn(dt);
            if (value.HasValue)
                return value.Value;
            else
                return defValue;
        }

        static public string LimitString(string s, int length)
        {
            if (string.IsNullOrEmpty(s))
                return "";
            if (s.Length > length)
                return s.Substring(0, length);
            return s;
        }

        static public bool StringIsAllDigits(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (!char.IsDigit(s[i]))
                    return false;
            }
            return true;
        }

        static public DateTime? GetDateTimeFromRequestString(string sdate, string stime)
        {
            return GetDateTimeFromRequestString(sdate, stime, "EN");
        }

        static public DateTime? GetDateTimeFromRequestString(string sdate, string stime, string lang)
        {
            DateTime? dt = null;
            string syear = "";
            string smonth = "";
            string sday = "";
            string shour = "00";
            string smin = "00";

            if (!string.IsNullOrEmpty(sdate))
            {
                if (sdate.Length == 8 && Utils.Validations.IsNumeric(sdate))
                {
                    syear = sdate.Substring(0, 4);
                    smonth = sdate.Substring(4, 2);
                    sday = sdate.Substring(6, 2);
                }
                else
                {
                    throw new Exception(lang == "EN" ? "Err604:Invalid date (DDMMYYYY)" : "Err604:Μη έγκυρη ημερομηνία (DDMMYYYY).");
                }

                if (string.IsNullOrEmpty(stime))
                {
                    stime = "0000";
                }
                if (Utils.Validations.IsNumeric(stime) && stime.Length == 3)
                {
                    stime = "0" + stime;
                }
                if (Utils.Validations.IsNumeric(stime) && stime.Length == 4)
                {
                    shour = stime.Substring(0, 2);
                    smin = stime.Substring(2, 2);
                }
                else
                {
                    throw new Exception(lang == "EN" ? "Err605:Invalid time (HHMM)." : "Err605:Μη έγκυρη ώρα (HHMM).");
                }

                if (Utils.Validations.IsNumeric(syear) && Utils.Validations.IsNumeric(smonth) && Utils.Validations.IsNumeric(sday) &&
                    Utils.Validations.IsNumeric(shour) && Utils.Validations.IsNumeric(smin))
                {
                    try
                    {
                        int year = Convert.ToInt32(syear);
                        int month = Convert.ToInt32(smonth);
                        int day = Convert.ToInt32(sday);
                        int hour = Convert.ToInt32(shour);
                        int min = Convert.ToInt32(smin);
                        int sec = 0;

                        dt = new DateTime(year, month, day, hour, min, sec);
                    }
                    catch (Exception)
                    {
                        dt = null;
                    }
                }
                else
                {
                    throw new Exception(lang == "EN" ? "Err606:Invalid date or time (DDMMYYYY HHMM)." : "Err606:Μη έγκυρη ημερομηνία ή ώρα (DDMMYYYY HHMM).");
                }
            }

            return dt;
        }

        /// <summary>
        /// Removes the + sign, removes double zeros from the start 
        /// and adds 30 if the number begins with 69 and has 10 digits
        /// </summary>
        /// <param name="phone"></param>
        static public string FixPhoneNumber(string phone)
        {
            if (string.IsNullOrEmpty(phone))
                return "";

            string newphone = "";
            for (int i = 0; i < phone.Length; i++)
            {
                if (char.IsDigit(phone[i]))
                    newphone += phone[i];
            }
            if (newphone.StartsWith("00"))
                newphone = newphone.Substring(2);
            if (newphone.StartsWith("69") && newphone.Length == 10)
                newphone = "30" + newphone;
            if (newphone.StartsWith("2") && newphone.Length == 10)
                newphone = "30" + newphone;
            //if (newphone.StartsWith("9") && newphone.Length == 8)
            //	newphone = "357" + newphone;
            return newphone;
        }

        static public string FixMobilePhoneNumber(string phone)
        {
            if (string.IsNullOrEmpty(phone))
                return "";

            string newphone = "";
            for (int i = 0; i < phone.Length; i++)
            {
                if (char.IsDigit(phone[i]))
                    newphone += phone[i];
            }
            if (newphone.StartsWith("00"))
                newphone = newphone.Substring(2);
            if (newphone.StartsWith("69") && newphone.Length == 10)
                newphone = "30" + newphone;
            if ((newphone.StartsWith("99") || newphone.StartsWith("97") || newphone.StartsWith("96") || newphone.StartsWith("95")) && newphone.Length == 8)
                newphone = "357" + newphone;
            return newphone;
        }

        static public string hexencode2(string text)
        {
            if (string.IsNullOrEmpty(text))
                return "";
            string hex = "";
            foreach (char c in text)
            {
                int tmp = c;
                hex += "%" + String.Format("{0:x2}", (uint)System.Convert.ToUInt32(tmp.ToString()));
            }
            return hex;
        }

        public static bool IsEmail1(string email)
        {
            const string MatchEmailPattern =
                           @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                            + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				                        [0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                            + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				                        [0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
                            + @"([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z]{1}[a-zA-Z0-9-]{1,23})$";

            if (!string.IsNullOrEmpty(email))
                return Regex.IsMatch(email, MatchEmailPattern);
            else
                return false;
        }

        static public bool IsEmail(string emailAddress)
        {
            try
            {
                MailAddress email = new MailAddress(emailAddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        static public bool IsValidEmail(string inputEmail)
        {
            if (inputEmail == null || inputEmail.Length == 0)
                return (false);

            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
            {
                string[] args = inputEmail.Split('.').ToArray();
                switch (args[args.Length - 1])
                {
                    case "email":

                        return true;
                }

                return (false);
            }
        }

        public static bool isScheduleDateValid(string date)//in format year month day (e.g. 20110312)
        {
            if (!Validations.IsInteger(date) || date.Length != 8)
                return false;

            string year = date.Substring(0, 4);
            string month = date.Substring(4, 2);
            string day = date.Substring(6, 2);


            if (!Validations.IsInteger(year) || !Validations.IsInteger(month) || !Validations.IsInteger(day))
                return false;

            int yearInt = Convert.ToInt32(year);
            int monthInt = Convert.ToInt32(month);
            int dayInt = Convert.ToInt32(day);

            if (yearInt < 2011 || monthInt < 0 || monthInt > 12 || dayInt < 0 || dayInt > 31)
                return false;

            DateTime dateToSend = new DateTime(yearInt, monthInt, dayInt);
            if (dateToSend < DateTime.Now)
                return false;

            return true;
        }

        public static bool isScheduleTimeValid(string time)// in format hour minutes (e.g. 1943)
        {
            if (!Validations.IsInteger(time) || time.Length != 4)
                return false;

            int hour = Convert.ToInt32(time.Substring(0, 2));
            int minutes = Convert.ToInt32(time.Substring(2, 2));

            if (hour < 0 || hour > 24)
                return false;

            if (minutes < 0 || minutes > 60)
                return false;

            if (hour == 24 && minutes != 0)
                return false;

            return true;
        }

        public static bool isScheduleDateValid(string date, string time)//in format year month day (e.g. 20110312)
        {
            if (!Validations.IsInteger(date) || date.Length != 8)
                return false;

            string year = date.Substring(0, 4);
            string month = date.Substring(4, 2);
            string day = date.Substring(6, 2);


            if (!Validations.IsInteger(year) || !Validations.IsInteger(month) || !Validations.IsInteger(day))
                return false;

            int yearInt = Convert.ToInt32(year);
            int monthInt = Convert.ToInt32(month);
            int dayInt = Convert.ToInt32(day);

            if (yearInt < 2011 || monthInt < 0 || monthInt > 12 || dayInt < 0 || dayInt > 31)
                return false;

            if (!Validations.IsInteger(time) || time.Length != 4)
                return false;

            int hour = Convert.ToInt32(time.Substring(0, 2));
            int minutes = Convert.ToInt32(time.Substring(2, 2));

            if (hour < 0 || hour > 24)
                return false;

            if (minutes < 0 || minutes > 60)
                return false;

            if (hour == 24 && minutes != 0)
                return false;

            DateTime dateToSend = new DateTime(yearInt, monthInt, dayInt, hour, minutes, 0);
            if (dateToSend < DateTime.Now)
                return false;

            return true;
        }

        public static bool isWorkDay(DateTime targetDate)
        {
            return targetDate.DayOfWeek != DayOfWeek.Saturday && targetDate.DayOfWeek != DayOfWeek.Sunday;
        }

        public static DateTime GetDateTimeFromDateInTimeIn(string dateIn, string timeIn)
        {
            int t;
            int? sendTime = null;
            if (Int32.TryParse(timeIn, out t))
                sendTime = t;
            string year = dateIn.Substring(0, 4);
            string month = dateIn.Substring(4, 2);
            string day = dateIn.Substring(6, 2);
            TimeSpan? ts = Validations.TimeSpanFromTimeInPad4(sendTime);
            DateTime dtToSend = new DateTime(Convert.ToInt32(year),
                                            Convert.ToInt32(month),
                                            Convert.ToInt32(day),
                                            ts.Value.Hours,
                                            ts.Value.Minutes,
                                            0);

            return dtToSend;
        }

        public static string ReturnValidIdsWithCommaDelimiterForSearch(string inputSearch)
        {
            string userIds = string.Empty;
            List<string> userIdsList = inputSearch.Trim().Split(',').ToList();
            foreach (string id in userIdsList)
            {
                if (Validations.IsNumber(id))
                {
                    userIds += id + ",";
                }
            }

            return (userIds.EndsWith(",")) ? userIds.Substring(0, userIds.Length - 1) : userIds;
        }
    }
}