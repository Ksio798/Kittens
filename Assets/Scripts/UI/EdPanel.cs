using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EdPanel : MonoBehaviour
{
    public TextMeshProUGUI TextInfo;
    public Image InfoImage;

    public void SetInfo(EducationData ed)
    {
        TextInfo.text = ed.EdInfo;
        if (InfoImage != null)
            InfoImage.sprite = ed.EdSprite;

    }
}
