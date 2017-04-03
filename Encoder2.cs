﻿using System;
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
    public class Encoder2 : EnctoDec, EnctoCan
    {
        public BitArray CodedBitString;
        private Dictionary<List<bool>, string> CodeTable;
        public Encoder2(string input)
        {
            HuffmanTree2 huffmanTree = new HuffmanTree2();
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

    }
}
