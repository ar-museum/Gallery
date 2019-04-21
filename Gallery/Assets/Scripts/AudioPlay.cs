using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{
 
    private AudioSource clipSource;
     
    // Start is called before the first frame update
    void Start()
    {
        clipSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clipSource.PlayDelayed(0);
        }
    }
}
