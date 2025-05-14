using UnityEngine;

public class DiceManager : MonoBehaviour
{
    public Diceroll player1DiceUI;
    public Diceroll player2DiceUI;

    private int currentTurnIndex = 0;

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
}