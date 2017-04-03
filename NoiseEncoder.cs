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
    public class NoiseEncoder : NoiseEnctoCan
    {
        public ArrayList resultString = new ArrayList();
        public BitArray finaloutput;
        private int[] result = new int[7] { 0, 0, 0, 0, 0, 0, 0 };
        private int[,] encodeMatrix = new int[4, 7] { { 1, 0, 0, 0, 1, 1, 1 }, { 0, 1, 0, 0, 1, 1, 0 }, { 0, 0, 1, 0, 1, 0, 1 }, { 0, 0, 0, 1, 0, 1, 1 } };
        public NoiseEncoder(BitArray EncoderString)
        {
            int[] bufferString = new int[EncoderString.Length];// для перевода в инт массив
            int[] bufferString1 = new int[4];// для считывания по 4 символа
            for (int i = 0; i < EncoderString.Length; i++)
            {
                if (EncoderString[i] == true)
                    bufferString[i] = 1;
                else
                    bufferString[i] = 0;
                //Console.Write(bufferString[i]);
                //Console.WriteLine("Разделитель");
                
            }
            /*Console.WriteLine("////////");
            Console.WriteLine("Исходная строка");
            foreach (bool bit in EncoderString)
            {
                Console.Write((bit ? 1 : 0) + "");
            }
            Console.WriteLine();*/
            do
            {
                for (int i = 0; i < 4; i++)
                {
                    bufferString1[i] = bufferString[i];
                    //Console.Write("bufferString1={0,1:g}", bufferString1[i]);
                    //Console.WriteLine();
                    bufferString[i] = 2;
                    if (i == 3)
                    {
                        bufferString = bufferString.Where(x => x != 2).ToArray();
                        /*Console.WriteLine("bufferstring в данный момент");
                    foreach (int num in bufferString)
                    {
                        Console.Write(num);
                    }
                    Console.WriteLine();*/
                    }
                    
                }
                
                for (int m = 0; m < 7; m++)
                {
                    for (int n = 0; n < 4; n++)
                    {
                        result[m] += bufferString1[n] * encodeMatrix[n, m];
                        //Console.Write("bufferString1={0,1:g}", bufferString1[n]);
                        //Console.WriteLine();
                        if ((result[m] % 2) == 0)
                            result[m] = 0;
                                                
                    }
                    
                    resultString.Add(result[m]);
                    Array.Clear(result, 0, 7);
                    //Console.Write("кодовое 4 слово ");
                    /*for (int h = 0; h < bufferString1.Length; h++)
                    {
                        Console.Write(bufferString1[h]);
                    }
                    /*foreach (int num in bufferString1)
                    {
                        Console.Write(bufferString1);
                    }*/
                   // Console.WriteLine();
                   // Console.Write("устойчивое слово ");
                    /*foreach (int num1 in resultString)
                    {
                        Console.Write(num1);
                    }
                    Console.WriteLine();*/
                }

            } while (bufferString.Length > 3);
            
            int j=0;
            /*Console.WriteLine("Устойчивая строка:");
            foreach (int num in resultString)
            {
                Console.Write(num);
            }*/
            BitArray output = new BitArray(resultString.Count);
            foreach (int num in resultString)
            {
                if (num==0)
                {
                    output.Set(j, false);
                    j++;
                }
                if (num==1)
                {
                    output.Set(j, true);
                    j++;
                }
            }
            finaloutput = output;
            Console.WriteLine();
            Console.WriteLine("Строка кототрую нужно проверить, сука:");
            foreach (bool bit in finaloutput)
            {
                Console.Write((bit ? 1 : 0) + "");
            }
            Console.WriteLine("Ну что, проверил, сука?");
        }//конец конструктора
        
        //Метод вернет закодированную строку в формате BitArray для передачи ее в канал
        public BitArray getNoiseEncodedStr()
        {
            return this.finaloutput;
        }
    }
}

