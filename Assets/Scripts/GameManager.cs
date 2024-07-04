using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Character player;
    public static GameManager Instance { get; private set; }
    
    public Character Player => player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
