using UnityEngine;

[CreateAssetMenu(fileName = "NewEdData", menuName = "Game Data/Education")]
public class EducationData : ScriptableObject
{
	public Sprite EdSprite;
	[TextArea(3, 10)]
	public string EdInfo;
	
}
