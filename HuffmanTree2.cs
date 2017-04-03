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
    public class HuffmanTree2
    {
        private List<Node> nodes = new List<Node>();
        public Node Root { get; set; }
        //public Dictionary<char, int> Frequencies = new Dictionary<char, int>();
        public Dictionary<List<bool>, string> CodeTable = new Dictionary<List<bool>, string>();
        public Dictionary<string, int> BlockFrequencies = new Dictionary<string, int>();


        public void Build(string source)
        {

            if ((source.Length) % 2 != 0)
                source +=" ";
            Console.WriteLine(source);
            for (int i = 0; i < (source.Length - 1); i += 2)
            {
                if (!BlockFrequencies.ContainsKey(source[i].ToString() + source[i + 1].ToString()))
                {
                    BlockFrequencies.Add(source[i].ToString() + source[i + 1].ToString(), 0);
                }
                BlockFrequencies[source[i].ToString() + source[i + 1].ToString()]++;
            }

            foreach (KeyValuePair<string, int> symbol in BlockFrequencies)
            {
                nodes.Add(new Node() { Symbol = symbol.Key, Frequency = symbol.Value });
            }

            while (nodes.Count > 1)
            {
                List<Node> orderedNodes = nodes.ToList<Node>();

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
        //Метод возвращает кодовую таблицу
        public Dictionary<List<bool>, string> ReturnCodeTable(string SourceString)
        {
            if ((SourceString.Length) % 2 != 0)
                SourceString += " ";
            for (int i = 0; i < (SourceString.Length - 1); i += 2)
            {
                List<bool> encodedSymbol = this.Root.Traverse(SourceString[i].ToString() + SourceString[i + 1].ToString(), new List<bool>());
                if (!CodeTable.ContainsValue(SourceString[i].ToString() + SourceString[i + 1].ToString()))
                    CodeTable.Add(encodedSymbol, SourceString[i].ToString() + SourceString[i + 1].ToString());
            }
            return CodeTable;
        }

        //Метод возвращает код всей входной строки
        public BitArray Encode(string source)
        {
            if ((source.Length) % 2 != 0)
                source += " ";
            List<bool> encodedSource = new List<bool>();
            for (int i = 0; i < (source.Length - 1); i += 2)
            {
                List<bool> encodedSymbol = this.Root.Traverse(source[i].ToString() + source[i + 1].ToString(), new List<bool>());

                encodedSource.AddRange(encodedSymbol);
            }
            BitArray bits = new BitArray(encodedSource.ToArray());
            return bits;
        }

    }
}