using UnityEngine;

public class KotaltCat : Cat
{
	public override bool OnSeat(Item[] items, int index)
	{
		if (FindEnemy(items))
		{
			foreach (Item item in items)
			{
				if (item != null && item.Type == ItemType.Enemy)
				{
					Destroy(item.gameObject);
				}
			}
		}

		return NearSame(items, index);
	}
}
