using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WeaponDestroy : MonoBehaviour
{

    // ���� ����Ʈ ������ ����
    public GameObject wpDestroyEffect;

    // ��ư�� ������ �� ������
    bool isRotating = false;

    private void Update()
    {

        if(Input.GetButtonDown("Fire2"))
        {
            isRotating = true;
        }
        // rotate �� ��ư�� ������ ���� �ϰ� �ʹ�
        if (isRotating)
        {
            transform.Rotate(transform.right, 30f, Space.World);
            
        }
        
    }

    // �浹���� ���� ó��
    private void OnCollisionEnter(Collision collision)
    {

        // ����Ʈ �������� �����Ѵ�
        GameObject eff = Instantiate(wpDestroyEffect);
        // ����Ʈ �������� ��ġ�� ���� ������Ʈ �ڽ��� ��ġ�� �����ϴ�
        eff.transform.position = transform.position;

        // �ڱ� �ڽ��� �����Ѵ�
        Destroy(gameObject);

       
    }
}
