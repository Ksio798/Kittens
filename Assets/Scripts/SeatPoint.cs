using System;
using UnityEngine;

public class SeatPoint : MonoBehaviour
{
	public Action<Cat, SeatPoint> OnEnter;
	public Action<Cat, SeatPoint> OnExit;
	public Action<Item, int> OnAdd;
	public Action<int> OnRemove;

	public int Order;

	Item child = null;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// Вызывается, когда объект входит в 2D-триггер этой точки
		// Уведомляем только если вошёл кот и место свободно (child == null)
		if (collision.GetComponent<Cat>() != null && child == null)
			OnEnter?.Invoke(collision.GetComponent<Cat>(), this);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		// Вызывается, когда объект выходит из 2D-триггера этой точки
		// Уведомляем только если вышел кот и место свободно (child == null)
		if (collision.GetComponent<Cat>() != null && child == null)
			OnExit?.Invoke(collision.GetComponent<Cat>(), this);
	}

	void OnTransformChildrenChanged()//Вызывается, когда изменились дочерние объекты точки
	{
		if (transform.childCount > 0)
		{
			child = transform.GetChild(0).GetComponent<Item>();
			if (child != null)// Поднимаем предмет вверх на половину его высоты, чтобы он "стоял" на точке
			{
				child.transform.position = new Vector3(transform.position.x, transform.position.y +
					child.GetComponent<SpriteRenderer>().bounds.size.y / 2, transform.position.z);
				OnAdd?.Invoke(child, Order);// Сообщаем, что слот заняли, чтобы обновить массив состояния
			}
		}
		else
		{
			child = null;
			OnRemove?.Invoke(Order);// Сообщаем, что слот освободился, чтобы очистить массив состояния
		}

	}
}
