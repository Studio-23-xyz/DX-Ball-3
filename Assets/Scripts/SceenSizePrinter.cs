using UnityEngine;

public class ScreenSizePrinter : MonoBehaviour
{
    private BoxCollider2D boxCollider;

    public static ScreenSizePrinter Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();

        if (boxCollider == null)
        {
            Debug.LogError("No BoxCollider2D found on this GameObject.");
            return;
        }

        UpdateColliderSize();
    }

    public float Width
    {
        get
        {
            return boxCollider.size.x * transform.localScale.x;
        }
    }

    public float Height
    {
        get
        {
            return boxCollider.size.y * transform.localScale.y; 
        }
    }

    private void UpdateColliderSize()
    {
        Camera mainCamera = Camera.main;

        if (mainCamera != null)
        {
            float screenHeight = 2f * mainCamera.orthographicSize;
            float screenWidth = screenHeight * mainCamera.aspect;

            boxCollider.size = new Vector2(screenWidth, screenHeight);
        }
    }

    void Update()
    {
        Debug.Log("Collider Width: " + Width);
        Debug.Log("Collider Height: " + Height);
    }
}
