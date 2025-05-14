using TMPro;
using UnityEngine;
using System.Collections;

public class EndingManager : MonoBehaviour
{
    public TMP_Text textUI;
    public float typingSpeed = 0.03f;
    private void Start()
    {
        StartCoroutine(ShowResultSummaryTyping());
    }

    public void Enndingbutton()
    {
        Application.Quit();
    }

    IEnumerator ShowResultSummaryTyping()
    {
        // string summary = TurnManager.Instance?.AllResult?.textUploader.battleResult.resultSummary;
        string summary = TextUploader.battleResult?.resultSummary;
        
        if (string.IsNullOrEmpty(summary))
        {
            textUI.text = "전투 결과가 없습니다.";
            yield break;
        }

        textUI.text = "";
        foreach (char c in summary)
        {
            textUI.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}