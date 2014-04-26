using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Util
{
    class Random
    {
        public static System.Security.Cryptography.RNGCryptoServiceProvider c = new System.Security.Cryptography.RNGCryptoServiceProvider();

        public static int GenerateGoodRandomNumber(int min, int max)
        {
            
            // Ein integer benötigt 4 Byte
            byte[] randomNumber = new byte[4];
            // dann füllen wir den Array mit zufälligen Bytes
            c.GetBytes(randomNumber);
            // schließlich wandeln wir den Byte-Array in einen Integer um
            int result = Math.Abs(BitConverter.ToInt32(randomNumber, 0));
            // da bis jetzt noch keine Begrenzung der Zahlen vorgenommen wurde,
            // wird diese Begrenzung mit einer einfachen Modulo-Rechnung hinzu-
            // gefügt
            return result % max + min;
        }
    }
}
