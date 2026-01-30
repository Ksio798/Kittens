using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EdPanel : MonoBehaviour//Скрипт для панели с обучением
{
    public TextMeshProUGUI TextInfo;
    public Image InfoImage;

    public void SetInfo(EducationData ed)//Установка значений из полученного EducationData
	{
        TextInfo.text = ed.EdInfo;
        if (InfoImage != null)
            InfoImage.sprite = ed.EdSprite;
    }
}
