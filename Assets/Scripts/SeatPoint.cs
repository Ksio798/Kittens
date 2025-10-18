using System;
using UnityEngine;

public class SeatPoint : MonoBehaviour
{
	public Action<Cat, Transform> OnEnter;
	public Action<Cat, Transform> OnExit;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<Cat>() != null)
			OnEnter?.Invoke(collision.GetComponent<Cat>(), transform);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.GetComponent<Cat>() != null)
			OnExit?.Invoke(collision.GetComponent<Cat>(), transform);
	}
}
