using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlatform : MonoBehaviour
{
	public List<Cat> Cats = new List<Cat>();//Список ппрефабов котов на уровне
	public Action OnWin;

	void Start()//Генерируем котов из префабов и садим в нужную позицию
	{
		for (int i = 0; i < Cats.Count; i++)
		{
			Cat c = Instantiate(Cats[i]);
			c.transform.position = transform.position;
			c.transform.SetParent(transform);
		}
	}

	void OnTransformChildrenChanged()//Если на стартовой позиции не осталось котов, вызываем сообщение о победе
	{
		if (transform.childCount == 0)
			OnWin?.Invoke();
	}
}
