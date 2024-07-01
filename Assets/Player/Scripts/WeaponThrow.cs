using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponThrow : MonoBehaviour
{

    // ��ô ���� ������Ʈ
    public GameObject wpFactory;

    // �÷��̾�
    public GameObject player;

    // ���� ����Ʈ
    private GameObject weaponPoint;

    // ��ô �Ŀ�
    public float throwPower = 15f;
    // ȸ�� �Ŀ�
    public float torquePower = 10f;


    // Update is called once per frame
    void Update()
    {
        // ���콺 ������ ��ư�� ������ �ü��� �ٶ󺸴� �������� ����ź�� ������ �ʹ�

        // 1. ���콺 ������ ��ư�� �Է¹޴´�
        if (Input.GetMouseButtonDown(1))
        {

            // weapon ������Ʈ�� ����
            weaponPoint = GameObject.Find("WeaponPoint");

            GameObject weapon = weaponPoint.GetComponentInChildren<Weapon>().gameObject;

            weapon.transform.parent = null;

            // ���� ������Ʈ�� Rigidbody ������Ʈ�� �����´�
            Rigidbody rb = weapon.GetComponent<Rigidbody>();
            rb.useGravity = true;
            rb.isKinematic = false;


            // �÷��̾��� ���� �������� ���⿡ �������� ���� ���Ѵ�
            rb.AddForce(player.transform.forward * throwPower * Time.deltaTime, ForceMode.Impulse);
            rb.AddTorque(transform.forward * torquePower * Time.deltaTime, ForceMode.Impulse);
        }
        
    }
}
