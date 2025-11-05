using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SeatingArea : MonoBehaviour
{
	public GameObject PointParent;
	public int SeatCount = 5;
	public SeatPoint PointPrefab;
	public List<Item> AddedItems = new List<Item>();

	Item[] Items;
	List<SeatPoint> seatPoints = new List<SeatPoint>();
	Cat currentCat;
	SeatPoint currentPoint;
	void Start()
	{
		Items = new Item[SeatCount];

		for (int i = 0; i < SeatCount; i++)
		{
			SeatPoint point = Instantiate(PointPrefab);
			point.transform.position = PointParent.transform.position;
			point.transform.SetParent(PointParent.transform);
			point.OnEnter += OnEnter;
			point.OnExit += OnExit;
			point.OnAdd += addItem;
			point.Order = i;
			seatPoints.Add(point);

			if (i < AddedItems.Count && AddedItems[i] != null)
			{
				Item item = Instantiate(AddedItems[i]);
				item.transform.position = point.transform.position;
				item.transform.SetParent(point.transform);
			}
		}


		//seatPoints = PointParent.transform.GetComponentsInChildren<SeatPoint>().ToList();
		//Items = new Item[seatPoints.Count];

		//for (int i = 0; i < seatPoints.Count; i++)
		//{
		//	seatPoints[i].OnEnter += OnEnter;
		//	seatPoints[i].OnExit += OnExit;
		//	seatPoints[i].OnAdd += addItem;
		//	seatPoints[i].Order = i;
		//}
	}

	// Update is called once per frame
	void Update()
	{

	}


	private void OnEnter(Cat cat, SeatPoint t)
	{
		currentCat = cat;
		currentPoint = t;
		currentCat.OnUp += OnUp;
	}

	private void OnExit(Cat cat, SeatPoint t)
	{
		if (currentCat == cat && currentPoint == t)
		{
			currentCat.OnUp -= OnUp;
			currentCat = null;
			currentPoint = null;
		}
	}

	private void OnUp(Cat c)
	{
		if (currentCat != null && currentPoint != null)
		{
			if (currentCat.OnSeat(Items, currentPoint.Order))
			{
				Items[currentPoint.Order] = currentCat.GetComponent<Item>();
				currentCat.SetPos(currentPoint.transform);
			}
			currentCat.OnUp -= OnUp;
			currentCat = null;
			currentPoint = null;
		}
	}

	private void addItem(Item i, int index)
	{
		Items[index] = i;
	}
}
