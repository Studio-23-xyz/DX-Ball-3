using UnityEngine;

public class GridLayout : MonoBehaviour
{
    public GameObject[] prefabs; // Array of prefabs to use for layout
    public int numRows = 4; // Number of rows in the grid
    public int numCols = 8; // Number of columns in the grid
    public float xOffset = 0f; // X offset for the grid
    public float yOffset = 0f; // Y offset for the grid
    public float xPadding = 1f; // Padding between columns
    public float yPadding = 1f; // Padding between rows
    public Transform parentTransform; // Parent transform to hold instantiated prefabs

    private void Start()
    {
        if (parentTransform == null)
        {
            Debug.LogError("Parent Transform is not assigned. Please assign a parent Transform.");
            return;
        }

        UpdateGrid();
    }

    public void UpdateGrid()
    {
        // Clear existing prefabs
        foreach (Transform child in parentTransform)
        {
            Destroy(child.gameObject);
        }

        // Make sure prefabs array is not empty
        if (prefabs.Length == 0)
        {
            Debug.LogError("Prefab array is empty. Please assign prefabs.");
            return;
        }

        for (int row = 0; row < numRows; row++)
        {
            for (int col = 0; col < numCols; col++)
            {
                int prefabIndex = GetPrefabIndex(row, col);
                if (prefabIndex >= 0 && prefabIndex < prefabs.Length)
                {
                    GameObject prefab = prefabs[prefabIndex];

                    // Calculate position
                    Vector3 size = prefab.GetComponent<Renderer>().bounds.size;
                    float xPos = col * (size.x + xPadding) + xOffset;
                    float yPos = -row * (size.y + yPadding) - yOffset; // Negative for downward rows
                    Vector3 position = new Vector3(xPos, yPos, 0);

                    // Instantiate prefab and set parent
                    GameObject instance = Instantiate(prefab, position, Quaternion.identity);
                    instance.transform.SetParent(parentTransform);
                }
            }
        }
    }

    int GetPrefabIndex(int row, int col)
    {
        // Define a new pattern for reversing direction
        int patternLength = prefabs.Length; // Adjust pattern length based on available prefabs

        // Reverse the pattern direction by changing the index calculation
        int index = (row + (numCols - col - 1)) % patternLength;

        return index;
    }

    // Optionally, call UpdateGrid from other scripts or UI events to refresh in real-time
    public void SetGridParameters(int rows, int cols, float xOffset, float yOffset, float xPadding, float yPadding)
    {
        numRows = rows;
        numCols = cols;
        this.xOffset = xOffset;
        this.yOffset = yOffset;
        this.xPadding = xPadding;
        this.yPadding = yPadding;
        UpdateGrid();
    }
}
