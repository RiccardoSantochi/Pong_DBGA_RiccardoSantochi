using UnityEngine;

public class GameManager : MonoBehaviour
{

    //Con static creo una sola copia della variabile instance e la utilizzo per utti gli oggetti della classe.
    public static GameManager instance;
    
    public int ScorePlayer1, ScorePlayer2;
    public GameObject ball;

    public System.Action onReset;

    public int MaxScore = 10;

    public GameUI gameUI;
    public GameAudioManager GameAudio;

    public CameraShake ScreenShake;


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
            GameAudio.PlayWinSound();
            ball.SetActive(false);
        }
        else if (onReset != null)
        {
            onReset.Invoke();
        }
    }

    //Imposto i punteggi di entrambi i player a zero.
    private void OnStartGame()
    {
        ScorePlayer1 = 0;
        ScorePlayer2 = 0;

        // Attivo la pallina.
        ball.SetActive(true);
        gameUI.UpdateScores(ScorePlayer1, ScorePlayer2);
    }
}
