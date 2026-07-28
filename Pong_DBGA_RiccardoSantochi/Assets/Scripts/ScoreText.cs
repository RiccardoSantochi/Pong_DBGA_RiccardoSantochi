using UnityEngine;
using TMPro; // uso questa direttiva per poter usare le classi della libreria TextMesh Pro


public class ScoreText : MonoBehaviour
{
    // Qui utilizzo la classe "textmeshProUGUI(UnityGraphicalUserInterface) perché il testo appartiene alla UI ovvero al Canvas"
    public TextMeshProUGUI Text;

    public void SetScore(int value)
    {
        // questo metodo imposta il testo visualizzato convertendo il numero in una stringa
        Text.text = value.ToString();
    }

    
}
