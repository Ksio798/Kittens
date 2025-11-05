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

	public void LoadNext()
	{
		if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		else
			SceneManager.LoadScene(0);
	}

	public void LoadLast()
	{
		if (SaveController.Instance.Save != null && SaveController.Instance.Save.LevelsId.Count > 0)
		{
			SaveData sd = SaveController.Instance.Save;
			int index = sd.LevelsId[sd.LevelsId.Count - 1] + 1 < SceneManager.sceneCountInBuildSettings ? sd.LevelsId[sd.LevelsId.Count - 1] + 1 : 0;
			SceneManager.LoadScene(index);
		}
		else
			SceneManager.LoadScene(1);
	}
}
