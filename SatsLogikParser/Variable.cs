using System;

namespace SatsLogikParser
{
    class Variable
    {
        char charvar;
        int placement;
        bool onoff;
        public Variable(char charvar, int placement, bool negation)
        {
            this.charvar = charvar;
            this.placement = placement;
            this.onoff = false;
        }
        public void Info()
        {
            Console.WriteLine("Plats i input: " + placement + "\nVariabel: " + charvar + "\nNegation: " + onoff);
        }
        public char Charvar
        {
            get { return charvar; }
        }
        public int Placement
        {
            get { return placement; }
        }
        public bool Onoff
        {
            get { return onoff; }
        }
        public void changebool(int x, char[] alpha)
        {
            if (charvar == alpha[x])
            { onoff = !onoff; Console.WriteLine("Changed the var " + alpha[x] + " with pos " + placement + " results: " + onoff); }
        }
    }
}
