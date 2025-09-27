using System;
using UnityEngine;
using UnityEngine.InputSystem;
public enum Type
{
	first, second, third, fourth, fifth, sixth
}

public class Cat : MonoBehaviour
{
	public Action<Cat> OnChangeParent;

	public Type CatType;
	public Sprite CatSprite;
	public Transform Point;
	public bool CanMove = false;

	Transform parent;
	float speed = 15f;
	Vector3 dragOffset;
	SpriteRenderer spriteRenderer;
	bool isChangedParent = false;

	public void ChangeParent(Transform newParent, Transform point)
	{
		OnChangeParent?.Invoke(this);
		parent = newParent;
		Point = point;
		isChangedParent = true;
		transform.position = Point.position;
	}

	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = CatSprite;
		parent = transform.parent;
	}


	void Update()
	{


	}

	private void OnMouseDown()
	{
		dragOffset = transform.position - GetMousePos();
		isChangedParent = false;
	}

	private void OnMouseDrag()
	{
		if (CanMove)
			this.transform.position = Vector3.MoveTowards(this.transform.position, GetMousePos() - dragOffset, speed * Time.deltaTime);
	}

	private void OnMouseUp()
	{
		if (!isChangedParent)
		{
			this.transform.position = Point.position;
		}
	}
	Vector3 GetMousePos()
	{
		var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		pos.z = 0;
		return pos;
	}
}
