using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shelf : MonoBehaviour
{
	public GameObject PointParent;
	public Board Board;

	List<Transform> transforms;
	int usedCount = 0;
	int maxCount;
	
	List<Cat> catList;
	Cat currentCat;

	void Start()
	{
		transforms = PointParent.GetComponentsInChildren<Transform>().ToList();
		transforms.RemoveAt(0);
		maxCount = transforms.Count;
		catList = Board.GetComponentsInChildren<Cat>().ToList();
		SetCats();
		Board.OnEnter += OnEnter;
		Board.OnExit += OnExit;
	}


	void Update()
	{
		if (Input.GetMouseButtonUp(0) && currentCat != null)
			addCat();	
	}

	private void SetCats()
	{
		for (int i = 0; i < catList.Count; i++)
		{
			catList[i].OnChangeParent += OnDelCat;
			catList[i].transform.position = transforms[i].position;
			catList[i].Point = transforms[i];
			usedCount++;
		}
		catList.Last().CanMove = true;
	}

	private void OnEnter(Cat cat)
	{
		currentCat = cat;
	}

	private void OnExit()
	{
		currentCat = null;
	}

	private void addCat()
	{
		if (catList.Count == 0 || catList.Count < maxCount && !catList.Contains(currentCat) && currentCat.CatType == catList.Last().CatType)
		{
			currentCat.transform.SetParent(Board.transform);
			currentCat.ChangeParent(Board.transform, transforms[usedCount]);
			currentCat.OnChangeParent += OnDelCat;
			if (catList.Count != 0)
				catList.Last().CanMove = false;
			catList.Add(currentCat);
			currentCat = null;
			usedCount++;
		}
	}

	private void OnDelCat(Cat cat)
	{
		usedCount--;
		catList.Remove(cat);
		cat.OnChangeParent -= OnDelCat;
		if (catList.Count != 0)
			catList.Last().CanMove = true;
	}
}
