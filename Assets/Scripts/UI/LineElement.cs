using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LineElement : MonoBehaviour//Скрипт для линии с кнопкой при выборе уровня
{
	[SerializeField]
	Image levelIm;
	[SerializeField]
	TextMeshProUGUI leveltext;

	public Button LevelButton;


	public void SetInfo(Sprite s, string text)//Установка необходимого изображения и номера уровня
	{
		levelIm.sprite = s;
		leveltext.text = text;
	}
}
