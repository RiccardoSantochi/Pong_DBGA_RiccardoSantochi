using UnityEngine;

public class Ball : MonoBehaviour
{
    //Variabile della scia che lascia la pallina dietro di se.
    public TrailRenderer ballTrail;
    //Variabile fondamentale per controllare la velocità e la fisica della pallina.
    public Rigidbody2D rb;

    //La massima inclinazione verticale assegnata al tiro iniziale della pallina. 
    public float MaxStartAngle = 0.8f;
    private float startX = 0f;
    public float maxstartY = 2f;

    //Variabile per aumentare la velocità della pallina ogni volta che collide con una racchetta.
    public float BallSpeedMultiplier = 1.1f;

    public float BallSpeed = 10f;
    /* se aumentavo la velocità all'infinito con il Multiplier,
       ad una certa soglia la pallina si bloccava al centro della mappa.*/ 
    public float maxSpeed = 30f;

    public GameAudioManager audioManager;

    public ParticleSystem collisionVFX;
    private void Start()
    {
        /* Quando il GameManager richiama l'evento "onReset"
        dopo un goal, viene eseguito il reset della palla*/
        GameManager.instance.onReset += ResetBall;

        /*Quando la GameUI richiama "onStartGame" 
        viene eseguito il reset della pallina*/
        GameManager.instance.gameUI.onStartGame += ResetBall;

    }


    private void ResetBall()
    {
      //Richiamo le funzione per riposizionare e rilanciare la pallina.
        ResetballPosition();
        BallShot();
    }

    private void BallShot()
    {
        Vector2 direction;

        /* Uso la classe di Unity "Random.value" 
        per generare un numero randomico decimale tra 0 e 1.*/
        if (Random.value<0.5f)
        {
            //Imposto la direzione a sinistra.
            direction = new Vector2(-1,0);
        }
        else
        {
            //Imposto la direzione a destra.
            direction = new Vector2(1,0);
        }

        /* Ora imposto una direzione randomica sulla Y compresa tra il seguente intervallo 
          per evitare che il tiro sia sempre perfettamente orizzontale.*/
        direction.y = Random.Range(-MaxStartAngle, MaxStartAngle);

        /*Imposto la velovità iniziale nella direzione del vettore direction.
          - direction stabilisce la direzione;
          - BallSpeed stabilisce quanto velocemente
            deve muoversi la pallina.*/
        rb.linearVelocity = direction * BallSpeed;

        //Genero un effetto VFX richiamando la funzione.
        EmitterVFX(30);
    }

    private void ResetballPosition()
    {
        /* Disattiva la scia per evitare che venga disegnata
        durante il reset della posizione*/
        ballTrail.emitting = false;

        //Genero una posizione Y randomica compresa tra il seguente intervallo 
        float posY = Random.Range(-maxstartY, maxstartY);

        /*Creo la nuova posizione della pallina
          e la assegno la nuova posizione calcolata al suo Transform.*/
        Vector2 position = new Vector2(startX, posY);
        transform.position = position;
          
        ballTrail.Clear(); // Cancella la vecchia scia
        ballTrail.emitting = true;// Riattiva la scia


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        /*Controllo se l'oggetto con cui la pallina ha colliso possieda 
           lo script "GoalZOne".
           Se sto colpendo un muro o un altro oggetto, goalzone sarà null*/
        GoalZone goalzone = collision.GetComponent<GoalZone>();

        /*Controllo se l'oggetto colpito possiede il Tag "Goal".
          Se è true:
            -riproduco il suono del punto;
            -attivo la vibrazione della camera dal GameManager.
            -comunico al GameManager che è stato segnato un punto
             specificando a quale giocatore PlayerNumber deve essere assegnato*/
        if (collision.gameObject.CompareTag("Goal") && goalzone != null)
        {
            audioManager.PlayScoreSound();
            GameManager.instance.ScreenShake.StartShake(0.054f, 0.2f);
            GameManager.instance.OnGoalZoneReached(goalzone.PlayerNumber);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        /* Controllo se l'oggetto con cui ho colliso possiede
           lo script "Paddle".
           Se sto colpendo un muro o un altro oggetto, paddle sarà null.*/
        Paddle paddle = collision.collider.GetComponent<Paddle>();


        //controllo che la collisione sia avvenuta con una racchetta 
        //Controllo se l'oggetto colpito possiede il Tag "Paddle".
             
        if (paddle != null && collision.gameObject.CompareTag("Paddle"))
        {

           /*Se è true:
              -riproduco il suono del punto;
              -emetto dei particellari
              -attivo la vibrazione della camera dal GameManager.*/
            audioManager.PlayPaddleSound();
            EmitterVFX(19);
            GameManager.instance.ScreenShake.StartShake(0.056f, 0.05f);
            

          /*Calcolo la nuova velocità "BallSpeed" della pallina
            moltiplicandola per il multiplier.
            Uso:
            - struttura "Mathf.Min" che restituisce il valore piu piccolo tra i due parametri
            in modo tale da non superare la velocità massima impostata
            - .magnitude con cui ho la lunghezza del vettore,la velocità complessiva dell'oggetto senza considerare la direzione */
            float newSpeed = Mathf.Min(rb.linearVelocity.magnitude * BallSpeedMultiplier, maxSpeed);
           
            // Salvo la direzione della pallina, riportando la 
             //lunghezza del vettore a (1,0)
            Vector2 direction = rb.linearVelocity.normalized;

            //Imposto una nuova direzione dell'asse Y compreso randomicamente tra il seguente intervallo
            // in modo tale da variare l'angolazione del tiro, senza che sia sempre perfettamente orizzontale.
            direction.y += Random.Range(-0.25f, 0.25f);

            //Serve per garantire che la velocità finale sia davvero quella contenuta in "newSpeed"
            direction=direction.normalized;


            //Applico al Rigidbody2D la nuova velocità:
            rb.linearVelocity = direction * newSpeed;

        }
        /*Controllo se l'oggetto colpito possiede il Tag "Wall".
         Se è true:
           -riproduco il suono del punto;
           -emetto dei paritcellari
           -attivo la vibrazione della camera dal GameManager.*/
        else if (collision.gameObject.CompareTag("Wall"))
        {
            audioManager.PlayWallSound();
            EmitterVFX(10);
            GameManager.instance.ScreenShake.StartShake(0.077f, 0.05f);
        }
    }

    /*Metodo per emettere il numero 
     di particelle desiderato ad ogni impatto*/
    private void EmitterVFX(int amount)
    {
        collisionVFX.Emit(amount);
    }


    // Per la fisica uso FixedUpdate()
    private void FixedUpdate()
    {

        // Calcolo la velocità attuale della pallina
        float currentSpeed = rb.linearVelocity.magnitude;

        // Se la pallina è praticamente ferma,
        // non faccio nulla (evita problemi durante il reset).
        if (currentSpeed < 0.01f)
        {
            return;
        }
           
        // Se la velocità è scesa sotto il minimo,
        // la riporto al valore desiderato mantenendo
        // la stessa direzione.
        if (currentSpeed < BallSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * BallSpeed;
        }
    }
}
