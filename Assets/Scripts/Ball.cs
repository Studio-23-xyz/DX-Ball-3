using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 10f;
    public float yOffset = 0.5f; 

    private bool isLaunched = false;
    private Transform paddleTransform; 

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        paddleTransform = GameObject.FindGameObjectWithTag("Paddle").transform;
        ResetBall();
    }

    private void Update()
    {
        if (!isLaunched && Input.GetKeyDown(KeyCode.Space))
        {
            LaunchBall();
        }
    }

    private void FixedUpdate()
    {
        if (isLaunched)
        {
            rb.velocity = rb.velocity.normalized * speed;
        }
        else
        {
            StickToPaddle();
        }
    }

    public void ResetBall()
    {
        rb.velocity = Vector2.zero;
        isLaunched = false;
        StickToPaddle();
    }

    private void StickToPaddle()
    {
        transform.position = new Vector2(paddleTransform.position.x, paddleTransform.position.y + yOffset);
    }

    private void LaunchBall()
    {
        isLaunched = true;
        rb.AddForce(Vector2.up * speed, ForceMode2D.Impulse);
    }
}
