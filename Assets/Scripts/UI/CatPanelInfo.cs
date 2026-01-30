using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CatPanelInfo : MonoBehaviour//Скрипт для панели с информацией об одном коте
{
    public Image CatImage;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Info;
    public TextMeshProUGUI Description;

    public void Set(CatData data)//Устанапливаем значения полученные из data
    {
        CatImage.sprite = data.CatSprite;
        Name.text = data.CatName;
        Info.text = data.CatInfo;
        Description.text = data.Description;
    }
}
