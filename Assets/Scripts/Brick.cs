using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Brick : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }
    public Sprite[] states;
    public int health { get; private set; }
    public int point = 100;
    public bool unbreakable;

    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component missing!", this);
        }
    }

    private void Start()
    {
        ResetBrick();
    }

    public void ResetBrick()
    {
        gameObject.SetActive(true);

        if (!unbreakable)
        {
            this.health = states.Length;
            if (states.Length > 0 && spriteRenderer != null)
            {
                this.spriteRenderer.sprite = states[health - 1];
            }
        }
    }

    private void Hit()
    {
        if (this.unbreakable)
        {
            return;
        }

        this.health--;

        if (this.health <= 0)
        {
            this.gameObject.SetActive(false);
        } else
        {
            this.spriteRenderer.sprite = this.states[this.health - 1];
        }

        FindObjectOfType<GameManager>().Hit(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            Hit();
        }
    }
}
