using System;

namespace EvolutionaryAlgorithm.Helpers
{
    public class UniformRandomGenerator : IUniformRandomGenerator
    {
        private const ulong N = 624;
        private const ulong M = 397;
        private const ulong MatrixA = 0x9908B0DFUL;		// constant vector a 
        private const ulong UpperMask = 0x80000000UL;		// most significant w-r bits
        private const ulong LowerMask = 0X7FFFFFFFUL;		// least significant r bits

        private static ulong[] mt = new ulong[N + 1];	// the array for the state vector
        private static ulong _mti = N + 1;			// mti==N+1 means mt[N] is not initialized

        public UniformRandomGenerator()
        {
            var init = new ulong[4];
            init[0] = (ulong)DateTime.UtcNow.Ticks;
            init[1] = (ulong)DateTime.UtcNow.Ticks;
            init[2] = (ulong)DateTime.UtcNow.Ticks;
            init[3] = (ulong)DateTime.UtcNow.Ticks;
            const ulong length = 4;
            init_by_array(init, length);
        }

        // initializes mt[N] with a seed
        private void init_genrand(ulong s)
        {
            mt[0] = s & 0xffffffffUL;
            for (_mti = 1; _mti < N; _mti++)
            {
                mt[_mti] = (1812433253UL * (mt[_mti - 1] ^ (mt[_mti - 1] >> 30)) + _mti);
                /* See Knuth TAOCP Vol2. 3rd Ed. P.106 for multiplier. */
                /* In the previous versions, MSBs of the seed affect   */
                /* only MSBs of the array mt[].                        */
                /* 2002/01/09 modified by Makoto Matsumoto             */
                mt[_mti] &= 0xffffffffUL;
                /* for >32 bit machines */
            }
        }


        // initialize by an array with array-length
        // init_key is the array for initializing keys
        // key_length is its length
        private void init_by_array(ulong[] initKey, ulong keyLength)
        {
            ulong i, j, k;
            init_genrand(19650218UL);
            i = 1; j = 0;
            k = (N > keyLength ? N : keyLength);
            for (; k > 0; k--)
            {
                mt[i] = (mt[i] ^ ((mt[i - 1] ^ (mt[i - 1] >> 30)) * 1664525UL))
                        + initKey[j] + j;		// non linear 
                mt[i] &= 0xffffffffUL;	// for WORDSIZE > 32 machines
                i++; j++;
                if (i >= N) { mt[0] = mt[N - 1]; i = 1; }
                if (j >= keyLength) j = 0;
            }
            for (k = N - 1; k > 0; k--)
            {
                mt[i] = (mt[i] ^ ((mt[i - 1] ^ (mt[i - 1] >> 30)) * 1566083941UL))
                        - i;					// non linear
                mt[i] &= 0xffffffffUL;	// for WORDSIZE > 32 machines
                i++;
                if (i >= N) { mt[0] = mt[N - 1]; i = 1; }
            }
            mt[0] = 0x80000000UL;		// MSB is 1; assuring non-zero initial array
        }

        // generates a random number on [0,0x7fffffff]-interval
        private long genrand_int31()
        {
            return (long)(genrand_int32() >> 1);
        }
        // generates a random number on [0,1]-real-interval
        private double genrand_real1()
        {
            return genrand_int32() * (1.0 / 4294967295.0); // divided by 2^32-1 
        }
        // generates a random number on [0,1)-real-interval
        private double genrand_real2()
        {
            return genrand_int32() * (1.0 / 4294967296.0); // divided by 2^32
        }
        // generates a random number on (0,1)-real-interval
        private double genrand_real3()
        {
            return ((genrand_int32()) + 0.5) * (1.0 / 4294967296.0); // divided by 2^32
        }
        // generates a random number on [0,1) with 53-bit resolution
        private double genrand_res53()
        {
            ulong a = genrand_int32() >> 5;
            ulong b = genrand_int32() >> 6;
            return (a * 67108864.0 + b) * (1.0 / 9007199254740992.0);
        }
        // These real versions are due to Isaku Wada, 2002/01/09 added 

        // generates a random number on [0,0xffffffff]-interval
        private ulong genrand_int32()
        {
            ulong y;
            ulong[] mag01 = new ulong[2];
            mag01[0] = 0x0UL;
            mag01[1] = MatrixA;
            /* mag01[x] = x * MATRIX_A  for x=0,1 */

            if (_mti >= N)
            {
                // generate N words at one time
                ulong kk;

                if (_mti == N + 1)   /* if init_genrand() has not been called, */
                    init_genrand(5489UL); /* a default initial seed is used */

                for (kk = 0; kk < N - M; kk++)
                {
                    y = (mt[kk] & UpperMask) | (mt[kk + 1] & LowerMask);
                    mt[kk] = mt[kk + M] ^ (y >> 1) ^ mag01[y & 0x1UL];
                }
                for (; kk < N - 1; kk++)
                {
                    y = (mt[kk] & UpperMask) | (mt[kk + 1] & LowerMask);
                    //mt[kk] = mt[kk+(M-N)] ^ (y >> 1) ^ mag01[y & 0x1UL];
                    mt[kk] = mt[kk - 227] ^ (y >> 1) ^ mag01[y & 0x1UL];
                }
                y = (mt[N - 1] & UpperMask) | (mt[0] & LowerMask);
                mt[N - 1] = mt[M - 1] ^ (y >> 1) ^ mag01[y & 0x1UL];

                _mti = 0;
            }

            y = mt[_mti++];

            /* Tempering */
            y ^= (y >> 11);
            y ^= (y << 7) & 0x9d2c5680UL;
            y ^= (y << 15) & 0xefc60000UL;
            y ^= (y >> 18);

            return y;
        }

        public int GetIntegerRandomNumber(int low, int high)
        {
            return (Math.Abs((int)genrand_int32() % (high - low + 1)) + low);
        }

        public double GetContinousRandomNumber(double low, double high)
        {
            return low + genrand_real1() * (high - low);
        }
    }
}
