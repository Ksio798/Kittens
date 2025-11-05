using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnPlatform : MonoBehaviour
{
	public List<Cat> Cats = new List<Cat>();
	public Action OnWin;

	void Start()
	{
		for (int i = 0; i < Cats.Count; i++)
		{
			Cat c = Instantiate(Cats[i]);
			c.transform.position = transform.position;
			c.transform.SetParent(transform);
		}
	}

	void OnTransformChildrenChanged()
	{
		if (transform.childCount == 0)
			OnWin?.Invoke();
	}






}
