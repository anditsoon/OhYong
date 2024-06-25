using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponThrow : MonoBehaviour
{
    // �߻� ��ġ
    public GameObject throwPosition;

    // ��ô ���� ������Ʈ
    public GameObject wpFactory;

    // �÷��̾�
    public GameObject player;

    // ��ô �Ŀ�
    public float throwPower = 15f;

    // Update is called once per frame
    void Update()
    {
        // ���콺 ������ ��ư�� ������ �ü��� �ٶ󺸴� �������� ����ź�� ������ �ʹ�

        // 1. ���콺 ������ ��ư�� �Է¹޴´�
        if (Input.GetMouseButtonDown(1))
        {
            // ���� ������Ʈ�� ������ �� ������ ���� ��ġ�� �߻� ��ġ�� �Ѵ�
            GameObject weapon = Instantiate(wpFactory);
            weapon.transform.position = throwPosition.transform.position;

            // ���� ������Ʈ�� Rigidbody ������Ʈ�� �����´�
            Rigidbody rb = weapon.GetComponent<Rigidbody>();

            // �÷��̾��� ���� �������� ����ź�� �������� ���� ���Ѵ�
            rb.AddForce(player.transform.forward * throwPower, ForceMode.Impulse);
        }
        
    }
}
