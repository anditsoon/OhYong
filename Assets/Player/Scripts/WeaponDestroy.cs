using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDestroy : MonoBehaviour
{

    // ���� ����Ʈ ������ ����
    public GameObject wpDestroyEffect;

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
