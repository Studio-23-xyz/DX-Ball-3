using UnityEngine;

public class ObjectCycler : MonoBehaviour
{
    public GameObject[] balls;
    private int _currentIndex = 0;

    void Start()
    {
        foreach (GameObject ball in balls)
        {
            ball.SetActive(false);
        }

        if (balls.Length > 0)
        {
            balls[_currentIndex].SetActive(true);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CycleBalls(-1);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            CycleBalls(1);
        }
    }

    private void CycleBalls(int direction)
    {
        balls[_currentIndex].SetActive(false);

        _currentIndex += direction;

        if (_currentIndex < 0)
        {
            _currentIndex = balls.Length - 1;
        }
        else if (_currentIndex >= balls.Length)
        {
            _currentIndex = 0;
        }

        balls[_currentIndex].SetActive(true);
    }
}
