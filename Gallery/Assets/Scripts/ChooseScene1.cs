using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Text;

public class ChooseScene1 : MonoBehaviour
{
    public void changeIt1(int i)
    {
        SceneManager.LoadScene(i);
        FileStream fs = File.Create("Assets/nameeee.txt");
        string path = "Assets/nameeee.txt";
        fs.Dispose();
        using (StreamWriter writer = new StreamWriter(path, true))
        {
            writer.WriteLine("op2");
            writer.WriteLine("cevaaa");
        }

    }
}
