using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    public Brick Brick;
    private TextMeshProUGUI _healthText;

    private Camera _camera;
    void Start()
    {
        if(Brick ==  null)
        {
            Brick = GetComponent<Brick>();
        }

        _camera = Camera.main;

        GameObject textObject = new GameObject("HealthText");
        textObject.transform.SetParent(transform, false);
    }

    void Update()
    {
        
    }
}
