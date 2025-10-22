using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject MenuPanel;

    public void OpenClosePanel()
    {
        MenuPanel.SetActive(!MenuPanel.activeSelf);
    }
}
