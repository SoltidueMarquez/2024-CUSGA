using System.IO;
using UnityEngine;

public static class SImpleJsonUtil
{
    /// <summary>
    /// 根据fileName读取json文件
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
        public static string ReadData(string fileName)
        {
        //如果文件不存在返回空
        if (!File.Exists(Application.streamingAssetsPath + "/" + fileName))
        {
            return "";
        }
            string readData;
            string fileUrl = Application.streamingAssetsPath + "/" + fileName;

            using (StreamReader sr = new StreamReader(fileUrl))
            {
                readData = sr.ReadToEnd();
                sr.Close();
            }

            return readData;
        }
        /// <summary>
        /// 根据fileName写入json文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="writeData"></param>
        public static void WriteData(string fileName, string writeData)
        {
            //如果文件不存在则创建文件
            if (!File.Exists(Application.streamingAssetsPath + "/" + fileName))
        {
                File.Create(Application.streamingAssetsPath + "/" + fileName).Dispose();
            }
            
            string fileUrl = Application.streamingAssetsPath + "/" + fileName;

            using (StreamWriter sw = new StreamWriter(fileUrl))
            {
                sw.Write(writeData);
                sw.Close();
            }
        }
}
