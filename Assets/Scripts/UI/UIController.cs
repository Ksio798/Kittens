using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
	public GameObject MenuPanel;
	public GameObject InfoPanel;
	public GameObject WinPanel;
	public SpawnPlatform Spawn;

	public List<CatData> CatDatas = new List<CatData>();//Информация о котах на уровне
	public Transform CatContent;

	public CatPanelInfo PanelPrefab;
	public void OpenMenu()//Открытие\закрытие меню пузы
	{
		InfoPanel.SetActive(false);
		MenuPanel.SetActive(!MenuPanel.activeSelf);
	}

	public void OpenInfo()//Открытие\закрытие меню с информацией о котах
	{
		MenuPanel.SetActive(false);
		InfoPanel.SetActive(!InfoPanel.activeSelf);
	}

	public void OpenWin()//Открытие панели при победе
	{
		WinPanel.SetActive(true);
	}

	public void Cancel()//Активация отката последнего хода по кнопке
	{
		if (Cat.instance != null)
			Cat.instance.Cancel();
	}

	private void Start()
	{
		foreach (var cat in CatDatas)//Установка информации о котах на уровне
		{
			CatPanelInfo panelInfo = Instantiate(PanelPrefab);
			panelInfo.Set(cat);
			panelInfo.transform.SetParent(CatContent);
		}

		Spawn.OnWin += OpenWin;//Подписка на событие при победе
	}
}
