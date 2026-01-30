using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public SpawnPlatform Spawn;

    void Start()//Подписка на событие при победе
    {
        Spawn.OnWin += SaveLevel;
    }

    void SaveLevel()//Сохрание текущей пройденной сцены
    {
        if (SaveController.Instance == null)
            return;
        SaveController.Instance.CreateSave(SceneManager.GetActiveScene().buildIndex);
        SaveController.Instance.SaveData();
    }
}
