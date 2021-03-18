using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TILab2
{
    class Keys
    {
        
        private ulong q;

        private ulong p;

        public ulong Module;   
        
        public ulong SecretE { get; }

        public ulong PublicE { get; }

        public ulong[] PrivateKey { get; }

        public ulong[] PublicKey { get; }

        public Keys()
        {
            PrivateKey = new ulong[2];
            PublicKey = new ulong[2];
        }

        public void GenerateKeys()
        {
            bool IsSimple = false;
            Random random = new Random();

            while (!IsSimple)
            {
                p = (ulong)random.Next(100000, 1000000);
                if (CheckNumber(p))
                {
                    IsSimple = true;
                    for (ulong i = 2; i < Math.Sqrt(p) + 1; i++)
                    {
                        if (p % i == 0)
                        {
                            IsSimple = false;
                            break;
                        }
                    }
                }
            }

            IsSimple = false;
            while (!IsSimple)
            {
                q = (ulong)random.Next(100000, 1000000);
                if (CheckNumber(q))
                {
                    IsSimple = true;
                    for (ulong i = 2; i < Math.Sqrt(q) + 1; i++)
                    {
                        if (q % i == 0)
                        {
                            IsSimple = false;
                            break;
                        }
                    }
                }
            }

            Module = p * q;

        }

        private bool CheckNumber(ulong Num)
        {
            for (ulong i = 2; i < Num && i < 11; i++)
            {
                if (!MillerRabinTest(Num, i))
                {
                    return false;
                }
            }
            return true;
        }

        private bool MillerRabinTest(ulong Num, ulong a)
        {

            if (Num % 2 == 0)
                return false;
            ulong s = 0, d = Num - 1;
            while (d % 2 == 0)
            {
                d /= 2;
                s++;
            }

            ulong r = 1;
            ulong x = (ulong)BigInteger.ModPow(a, d, Num);
            if (x == 1 || x == Num - 1)
                return true;

            x = (ulong)BigInteger.ModPow((ulong)Math.Pow(a, d), (ulong)Math.Pow(2, r), Num);

            if (x == 1)
                return false;

            if (x != Num - 1)
                return false;

            return true;
        }

    }
}
