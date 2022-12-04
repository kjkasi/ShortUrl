using System.Linq;
using System;

namespace ShortUrl.Api.Services
{
    //https://github.dev/zhaopeiym/ShortURL/blob/master/ShortURL/ShortURL/GenerateShortURL.cs
    public class AliasService : IAliasService
    {
        private readonly string _seqKey;
        private int CodeLength { get; } = 6;

        public AliasService(string seqKey)
        {
            _seqKey = seqKey;
        }

        private int MaxLength
        {
            get
            {
                return Convert("".PadLeft(CodeLength, _seqKey.Last())).ToString().Length;
            }
        }

        private static string GenerateKeys()
        {
            string[] Chars = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z".Split(',');
            int SeekSeek = unchecked((int)DateTime.Now.Ticks);
            Random SeekRand = new Random(SeekSeek);
            for (int i = 0; i < 100000; i++)
            {
                int r = SeekRand.Next(1, Chars.Length);
                string f = Chars[0];
                Chars[0] = Chars[r - 1];
                Chars[r - 1] = f;
            }
            return string.Join("", Chars);
        }

        private string Convert(long id)
        {
            if (id < 62)
            {
                return _seqKey[(int)id].ToString();
            }
            int y = (int)(id % 62);
            long x = id / 62;

            return Convert(x) + _seqKey[y];
        }

        private long Convert(string num)
        {
            long v = 0;
            int Len = num.Length;
            for (int i = Len - 1; i >= 0; i--)
            {
                int t = _seqKey.IndexOf(num[i]);
                double s = (Len - i) - 1;
                long m = (long)(Math.Pow(62, s) * t);
                v += m;
            }
            return v;
        }

        public string ConfusionConvert(long num)
        {
            if (num.ToString().Length > MaxLength)
                throw new Exception($"The conversion value cannot exceed the maximum number of digits{MaxLength}");
            var n = num.ToString()
                   .PadLeft(MaxLength, '0')
                   .ToCharArray()
                   .Reverse();
            return Convert(long.Parse(string.Join("", n))).PadLeft(CodeLength, _seqKey.First());
        }

        public long ConfusionConvert(string num)
        {
            if (num.Length > CodeLength + 1)
                throw new Exception($"The conversion value cannot exceed the maximum number of digits{CodeLength + 1}");
            var n = Convert(num).ToString().PadLeft(MaxLength, '0')
                .ToCharArray()
                .Reverse();
            return long.Parse(string.Join("", n));
        }
    }
}
