using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]

public class Item : ScriptableObject
{
    public string itemName;     //�������� �̸�
    [TextArea]
    public string itemDesc;     // �������� ����
    public ItemType itemType;   //�������� ����
    public Sprite itemImage;    //�������� �̹���
    public GameObject itemPrefab; //�������� ������
    public int itemCode;        //�������� �ڵ�

    public enum ItemType
    {
        Material_Basic,     // �⺻������ ������ �ִ� ��� (���̽� ����̱⿡ �κ��丮���� ��� �� 1��Ű�� �̵�)
        Material_Store,     // �������� ���� �� �ִ� ��� (���̽� ���+���� ����̱⿡ ��� �� 2��Ű�� �̵�)
        Material_Garden,    // �Թ翡�� ���� �� �ִ� ��� (���̽� ���+�� ����̱⿡ ��� �� 2��Ű�� �̵�)
        Etc                 // ��Ÿ
    }
}