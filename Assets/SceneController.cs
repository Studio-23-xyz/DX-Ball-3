using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LoadLevel(int level)
    {
        if (Instance == null) return;

        string sceneName = $"Level{level}";
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnLevelLoaded;
        //GameManager.Instance.OnLevelLoaded();
    }
}
