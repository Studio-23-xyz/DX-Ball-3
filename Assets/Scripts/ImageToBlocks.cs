using UnityEngine;

public class ImageToBlocks : MonoBehaviour
{
    public Texture2D Image;
    public GameObject RedBlockPrefab;
    public GameObject BlueBlockPrefab;
    public GameObject GreenBlockPrefab;

    public float BlockSize = 1.0f;

    void Start()
    {
        GenerateBlocksFromImage();
    }

    private void GenerateBlocksFromImage()
    {
        GameObject bricks = new GameObject("bricks");
        int width = Image.width;
        int height = Image.height;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color pixelColor = Image.GetPixel(x, y);

                GameObject blockToInstantiate = DeterminePrimaryColor(pixelColor);

                if (blockToInstantiate != null)
                {
                    BoxCollider2D boundbox = blockToInstantiate.GetComponent<BoxCollider2D>();
                    Vector3 position;

                    if (boundbox != null)
                    {
                        position = new Vector3(x * boundbox.size.x * blockToInstantiate.transform.localScale.x,
                                               y * boundbox.size.y * blockToInstantiate.transform.localScale.y,
                                               0);
                    }
                    else
                    {
                        position = new Vector3(x * BlockSize, y * BlockSize, 0);
                    }

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
