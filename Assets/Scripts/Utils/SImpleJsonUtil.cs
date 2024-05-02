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
        string readData;
#if UNITY_EDITOR
        //如果文件不存在返回空
        if (!File.Exists(Application.streamingAssetsPath + "/" + fileName))
        {
            return "";
        }
        string fileUrl = Application.streamingAssetsPath + "/" + fileName;

        using (StreamReader sr = new StreamReader(fileUrl))
        {
            readData = sr.ReadToEnd();
            sr.Close();
        }
#endif
#if !UNITY_EDITOR
        if (!File.Exists(Application.persistentDataPath + "/" + fileName))
        {
            return "";
        }
        string fileUrl = Application.persistentDataPath + "/" + fileName;

        using (StreamReader sr = new StreamReader(fileUrl))
        {
            readData = sr.ReadToEnd();
            sr.Close();
        }
#endif
        return readData;
    }
    /// <summary>
    /// 根据fileName写入json文件
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="writeData"></param>
    public static void WriteData(string fileName, string writeData)
    {
#if UNITY_EDITOR
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
#endif
#if !UNITY_EDITOR
        //如果文件不存在则创建文件
        if (!File.Exists(Application.persistentDataPath + "/" + fileName))
        {
            File.Create(Application.persistentDataPath + "/" + fileName).Dispose();
        }

        string fileUrl = Application.persistentDataPath + "/" + fileName;

        using (StreamWriter sw = new StreamWriter(fileUrl))
        {
            sw.Write(writeData);
            sw.Close();
        }
#endif
    }
    /// <summary>
    /// 删档
    /// </summary>
    /// <param name="fileName"></param>
    public static void DeleteData(string fileName)
    {
#if UNITY_EDITOR
        if (File.Exists(Application.streamingAssetsPath + "/" + fileName))
        {
            File.Delete(Application.streamingAssetsPath + "/" + fileName);
        }
#endif
#if !UNITY_EDITOR
        if (File.Exists(Application.persistentDataPath + "/" + fileName))
        {
            File.Delete(Application.persistentDataPath + "/" + fileName);
        }
#endif
    }
}
