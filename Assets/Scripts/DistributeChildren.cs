using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class DistributeChildren : MonoBehaviour
{
	public bool SetWidthScale, SetWidth, SetHeightScale, MoveBottom, UpdateOnChange, HorisontalSet, VertcialSet;

	public float leftPadding = 0, rightPadding = 0, offset = 0f;

	List<Transform> transforms;
	SpriteRenderer sr = null;
	float width = 1.0f, height = 1.0f, childWidth = 0.0f, spacing;
	float screenHeight, screenWidth;

	private void Awake()
	{
		sr = GetComponent<SpriteRenderer>();
	}

	void Start()
	{
		screenHeight = 2f * Camera.main.orthographicSize;
		screenWidth = screenHeight * Camera.main.aspect;

		setWidthScale();
		setHeightScale();		
		moveToBottom();
		setWidth();

		getSize();

		setChildrenHorisontal();
		setChildrenVertcial();
	}

	void setChildrenHorisontal()
	{
		if (!HorisontalSet) return;

		transforms = Enumerable.Range(0, transform.childCount).Select(i => transform.GetChild(i)).ToList();

		float usableWidth = screenWidth - leftPadding - rightPadding;
		spacing = transforms.Count - 1 > 0 ? usableWidth / (transforms.Count - 1) : 0;

		for (int i = 0; i < transforms.Count; i++)
		{
			transforms[i].position = transform.position;
			float x = spacing != 0 ? -screenWidth / 2f + leftPadding + spacing * i : 0;
			transforms[i].position = new Vector3(x, transforms[i].position.y, transforms[i].position.z);
		}
	}

	void setChildrenVertcial()
	{
		if (!VertcialSet) return;

		transforms = Enumerable.Range(0, transform.childCount).Select(i => transform.GetChild(i)).ToList();
		spacing = transforms.Count - 1 > 0 ? height / (transforms.Count - 1) : 0;

		for (int i = 0; i < transforms.Count; i++)
		{
			transforms[i].position = transform.position;
			float y = spacing != 0 ? -height / 2 + spacing * i : 0;
			transforms[i].position = new Vector3(transforms[i].position.x, y, transforms[i].position.z);
		}
	}

	void setWidthScale()
	{
		if(!SetWidthScale) return;

		float spriteWidth = sr.bounds.size.x / transform.localScale.x;
		float scaleX = screenWidth / spriteWidth;

		transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
	}

	void setWidth()
	{
		if (!SetWidth) return;

		transform.localScale = Vector3.one;
		sr.size = new Vector2(screenWidth, sr.size.y);
	}

	void setHeightScale()
	{
		if(!SetHeightScale) return;

		float spriteWidth = sr.bounds.size.y / transform.localScale.y;
		float scaleY = screenHeight / spriteWidth;

		transform.localScale = new Vector3(transform.localScale.x, scaleY, transform.localScale.z);
	}

	void moveToBottom()
	{
		if (!MoveBottom) return;

		Camera cam = Camera.main;

		Vector3 bottom = cam.ScreenToWorldPoint(new Vector3(Screen.width / 2f, 0f, cam.nearClipPlane));
		bottom.z = transform.position.z;

		float halfHeight = sr != null ? sr.bounds.size.y / 2f : 0f;

		transform.position = new Vector3(bottom.x, bottom.y + halfHeight + offset, transform.position.z);
	}

	void getSize()
	{
		if (transform.childCount == 0) return;

		var child = transform.GetChild(0);
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
		{
			getSize();
			setChildrenHorisontal();
			setChildrenVertcial();
		}
	}

}
