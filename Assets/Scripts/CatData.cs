using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewCatData", menuName = "Game Data/Cat")]//Создание пункта в меню
public class CatData : ScriptableObject //Скрипт для хранения полной информации о конкретном коте
{
	public Sprite CatSprite;
	public string CatName;
	[TextArea(3, 10)]
	public string CatInfo;
	[TextArea(3, 10)]
	public string Description;
}
