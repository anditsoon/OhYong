using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponThrow : MonoBehaviour
{

    // 투척 무기 오브젝트
    public GameObject wpFactory;

    // 플레이어
    public GameObject player;

    // 무기 포인트
    private GameObject weaponPoint;

    // 투척 파워
    public float throwPower = 15f;
    // 회전 파워
    public float torquePower = 10f;


    // Update is called once per frame
    void Update()
    {
        // 마우스 오른쪽 버튼을 누르면 시선이 바라보는 방향으로 수류탄을 던지고 싶다

        // 1. 마우스 오른쪽 버튼을 입력받는다
        if (Input.GetMouseButtonDown(1))
        {

            // weapon 오브젝트와 연결
            weaponPoint = GameObject.Find("WeaponPoint");

            GameObject weapon = weaponPoint.GetComponentInChildren<Weapon>().gameObject;

            weapon.transform.parent = null;

            // 무기 오브젝트의 Rigidbody 컴포넌트를 가져온다
            Rigidbody rb = weapon.GetComponent<Rigidbody>();
            rb.useGravity = true;
            rb.isKinematic = false;


            // 플레이어의 정면 방향으로 무기에 물리적인 힘을 가한다
            rb.AddForce(player.transform.forward * throwPower * Time.deltaTime, ForceMode.Impulse);
            rb.AddTorque(transform.forward * torquePower * Time.deltaTime, ForceMode.Impulse);
        }
        
    }
}
