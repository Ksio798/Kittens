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
	public Action<Cat> OnUp;

	public Type CatType;
	public Sprite CatSprite;
	public Transform Point;

	bool CanMove = true;

	Transform parent;
	Vector3 oldPos;
	float speed = 15f;
	Vector3 dragOffset;
	SpriteRenderer spriteRenderer;


	public void SetPos(Transform t, Transform p)
	{
		transform.position = new Vector3(t.position.x, t.position.y + spriteRenderer.bounds.size.y / 2, t.position.z);
		CanMove = false;
		transform.SetParent(p);
	}

	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = CatSprite;
		parent = transform;
	}


	void Update()
	{


	}

	void OnTransformParentChanged()
	{
		OnChangeParent?.Invoke(this);
	}

	private void OnMouseDown()
	{
		//Debug.Log("Down");
		dragOffset = transform.position - GetMousePos();
		oldPos = transform.position;
	}

	private void OnMouseDrag()
	{
		//Debug.Log("Drag");
		if (CanMove)
			this.transform.position = Vector3.MoveTowards(this.transform.position, GetMousePos() - dragOffset, speed * Time.deltaTime);
	}

	private void OnMouseUp()
	{
		//Debug.Log("Up");
		OnUp?.Invoke(this);
		if (CanMove)
			transform.position = oldPos;
	}
	Vector3 GetMousePos()
	{
		var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		pos.z = 0;
		return pos;
	}
}
