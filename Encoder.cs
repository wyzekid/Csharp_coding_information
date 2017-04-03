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
    public class Encoder : EnctoDec, EnctoCan, EnctoNoiseEnc
    {
        public BitArray CodedBitString;
        private Dictionary<List<bool>, string> CodeTable;
        public Encoder(string input)
        {
            HuffmanTree huffmanTree = new HuffmanTree();
            huffmanTree.Build(input);
            this.CodedBitString = huffmanTree.Encode(input);
            this.CodeTable = huffmanTree.ReturnCodeTable(input);
            

        }
        
        public BitArray getCanalCodedStr()
        {
            return this.CodedBitString;
        }
        
        public Dictionary<List<bool>, string> getCodeTable()
        {
            return this.CodeTable;
        }
        //метод для передачи строки в устойчивый кодер
        public BitArray getEncodedCodedStr()
        {
            return this.CodedBitString;
        }
                        
    }
}
