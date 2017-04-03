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

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "txt files|*.txt";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.LoadFile(openFileDialog.FileName, RichTextBoxStreamType.PlainText);
            }
              
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Dictionary<List<bool>, string> CodeTable = new Dictionary<List<bool>, string>();
            string input = richTextBox1.Text;
            Source source = new Source(input);
            string sourceinput = source.inputstring;
            if (radioButton1.Checked)
            {
                HuffmanTree huffmanTree = new HuffmanTree();
                huffmanTree.Build(sourceinput);
                Encoder encoder = new Encoder(sourceinput);
                BitArray encoded = encoder.CodedBitString;
                double zeros = 0;
                double edins = 0;
                foreach (bool bit in encoded)
                {
                    richTextBox2.AppendText((bit ? 1 : 0) + "");
                }
                richTextBox2.Text += "\n";
                richTextBox2.Text += "Помехоустойчивый код \n";
                // 
                CodeTable = encoder.getCodeTable();
                foreach (KeyValuePair<List<bool>, string> symbol in CodeTable)
                {
                    richTextBox3.Text += symbol.Value.ToString();
                    richTextBox3.AppendText("       ");
                    foreach (bool bit in symbol.Key)
                    {
                        richTextBox3.AppendText((bit ? 1 : 0) + "");
                    }
                    richTextBox3.Text += "\n";
                }
                EnctoNoiseEnc objnoiseencoder = new Encoder(input);
                NoiseEncoder objnencoder = new NoiseEncoder(encoded);
                BitArray noisecodedstring = objnencoder.getNoiseEncodedStr();//помехоустойчивая кодовая строка
                foreach (bool bit in noisecodedstring)
                {
                    richTextBox2.AppendText((bit ? 1 : 0) + "");
                    if (bit == false)
                        zeros++;
                    if (bit == true)
                        edins++;
                }
                textBox2.AppendText(zeros.ToString());
                textBox3.AppendText(edins.ToString());
                double sred = Math.Round((zeros+edins)/input.Length,3);
                textBox4.AppendText(sred.ToString());
                                                
            }
            if (radioButton2.Checked)
            {
                HuffmanTree1 huffmanTree = new HuffmanTree1();
                huffmanTree.Build(sourceinput);
                Encoder1 encoder = new Encoder1(sourceinput);
                BitArray encoded = encoder.CodedBitString;
                int zeros = 0;
                int edins = 0;
                foreach (bool bit in encoded)
                {
                    richTextBox2.AppendText((bit ? 1 : 0) + "");
                    if (bit == false)
                        zeros++;
                    if (bit == true)
                        edins++;
                }
                textBox2.AppendText(zeros.ToString());
                textBox3.AppendText(edins.ToString());
                // 
                CodeTable = encoder.getCodeTable();
                foreach (KeyValuePair<List<bool>, string> symbol in CodeTable)
                {
                    richTextBox3.Text += symbol.Value.ToString();
                    richTextBox3.AppendText("       ");
                    foreach (bool bit in symbol.Key)
                    {
                        richTextBox3.AppendText((bit ? 1 : 0) + "");
                    }
                    richTextBox3.Text += "\n";
                }

            }
            if (radioButton3.Checked)
            {
                HuffmanTree2 huffmanTree = new HuffmanTree2();
                huffmanTree.Build(sourceinput);
                Encoder2 encoder = new Encoder2(sourceinput);
                BitArray encoded = encoder.CodedBitString;
                int zeros = 0;
                int edins = 0;
                foreach (bool bit in encoded)
                {
                    richTextBox2.AppendText((bit ? 1 : 0) + "");
                    if (bit == false)
                        zeros++;
                    if (bit == true)
                        edins++;
                }
                // 
                textBox2.AppendText(zeros.ToString());
                textBox3.AppendText(edins.ToString());
                CodeTable = encoder.getCodeTable();
                foreach (KeyValuePair<List<bool>, string> symbol in CodeTable)
                {
                    richTextBox3.Text += symbol.Value.ToString();
                    richTextBox3.AppendText("       ");
                    foreach (bool bit in symbol.Key)
                    {
                        richTextBox3.AppendText((bit ? 1 : 0) + "");
                    }
                    richTextBox3.Text += "\n";
                }

            }
         
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string input = richTextBox1.Text;
            double getnoise = double.Parse(textBox1.Text);
            if (radioButton1.Checked)
            {
                EnctoDec objencoder1 = new Encoder(input);
                EnctoNoiseEnc objnoiseencoder = new Encoder(input);
                Encoder encoder = new Encoder(input);
                BitArray codedstring = encoder.CodedBitString;
                Console.WriteLine("Строка из кодера");
                foreach (bool bit in codedstring)
                {
                    Console.Write((bit ? 1 : 0) + "");
                }
                Console.WriteLine();
                Console.WriteLine("Строка из устойчивого кодера");
                NoiseEncoder objnencoder = new NoiseEncoder(codedstring);
                BitArray noisecodedstring = objnencoder.getNoiseEncodedStr();//строка из устойчивого кодера
                foreach (bool bit in noisecodedstring)
                {
                    Console.Write((bit ? 1 : 0) + "");
                }
                Console.WriteLine();
                Console.WriteLine("Cтрока из канала");
                CantoDec objcanal = new Canal(noisecodedstring, getnoise);
                BitArray noiseStringFromCanal = objcanal.getNoiseCodedStr(noisecodedstring, getnoise);//строка из канала
                foreach (bool bit in noiseStringFromCanal)
                {
                    Console.Write((bit ? 1 : 0) + "");
                }
                Console.WriteLine();
                Console.WriteLine("Строка из устойчивого декодера");
                NoiseDecoder objndecoder = new NoiseDecoder(noiseStringFromCanal);
                BitArray StringFromNoiseDec = objndecoder.getStringNoiseDec();//строка из устойчивого декодера
                ArrayList FulldecodedString = objndecoder.getFullDecodedString();
                foreach (int num in FulldecodedString)
                {
                    Console.Write(num);
                }
                Array FulldecodedString1 = FulldecodedString.ToArray();
                BitArray FullBitdecodedString = new BitArray(FulldecodedString.Count);
                for (int i=0; i < FulldecodedString.Count; i++)
                {
                    if ((int)FulldecodedString1.GetValue(i)==0)
                        FullBitdecodedString.Set(i, false);
                    else
                        FullBitdecodedString.Set(i, true);
                }
                int errorsdone=0;
                int errorsundone=0;
                int errorswrong=0;
                int dlin1 = noisecodedstring.Length;
                int dlin2 = noiseStringFromCanal.Length;
                int dlin3 = FullBitdecodedString.Length;
                Console.WriteLine("Длина кодера: {0,1:g}", dlin1);
                Console.WriteLine("Длина канала: {0,1:g}", dlin2);
                Console.WriteLine("Длина декодера: {0,1:g}", dlin3);
                bool elem = false;
                bool elem1 = false;
                bool elem2 = false;
                Console.WriteLine();
                for (int i = 0; i < noisecodedstring.Length; i++)
                {
                    /*if ((noisecodedstring[i] == FullBitdecodedString[i]) && (FullBitdecodedString[i] != noiseStringFromCanal[i]))
                        errorsdone++;
                    if ((noisecodedstring[i] != FullBitdecodedString[i]) && (FullBitdecodedString[i] == noiseStringFromCanal[i]))
                        errorsundone++;*/
                   // if 
                    elem = noisecodedstring.Get(i);//кодер
                    elem1 = FullBitdecodedString.Get(i);//декодер
                    elem2 = noiseStringFromCanal.Get(i);
                    //канал
                    //elem1 = noisecodedstring.Get(i);
                    //elem2 = noisecodedstring.Get(i);
                    /*if ((noisecodedstring.Get(i) == FullBitdecodedString.Get(i)) && (FullBitdecodedString.Get(i)!=noiseStringFromCanal.Get(i)))
                        errorsdone++;
                    if ((!noisecodedstring[i].Equals(FullBitdecodedString[i])) && (FullBitdecodedString[i].Equals(noiseStringFromCanal[i])))
                        errorsundone++;*/
                    //Console.WriteLine();
                    if (elem == elem1)
                    {
                        Console.Write(elem);
                        if (elem != elem2)
                            errorsdone++;
                    }
                        if (elem == elem2)
                        if (elem1 != elem2)
                            errorsundone++;
                    if (elem == elem2)
                        if (elem2 != elem1)
                            errorswrong++;
                }
                Console.WriteLine("Количество ошибок, исправленных декодером: {0,1:g}", errorsdone);
                Console.WriteLine("Количество ошибок, неисправленных декодером: {0,1:g}", errorsundone);
                Console.WriteLine("Количество ошибок, неверно исправленных декодером: {0,1:g}", errorswrong);
                foreach (bool bit in StringFromNoiseDec)
                {
                    Console.Write((bit ? 1 : 0) + "");
                }
                Console.WriteLine();
                Decoder objdecoder = new Decoder(StringFromNoiseDec, objencoder1.getCodeTable());
                string decodedstring = objdecoder.Decode(StringFromNoiseDec, objencoder1.getCodeTable());
                Destination dest = new Destination(decodedstring);
                string outputdecode = dest.outputstring;
                richTextBox4.AppendText(outputdecode);
                
             }
            
            if (radioButton2.Checked)
            {
                EnctoCan objencoder = new Encoder1(input);
                EnctoDec objencoder1 = new Encoder1(input);
                CantoDec objcanal = new Canal(objencoder.getCanalCodedStr(), getnoise);
                BitArray noiseString = objcanal.getNoiseCodedStr(objencoder.getCanalCodedStr(), getnoise);
                Decoder objdecoder = new Decoder(noiseString, objencoder1.getCodeTable());
                string decodedstring = objdecoder.Decode(noiseString, objencoder1.getCodeTable());
                Destination dest = new Destination(decodedstring);
                string outputdecode = dest.outputstring;
                richTextBox4.AppendText(outputdecode);
            }
            
            if (radioButton3.Checked)
            {
                EnctoCan objencoder = new Encoder2(input);
                EnctoDec objencoder1 = new Encoder2(input);
                CantoDec objcanal = new Canal(objencoder.getCanalCodedStr(), getnoise);
                BitArray noiseString = objcanal.getNoiseCodedStr(objencoder.getCanalCodedStr(), getnoise);
                Decoder objdecoder = new Decoder(noiseString, objencoder1.getCodeTable());
                string decodedstring = objdecoder.Decode(noiseString, objencoder1.getCodeTable());
                Destination dest = new Destination(decodedstring);
                string outputdecode = dest.outputstring;
                richTextBox4.AppendText(outputdecode);
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox2.Clear();
            richTextBox3.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            richTextBox4.Clear();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
    //из кодера в декодер
    public interface EnctoDec
    {
        //BitArray getCodedStr();
        Dictionary<List<bool>, string> getCodeTable();
        
    }
    //из кодера в канал
    public interface EnctoCan
    {
        BitArray getCanalCodedStr();
        
    }
    
    //из канала в декодер
    public interface CantoDec
    {
        BitArray getNoiseCodedStr(BitArray CodedString, double noise);
    }

    public interface EnctoNoiseEnc
    {
        BitArray getEncodedCodedStr();
    }

    public interface NoiseEnctoCan
    {
        BitArray getNoiseEncodedStr();
    }
    public interface NoiseDectoDec
    {
        BitArray getStringNoiseDec();
    }
}
