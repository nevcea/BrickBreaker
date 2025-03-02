using UnityEngine;

public class Paddle : MonoBehaviour
{
    public Rigidbody2D rBody { get; private set; }
    public Vector2 direction { get; private set; }
    [SerializeField] float speed = 30f;
    [SerializeField] float maxBounceAngle = 75f;

    private void Awake()
    {
        this.rBody = GetComponent<Rigidbody2D>();
    }

    public void ResetPaddle()
    {
        this.transform.position = new Vector2(0f, this.transform.position.y);
        this.rBody.velocity = Vector2.zero;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            this.direction = Vector2.left;
        } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            this.direction = Vector2.right;
        } else { direction = Vector2.zero; }
    }

    private void FixedUpdate()
    {
        if (this.direction != Vector2.zero)
        {
            this.rBody.AddForce(this.direction * this.speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();

        if (ball != null)
        {
            Vector3 paddlePosition = this.transform.position;
            Vector2 contactPoint = collision.GetContact(0).point;
            
            float offset = paddlePosition.x - contactPoint.x;
            float width = collision.otherCollider.bounds.size.x / 2;

            float currentAngle = Vector2.SignedAngle(Vector2.up, ball.rBody.velocity);
            float bounceAngle = (offset / width) * this.maxBounceAngle;
            float newAngle = Mathf.Clamp(currentAngle + bounceAngle, -this.maxBounceAngle, this.maxBounceAngle);

            Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
            ball.rBody.velocity = rotation * Vector2.up * ball.rBody.velocity.magnitude;
        }
    }
}
