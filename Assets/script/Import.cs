using TMPro;
using UnityEngine;

public class MessageBox : MonoBehaviour
{
    public TMP_InputField inputField;

    void Start()
    {
        // 초기화 시 리스너 등록
        inputField.onEndEdit.AddListener(OnTextEntered);
    }
    
    void OnTextEntered(string text)
    {
        Debug.Log("사용자가 입력한 텍스트: " + text);

       
        inputField.text = ""; 
    }
}