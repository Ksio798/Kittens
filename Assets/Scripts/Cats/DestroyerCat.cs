using System;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerCat : Cat
{
	List<Pair> pairs = new List<Pair>();

	public override bool OnSeat(Item[] items, int index) //Переопределение метода проверки условий размещения
	{
		if(!base.OnSeat(items, index))
			return false;

		if (index - 1 >= 0 && items[index - 1] != null)
			destroyItem(items[index - 1]);

		if (index + 1 <= items.Length - 1 && items[index + 1] != null)
			destroyItem(items[index + 1]);

		return base.OnSeat(items, index);
	}

	public override void Cancel() //Переопределение метода возврата с добавлением уничтожения объектов
	{
		base.Cancel();

		foreach (Pair pair in pairs)
		{
			Item i = pair._item;
			i.gameObject.SetActive(true);
			i.transform.SetParent(pair.pos);
		}

		pairs.Clear();
	}

	void destroyItem(Item item) // Дезактивация уничтожаемых объектов и запоминание для оката при необходимости
	{
		if (item.Type == ItemType.Vase)
		{
			Pair p = new Pair();
			p.pos = item.transform.parent;
			p._item = item;
			pairs.Add(p);

			item.gameObject.SetActive(false);
			item.transform.SetParent(transform);
		}
	}
}
