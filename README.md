# L0002b-Inl3

Linus Östlund
L0002b, Inlämning 3
Sommarterminen (juni-augusti) 2021

I repot ligger en `Windows Forms Application` som går att köra enligt uppgiftlydelsen. 

## Om inlämningen

Själva personnumret kontrolleras först av ett Regex för att försäkra sig om att året är inom ett rimligt spann.

```c#
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
```

Därefter beräknas talsumman av varje rad enligt uppgiftslydelsen (se metod `getNumberSum`.

```c#

        /**
         * Kollar om personnumret är korrekt enligt 21Algoritmen.
         * Tar in personnumret som ååmmdd-xxxx eller utan bindestreck '-'
         */
        private Boolean checkValidSSN(String input)
        {
            input = input.Replace("-", "");
            
            if(!checkSSNFormat(input)) return false; // kontrollera rimligt tidsintervall

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
```
