using UnityEngine;

using UnityEngine.InputSystem;

public class Paddle : MonoBehaviour
{
    /* Riferimento al Rigidbody2D della paddle.
     Serve per modificarne la velocità.*/
    public Rigidbody2D rb;

    // Indico quale giocatore controlla questa paddle:
    // 1 = giocatore sinistro
    // 2 = giocatore destro
    public int PlayerNumber;


    public float moveSpeedPaddle = 2f;
    public float moveSpeedAi = 1f;


    public bool isAi;
    public Transform Ball;
    //Questa variabile è una zona di tolleranza attorno all posizione Y della paddle
    //ovvero, l'AI non si allineerà perfettamente al millimetro con la pallina.
    public float AiDeadZone = 0.15f;

    private Rigidbody2D rbBall;

    private void Start()
    {
        if (Ball != null)
        {
            //Cerco il componente "Rigidbody2D"
            //dal GameObject della pallina.
            rbBall = Ball.GetComponent<Rigidbody2D>();
        }
    }

    private void Update()
    {

        float movement = 0f;
        // Salvo la velocità attuale del Rigidbody2D.
        Vector2 velocity = rb.linearVelocity;

        //Controllo se il Player è controllato dal computer
        if (isAi == true)
        {
            movement = GetAiMovement();

            velocity.y = movement * moveSpeedAi;

        }
        else
        {
            movement = GetPlayerMovement();
            // Modifico soltanto la velocità verticale.
            velocity.y = movement * moveSpeedPaddle;

        }


        // Applico la nuova velocità alla paddle.
        rb.linearVelocity = velocity;
    }


    private float GetPlayerMovement()
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
            return 0f;
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

        return movement;

    }

    private float GetAiMovement()
    {

        float movement = 0f;

        if (Ball == null || rbBall == null)
        {
            return 0f;
        }
        /*
            * Se la palla si muove verso destra,
            * quindi verso la paddle AI, l'AI la segue.
            */
        if (rbBall.linearVelocity.x > 0f)
        {
            // Se la palla è sopra, la paddle sale.
            if (Ball.position.y > transform.position.y + AiDeadZone)
            {
                movement = 1f;
            }

            // Se la palla è sotto, la paddle scende.
            if (Ball.position.y < transform.position.y - AiDeadZone)
            {
                movement = -1f;
            }
        }
        else
        {
            /*
             * Se la palla si muove verso sinistra oppure è ferma,
             * l'AI torna gradualmente al centro del campo.
             */

            // La paddle è sopra il centro: scende.
            if (transform.position.y > AiDeadZone)
            {
                movement = -1f;
            }

            // La paddle è sotto il centro: sale.
            if (transform.position.y < -AiDeadZone)
            {
                movement = 1f;
            }
        }

        return movement;

    }
}


