using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SeatingArea : MonoBehaviour
{
	public GameObject PointParent;
	public GameObject ObjectsContainer;

	List<SeatPoint> seatPoints;
	Cat currentCat;
	Transform currentTransform;
	void Start()
	{
		seatPoints = PointParent.transform.GetComponentsInChildren<SeatPoint>().ToList();

		foreach (SeatPoint point in seatPoints)
		{
			point.OnEnter += OnEnter;
			point.OnExit += OnExit;
		}
	}

	// Update is called once per frame
	void Update()
	{

	}


	private void OnEnter(Cat cat, Transform t)
	{
		currentCat = cat;
		currentTransform = t;
		currentCat.OnUp += OnUp;
	}

	private void OnExit(Cat cat, Transform t)
	{
		if (currentCat == cat && currentTransform == t)
		{
			currentCat = null;
			currentTransform = null;
		}
	}

	private void OnUp(Cat c)
	{
		if (currentCat != null && currentTransform != null)
			currentCat.SetPos(currentTransform, ObjectsContainer.transform);
	}
}
