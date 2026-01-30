using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
	public SceneLoader Loader;

	public GameObject LevelsPanel;
	public GameObject LevelsButton;

	[SerializeField]
	List<LineElement> LinesPrefabs = new List<LineElement>();
	[SerializeField]
	List<Sprite> SpriteDecor = new List<Sprite>();
	[SerializeField]
	Transform LevelsParent;
	[SerializeField]
	TextMeshProUGUI Leveltext;

	void Start()//Загрузка сохранения при старте игры
	{
		SaveController.Instance.LoadData();
		EducationController.ids.Clear();

		if (SaveController.Instance.Save != null && SaveController.Instance.Save.LevelsId.Count > 0)
		{
			LevelsButton.SetActive(true);
			Leveltext.text = SaveController.Instance.Save.LevelsId.Count.ToString();
			setLevels();//Вызов функции расстановки кнопок урвоней, если есть сохранение
		}
		else
		{
			LevelsButton.SetActive(false);
		}
	}

	public void Del()//Удаление сохранения по кнопке
	{
		SaveController.Instance.DeleteSave();
	}

	public void OpenPanel()//Отрытие\закрытие панели с уровнями по кнопке
	{
		LevelsPanel.SetActive(!LevelsPanel.activeSelf);
	}

	void setLevels()//Размещение кнопок для выбора уровня
	{
		int index = 0;

		foreach (int i in SaveController.Instance.Save.LevelsId)
		{
			int r = Random.Range(0, SpriteDecor.Count);

			LineElement line = Instantiate(LinesPrefabs[index]);//Генерация части изображения из префаба
			line.SetInfo(SpriteDecor[r], i.ToString());
			line.LevelButton.onClick.AddListener(() => { Loader.LoadByIndex(i); });//Назначение каждой кнопке функции перехода на нужный уровень
			line.transform.SetParent(LevelsParent);
			index++;
			if (index > LinesPrefabs.Count - 1)
				index = 0;
		}
	}
}
