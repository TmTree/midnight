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
    public static string textResultSummary;                     // 요약 (엔딩용)
    
    public static BattleResult battleResult;

    public static TextUploader Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject); // 씬 전환에도 유지하고 싶다면 추가
    }

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
            Debug.Log($"Response {request.downloadHandler.text}");
            BattleResponse response = JsonUtility.FromJson<BattleResponse>(jsonResponse);

            // 2차 파싱 (중첩 JSON 문자열)
            //BattleResult battleResult = JsonUtility.FromJson<BattleResult>(response.result);

            battleResult = JsonUtility.FromJson<BattleResult>(response.result);
            
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
                $"{response.player1}: {turn.player1}\n" +
                $"dice1: {turn.dice1}\n" +
                $"{response.master}: {turn.master1}\n" +
                $"{response.monster}: {turn.monster}\n" +
                $"{response.player2}: {turn.player2}\n" +
                $"dice2: {turn.dice2}\n" +
                $"{response.master}: {turn.master2}\n"
            ));

            turnLines = fullTurns.Split('\n').ToList();
            textResultSummary = battleResult.resultSummary.ToString();

            Debug.Log("▶ 전투 대사:\n" + fullTurns);
            Debug.Log("🏁 요약:\n" + textResultSummary);
            
// 첫 번째 턴 기준으로 dice1, dice2 추출
            if (battleResult != null && battleResult.turns != null && battleResult.turns.Count > 0)
            {
                var firstTurn = battleResult.turns[0];
                int.TryParse(firstTurn.dice1, out int dice1);
                int.TryParse(firstTurn.dice2, out int dice2);

                // 이미지 업데이트 - 시퀀스 기반 (idle → 성공/실패)
                // BattleImageManger.Instance?.PlayTurnImageSequence(response.player1, response.player2, dice1, dice2);
                if (BattleImageManger.Instance != null)
                {
                    BattleImageManger.Instance.PlayTurnImageSequence(response.player1, response.player2, dice1, dice2);
                }
                else
                {
                    Debug.Log("에러==============================");
                }
            }


        }
    }

    [System.Serializable]
    public class TextData
    {
        public string content;
    }
}