using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakingViewItem : MonoBehaviour
{
    [SerializeField]
    private Image AItem_Image;
    [SerializeField]
    private Image BItem_Image;
    //[SerializeField]
    private AlchemyMake alchemy;

    public bool ViewOn = false;

    // Start is called before the first frame update
    void Start()
    {
        alchemy = FindObjectOfType<AlchemyMake>();
        SetColor(AItem_Image, 0);
        SetColor(BItem_Image, 0);
    }

    void Update()
    {
        if (ViewOn)
        {
            if(alchemy.aNum > 0 || alchemy.bNum > 0)
            {
                ShowItemImage();
            }
        }
    }

    // �̹����� ���� ����.
    private void SetColor(Image image, float _alpha)
    {
        // �������� ���°� null ���¸� �̹��� ���İ� 0���� ����� �޼ҵ�.
        Color color = image.color;
        color.a = _alpha;
        image.color = color;
    }

    public void ShowItemImage()
    {
        if(ViewOn)
        {
            if (alchemy.aItem != null)
            {
                if (alchemy.aNum > 0)
                {
                    if(AItem_Image.sprite==null)
                    {
                        SetColor(AItem_Image, 1);
                        AItem_Image.sprite = alchemy.aItem.itemImage;
                        Debug.Log("alchemy.aItem.itemImage ��");
                    }
                }
            }
            if (alchemy.bItem != null)
            {
                if (alchemy.bNum > 0)
                {
                    if(BItem_Image.sprite == null)
                    {
                        SetColor(BItem_Image, 1);
                        BItem_Image.sprite = alchemy.bItem.itemImage;
                        Debug.Log("alchemy.bItem.itemImage ��");
                    }
                }
            }
        }
    }

    public void ResetItemImage()
    {
        AItem_Image.sprite = null;
        BItem_Image.sprite = null;
    }
}
