using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    // �̵� �ӵ� ����
    public float moveSpeed = 7f;

    // ĳ���� ��Ʈ�ѷ� ����
    CharacterController cc;

    // �߷� ����
    float gravity = -20f;

    // ���� �ӷ� ����
    float yVelocity = 0;

    // ������ ����
    public float jumpPower = 10f;

    // ���� ���� ����
    //public bool isJumping = 

    // ����� ���ӿ�����Ʈ
    public GameObject model;

    private void Start()
    {
        // ĳ���� ��Ʈ�ѷ� ������Ʈ �޾ƿ���
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // w, a, s, d Ű�� �Է��ϸ� ĳ���͸� �� �������� �̵���Ű�� �ʹ�
        // [spacebar] Ű�� ������ ĳ���͸� �������� ������Ű�� �ʹ�

        // 1. ������� �Է��� �޴´�
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 2. �̵� ������ �����Ѵ�
        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;

        // 2-1. ���� ī�޶� �������� ������ ��ȯ�Ѵ�
        dir = Camera.main.transform.TransformDirection(dir);
       

        //3. �̵� �ӵ��� ���� �̵��Ѵ�
        // p = p0 + vt
        cc.Move(dir * moveSpeed * Time.deltaTime);

        // dir �� ũ�Ⱑ 0���� ũ�� (�����ϋ���)
        if(dir.magnitude > 0)
        {
            // �����̴� ������ ����� �չ������� ����
            model.transform.forward = dir;

        }
    }
}
