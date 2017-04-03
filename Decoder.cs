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
    public class Decoder
    {
        public Decoder(BitArray bits, Dictionary<List<bool>, string> CodeTable)
        {
        }
        public string Decode(BitArray bits, Dictionary<List<bool>, string> CodeTable)
        {
            string decoded = "";
            
            List<bool> templist = new List<bool>();
            for (int i = 0; i < bits.Length; i++)
            {
                templist.Add(bits[i]);
                foreach (KeyValuePair<List<bool>, string> number in CodeTable)
                {
                    if (ListEquals(templist, number.Key))
                    {
                        decoded += CodeTable[number.Key];
                        templist.Clear();
                    }
                }
            }

            return decoded;
        }

        public static bool ListEquals(List<bool> list1, List<bool> list2)
        {
            if (list1.Count != list2.Count)
                return false;
            else
            {
                for (int i = 0; i < list1.Count; i++)
                {
                    if (!list1[i].Equals(list2[i]))
                        return false;
                }
                return true;
            }
        }
    }
}
