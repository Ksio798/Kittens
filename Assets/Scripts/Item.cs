using UnityEngine;

public enum ItemType
{
	Cat, Vase, Box, Enemy
}

public enum ItemColor
{
	Black, White, Common, Kotalt, Destroyer
}

public class Item : MonoBehaviour //Характеристики конкретного объекта
{
	public ItemType Type;
	public ItemColor Color;
}
