using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public SpawnPlatform Spawn;

    void Start()
    {
        Spawn.OnWin += SaveLevel;
    }

    
    void SaveLevel()
    {
        SaveController.Instance.CreateSave(SceneManager.GetActiveScene().buildIndex);
        SaveController.Instance.SaveData();
    }
}
