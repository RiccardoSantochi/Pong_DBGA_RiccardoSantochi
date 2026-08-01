using UnityEngine;

public class GameManager : MonoBehaviour
{

   /*Essendo static, la variabile "instance" è condivisa
    da tutte le istanze della classe GameManager.
    Nel progetto viene usata per conservare il riferimento
    all'unico GameManager principale.
    Gli altri script possono accedere al GameManager tramite: GameManager.instance*/
    public static GameManager instance;
    
    public int ScorePlayer1, ScorePlayer2;
    public GameObject ball;

    //Delegate usato come evento di reset
    //Lo richiamo per eseguire piu metodi registrati.
    public System.Action onReset;

    public int MaxScore = 10;

    public GameUI gameUI;
    public GameAudioManager GameAudio;

    public CameraShake ScreenShake;

    //Appena il GameObject è inizializzato Awake viene eseguito
    //Uso questo metodo in modo tale che,
    //se ci sono dei duplicati, li distruggo
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

    /*Quando il GameManager viene distrutto,
    rimuove il metodo OnStartGame dall'evento della GameUI*/
    private void OnDestroy()
    {
        gameUI.onStartGame -= OnStartGame;
    }


    /* quando la pallina entra in una delle due zone goal
       assegno il punteggio in base al PlayerNumber */
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
        //Aggiorno punteggi UI
        gameUI.UpdateScores(ScorePlayer1, ScorePlayer2);

        //Controllo se uno dei due ha vinto
        CheckWinner();
    }

    /*Controllo se uno dei giocatori
    ha raggiunto il punteggio massimo.*/
    private void CheckWinner()
    {
        int winnerPlayer = 0;

        if (ScorePlayer1 >= MaxScore)
        {
            winnerPlayer = 1;
        }
        else if (ScorePlayer2 >= MaxScore)
        {
            winnerPlayer = 2;
        }
        else
        {
            winnerPlayer = 0;
        }

        /*Se il "winnerPlayer" è diverso da zero
        allora è stato trovato un vincitore.*/
        if (winnerPlayer != 0)
        {
            //Comunico alla GameUI chi ha vinto.
            gameUI.OnGameEnds(winnerPlayer);
            GameAudio.PlayWinSound();
            ball.SetActive(false);
        }
        /*Controllo se non ci sono ancora vincitori 
        e se "onReset" contiene almeno un metodo*/
        else if (onReset != null)
        {
            /*Eseguo tutti i metodi iscritti al delegate
            quindi riposiziono e rilancio la pallina.*/
            onReset.Invoke();
        }
    }

    //Imposto i punteggi di entrambi i player a zero.
    private void OnStartGame()
    {
        ScorePlayer1 = 0;
        ScorePlayer2 = 0;

        // Attivo la pallina e aggiorno i testi dei punteggi.
        ball.SetActive(true);
        gameUI.UpdateScores(ScorePlayer1, ScorePlayer2);
    }
}
