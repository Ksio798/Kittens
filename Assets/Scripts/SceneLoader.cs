using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public void LoadByIndex(int index)
    {
		SceneManager.LoadScene(index);
	}

	public void ReLoad()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
