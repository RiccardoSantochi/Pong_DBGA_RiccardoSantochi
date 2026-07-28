using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameManager gameManager;
    public Rigidbody2D rb2d;

    public float MaxStartAngle = 0.8f;
    public float movespeed = 8f;
    public float startX = 0f;
    public float maxstartY = 4f;
    public float BallSpeedMultiplier = 1.1f;

    public float minSpeed = 8f;
    public float maxSpeed = 18f;// se non l'aggiungevo, la ball ad una certa velocità si bloccava al centro della mappa
    private void Start()
    {
        FirstStep();
    }

    private void FirstStep()
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

        rb2d.linearVelocity = direction * movespeed;
    }

    private void Resetball()
    {
        float posY = Random.Range(-maxstartY, maxstartY);
        Vector2 position = new Vector2(startX,posY);
        transform.position = position;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GoalZone goalzone = collision.GetComponent<GoalZone>();

        if (goalzone != null)
        {
            gameManager.OnGoalZoneReached(goalzone.PlayerNumber);
            Resetball();
            FirstStep();
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
            // Calcolo la nuova velocità della pallina.
            //
            // rb2d.linearVelocity.magnitude
            // restituisce la velocità attuale della pallina
            // senza considerare la direzione.
            //
            // Per esempio:
            // una velocità di (8, 0) ha magnitude 8;
            // una velocità di (-8, 0) ha comunque magnitude 8.
            //
            // Moltiplico la velocità attuale per BallSpeedMultiplier,
            // così la pallina accelera dopo ogni colpo sul paddle.
            //
            // Mathf.Min confronta il risultato con maxSpeed
            // e sceglie il valore più piccolo.
            //
            // In questo modo la pallina accelera,
            // ma non può mai superare la velocità massima.

            float newSpeed = Mathf.Min(
                rb2d.linearVelocity.magnitude * BallSpeedMultiplier,
                maxSpeed
            );
            // Salvo la direzione attuale della pallina.
            //
            // rb2d.linearVelocity contiene sia:
            // - la direzione;
            // - la velocità.
            //
            // Con ".normalized" trasformo il vettore
            // in una direzione con lunghezza uguale a 1.
            //
            // Per esempio:
            // (8, 4) potrebbe diventare circa (0.89, 0.45).
            //
            // La direzione resta la stessa,
            // ma il valore della velocità viene temporaneamente eliminato.
            Vector2 direction = rb2d.linearVelocity.normalized;


            // Modifico la componente verticale della direzione.
            //
            // direction.y rappresenta il movimento verso l'alto o il basso.
            //
            // Random.Range(-0.3f, 0.3f)
            // genera un numero casuale compreso tra -0.3 e 0.3.
            //
            // Se il numero è positivo,
            // la pallina viene inclinata un po' più verso l'alto.
            //
            // Se il numero è negativo,
            // la pallina viene inclinata un po' più verso il basso.
            //
            // Se il numero è vicino a zero,
            // la traiettoria cambia molto poco.
            //
            // Questo serve a rendere i rimbalzi meno prevedibili.
            direction.y += Random.Range(-0.3f, 0.3f);


            // Dopo aver modificato direction.y,
            // il vettore potrebbe non avere più lunghezza 1.
            //
            // Normalize corregge il vettore
            // mantenendo lo stesso angolo,
            // ma riportando la sua lunghezza a 1.
            //
            // Questo è importante perché vogliamo controllare
            // la velocità usando soltanto "newSpeed".
            direction.Normalize();


            // Applico finalmente la nuova velocità al Rigidbody2D.
            //
            // "direction" indica dove deve andare la pallina.
            // "newSpeed" indica quanto velocemente deve muoversi.
            //
            // Moltiplicandoli ottengo un vettore completo
            // che contiene sia direzione sia velocità.
            rb2d.linearVelocity = direction * newSpeed;

        }
    }

    private void FixedUpdate()
    {

        // Calcolo la velocità attuale della pallina
        float currentSpeed = rb2d.linearVelocity.magnitude;

        // Se la pallina è praticamente ferma,
        // non faccio nulla (evita problemi durante il reset).
        if (currentSpeed < 0.01f)
            return;

        // Se la velocità è scesa sotto il minimo,
        // la riporto al valore desiderato mantenendo
        // la stessa direzione.
        if (currentSpeed < minSpeed)
        {
            rb2d.linearVelocity =
                rb2d.linearVelocity.normalized * minSpeed;
        }
    }
}
