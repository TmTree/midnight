// using UnityEngine;
// using UnityEngine.Networking;
// using TMPro;
// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
//
// [System.Serializable]
// public class BattleResponse
// {
//     public string situation;
//     public string player1;
//     public string player2;
//     public string monster;
//     public string master;
//     public string result;
// }
//
//
// public class TextUploader : MonoBehaviour
// {
//     public TMP_Text responseText; // 서버 응답 출력 텍스트
//     public string apiUrl = "http://192.168.0.75:8000";
//     public List<string> arr = new List<string>();
//         
//     public void UploadText(string userText)
//     {
//         // 요청 바디 만들기
//         string jsonBody = JsonUtility.ToJson(new TextData { content = userText });
//
//         StartCoroutine(SendPostRequest(jsonBody));
//     }
//
//     IEnumerator SendPostRequest(string jsonBody)
//     {
//         UnityWebRequest request = new UnityWebRequest(apiUrl + "receive-text", "POST");
//
//         byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);
//         request.uploadHandler = new UploadHandlerRaw(bodyRaw);
//         request.downloadHandler = new DownloadHandlerBuffer();
//         request.SetRequestHeader("Content-Type", "application/json");
//
//         Debug.Log("📤 Request Body: " + jsonBody);
//
//         yield return request.SendWebRequest();
//
//         if (request.result != UnityWebRequest.Result.Success)
//         {
//             Debug.LogError("❌ 요청 실패: " + request.error);
//             if (responseText != null)
//                 responseText.text = "요청 실패: " + request.error;
//         }
//         else
//         {
//             Debug.Log("✅ 서버 응답: " );
//             if (responseText != null)
//                 responseText.text = request.downloadHandler.text;
//         }
//         
//         
//         arr = responseText.text.Split("\n" ).ToList();
//         for (int i = 0; i < arr.Count; i++)
//         {
//             Debug.Log(arr[i]);
//         }
//     }
//
//     [System.Serializable]
//     public class TextData
//     {
//         public string content;
//     }
// }
// using UnityEngine;
// using UnityEngine.Networking;
// using TMPro;
// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
//
// [System.Serializable]
// public class BattleResponse
// {
//     public string situation;
//     public string player1;
//     public string player2;
//     public string monster;
//     public string master;
//     public string result;
// }
//
// [System.Serializable]
// public class BattleResult
// {
//     public string situation;
//     public List<Turn> turns;
//     public string result_summary;
// }
//
// [System.Serializable]
// public class Turn
// {
//     public string player1;
//     public string dice1;
//     public string master1;
//     public string monster;
//     public string player2;
//     public string dice2;
//     public string master2;
// }
//
//
// public class TextUploader : MonoBehaviour
// {
//     [Header("UI References")]
//     public TMP_Text situationText;   // 상황 텍스트 (처음 씬에서 출력)
//     public TMP_Text monsterText;     // 몬스터 설명 텍스트
//     public TMP_Text masterText;      // 마스터 설명 텍스트
//
//     [Header("Server")]
//     public string apiUrl = "http://192.168.0.75:8000/";
//
//     [Header("Parsed Result")]
//     public List<string> turnLines = new List<string>();     // 전투 대사 줄별 저장
//     public static string fullTurns;                         // 다음 씬에 넘길 전투 텍스트
//     public static string resultSummary;                     // 결과 요약 (엔딩용)
//
//     public void UploadText(string userText)
//     {
//         string jsonBody = JsonUtility.ToJson(new TextData { content = userText });
//         StartCoroutine(SendPostRequest(jsonBody));
//     }
//
//     IEnumerator SendPostRequest(string jsonBody)
//     {
//         UnityWebRequest request = new UnityWebRequest(apiUrl + "receive-text", "POST");
//         byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);
//         request.uploadHandler = new UploadHandlerRaw(bodyRaw);
//         request.downloadHandler = new DownloadHandlerBuffer();
//         request.SetRequestHeader("Content-Type", "application/json");
//
//         Debug.Log("📤 전송 내용: " + jsonBody);
//         yield return request.SendWebRequest();
//
//         if (request.result != UnityWebRequest.Result.Success)
//         {
//             Debug.LogError("❌ 요청 실패: " + request.error);
//             if (situationText != null)
//                 situationText.text = "서버 오류: " + request.error;
//         }
//         else
//         {
//             Debug.Log("✅ 서버 응답 수신");
//
//             // JSON 파싱
//             string jsonResponse = request.downloadHandler.text;
//             BattleResponse response = JsonUtility.FromJson<BattleResponse>(jsonResponse);
//
//             // 상황 텍스트 출력
//             if (situationText != null)
//                 situationText.text = response.situation;
//
//             if (monsterText != null)
//                 monsterText.text = response.monster;
//
//             if (masterText != null)
//                 masterText.text = response.master;
//
//             // 결과 텍스트 분리
//             string[] parts = response.result.Split(new string[] { "---" }, System.StringSplitOptions.None);
//
//             if (parts.Length >= 1)
//             {
//                 string battlePart = parts[0];
//                 fullTurns = ExtractTurnOnly(battlePart);
//                 turnLines = fullTurns.Split('\n').ToList();
//             }
//
//             if (parts.Length >= 2)
//             {
//                 resultSummary = parts[1].Trim();
//             }
//
//             Debug.Log("▶ 전투 대사:\n" + fullTurns);
//             Debug.Log("🏁 요약:\n" + resultSummary);
//         }
//     }
//
//     private string ExtractTurnOnly(string rawText)
//     {
//         int index = rawText.IndexOf("#1턴");
//         if (index >= 0)
//         {
//             return rawText.Substring(index).Trim();
//         }
//         return rawText.Trim(); // 혹시나 턴 정보가 없을 경우 전체 반환
//     }
//
//     [System.Serializable]
//     public class TextData
//     {
//         public string content;
//     }
// }
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class BattleResponse
{
    public string situation;
    public string player1;
    public string player2;
    public string monster;
    public string master;
    public string result; // 이건 실제로 JSON 문자열
}

