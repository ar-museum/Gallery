using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseScene : MonoBehaviour
{
    public void changeIt(int i)
    { SceneManager.LoadScene(i); }
}
