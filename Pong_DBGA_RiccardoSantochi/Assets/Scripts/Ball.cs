using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameManager gameManager;
    public Rigidbody2D rb2d;
    public float MaxStartAngle = 0.8f;
    public float movespeed = 1f;
    public float startX = 0f;
    public float maxstartY = 4f;

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

        if (goalzone)
        {
            gameManager.OnGoalZoneReached(goalzone.PlayerNumber);
            Resetball();
            FirstStep();
        }
    }




}
