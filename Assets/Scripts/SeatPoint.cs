using System;
using UnityEngine;

public class SeatPoint : MonoBehaviour
{
	public Action<Cat, SeatPoint> OnEnter;
	public Action<Cat, SeatPoint> OnExit;
	public Action<Item, int> OnAdd;
	public int Order;

	Item child = null;
	private void Start()
	{
		if (transform.childCount != 0)
		{
			child = transform.GetChild(0).GetComponent<Item>();
			child.transform.position = new Vector3(transform.position.x, transform.position.y + child.GetComponent<SpriteRenderer>().bounds.size.y / 2, transform.position.z);
			OnAdd?.Invoke(child, Order);
		}
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<Cat>() != null && child == null)
			OnEnter?.Invoke(collision.GetComponent<Cat>(), this);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.GetComponent<Cat>() != null && child == null)
			OnExit?.Invoke(collision.GetComponent<Cat>(), this);
	}

	void OnTransformChildrenChanged()
	{
		child = transform.GetChild(0).GetComponent<Item>();
		OnAdd?.Invoke(child, Order);
	}
}
