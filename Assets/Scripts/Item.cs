using UnityEngine;

public enum ItemType
{
	Cat, Vase, Box
}

public enum ItemColor
{
	Black, White, Orange
}

public class Item : MonoBehaviour
{
	public ItemType Type;
	public ItemColor Color;
}
