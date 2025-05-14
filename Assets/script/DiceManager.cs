using System.Collections;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    public Diceroll player1DiceUI;
    public Diceroll player2DiceUI;
    public UnityEngine.UI.Image diceImage; // 오른쪽 상단 Dice UI 이미지

    public Sprite[] diceSprites; // 1~6 주사위 스프라이트 배열 (인덱스 0 = 1, ..., 인덱스 5 = 6)

    private int currentTurnIndex = 0;
    private Coroutine rollingCoroutine;

    public void ShowDiceForCurrentTurn()
    {
        if (TurnManager.Instance == null || TurnManager.Instance.AllTurns.Count == 0)
        {
            Debug.LogWarning("⚠️ Turn 데이터가 없습니다.");
            return;
        }

        if (currentTurnIndex >= TurnManager.Instance.AllTurns.Count)
        {
            Debug.Log("🎯 모든 턴이 끝났습니다.");
            return;
        }

        Turn turn = TurnManager.Instance.AllTurns[currentTurnIndex];

        // player1 주사위 표시
        if (int.TryParse(turn.dice1, out int dice1Value))
        {
            player1DiceUI.SetDice(dice1Value);
            if (rollingCoroutine != null) StopCoroutine(rollingCoroutine);
            rollingCoroutine = StartCoroutine(AnimateDice(dice1Value));
        }

        // player2 주사위 표시
        if (int.TryParse(turn.dice2, out int dice2Value))
        {
            player2DiceUI.SetDice(dice2Value);
        }

        Debug.Log($"🎲 Turn {currentTurnIndex + 1}의 주사위를 표시했습니다.");
    }

    public void NextTurn()
    {
        currentTurnIndex++;
        ShowDiceForCurrentTurn();
    }

    public void ResetTurns()
    {
        currentTurnIndex = 0;
        ShowDiceForCurrentTurn();
    }

    private IEnumerator AnimateDice(int finalDiceValue)
    {
        float duration = 2f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            int randomDice = Random.Range(1, 7);
            diceImage.sprite = diceSprites[randomDice - 1];
            yield return new WaitForSeconds(0.1f);
            elapsed += 0.1f;
        }

        // 최종 주사위 값으로 고정
        diceImage.sprite = diceSprites[finalDiceValue - 1];
    }
}
