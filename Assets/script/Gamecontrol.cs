using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamecontrol : MonoBehaviour
{
    public GameObject main2CanvasGO;
    
    public GameObject mainManager;
    public GameObject gameManager;
    
    public void EndButton()
    {
        //SceneManager.LoadScene("main");
        main2CanvasGO.SetActive(false);
        mainManager.SetActive(true);
        gameManager.SetActive(true);
    }
}
