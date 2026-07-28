using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int ScorePlayer1, ScorePlayer2;
    public ScoreText scoreTextLeft, scoreTextRight;
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
        UpdateScores();
    }


    private void UpdateScores()// con questo metodo aggiorno il valore dei punteggi
    {
        scoreTextLeft.SetScore(ScorePlayer1);
        scoreTextRight.SetScore(ScorePlayer2);
    }
}
