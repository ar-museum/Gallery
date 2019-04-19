using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Net;
using System.IO;

public class Exhibit : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private UnityEngine.UI.Image imgOpera = null;
    public Text txtTitle;
    public Text txtDescription;

    // Start is called before the first frame update
    void Start()
    {
        loadContent();
        Invoke("setContentDimension",0.1f);
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
        float offset = imgRectTransform.rect.height - firstHeight;
        imgRectTransform.offsetMin = new Vector2(imgRectTransform.offsetMin.x, offset);
        RectTransform lastChildRectTransform = lastChild.GetComponent<RectTransform>();
        lastChildRectTransform.offsetMax = new Vector2(lastChildRectTransform.offsetMax.x, offset);
    }

    // Update is called once per frame
    void Update()
    {
        setContentDimension();
    }
}
