using UnityEngine;

public class CityManager : MonoBehaviour
{
    public static CityManager Instance { get; private set; }

    public Transform[] cityTransforms; // Array of city area transforms

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
}
