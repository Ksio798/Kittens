using System.Collections.Generic;
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

	[SerializeField]
	List<LineElement> LinesPrefabs = new List<LineElement>();
	[SerializeField]
	List<Sprite> SpriteDecor = new List<Sprite>();
	[SerializeField]
	Transform LevelsParent;
	[SerializeField]
	public TextMeshProUGUI Leveltext;
	int index = 0;

	void Start()
	{
		SaveController.Instance.LoadData();
		EducationController.ids.Clear();

		if (SaveController.Instance.Save != null && SaveController.Instance.Save.LevelsId.Count > 0)
		{
			ButtonText.text = "Продолжить";
			LevelsButton.SetActive(true);
			Leveltext.text = SaveController.Instance.Save.LevelsId.Count.ToString();
			setButtons();
			setLevels();
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

	void setLevels()
	{
		foreach (int i in SaveController.Instance.Save.LevelsId)
		{
			int r = Random.Range(0, SpriteDecor.Count);

			LineElement line = Instantiate(LinesPrefabs[index]);
			line.SetInfo(SpriteDecor[r], i.ToString());
			line.LevelButton.onClick.AddListener(() => { Loader.LoadByIndex(i); });
			line.transform.SetParent(LevelsParent);
			index++;
			if (index > LinesPrefabs.Count - 1)
				index = 0;
		}
	}
}