[System.Serializable]
public class BattleResult
{
    public string situation;
    public List<Turn> turns;
    public string result_summary;
}

[System.Serializable]
public class Turn
{
    public string player1;
    public string dice1;
    public string master1;
    public string monster;
    public string player2;
    public string dice2;
    public string master2;
}

public class TextUploader : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text situationText;   // 상황 텍스트
    public TMP_Text monsterText;     // 몬스터 설명
    public TMP_Text masterText;      // 마스터 설명

    [Header("Server")]
    public string apiUrl = "http://192.168.0.75:8000/";

    [Header("Parsed Result")]
    public List<string> turnLines = new List<string>();     // 줄 단위 대사
    public static string fullTurns;                         // 전체 대사
    public static string resultSummary;                     // 요약 (엔딩용)

    public void UploadText(string userText)
    {
        string jsonBody = JsonUtility.ToJson(new TextData { content = userText });
        StartCoroutine(SendPostRequest(jsonBody));
    }

    IEnumerator SendPostRequest(string jsonBody)
    {
        UnityWebRequest request = new UnityWebRequest(apiUrl + "receive-text", "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        Debug.Log("📤 전송 내용: " + jsonBody);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("❌ 요청 실패: " + request.error);
            if (situationText != null)
                situationText.text = "서버 오류: " + request.error;
        }
        else
        {
            Debug.Log("✅ 서버 응답 수신");

            // 1차 파싱
            string jsonResponse = request.downloadHandler.text;
            BattleResponse response = JsonUtility.FromJson<BattleResponse>(jsonResponse);

            // 2차 파싱 (중첩 JSON 문자열)
            BattleResult battleResult = JsonUtility.FromJson<BattleResult>(response.result);

            // 상황 설명 출력 (더 상세한 설명이므로 이걸 사용)
            if (situationText != null)
                situationText.text = battleResult.situation;

            if (monsterText != null)
                monsterText.text = response.monster;

            if (masterText != null)
                masterText.text = response.master;

            // 턴별 출력 생성
            fullTurns = string.Join("\n", battleResult.turns.Select((turn, i) =>
                $"# {i + 1}턴\n" +
                $"{response.player1}: {turn.player1} (🎲 {turn.dice1})\n" +
                $"{response.master}: {turn.master1}\n" +
                $"{response.monster}: {turn.monster}\n" +
                $"{response.player2}: {turn.player2} (🎲 {turn.dice2})\n" +
                $"{response.master}: {turn.master2}\n"
            ));

            turnLines = fullTurns.Split('\n').ToList();
            resultSummary = battleResult.result_summary;

            Debug.Log("▶ 전투 대사:\n" + fullTurns);
            Debug.Log("🏁 요약:\n" + resultSummary);
        }
    }

    [System.Serializable]
    public class TextData
    {
        public string content;
    }
}
