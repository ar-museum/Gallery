using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Net;
using System.IO;

public class UiManager : MonoBehaviour
{
    //private const string Path = "Sprites/sd.jpg";
    //public Image imgOpera;
    public Text txtDescription;
    public Text txtTitle;
    [SerializeField] private UnityEngine.UI.Image imgOpera = null;
    // Start is called before the first frame update
    void Start()
    {
        //sprite = Resources.Load<Sprite>(Path);
        txtDescription.text = "Asta e o descriere";
        txtTitle.text = "Title";
        if (imgOpera != null)
        {
            imgOpera.sprite = Resources.Load<Sprite>("Sprites/sd");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetHtmlFromUri(string resource)
    {
        string html = string.Empty;
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(resource);
        try
        {
            using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
            {
                bool isSuccess = (int)resp.StatusCode < 299 && (int)resp.StatusCode >= 200;
                if (isSuccess)
                {
                    using (StreamReader reader = new StreamReader(resp.GetResponseStream()))
                    {
                        //We are limiting the array to 80 so we don't have
                        //to parse the entire html document feel free to 
                        //adjust (probably stay under 300)
                        char[] cs = new char[80];
                        reader.Read(cs, 0, cs.Length);
                        foreach (char ch in cs)
                        {
                            html += ch;
                        }
                    }
                }
            }
        }
        catch
        {
            return "";
        }
        return html;
    }
}
