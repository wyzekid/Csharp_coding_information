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
    public class NoiseDecoder
    {
        public ArrayList resultString = new ArrayList();
        public ArrayList resultfullString = new ArrayList();
        public BitArray finaloutput;
        private int[] syndrom = new int[3] { 0, 0, 0};
        private int[] matrixrow0 = new int[3] { 0, 0, 0 };
        private int[,] decodeMatrix = new int[7, 3] { { 1, 1, 1 }, { 1, 1, 0 }, { 1, 0, 1 }, { 0, 1, 1 }, { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } };
        public NoiseDecoder(BitArray CanalString)
        {
            int errornum = 0;
            int[] bufferString = new int[CanalString.Length];
            int[] bufferString1 = new int[7];
            //Переводим в интовый массив
            for (int i = 0; i < CanalString.Length; i++)
            {
                if (CanalString[i] == true)
                    bufferString[i] = 1;
                else
                    bufferString[i] = 0;
            }
            //Записываем 7 символов в буферный массив
            do
            {

                for (int i = 0; i < 7; i++)
                {
                    bufferString1[i] = bufferString[i];
                    bufferString[i] = 2;
                    if (i == 6)
                    {
                        bufferString = bufferString.Where(x => x != 2).ToArray();
                    }
                }
                //Записываем синдром для данной семерки символов
                for (int m = 0; m < 3; m++)
                {
                    for (int n = 0; n < 7; n++)
                    {
                        syndrom[m] += bufferString1[n] * decodeMatrix[n, m];
                        if ((syndrom[m] % 2) == 0)
                            syndrom[m] = 0;
                    }

                }
                //Сравниваем синдромы со строками матрицы
                if (!syndrom.SequenceEqual(matrixrow0))
                  {
                      for (int i = 0; i < 7; i++)
                      {
                          if (syndrom.SequenceEqual(getMatrixRow(decodeMatrix, i)))
                              errornum = i;
                      }
                    /*
                    */
                    //Инвертируем символ в соответсвующей позиции
                    if (bufferString1[errornum] == 1)
                        bufferString1[errornum] = 0;
                    else
                        bufferString1[errornum] = 1;
                }
                for (int i = 0; i < 7; i++)
                {
                    resultfullString.Add(bufferString1[i]);
                }
                for (int i = 0; i < 4; i++)
                {
                    resultString.Add(bufferString1[i]);
                }
                Array.Clear(syndrom, 0, 3);
            } while (bufferString.Length > 0);
            int j = 0;
            BitArray output = new BitArray(resultString.Count);
            foreach (int num in resultString)
            {
                if (num == 0)
                {
                    output.Set(j, false);
                    j++;
                }
                if (num == 1)
                {
                    output.Set(j, true);
                    j++;
                }
            }
            finaloutput = output;
            
        }//конец конструктора
        public int[] getMatrixRow(int[,] matrix, int rownumber)
        {
            int[] matrixrow={0,0,0};
            for (int n = 0; n < 3; n++)
            {
                matrixrow[n] = matrix[rownumber, n];
            }
            return matrixrow;

        }

        public BitArray getStringNoiseDec()
        {
            return this.finaloutput;
        }
        public ArrayList getFullDecodedString()
        {
            return this.resultfullString;
        }
    
    }
}
