using UnityEngine;

public class Ball : MonoBehaviour
{

    public TrailRenderer ballTrail;//Variabile della scia che lascia la pallina dietro se.
    public Rigidbody2D rb;

    public float MaxStartAngle = 0.8f;
    private float startX = 0f;
    public float maxstartY = 3f;

    public float BallSpeedMultiplier = 1.1f;//Variabile per aumentare la velocità della pallina.

    public float minSpeed = 15f;
    public float maxSpeed = 18f;// se non l'aggiungevo, la ball ad una certa velocità si bloccava al centro della mappa

    public GameAudioManager audioManager;

    public ParticleSystem collisionVFX;
    private void Start()
    {
        GameManager.instance.onReset += ResetballPosition;
        GameManager.instance.gameUI.onStartGame += ResetBall;

        
    }


    private void ResetBall()
    {
        FirstBallShot();
        ResetballPosition();
    }
    private void FirstBallShot()
    {
        Vector2 direction;

        if (Random.value<0.5f)
        {
            direction = Vector2.left;
        }
        else
        {
            direction = Vector2.right;
        }

        direction.y = Random.Range(-MaxStartAngle, MaxStartAngle);

        rb.linearVelocity = direction * minSpeed;

        EmitterVFX(30);
    }

    private void ResetballPosition()
    {
        // Disattiva la scia
        ballTrail.emitting = false;

        float posY = Random.Range(-maxstartY, maxstartY);
        Vector2 position = new Vector2(startX, posY);
        transform.position = position;
          
        ballTrail.Clear(); // Cancella la vecchia scia
        ballTrail.emitting = true;// Riattiva la scia


        FirstBallShot();


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GoalZone goalzone = collision.GetComponent<GoalZone>();

        // Se entra nella zona goal
        if (collision.gameObject.CompareTag("Goal"))
        {
            audioManager.PlayScoreSound();
            GameManager.instance.ScreenShake.StartShake(0.054f, 0.2f);
        }
        if (goalzone != null)
        {
            GameManager.instance.OnGoalZoneReached(goalzone.PlayerNumber);
            //ResetballPosition();
            

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        /* Controllo se l'oggetto con cui ho colliso possiede
           lo script "Paddle".
           Se sto colpendo un muro o un altro oggetto, paddle sarà null.*/
        Paddle paddle = collision.collider.GetComponent<Paddle>();

        //controllo che la collisione sia avvenuta con un paddle
        if (paddle != null)
        {

            // Se colpisce una paddle
            if (collision.gameObject.CompareTag("Paddle"))
            {
                audioManager.PlayPaddleSound();
                EmitterVFX(19);
                GameManager.instance.ScreenShake.StartShake(0.056f, 0.05f);
            }

           
            float newSpeed = Mathf.Min(
                rb.linearVelocity.magnitude * BallSpeedMultiplier,
                maxSpeed
            );
           
            Vector2 direction = rb.linearVelocity.normalized;


            direction.y += Random.Range(-0.25f, 0.25f);


            
            direction.Normalize();


            
            rb.linearVelocity = direction * newSpeed;

        }
        // Se colpisce un muro
        else if (collision.gameObject.CompareTag("Wall"))
        {
            audioManager.PlayWallSound();
            EmitterVFX(10);
            GameManager.instance.ScreenShake.StartShake(0.077f, 0.05f);
        }
    }


    private void EmitterVFX(int amount)
    {
        collisionVFX.Emit(amount);
    }



    private void FixedUpdate()
    {

        // Calcolo la velocità attuale della pallina
        float currentSpeed = rb.linearVelocity.magnitude;

        // Se la pallina è praticamente ferma,
        // non faccio nulla (evita problemi durante il reset).
        if (currentSpeed < 0.01f)
            return;

        // Se la velocità è scesa sotto il minimo,
        // la riporto al valore desiderato mantenendo
        // la stessa direzione.
        if (currentSpeed < minSpeed)
        {
            rb.linearVelocity =
                rb.linearVelocity.normalized * minSpeed;
        }
    }
}
