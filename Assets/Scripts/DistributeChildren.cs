using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class DistributeChildren : MonoBehaviour
{
	public bool SetWidth, MoveBottom, UpdateOnChange, HorisontalSet, VertcialSet;

	public float leftPadding = 0, rightPadding = 0, offset = 0f;

	List<Transform> transforms;
	SpriteRenderer sr = null;
	float width = 1.0f, height = 1.0f, childWidth = 0.0f, spacing;

	void Start()
	{
		sr = GetComponent<SpriteRenderer>();

		if (SetWidth) setPos();
		if (MoveBottom) moveToBottom();

		getSize();

		if (HorisontalSet) setChildrenHorisontal();
		if (VertcialSet) setChildrenVertcial();
	}

	void setChildrenHorisontal()
	{
		transforms = Enumerable.Range(0, transform.childCount).Select(i => transform.GetChild(i)).ToList();

		float usableWidth = width - leftPadding - rightPadding;
		spacing = transforms.Count - 1 > 0 ? usableWidth / (transforms.Count - 1) : 0;

		for (int i = 0; i < transforms.Count; i++)
		{
			transforms[i].position = transform.position;
			float x = spacing != 0 ? -width / 2f + leftPadding + spacing * i : 0;
			transforms[i].position = new Vector3(x, transforms[i].position.y, transforms[i].position.z);
		}
	}

	void setChildrenVertcial()
	{
		transforms = Enumerable.Range(0, transform.childCount).Select(i => transform.GetChild(i)).ToList();
		spacing = transforms.Count - 1 > 0 ? height / (transforms.Count - 1) : 0;

		for (int i = 0; i < transforms.Count; i++)
		{
			transforms[i].position = transform.position;
			float y = spacing != 0 ? -height / 2 + spacing * i : 0;
			transforms[i].position = new Vector3(transforms[i].position.x, y, transforms[i].position.z);
		}
	}

	void setPos()
	{

		float spriteWidth = sr.bounds.size.x / transform.localScale.x;
		float screenHeight = 2f * Camera.main.orthographicSize;
		float screenWidth = screenHeight * Camera.main.aspect;

		float scaleX = screenWidth / spriteWidth;

		transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
	}

	void moveToBottom()
	{
		Camera cam = Camera.main;

		Vector3 bottom = cam.ScreenToWorldPoint(new Vector3(Screen.width / 2f, 0f, cam.nearClipPlane));
		bottom.z = transform.position.z;

		float halfHeight = sr != null ? sr.bounds.size.y / 2f : 0f;

		transform.position = new Vector3(bottom.x, bottom.y + halfHeight + offset, transform.position.z);
	}

	void getSize()
	{
		if (transform.childCount == 0) return;

		var child = transform.GetChild(transform.childCount - 1);
		if (child.GetComponent<SpriteRenderer>() != null)
			childWidth = child.GetComponent<SpriteRenderer>().bounds.size.x;
		else if (child.GetComponent<BoxCollider2D>() != null)
			childWidth = child.GetComponent<BoxCollider2D>().size.x * transform.localScale.x;
		width = sr.bounds.size.x - childWidth;

		height = sr.bounds.size.y;
	}

	void OnTransformChildrenChanged()
	{
		if (UpdateOnChange)
			setChildrenHorisontal();
	}

}
