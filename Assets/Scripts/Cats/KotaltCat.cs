using System.Collections.Generic;
using UnityEngine;

class Pair
{
	public Transform pos;
	public Item _item;
}


public class KotaltCat : Cat
{
	List<Pair> pairs = new List<Pair> ();

	public override bool OnSeat(Item[] items, int index)
	{
		if (FindEnemy(items))
		{
			foreach (Item item in items)
			{
				if (item != null && item.Type == ItemType.Enemy)
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

		return NearSame(items, index);
	}

	public override void Cancel()
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
}
