using UnityEngine;

public class ImageToBlocks : MonoBehaviour
{
    public Texture2D Image;
    public GameObject RedBlockPrefab;
    public GameObject BlueBlockPrefab;
    public GameObject GreenBlockPrefab;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        GenerateBlocksFromImage();
    }

    private void GenerateBlocksFromImage()
    {
        GameObject bricks = new GameObject("bricks");
        int width = Image.width;
        int height = Image.height;

        // Get the camera bounds
        float screenHeight = ScreenSizePrinter.Instance.Height;
        float screenWidth = ScreenSizePrinter.Instance.Width;

        // Determine the limiting factor (width or height)
        float imageAspectRatio = (float)width / height;
        float cameraAspectRatio = screenWidth / screenHeight;

        float prefabScale;

        if (imageAspectRatio > cameraAspectRatio)
        {
            // Image is wider than camera, limiting factor is width
            prefabScale = screenWidth / width / RedBlockPrefab.GetComponent<BoxCollider2D>().size.x;
        }
        else
        {
            // Image is taller than camera, limiting factor is height
            prefabScale = screenHeight / height / RedBlockPrefab.GetComponent<BoxCollider2D>().size.y;
        }

        // Resize prefabs and calculate positions
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color pixelColor = Image.GetPixel(x, y);

                GameObject blockToInstantiate = DeterminePrimaryColor(pixelColor);

                if (blockToInstantiate != null)
                {
                    // Set the prefab scale
                    blockToInstantiate.transform.localScale = new Vector3(prefabScale, prefabScale, 1);

                    // Positioning each block directly on the pixel coordinates
                    Vector3 position = new Vector3(
                        (x * prefabScale * RedBlockPrefab.GetComponent<BoxCollider2D>().size.x) - (screenWidth / 2) + (prefabScale * RedBlockPrefab.GetComponent<BoxCollider2D>().size.x / 2),
                        (y * prefabScale * RedBlockPrefab.GetComponent<BoxCollider2D>().size.y) - (screenHeight / 2) + (prefabScale * RedBlockPrefab.GetComponent<BoxCollider2D>().size.y / 2),
                        0
                    );

                    Instantiate(blockToInstantiate, position, Quaternion.identity, bricks.transform);
                }
            }
        }
    }

    private GameObject DeterminePrimaryColor(Color color)
    {
        float red = color.r;
        float green = color.g;
        float blue = color.b;

        if (red > green && red > blue)
        {
            return RedBlockPrefab;
        }
        else if (blue > red && blue > green)
        {
            return BlueBlockPrefab;
        }
        else if (green > red && green > blue)
        {
            return GreenBlockPrefab;
        }

        return null;
    }
}
