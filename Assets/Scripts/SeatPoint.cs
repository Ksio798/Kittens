using System;
using UnityEngine;
using UnityEngine.UI;

public class SeatPoint : MonoBehaviour
{
	public Action<Cat, SeatPoint> OnEnter;
	public Action<Cat, SeatPoint> OnExit;
	public Action<Item, int> OnAdd;
	public Action<int> OnRemove;

	public int Order;

	Item child = null;
	Color baceColor;
	private void Start()
	{

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
		if (transform.childCount > 0)
		{
			child = transform.GetChild(0).GetComponent<Item>();
			if (child != null)
			{
				child.transform.position = new Vector3(transform.position.x, transform.position.y + child.GetComponent<SpriteRenderer>().bounds.size.y / 2, transform.position.z);
				baceColor = GetComponent<SpriteRenderer>().color;
				GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
				OnAdd?.Invoke(child, Order);
			}
		}
		else
		{
			GetComponent<SpriteRenderer>().color = baceColor;
			child = null;
			OnRemove?.Invoke(Order);
		}

	}
}
