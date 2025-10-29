using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject InfoPanel;

    public List<CatData> CatDatas = new List<CatData>();
    public Transform CatContent;

    public CatPanelInfo PanelPrefab;
    public void OpenMenu()
    {   
        InfoPanel.SetActive(false);
		MenuPanel.SetActive(!MenuPanel.activeSelf);
    }

    public void OpenInfo() 
    { 
        MenuPanel.SetActive(false);
        InfoPanel.SetActive(!InfoPanel.activeSelf);
    }

	private void Start()
	{
		foreach (var cat in CatDatas)
        {
            CatPanelInfo panelInfo = Instantiate(PanelPrefab);
            panelInfo.Set(cat);
            panelInfo.transform.SetParent(CatContent);
        }
	}
}
