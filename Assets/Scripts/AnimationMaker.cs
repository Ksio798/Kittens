using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationMaker : MonoBehaviour
{
	[SerializeField]
	Image back;
	[SerializeField]
	List<Sprite> spritesBack = new List<Sprite>();

	[SerializeField] 
	float interval = 1f;
	float t;
	int index = 0;

    void Update()//Таймер срабатывающий раз в interval секунд
	{
		t += Time.deltaTime;
		if (t >= interval)
		{
			t -= interval;
			animationMake();
		}
	}

	void animationMake() // Поочередная смена спрайтов для анимации
	{
		back.sprite = spritesBack[index];
		index++;
		if (index == spritesBack.Count)
			index = 0;
		
	}
}
