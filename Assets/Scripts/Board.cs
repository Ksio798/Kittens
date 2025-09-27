using System;
using UnityEngine;

public class Board : MonoBehaviour
{
	public Action<Cat> OnEnter;
	public Action OnExit;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		OnEnter?.Invoke(collision.GetComponent<Cat>());
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		OnExit?.Invoke();
	}

	
}
