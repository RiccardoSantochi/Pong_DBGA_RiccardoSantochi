using UnityEngine;
using TMPro; // uso questa direttiva per poter usare le classi della libreria TextMesh Pro.


public class ScoreText : MonoBehaviour
{
    //Utilizzo la classe "textmeshProUGUI(UnityGraphicalUserInterface) perché il testo appartiene alla UI ovvero al Canvas"
    public TextMeshProUGUI Text;


    //Questo metodo imposta il testo visualizzato convertendo il numero in una stringa.
    public void SetScore(int value)
    {
        
        Text.text = value.ToString();
    }

    
}
