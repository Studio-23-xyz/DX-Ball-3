using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Brick : MonoBehaviour
{
    public int Points = 100;
    public bool Unbreakable;
    public int MaxHealth = 5; 

    private SpriteRenderer _spriteRenderer;
    private int _currentHealth;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        ResetBrick();
    }

    public void ResetBrick()
    {
        gameObject.SetActive(true);
        _currentHealth = MaxHealth; 

        UpdateBrickTransparency();
    }

    private void Hit()
    {
        if (Unbreakable)
        {
            return;
        }

        _currentHealth--;

        if (_currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            UpdateBrickTransparency();
        }

        GameManager.Instance.OnBrickHit(this);
    }

    private void UpdateBrickTransparency()
    {
        float alpha = (float)_currentHealth / MaxHealth;
        Color color = _spriteRenderer.color;
        color.a = alpha; 
        _spriteRenderer.color = color;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string brickTag = gameObject.tag;
        string ballTag = collision.gameObject.tag;

        if (brickTag == ballTag.Replace("Ball", "Brick"))
        {
            Hit();
        }
    }
}
