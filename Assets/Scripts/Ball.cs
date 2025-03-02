using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rBody { get; private set; }
    [SerializeField] float speed = 500f;

    private void Awake()
    {
        this.rBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        ResetBall();
    }

    public void ResetBall()
    {
        this.transform.position = Vector2.zero;
        this.rBody.velocity = Vector2.zero;

        Invoke(nameof(SetRandomTrajectory), 1f);
    }

    private void SetRandomTrajectory()
    {
        Vector2 force = Vector2.zero;
        force.x = Random.Range(-1f, 1f);
        force.y = -1f;
        this.rBody.AddForce(force.normalized * speed);
    }
}
