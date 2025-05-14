using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class startcontrol : MonoBehaviour
{
    public void startbutton()
    {
        SceneManager.LoadScene("main2");
    }

    public void exitbutton()
    {
        Application.Quit();
    }
}