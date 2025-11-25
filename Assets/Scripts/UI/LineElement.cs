using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LineElement : MonoBehaviour
{
	[SerializeField]
	Image levelIm;
	[SerializeField]
	TextMeshProUGUI leveltext;

	public Button LevelButton;


	public void SetInfo(Sprite s, string text)
	{
		levelIm.sprite = s;
		leveltext.text = text;
	}
}
