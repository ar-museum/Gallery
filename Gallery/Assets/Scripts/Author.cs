using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Net;
using System.IO;


[System.Serializable]
public class ExhibitData
{
    public string titlu;
    public string descriere;
    public string denumire;
    public string autor;
}

[System.Serializable]
public class ExhibitArray
{
    public List<ExhibitData> questions;
}

[System.Serializable]
public class JsonToObject
{
    public ExhibitData loadJson()
    {
        using (StreamReader r = new StreamReader("Assets/StreamingAssets/op2.json"))
        {
            string json = r.ReadToEnd();
            ExhibitData exhibit = JsonUtility.FromJson<ExhibitData>(json);
            return exhibit;
        }
    }
}


public class Author : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image imgOpera = null;
    public Text txtTitle;
    public Text txtDescription;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void loadContent()
    {
        JsonToObject jo = new JsonToObject();
        ExhibitData exhibit = jo.loadJson();
        txtDescription.text = exhibit.descriere;
        txtTitle.text = exhibit.titlu + '\n';
        if (imgOpera != null)
        {
            imgOpera.sprite = Resources.Load<Sprite>("Sprites/" + exhibit.denumire);
        }

    }

    void setContentDimension()
    {
        RectTransform thisRectTransform = GetComponent<RectTransform>();
        RectTransform imgRectTransform = imgOpera.GetComponent<RectTransform>();

        //image height before the resize
        float firstHeight = imgRectTransform.rect.height;
        //the "so-called" height of the children
        float childrenHeight = 0;
        Transform lastChild = null;

        foreach (Transform child in transform)
        {
            childrenHeight += child.GetComponent<RectTransform>().rect.height;
            lastChild = child;
        }

        //used only because somehow it works..
        float lastChildHeight = 0;
        foreach (Transform child in lastChild)
        {
            lastChildHeight += child.GetComponent<RectTransform>().rect.height;
        }

        thisRectTransform.sizeDelta = new Vector2(thisRectTransform.sizeDelta.x, (childrenHeight + lastChildHeight * 3));
        thisRectTransform.sizeDelta = new Vector2(thisRectTransform.sizeDelta.x, thisRectTransform.sizeDelta.y - (0.015f * thisRectTransform.sizeDelta.y * thisRectTransform.sizeDelta.y / 1000));

        //offset used to keep image aspect ratio
        float offset = 0;
        if(File.Exists("Assets/offset.txt"))
        {
            Debug.Log("I exist");
            using (TextReader reader = File.OpenText("Assets/offset.txt"))
            {
                offset = float.Parse(reader.ReadLine());
                Debug.Log(offset);
            }
        }
        else{
            FileStream fs = File.Create("Assets/offset.txt");
            string path = "Assets/offset.txt";
            fs.Dispose();
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                offset = imgRectTransform.rect.height - firstHeight;
                writer.WriteLine(offset);
            }
        }
        
        imgRectTransform.offsetMin = new Vector2(imgRectTransform.offsetMin.x, offset);
        RectTransform lastChildRectTransform = lastChild.GetComponent<RectTransform>();
        lastChildRectTransform.offsetMax = new Vector2(lastChildRectTransform.offsetMax.x, offset);
    }




    // Update is called once per frame
    void Update()
    {
        loadContent();
        Invoke("setContentDimension", 0.01f);
    }
    
    void OnApplicationQuit()
    {
        File.Delete("Assets/offset.txt");
    }

    /*
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
    */
}


