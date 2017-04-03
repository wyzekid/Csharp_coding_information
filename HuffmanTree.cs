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
    public class HuffmanTree
    {
        private List<Node> nodes = new List<Node>();
        public Node Root { get; set; }
        public Dictionary<string, int> Frequencies = new Dictionary<string, int>();
        public Dictionary<List<bool>,string> CodeTable = new Dictionary<List<bool>,string>();

        public void Build(string source)
        {
            for (int i = 0; i < source.Length; i++)
            {
                if (!Frequencies.ContainsKey(source[i].ToString()))
                {
                    Frequencies.Add(source[i].ToString(), 1);
                }

                Frequencies[source[i].ToString()]++;
            }

            foreach (KeyValuePair<string, int> symbol in Frequencies)
            {
                nodes.Add(new Node() { Symbol = symbol.Key, Frequency = symbol.Value });
            }

            while (nodes.Count > 1)
            {
                List<Node> orderedNodes = nodes.OrderBy(node => node.Frequency).ToList<Node>();

                if (orderedNodes.Count >= 2)
                {
                    // Берет первые 2 элемента из списка
                    List<Node> taken = orderedNodes.Take(2).ToList<Node>();

                    // Создает общий узел, суммируя частоты
                    Node parent = new Node()
                    {
                        Symbol = "*",
                        Frequency = taken[0].Frequency + taken[1].Frequency,
                        Left = taken[0],
                        Right = taken[1]
                    };

                    nodes.Remove(taken[0]);
                    nodes.Remove(taken[1]);
                    nodes.Add(parent);
                }

                this.Root = nodes.FirstOrDefault();
            }

        }

                  
        //Метод возвращает список символов и их код
        public Dictionary<List<bool>, string> ReturnCodeTable(string SourceString)
        {
            //Dictionary<char, List<bool>> CodeTable = new Dictionary<char,List<bool>>();
            for (int i = 0; i < SourceString.Length; i++)
            {
                List<bool> encodedSymbol = this.Root.Traverse(SourceString[i].ToString(), new List<bool>());
                if (!CodeTable.ContainsValue(SourceString[i].ToString()))
                    CodeTable.Add(encodedSymbol, SourceString[i].ToString());
            }
            return CodeTable;
        }
              
        
        //Метод возвращает код всей входной строки по коду Хаффмана
        public BitArray Encode(string source)
        {
            List<bool> encodedSource = new List<bool>();
            for (int i = 0; i < source.Length; i++)
            {
                List<bool> encodedSymbol = this.Root.Traverse(source[i].ToString(), new List<bool>());
                encodedSource.AddRange(encodedSymbol);

            }

            BitArray bits = new BitArray(encodedSource.ToArray());
            return bits;
        }

                   
    }
}
