using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int ScorePlayer1, ScorePlayer2;

    public System.Action onReset;

    public int MaxScore = 10;

    public GameUI gameUI;

    private void Awake()
    {

        if (instance!=null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            gameUI.onStartGame += OnStartGame;
        }

     }

    private void OnDestroy()
    {
        gameUI.onStartGame -= OnStartGame;
    }


    public void OnGoalZoneReached(int PlayerNumber)
    {
        
        if (PlayerNumber == 1)
        {
            ScorePlayer1++;
        }

        if (PlayerNumber == 2)
        {
            ScorePlayer2++;
        }
        gameUI.UpdateScores(ScorePlayer1, ScorePlayer2);

        CheckWinner();
    }

    private void CheckWinner()
    {
        int winnerPlayer = 0;

        if (ScorePlayer1 == MaxScore)
        {
            winnerPlayer = 1;
        }
        else if (ScorePlayer2 == MaxScore)
        {
            winnerPlayer = 2;
        }
        else
        {
            winnerPlayer = 0;
        }

        if (winnerPlayer != 0)
        {
            gameUI.OnGameEnds(winnerPlayer);
        }
        else if (onReset != null)
        {
            onReset.Invoke();
        }
    }
    private void OnStartGame()
    {
        ScorePlayer1 = 0;
        ScorePlayer2 = 0;

        gameUI.UpdateScores(ScorePlayer1, ScorePlayer2);
    }
}
