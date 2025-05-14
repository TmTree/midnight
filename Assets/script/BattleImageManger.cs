using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattleImageManger : MonoBehaviour
{
    public Image situationImage; // 이미지 표시할 UI Image

    // 전투 이미지 스프라이트들
    public Sprite knightIdleSprite;
    public Sprite knightSuccessSprite;
    public Sprite knightFailSprite;
    public Sprite archerIdleSprite;
    public Sprite archerSuccessSprite;
    public Sprite archerFailSprite;

    public static BattleImageManger Instance;

    // private void Awake()
    // {
    //     // 싱글톤 패턴
    //     if (Instance == null)
    //         Instance = this;
    // }
    
    private void Awake()
    {
        Instance = this;
        Debug.Log(Instance + "============this");
        DontDestroyOnLoad(gameObject); // 원한다면 씬 전환 시에도 유지
        Debug.Log(this + "============this");
    }

    // 이미지 시퀀스를 처리하는 함수
    public void PlayTurnImageSequence(string player1, string player2, int dice1, int dice2)
    {
        if (situationImage == null)
            return;
        // idle 이미지부터 시작
        StartCoroutine(PlayTurnSequenceCoroutine(player1, player2, dice1, dice2));
    }

    // 이미지 시퀀스 코루틴 (idle → 성공/실패)
    private IEnumerator PlayTurnSequenceCoroutine(string player1, string player2, int dice1, int dice2)
    {
        // Player1 (기사)의 idle 이미지 표시
        situationImage.sprite = knightIdleSprite;
        Debug.Log("situation image" + situationImage.sprite.name) ;
        yield return new WaitForSeconds(3f); // 1초 동안 idle 이미지 유지
        

        // Player1 (기사)의 성공/실패 이미지 표시
        situationImage.sprite = (dice1 > 3) ? knightSuccessSprite : knightFailSprite;
        yield return new WaitForSeconds(3f); // 1초 동안 결과 이미지 유지

        // Player2 (궁수)의 idle 이미지 표시
        situationImage.sprite = archerIdleSprite;
        yield return new WaitForSeconds(1f); // 1초 동안 idle 이미지 유지

        // Player2 (궁수)의 성공/실패 이미지 표시
        situationImage.sprite = (dice2 > 3) ? archerSuccessSprite : archerFailSprite;
        yield return new WaitForSeconds(1f); // 1초 동안 결과 이미지 유지
    }
}
