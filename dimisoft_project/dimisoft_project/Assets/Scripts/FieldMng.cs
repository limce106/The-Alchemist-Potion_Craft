using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldMng : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Garden")
        {
            Debug.Log("��Ḧ ��Ȯ�մϴ�.");
        }
    }
}
