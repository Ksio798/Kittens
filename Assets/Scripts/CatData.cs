using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewCatData", menuName = "Game Data/Cat")]
public class CatData : ScriptableObject
{
	public Sprite CatSprite;
	public string CatName;
	[TextArea(3, 10)]
	public string CatInfo;
	[TextArea(3, 10)]
	public string Description;
}
