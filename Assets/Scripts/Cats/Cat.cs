using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Pair
{
	public Transform pos;
	public Item _item;
}
public class Cat : MonoBehaviour
{
	public static Cat instance = null; // Текущий "активный" кот (последний установленный через SetPos)

	public Action<Cat> OnChangeParent; // Событие при смене родителя (перемещение между слотами)
	public Action<Cat> OnUp;           // Событие при отпускании мыши (окончание перетаскивания)

	public Sprite CatSprite; // Спрайт кота, назначаемый в Start

	protected bool CanMove = true; // Разрешение на перетаскивание/движение

	protected Item _item; // Компонент Item на этом же объекте (цвет/тип и т.д.)
	protected Transform oldParent; // Родитель до перемещения (для отката)
	protected Vector3 oldPos; // Позиция до начала перетаскивания
	protected float speed = 15f; // Скорость "подтягивания" к курсору при перетаскивании
	protected Vector3 dragOffset; // Смещение, чтобы объект не прыгал в центр курсора
	protected SpriteRenderer spriteRenderer; // Рендерер спрайта кота

	protected AudioSource audioSource; // Источник звука для проигрывания клипа при установке
	[SerializeField]
	protected List<AudioClip> placeSounds; // Набор звуков при "посадке" кота в слот

	public void SetPos(Transform t) // Ставим кота в новую позицию, устанавливаем нового родителя и проигрываем звук
	{
		transform.position = new Vector3(t.position.x, t.position.y + spriteRenderer.bounds.size.y / 2, t.position.z);
		CanMove = false;
		transform.SetParent(t);
		instance = this;

		if(audioSource != null && placeSounds.Count > 0)
		{
			int r = Random.Range(0, placeSounds.Count);
			audioSource.PlayOneShot(placeSounds[r]);
		}
	}

	public virtual bool OnSeat(Item[] items, int index) // Разрешаем посадку, если рядом нет котов другого цвета и на полке нет врага
	{
		return NearSame(items, index) && !FindEnemy(items);
	}

	public virtual void Cancel() // Откат к исходному положению
	{
		if (oldParent == transform.parent)
			return;

		transform.position = oldParent.position;
		transform.SetParent(oldParent);
		CanMove = true;
	}

	protected void Start()//Получаем ссылки на объекты и запоминаем значения по умолчанию
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = CatSprite;
		oldParent = transform.parent;
		_item = GetComponent<Item>();
		audioSource = GetComponent<AudioSource>();
	}

	void OnTransformParentChanged() // Unity-колбэк, вызывается при смене transform.parent, необходимо для обновления списка объектов на полке
	{
		OnChangeParent?.Invoke(this);
	}

	protected bool NearSame(Item[] items, int index) // Проверка, по соседству не должно быть кота другого цвета
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

	protected bool FindEnemy(Item[] items) // Проверка есть ли в массиве предмет типа Enemy
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
		dragOffset = transform.position - GetMousePos();
		oldPos = transform.position;
		spriteRenderer.sortingOrder = 5;
	}

	protected void OnMouseDrag() // Плавно тянем кота к позиции курсора с учётом смещения
	{
		if (CanMove)
			this.transform.position = Vector3.MoveTowards(this.transform.position, GetMousePos() - dragOffset, speed * Time.deltaTime);
	}

	protected void OnMouseUp() // Возвращаем порядок отрисовки и сообщаем, что мышь отпущена
	{
		spriteRenderer.sortingOrder = 3;
		OnUp?.Invoke(this);
		if (CanMove)
			transform.position = oldPos;
	}
	protected Vector3 GetMousePos() // Конвертируем позицию мыши из экранных координат в мировые
	{
		var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		pos.z = 0;
		return pos;
	}
}
