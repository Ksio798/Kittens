using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EducationController : MonoBehaviour
{
	public List<EducationData> Educations = new List<EducationData>();//Список файлов с обычением на кровне
	public EdPanel PanelPrefab;
	public EdPanel ImagePanelPrefab;

	public GameObject EdCanvas;
	public GameObject ParentPanel;

	public static List<int> ids = new List<int>();
	int index = 0;
	EdPanel oldPanel;

	void Start()
	{
		// При старте: если шагов нет или в этой сцене обучение уже показывали - ничего не делаем
		if (Educations.Count == 0 || ids.Contains(SceneManager.GetActiveScene().buildIndex)) return;

		EdCanvas.SetActive(true); // Включаем UI обучения
		ids.Add(SceneManager.GetActiveScene().buildIndex); // Запоминаем, что в этой сцене обучение уже показано

		setPanel(); // Создаём и показываем первый шаг обучения
	}

	public void NextEd()//Переходим к следующему шагу обучения или закрываем, если обычение закончилось
	{
		index++;

		if (index >= Educations.Count)
			EdCanvas.SetActive(false);
		else
			setPanel();
	}

	void setPanel()// Создаёт панель для текущего шага (Educations[index]) и заполняет её данными
	{
		if (oldPanel != null)
			Destroy(oldPanel.gameObject);

		EdPanel ed;
		if (Educations[index].EdSprite != null)// Выбираем тип панели с изображением или без
			ed = Instantiate(ImagePanelPrefab);
		else
			ed = Instantiate(PanelPrefab);
		

		ed.transform.position = ParentPanel.transform.position;
		ed.transform.SetParent(ParentPanel.transform);
		ed.SetInfo(Educations[index]);
		ed.transform.SetSiblingIndex(0);

		oldPanel = ed;
	}


}
