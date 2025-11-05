using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
	public SceneLoader Loader;

	public TextMeshProUGUI ButtonText;
	public GameObject LevelsPanel;
	public Transform ParentPanel;
	public GameObject LevelsButton;

	public Button ButtonPrefab;

	void Start()
	{
		SaveController.Instance.LoadData();
		EducationController.ids.Clear();

		if (SaveController.Instance.Save != null && SaveController.Instance.Save.LevelsId.Count > 0)
		{
			ButtonText.text = "Продолжить";
			LevelsButton.SetActive(true);
			setButtons();
		}
		else
		{
			LevelsButton.SetActive(false);
		}
	}

	public void Del()
	{
		SaveController.Instance.DeleteSave();
	}

	public void OpenPanel()
	{
		LevelsPanel.SetActive(!LevelsPanel.activeSelf);
	}

	void setButtons()
	{
		foreach (int i in SaveController.Instance.Save.LevelsId)
		{
			Button b = Instantiate(ButtonPrefab);
			b.GetComponentInChildren<TextMeshProUGUI>().text = i.ToString();
			b.onClick.AddListener(() => { Loader.LoadByIndex(i); });
			b.transform.SetParent(ParentPanel);
		}
	}
}
