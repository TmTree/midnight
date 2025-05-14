using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayController : MonoBehaviour
{
    public TMP_Text textUI; // 텍스트 UI
    private int currentIndex = 0;

    // TextUploader에서 가져온 줄 리스트
    private List<string> lines => TextUploader.Instance.turnLines;

    // 버튼에 연결할 메서드
    public void PlayButton()
    {
        // 유효한 인덱스인지 확인
        if (lines == null || lines.Count == 0) return;
        if (currentIndex >= lines.Count)
        {
            SceneManager.LoadScene("ending");
            return;
        }

        textUI.text = lines[currentIndex];
        currentIndex++;
    }
}
