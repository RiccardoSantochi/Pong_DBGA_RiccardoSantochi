using UnityEngine;

using UnityEngine.InputSystem;

public class Paddle : MonoBehaviour
{
    /* Riferimento al Rigidbody2D della paddle.
     Serve per modificarne la velocità.*/
    public Rigidbody2D rb;

    // Indicoi quale giocatore controlla questa paddle:
    // 1 = giocatore sinistro
    // 2 = giocatore destro
    public int PlayerNumber;

    
    public float moveSpeedPaddle = 2f;

    private void Update()
    {
        // Direzione del movimento:
        // 1 = verso l'alto
        // -1 = verso il basso
        // 0 = paddle ferma
        float movement = 0f;

        // Controllo che Unity abbia trovato una tastiera.
        // Se non esiste, interrompo il metodo.
        if (Keyboard.current == null)
        {
            return;
        }

        // Controlli del Player1.
        if (PlayerNumber == 1)
        {
            // Tasto W, movimento verso l'alto.
            if (Keyboard.current.wKey.isPressed)
            {
                movement = 1f;
            }

            // Tasto S, movimento verso il basso.
            if (Keyboard.current.sKey.isPressed)
            {
                movement = -1f;
            }
        }

        // Controlli Player2.
        if (PlayerNumber == 2)
        {
            //  Freccia su, movimento verso l'alto.
            if (Keyboard.current.upArrowKey.isPressed)
            {
                movement = 1f;
            }

            // Freccia giù, movimento verso il basso.
            if (Keyboard.current.downArrowKey.isPressed)
            {
                movement = -1f;
            }
        }

        // Salvo la velocità attuale del Rigidbody2D.
        Vector2 velocity = rb.linearVelocity;

        // Modifico soltanto la velocità verticale.
        velocity.y = movement * moveSpeedPaddle;

        // Applico la nuova velocità alla paddle.
        rb.linearVelocity = velocity;
    }
}
