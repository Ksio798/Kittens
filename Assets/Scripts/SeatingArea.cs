using System.Collections.Generic;
using UnityEngine;

public class SeatingArea : MonoBehaviour//Логика поведения одной полки
{
	public GameObject PointParent;
	public int SeatCount = 5;
	public SeatPoint PointPrefab;
	public List<Item> AddedItems = new List<Item>();

	Item[] Items;
	List<SeatPoint> seatPoints = new List<SeatPoint>();
	Cat currentCat;
	SeatPoint currentPoint;
	void Start()
	{
		Items = new Item[SeatCount];

		for (int i = 0; i < SeatCount; i++)// Создаём места посадки и подписываемся на их события
		{
			SeatPoint point = Instantiate(PointPrefab);
			point.transform.position = PointParent.transform.position;
			point.transform.SetParent(PointParent.transform);
			point.OnEnter += OnEnter;
			point.OnExit += OnExit;
			point.OnAdd += addItem;
			point.OnRemove += delItem;
			point.Order = i;
			seatPoints.Add(point);

			if (i < AddedItems.Count && AddedItems[i] != null)// Если задан стартовый предмет для этого места - создаём его и размещаем в точке
			{
				Item item = Instantiate(AddedItems[i]);
				item.transform.position = point.transform.position;
				item.transform.SetParent(point.transform);
			}
		}
	}

	private void OnEnter(Cat cat, SeatPoint t)// Вызывается, когда кот попадает в зону конкретной точки посадки
	{
		currentCat = cat;
		currentPoint = t;
		currentCat.OnUp += OnUp;//Подписка на событие от кота, пытаемся посадить на полку если палец отпущен
	}

	private void OnExit(Cat cat, SeatPoint t) // Вызывается, когда кот покидает зону точки посадки
	{
		if (currentCat == cat && currentPoint == t)// Снимаем только если это тот же кот и та же точка
		{
			currentCat.OnUp -= OnUp;// Убираем обработчик, чтобы не сработал в другом месте
			currentCat = null;
			currentPoint = null;
		}
	}

	private void OnUp(Cat c)// Вызывается при отпускании кота (конец перетаскивания), если кот был над точкой
	{
		if (currentCat != null && currentPoint != null)
		{
			if (currentCat.OnSeat(Items, currentPoint.Order))// Проверяем, можно ли посадить кота в эту точку, исходя из правил кота и текущих Items
			{
				Items[currentPoint.Order] = currentCat.GetComponent<Item>();//Добавляем кота в список объектов на полке
				currentCat.SetPos(currentPoint.transform);//Вызываем метод смены позиции
			}
			currentCat.OnUp -= OnUp;
			currentCat = null;
			currentPoint = null;
		}
	}


	private void addItem(Item i, int index)// Записывает предмет в массив состояния мест (вызывается событием SeatPoint)
	{
		Items[index] = i;
	}

	private void delItem(int index)// Очищает слот в массиве состояния мест (вызывается событием SeatPoint)
	{
		Items[index] = null;
	}
}
