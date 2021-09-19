using System;
using System.Collections.Generic;

namespace SatsLogikParser
{
    class Program
    {
        static void Main(string[] args)
        {
            double loop;
            string variabel;
            bool negation;
            bool tempbool1 = false;
            bool tempbool2;
            bool resultbool = false;
            bool novar;
            int currentswitch = 1;
            int number = 0;     //int för att skriva ut om en variabel är sann eller falsk (1/0)
            int resultat;       //int som skriver ut om satsen är sann eller inte
            char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            List<Variable> Variables = new List<Variable>();
            List<int> operations = new List<int>();
            List<bool> varbools = new List<bool>();
            List<bool> results = new List<bool>();
            int currentvar = 0;
            Console.WriteLine("Välkommen till satslogikparser 1.0!");

            while (true)
            {
                Console.WriteLine("Skriv in sats:");
                string input = Console.ReadLine();
                input.ToCharArray();
                for (int i = 0; i < input.Length; i++)
                {
                    novar = false;
                    for (int x = 0; x < currentvar + 1; x++)    //loopar igen för alla bokstäver man hittat +1 ifall det är en ny variabel
                    {

                        if (input[i] == alpha[x])
                        {
                            if (i > 0)  //om det inte är första char i stringen
                            {
                                if (input[i - 1] == '!')    //checka ifall det är en negation
                                { negation = true; }
                                else
                                { negation = false; }  //annars är det inte negation
                            }
                            else    //om det är första gör automatiskt till inte negation
                            {
                                negation = false;
                            }
                            Variables.Add(new Variable(alpha[x], i, negation));     //variabelbokstav, placering, negation eller inte
                            varbools.Add(false);
                            if (x == currentvar)    //om man hittat en variabel (A) och man hittar en ny variablel som matchar den andra bokstaven i alfabetet, dvs alpha[1](B) så betyder det att det är en ny variabel
                            { currentvar++; }
                            novar = true;
                        }
                    }
                    if (!novar)
                    {
                        if (input[i] == 'v' || input[i] == '^' || input[i] == '>' || input[i] == '=')
                        { operations.Add(i); }
                    }
                }
                loop = Math.Pow(2, currentvar);
                for (int i = 0; i < loop; i++)
                {
                    currentswitch = 1;
                    Console.Write("| ");
                    for (int x = 0; x < currentvar; x++)
                    {
                        if (i % currentswitch == 0 && i != 0)   //om nuvarande variabel ska byta från 1 till 0 och vice versa (bool 1 byter varannan, bool 2 byter efter 2, bool 3 byter efter 4 etc)
                        {
                            varbools[x] = !varbools[x];
                            foreach (var z in Variables)
                            { z.changebool(x, alpha); }
                            Console.WriteLine("Changing var " + alpha[x]);
                        }
                        if (varbools[x] == true)
                        { number = 1; }
                        else
                        { number = 0; }
                        Console.Write(number + " | ");
                        if (i != 0)
                        { currentswitch *= 2; }
                    }
                    resultat = 1;
                    int tempint1 = -1;
                    foreach (int x in operations)
                    {
                        int tempint2 = 0;
                        int currentz = 0;
                        resultat = 0;
                        foreach (var z in Variables)
                        {
                            if (z.Placement == (x - 1) && tempint1 == -1)
                            { tempint1 = currentz; }
                            else if (z.Placement == (x + 1))
                            { tempint2 = currentz; }
                            currentz++;
                        }
                        if (tempint1 == -1)
                        { tempbool1 = Variables[tempint1].Onoff; }
                        tempbool2 = Variables[tempint2].Onoff;
                        //Console.WriteLine(tempbool1 + "\n" + tempbool2);
                        if (input[x] == 'v')
                        { resultbool = DisjunktionSats(tempbool1, tempbool2); }
                        else if (input[x] == '^')
                        { resultbool = KonjunktionSats(tempbool1, tempbool2); }
                        else if (input[x] == '>')
                        { resultbool = ImplikationSats(tempbool1, tempbool2); }
                        else
                        { resultbool = EkvivalensSats(tempbool1, tempbool2); }
                        //tempbool1 = resultbool; //testkod för att ta resultatet från tidigare ekvation och sätta in det som tempbool1 inför kommande ekvation, verkar inte fungera just nu
                    }
                    if (resultbool)
                    { resultat = 1; }
                    else
                    { resultat = 0; }
                    Console.WriteLine(resultat);

                }
                //Console.WriteLine("Variables found: " + currentvar);
                foreach (var i in Variables)
                {
                    //i.Info();
                }
                Console.ReadLine();
                Variables.Clear();
                currentvar = 0;
                operations.Clear();
                varbools.Clear();
                results.Clear();
            }
        }

        public static bool DisjunktionSats(bool bool1, bool bool2)  //or/v
        {
            Console.WriteLine(bool1 + "    " + bool2);
            if (bool1 || bool2)
            { return true; }
            return false;
        }
        public static bool KonjunktionSats(bool bool1, bool bool2)  //and/^
        {
            if (bool1 && bool2)
            { return true; }
            return false;
        }
        public static bool ImplikationSats(bool bool1, bool bool2)  //>
        {
            if ((bool1 && bool2) || (!bool1 && !bool2) || bool2)
            { return true; }
            return false;
        }
        public static bool EkvivalensSats(bool bool1, bool bool2)   //=
        {
            if (bool1 == bool2)
            { return true; }
            return false;
        }
    }
}
