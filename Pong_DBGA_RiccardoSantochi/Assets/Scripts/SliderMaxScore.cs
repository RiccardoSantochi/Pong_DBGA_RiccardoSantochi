using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SliderMaxScore : MonoBehaviour
{
   
    public Slider scoreSlider;
    public TMP_Text scoreText;
    public GameManager gameManager;

    private void Start()
    {
        /*Lo slider usa solo numeri interi
          l'evento "On Value Changed" può inviare il valore corrente come float
          indipendentemente dal fatto che la proprietà Numeri interi sia abilitata.*/
        scoreSlider.wholeNumbers = true;

        // Imposto lo slider sul valore iniziale del GameManager
        scoreSlider.value = gameManager.MaxScore;

        // Aggiorno il testo
        UpdateMaxScore(scoreSlider.value);
    }

    public void UpdateMaxScore(float value)
    {
        // Converto il valore dello slider in int, dato che la variabile "value" dello slider rimarrebbe float, nonostante sia attivo il "Whole Numbers"
        int newMaxScore = Mathf.RoundToInt(value);

        // Modifico il MaxScore del GameManager
        gameManager.MaxScore = newMaxScore;

        // Mostro il valore sullo schermo
        scoreText.text = "Max score: " + newMaxScore;
    }
}

