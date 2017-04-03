using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace WindowsFormsApplication1
{
    public class Canal:CantoDec
    {
        public BitArray NoiseString;
        public Canal(BitArray CodedBitString, double noise)
        {
            
        }
        public BitArray getNoiseCodedStr(BitArray CodedBitString, double noise)
        {
            Console.WriteLine("Вот мы вошли в канал");
            NoiseString = CodedBitString;
            Random rnd = new Random();
            int errorsnum = 0;
            for (int i = 0; i < CodedBitString.Length; i++)
            {
                
                double r = Convert.ToDouble(rnd.Next(100) / 100.0);
                if (r < noise)
                {
                    errorsnum++;
                    if (CodedBitString[i] == false)
                        NoiseString.Set(i, true);
                    else
                        NoiseString.Set(i, false);
                    
                }
            }
            Console.WriteLine("Количество ошибок, возникших в канале: {0,1:g}", errorsnum);
            return this.NoiseString;
        }
    }
}
