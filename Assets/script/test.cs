using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    public Image image;
    
    void Start()
    {
        BattleImageManger.Instance.knightIdleSprite = image.sprite;
    }

}
