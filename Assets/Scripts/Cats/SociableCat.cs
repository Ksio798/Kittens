using UnityEngine;

public class SociableCat : Cat
{
	public override bool OnSeat(Item[] items, int index)
	{
		bool result = false;

		if (index - 1 >= 0 && items[index - 1] != null)
			if (items[index - 1].Type == ItemType.Cat)
				result = true;

		if (index + 1 <= items.Length - 1 && items[index + 1] != null)
			if (items[index + 1].Type == ItemType.Cat)
				result = true;

		return result;
	}
}
