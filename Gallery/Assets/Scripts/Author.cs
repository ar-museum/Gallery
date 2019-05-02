using UnityEngine.UI;
using UnityEngine;
using Scripts;

namespace Scripts
{
    [System.Serializable]
    public class AuthorData
    {
        public string titlu;
        public string descriere;
        public string denumire;
        public string autor;
    }

    public class Author : MonoBehaviour
    {
        [SerializeField] private UnityEngine.UI.Image imgOpera = null;
        [SerializeField] private Text txtTitle;
        [SerializeField] private Text txtDescription;
        private float offset;
        private float contentHeight;
        
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("is this love??");
            loadContent();
            if (!PlayerPrefs.HasKey("author" + "offset"))
                PlayerPrefs.SetFloat("author" + "offset", 0);
            if (!PlayerPrefs.HasKey("author" + "height"))
                PlayerPrefs.SetFloat("author" + "height", 0);
            Invoke("setContentDimension", 0.01f);
        }

        void loadContent()
        {
            JsonToObject jo = new JsonToObject();
            AuthorData author = jo.loadJson<AuthorData>();
            txtDescription.text = author.descriere;
            txtTitle.text = author.titlu + '\n';
            if (imgOpera != null)
            {
                imgOpera.sprite = Resources.Load<Sprite>("Sprites/" + author.denumire);
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

            if (PlayerPrefs.GetFloat("author" + "height") == 0)
            {
                contentHeight = (childrenHeight + lastChildHeight * 3);
                contentHeight = contentHeight - (0.015f * contentHeight * contentHeight / 1000);
                PlayerPrefs.SetFloat("author" + "height", contentHeight);
            }
            else
            {
                contentHeight = PlayerPrefs.GetFloat("author" + "height");
            }
            thisRectTransform.sizeDelta = new Vector2(thisRectTransform.sizeDelta.x, contentHeight);

            offset = PlayerPrefs.GetFloat("author" + "offset");

            if (offset == 0)
            {
                offset = imgRectTransform.rect.height - firstHeight;
                PlayerPrefs.SetFloat("author" + "offset", offset);
            }
            else
            {
                Debug.Log(PlayerPrefs.GetFloat("author" + "offset"));
                offset = PlayerPrefs.GetFloat("author" + "offset");
            }
            imgRectTransform.offsetMin = new Vector2(imgRectTransform.offsetMin.x, offset);
            RectTransform lastChildRectTransform = lastChild.GetComponent<RectTransform>();
            lastChildRectTransform.offsetMax = new Vector2(lastChildRectTransform.offsetMax.x, offset);
        }




        // Update is called once per frame
        void Update()
        {
        }

        void OnApplicationQuit()
        {
            PlayerPrefs.DeleteKey("author"+"offset");
            PlayerPrefs.DeleteKey("author"+"height");
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
}