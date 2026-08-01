using UnityEngine;
using TMPro;// uso questa direttiva per poter usare le classi della libreria TextMesh Pro.


public class GameUI : MonoBehaviour
{
    public ScoreText scoreTextPlayer1, scoreTextPlayer2;
    public GameObject MenuObject;
    public GameObject SettingsMenu;
    public GameObject MainMenu;

    public TextMeshProUGUI winText;

    public Paddle AiRightPaddle;
    public TextMeshProUGUI switchModeText;

    // Evento chiamato quando inizia la partita
    public System.Action onStartGame;

    public void Start()
    {
        MainMenu.SetActive(true);
        SettingsMenu.SetActive(false);

        AiRightPaddle.isAi = false;
        switchModeText.text = "Player VS Player";
    }

    // Aggiorna i punteggi mostrati sullo schermo
    public void UpdateScores(int ScorePlayer1, int ScorePlayer2)
    {
        scoreTextPlayer1.SetScore(ScorePlayer1);
        scoreTextPlayer2.SetScore(ScorePlayer2);
    }



    public void OnSwitchModeButtonClicked()
    {
        AiRightPaddle.isAi = !AiRightPaddle.isAi;

        if (AiRightPaddle.isAi)
        {
            switchModeText.text = "Player VS AI";
        }
        else
        {
            switchModeText.text = "Player VS Player";
        }
    }

    // Viene chiamato quando si preme il pulsante Play
    public void OnPlayGameButtonClicked()
    {
        // Nasconde il menu
        MenuObject.SetActive(false);

        // Controlla che l'evento esista prima di eseguirlo
        if (onStartGame != null)
        {
            onStartGame.Invoke();
        }
    }

    public void OnSettingsButtonClicked()
    {
        //Scompare il MainMenu
        //Appare il SettingsMenu
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(true);
    }

    public void OnBackButtonClicked()
    {
        //Appare il MainMenu
        //Disattivo il SettingsMenu
        MainMenu.SetActive(true);
        SettingsMenu.SetActive(false);
    }

    // Viene chiamato quando si preme il pulsante Quit
    public void OnQuitButtonClicked()
    {
        // Chiude il gioco compilato
        Application.Quit();
    }

    // Viene chiamato quando un giocatore vince
    public void OnGameEnds(int winnerPlayer)
    {
        // Mostra di nuovo il menu
        MenuObject.SetActive(true);

        // Scrive quale giocatore ha vinto
        winText.text = $"Player {winnerPlayer} wins!";
    }

    public void OnvolumeChanged(float value)
    {
        AudioListener.volume = value;
    }


}
