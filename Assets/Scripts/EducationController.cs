using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EducationController : MonoBehaviour
{
	public List<EducationData> Educations = new List<EducationData>();
	public EdPanel PanelPrefab;
	public EdPanel ImagePanelPrefab;

	public GameObject EdCanvas;
	public GameObject ParentPanel;

	public static List<int> ids = new List<int>();
	int index = 0;
	EdPanel oldPanel;

	void Start()
	{
		if (Educations.Count == 0 || ids.Contains(SceneManager.GetActiveScene().buildIndex)) return;

		EdCanvas.SetActive(true);
		ids.Add(SceneManager.GetActiveScene().buildIndex);

		setPanel();
	}

	public void NextEd()
	{
		index++;

		if (index >= Educations.Count)
			EdCanvas.SetActive(false);
		else
			setPanel();
	}

	void setPanel()
	{
		if (oldPanel != null)
			Destroy(oldPanel.gameObject);

		EdPanel ed;
		if (Educations[index].EdSprite != null)
		{
			ed = Instantiate(ImagePanelPrefab);
		}
		else
		{
			ed = Instantiate(PanelPrefab);
		}

		ed.transform.position = ParentPanel.transform.position;
		ed.transform.SetParent(ParentPanel.transform);
		ed.SetInfo(Educations[index]);
		ed.transform.SetSiblingIndex(0);

		oldPanel = ed;
	}


}
