using UnityEngine.UI;
using UnityEngine;


namespace Scripts
{
    [System.Serializable]
    public class ExhibitData
    {
        public string titlu;
        public string descriere;
        public string denumire;
        public string autor;
    }

    public class Exhibit : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] private UnityEngine.UI.Image imgOpera = null;
        [SerializeField] private Text txtTitle;
        [SerializeField] private Text txtDescription;
        private float offset;
        private float contentHeight;

        // Start is called before the first frame update
        void Start()
        {
            loadContent();
            if (!PlayerPrefs.HasKey("exhibit" + "offset"))
                PlayerPrefs.SetFloat("exhibit" + "offset", 0);
            if (!PlayerPrefs.HasKey("exhibit" + "height"))
                PlayerPrefs.SetFloat("exhibit" + "height", 0);
            Invoke("setContentDimension", 0.1f);
        }

        void loadContent()
        {
            JsonToObject jo = new JsonToObject();
            ExhibitData exhibit = jo.loadJson<ExhibitData>();
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

            if (PlayerPrefs.GetFloat("exhibit" + "height") == 0)
            {
                contentHeight = (childrenHeight + lastChildHeight * 3);
                contentHeight = contentHeight - (0.015f * contentHeight * contentHeight / 1000);
                PlayerPrefs.SetFloat("exhibit" + "height", contentHeight);
            }
            else
            {
                contentHeight = PlayerPrefs.GetFloat("exhibit" + "height");
            }
            thisRectTransform.sizeDelta = new Vector2(thisRectTransform.sizeDelta.x, contentHeight);

            offset = PlayerPrefs.GetFloat("exhibit" + "offset");

            if (offset == 0)
            {
                offset = imgRectTransform.rect.height - firstHeight;
                PlayerPrefs.SetFloat("exhibit" + "offset", offset);
            }
            else
            {
                Debug.Log(PlayerPrefs.GetFloat("exhibit" + "offset"));
                offset = PlayerPrefs.GetFloat("exhibit" + "offset");
            }
            imgRectTransform.offsetMin = new Vector2(imgRectTransform.offsetMin.x, offset);
            RectTransform lastChildRectTransform = lastChild.GetComponent<RectTransform>();
            lastChildRectTransform.offsetMax = new Vector2(lastChildRectTransform.offsetMax.x, offset);
        }

        void OnApplicationQuit()
        {
            PlayerPrefs.DeleteKey("exhibit" + "offset");
            PlayerPrefs.DeleteKey("exhibit" + "height");
        }

    }

}