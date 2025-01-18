using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaSearchSystem
{
    internal class SimilarCls
    {
        public static double calSimilarCosinelAllFea(ArrayList allFealst, ArrayList fealst, ArrayList fealst2, double[] feaVector, double[] feaVector2)
        {
            double[] array = new double[allFealst.Count];
            double[] array2 = new double[allFealst.Count];
            for (int i = 0; i < allFealst.Count; i++)
            {
                array[i] = FindFeaVal(allFealst[i], fealst, feaVector);
                array2[i] = FindFeaVal(allFealst[i], fealst2, feaVector2);
            }
            return calCosine(array, array2, allFealst.Count);
        }

        private static double calCosine(double[] array, double[] array2, int count)
        {
            double num = 0.0;
            double num2 = 0.0;
            double num3 = 0.0;

            for (int i = 0; i < count; i++)
            {
                num += array[i] * array2[i];
                num2 += Math.Pow(array[i], 2.0);
                num3 += Math.Pow(array2[i], 2.0);
            }

            return num / Math.Sqrt(num2 * num3);
        }

        private static double FindFeaVal(object obj, ArrayList fealst, double[] feaVector)
        {
            int num = checkFea((string)obj, fealst);
            if (num >= 0)
            {
                return feaVector[num];
            }
            return 0.0;
        }

        public static double[] getFeaVector2Amtiet(string str, ref ArrayList fealst)
        {
            str = str.ToLower();
            string[] strlist = getFeaArr(str);

            getFeature(strlist, ref fealst);

            double[] vertor = new double[fealst.Count];
            calVector(strlist, fealst, ref vertor);

            return vertor;
        }

        private static void calVector(string[] strlist, ArrayList fealst, ref double[] vertor)
        {
            for (int i = 0; i < fealst.Count; i++)
            {
                vertor[i] = 0.0;
                string text = (string)fealst[i];
                foreach (string text2 in strlist)
                {
                    if (text2.Trim() == text)
                    {
                        vertor[i] += 1.0;
                    }
                }
            }
        }

        private static void getFeature(string[] strlist, ref ArrayList fealst)
        {
            foreach (string text in strlist)
            {
                string text2 = text.Trim();
                if (text2 != "" && checkFea(text2, fealst) < 0)
                {
                    fealst.Add(text2);
                }
            }
        }

        private static int checkFea(string str, ArrayList fealst)
        {
            if (fealst.Count > 0)
            {
                for (int i = 0; i < fealst.Count; i++)
                {
                    string text = (string)fealst[i];
                    if (text == str)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        private static string[] getFeaArr(string str)
        {
            return str.Split(' ', '-', '"', '\'', '?', ',', ':', ';', '.', '(', ')');
        }

        public static ArrayList unifyFealst(ArrayList fealst, ArrayList fealst2)
        {
            ArrayList arrayList = new ArrayList();
            foreach (object item in fealst)
            {
                arrayList.Add(item);
            }

            foreach (object item2 in fealst2)
            {
                string text = (string)item2;
                if (text != "" && checkFea(text, arrayList) < 0)
                {
                    arrayList.Add(text);
                }
            }

            return arrayList;
        }

        internal static double[] getFeaVector2(string str1, ref ArrayList fealst)
        {
            str1 = str1.ToLower();

            string text = "text.txt";
            saveToFileUTF8(text, str1);

            string[] strlist = getTokenTxt(text);

            getFeature(strlist, ref fealst);
            double[] vector = new double[fealst.Count];

            calVector(strlist, fealst, ref vector);

            return vector;
        }

        private static string[] getTokenTxt(string fileIn)
        {
            string text = "tok.txt";
            string para = "-i " + fileIn + " -o " + text;
            RunExe("vnTokenizer.bat", para);
            string str = readFileUTF8(text);
            return getFeaArr(str);
        }

        private static string readFileUTF8(string text)
        {
            return File.ReadAllText(text, Encoding.UTF8);
        }

        private static void RunExe(string v, string para)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.CreateNoWindow = false;
            processStartInfo.UseShellExecute = false;
            processStartInfo.Arguments = para;
            processStartInfo.FileName = v;
            processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            Process process = Process.Start(processStartInfo);
            process.WaitForExit();
        }

        private static void saveToFileUTF8(string text, string str1)
        {
            StreamWriter streamWriter = new StreamWriter(text, append: false, Encoding.UTF8);
            streamWriter.Write(str1);
            streamWriter.Close();
        }
    }
}
