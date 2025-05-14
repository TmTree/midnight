using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class MessageBox : MonoBehaviour
{
    public TMP_InputField inputField;
    public TextUploader textUploader; // 텍스트 업로더 연결

    void Start()
    {
        inputField.onEndEdit.AddListener(OnTextEntered);
    }

    void OnTextEntered(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return;

        Debug.Log("사용자가 입력한 텍스트: " + text);

        // 텍스트를 서버로 전송
        textUploader.UploadText(text);

        inputField.text = ""; // 입력창 초기화
        inputField.ActivateInputField(); // 포커스 유지
    }
}