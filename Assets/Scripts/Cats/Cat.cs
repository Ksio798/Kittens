using System;
using UnityEngine;
using UnityEngine.InputSystem;
public enum Type
{
	first, second, third, fourth, fifth, sixth
}
public class Pair
{
	public Transform pos;
	public Item _item;
}
public class Cat : MonoBehaviour
{
	public static Cat instance = null;

	public Action<Cat> OnChangeParent;
	public Action<Cat> OnUp;

	public Type CatType;
	public Sprite CatSprite;
	//public Transform Point;

	protected bool CanMove = true;

	protected Item _item;
	protected Transform oldParent;
	protected Vector3 oldPos;
	protected float speed = 15f;
	protected Vector3 dragOffset;
	protected SpriteRenderer spriteRenderer;


	public void SetPos(Transform t)
	{
		transform.position = new Vector3(t.position.x, t.position.y + spriteRenderer.bounds.size.y / 2, t.position.z);
		CanMove = false;
		transform.SetParent(t);
		instance = this;
	}

	public virtual bool OnSeat(Item[] items, int index)
	{
		return NearSame(items, index) && !FindEnemy(items);
	}

	public virtual void Cancel()
	{
		if (oldParent == transform.parent)
			return;

		transform.position = oldParent.position;
		transform.SetParent(oldParent);
		CanMove = true;
	}

	protected void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = CatSprite;
		oldParent = transform.parent;
		_item = GetComponent<Item>();
	}


	protected void Update()
	{


	}

	void OnTransformParentChanged()
	{
		OnChangeParent?.Invoke(this);
	}

	protected bool NearSame(Item[] items, int index)
	{
		bool result = true;
		if (index - 1 >= 0 && items[index - 1] != null)
			if (items[index - 1].Type == ItemType.Cat && items[index - 1].Color != _item.Color)
				result = false;

		if (index + 1 <= items.Length - 1 && items[index + 1] != null)
			if (items[index + 1].Type == ItemType.Cat && items[index + 1].Color != _item.Color)
				result = false;

		return result;
	}

	protected bool FindEnemy(Item[] items)
	{
		bool result = false;

		foreach (Item item in items)
		{
			if (item != null && item.Type == ItemType.Enemy)
			{
				result = true;
				break;
			}
		}

		return result;
	}

	protected void OnMouseDown()
	{
		//Debug.Log("Down");
		dragOffset = transform.position - GetMousePos();
		oldPos = transform.position;
		spriteRenderer.sortingOrder = 5;
	}

	protected void OnMouseDrag()
	{
		//Debug.Log("Drag");
		if (CanMove)
			this.transform.position = Vector3.MoveTowards(this.transform.position, GetMousePos() - dragOffset, speed * Time.deltaTime);
	}

	protected void OnMouseUp()
	{
		spriteRenderer.sortingOrder = 3;
		//Debug.Log("Up");
		OnUp?.Invoke(this);
		if (CanMove)
			transform.position = oldPos;
	}
	protected Vector3 GetMousePos()
	{
		var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		pos.z = 0;
		return pos;
	}
}
