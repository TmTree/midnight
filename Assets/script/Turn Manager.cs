using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public BattleResult AllResult { get; private set; }
    public void SetResult(BattleResult result)
    {
        AllResult = result;
        AllTurns = result.turns;
    }

    public static TurnManager Instance { get; private set; }

    public List<Turn> AllTurns { get; private set; } = new List<Turn>();

    private void Awake()
    {
        // 싱글톤 패턴
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // 씬 전환에도 유지하고 싶다면 추가
    }

    public void SetTurns(List<Turn> turns)
    {
        AllTurns = turns;
    }
}
[System.Serializable]
public class BattleResult
{
    public string situation;
    public List<Turn> turns;
    public string resultSummary;
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