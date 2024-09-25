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
            //Debug.Log("ScreenSizePrinter Instance initialized.");
        }
        else
        {
            Debug.LogWarning("Multiple instances of ScreenSizePrinter detected. Destroying duplicate.");
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
            float width = boxCollider.size.x * transform.localScale.x;
            //Debug.Log($"Calculated Width: {width}");
            return width;
        }
    }

    public float Height
    {
        get
        {
            float height = boxCollider.size.y * transform.localScale.y;
            //Debug.Log($"Calculated Height: {height}");
            return height;
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

            //Debug.Log($"Updated BoxCollider2D size: {boxCollider.size} based on screen dimensions: {screenWidth}x{screenHeight}");
        }
        else
        {
            Debug.LogError("Main Camera not found. Cannot update BoxCollider2D size.");
        }
    }

    void Update()
    {
        //Debug.Log("Collider Width: " + Width);
        //Debug.Log("Collider Height: " + Height);
    }
}
