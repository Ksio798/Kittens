using UnityEngine;

[CreateAssetMenu(fileName = "NewEdData", menuName = "Game Data/Education")]//Создание пункта в меню
public class EducationData : ScriptableObject
{
	//Информация для конкретного шага обучения
	public Sprite EdSprite;
	[TextArea(3, 10)]
	public string EdInfo;
	
}
