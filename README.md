# Pong_DBGA_RiccardoSantochi
Repository del test pratico Pong per il corso di Game Programming alla DBGA 2026/2027

CONTEGGIO ORE SVILUPPO: 7-9 ore

DOCUMENTAZIONE DEL PROGETTO PONG 

Il progetto è una versione 2D di Pong, sviluppata in Unity 6.5(6000.5.5f1) 

Il giocatore controlla le racchette tramite tastiera e può scegliere tra modalità Player vs Player e Player vs AI.

Gli script principali del progetto sono "Paddle" , "Ball" e "GameManager":

- Paddle gestisce il movimento delle racchette, gli input e il comportamento dell’intelligenza artificiale.
L’AI segue la posizione verticale della pallina solo quando questa si muove nella sua direzione, altrimenti
si sposterà gradualmente alla posizione centrale del campo.
Ho anche aggiunto delle "AiDeadZone" che cerca di rendere il comportamento meno preciso e più naturale.

- Ball invece gestisce controlla posizione, lancio iniziale, velocità, collisioni ed effetti della pallina.
A ogni collisione con una racchetta, la velocità aumenta fino a un valore massimo stabilito.
La direzione viene leggermente modificata in modo casuale per rendere le partite meno prevedibili.
Le collisioni attivano suoni, VFX e un effetto di vibrazione della telecamera.

- Il GameManager gestisce l'inizio partita, il punteggio, il controllo del vincitore e il reset della pallina.
 Utilizzo gli eventi "System.Action" che vengono utilizzati per gestire eventi tra gli script.
 Quando il GameManager richiama l’evento, tutti i metodi iscritti vengono eseguiti,
 per esempio il reset della pallina e delle racchette.

NB: Nel progetto, nella cartella "Scenes" è presente la scena "Pong_RECOVERY" creata per sicurezza dopo la sovrascrizione 
    della scena "Pong" durante la creazione del branch "release".
