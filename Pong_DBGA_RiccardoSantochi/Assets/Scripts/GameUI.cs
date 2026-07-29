using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    public ScoreText scoreTextPlayer1, scoreTextPlayer2;
    public GameObject MenuObject;

    public TextMeshProUGUI winText;

    public System.Action onStartGame;
    public void UpdateScores(int ScorePlayer1, int ScorePlayer2)// con questo metodo aggiorno il valore dei punteggi
    {
        scoreTextPlayer1.SetScore(ScorePlayer1);
        scoreTextPlayer2.SetScore(ScorePlayer2);
    }


    public void OnPlayGameButtonClicked()
    {
        MenuObject.SetActive(false);

        if (onStartGame != null)
        {
            onStartGame.Invoke();
        }

    }
    public void OnGameEnds(int winnerPlayer)
    {
        MenuObject.SetActive(true);
        winText.text = $"Player {winnerPlayer} wins!";
    }

    


}
