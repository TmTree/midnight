using UnityEngine;
using UnityEngine.UI;

public class Diceroll : MonoBehaviour
{
    [Header("주사위 이미지")]
    public Image diceImage;

    [Header("주사위 숫자별 스프라이트 (1~6)")]
    public Sprite[] diceSprites; // 인덱스 0 = 주사위 1, 인덱스 5 = 주사위 6

    /// <summary>
    /// 주사위 숫자(1~6)를 받아 해당 이미지 표시
    /// </summary>
    public void SetDice(int value)
    {
        if (value < 1 || value > 6)
        {
            Debug.LogWarning("⚠️ 주사위 값은 1~6 사이여야 합니다: " + value);
            return;
        }

        if (diceSprites == null || diceSprites.Length < 6)
        {
            Debug.LogError("❌ diceSprites가 비어있거나 부족합니다.");
            return;
        }

        diceImage.sprite = diceSprites[value - 1];
    }
}