using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 7f;
    public Transform paddle;          // drag the Paddle here in the Inspector
    public float bottomLimit = -6f;

    Rigidbody2D rb;
    bool launched;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        ResetBall();
    }

    void Update()
    {
        if (GameManager.Instance.IsGameOver) return;

        if (!launched)
        {
            // Stick to the paddle until launch
            rb.position = new Vector2(paddle.position.x, paddle.position.y + 0.6f);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                launched = true;
                rb.linearVelocity = new Vector2(Random.Range(-0.5f, 0.5f), 1f).normalized * speed;
            }
        }
        else if (transform.position.y < bottomLimit)
        {
            GameManager.Instance.LoseLife();
            ResetBall();
        }
    }

    void FixedUpdate()
    {
        // Keep speed constant so the ball never slows down or speeds up
        if (launched && rb.linearVelocity.sqrMagnitude > 0.01f)
            rb.linearVelocity = rb.linearVelocity.normalized * speed;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // Aim the ball based on where it hit the paddle
        if (col.gameObject.GetComponent<PaddleController>() != null)
        {
            float paddleWidth = col.collider.bounds.size.x;
            float hitFactor = (transform.position.x - col.transform.position.x) / paddleWidth;
            Vector2 dir = new Vector2(hitFactor * 2f, 1f).normalized;
            rb.linearVelocity = dir * speed;
        }
    }

    void ResetBall()
    {
        launched = false;
        rb.linearVelocity = Vector2.zero;
    }
}