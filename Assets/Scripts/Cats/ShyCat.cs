using UnityEngine;

public class ShyCat : Cat
{
	public override bool OnSeat(Item[] items, int index) //Переопределение метода проверки условий размещения, на полке вообще не должно быть котов
	{
		bool result = true;
		foreach (Item item in items)
		{
			if (item != null && item.Type == ItemType.Cat)
			{
				result = false;
				break;
			}
		}

		return result && !FindEnemy(items);
	}
}
