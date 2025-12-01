using UnityEngine;

public enum ItemType
{
	Cat, Vase, Box, Enemy
}

public enum ItemColor
{
	Black, White, Common, Kotalt, Destroyer
}

public class Item : MonoBehaviour
{
	public ItemType Type;
	public ItemColor Color;
}
