using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Paddle : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _direction;

    public float Speed = 30f;
    public float MaxBounceAngle = 75f;

    private PaddleInputActions _inputActions;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        Debug.Log("Paddle: Rigidbody2D component initialized.");

        _inputActions = new PaddleInputActions();
    }

    private void OnEnable()
    {
        _inputActions.Player.Move.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Player.Move.Disable();
    }

    private void Start()
    {
        ResetPaddle();
        Debug.Log("Paddle: Initialized and reset to start position.");
    }

    public void ResetPaddle()
    {
        _rb.velocity = Vector2.zero;
        transform.position = new Vector2(0f, transform.position.y);
        Debug.Log("Paddle: Position reset.");
    }

    private void Update()
    {
        //if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        //{
        //    _direction = Vector2.left;
        //    Debug.Log("Paddle: Moving left.");
        //}
        //else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        //{
        //    _direction = Vector2.right;
        //    Debug.Log("Paddle: Moving right.");
        //}
        //else
        //{
        //    _direction = Vector2.zero;
        //    //Debug.Log("Paddle: No movement.");
        //}
        _direction = _inputActions.Player.Move.ReadValue<Vector2>();
        _direction.y = 0;
    }

    private void FixedUpdate()
    {
        if (_direction != Vector2.zero)
        {
            _rb.AddForce(_direction * Speed);
            Debug.Log($"Paddle: Applying force in direction {_direction}, Speed: {Speed}");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.tag.Contains("Ball"))
        {
            return;
        }

        Rigidbody2D ball = collision.rigidbody;
        Collider2D paddle = collision.otherCollider;

        Vector2 ballDirection = ball.velocity.normalized;
        Vector2 contactDistance = paddle.bounds.center - ball.transform.position;

        float bounceAngle = (contactDistance.x / paddle.bounds.size.x) * MaxBounceAngle;
        ballDirection = Quaternion.AngleAxis(bounceAngle, Vector3.forward) * ballDirection;
        ball.velocity = ballDirection * ball.velocity.magnitude;

        Debug.Log($"Paddle: Collided with Ball. Contact distance: {contactDistance.x}, Bounce angle: {bounceAngle}");
    }
}
