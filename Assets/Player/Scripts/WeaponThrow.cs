using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponThrow : MonoBehaviour
{
    // 발사 위치
    public GameObject throwPosition;

    // 투척 무기 오브젝트
    public GameObject wpFactory;

    // 플레이어
    public GameObject player;

    // 투척 파워
    public float throwPower = 15f;

    // Update is called once per frame
    void Update()
    {
        // 마우스 오른쪽 버튼을 누르면 시선이 바라보는 방향으로 수류탄을 던지고 싶다

        // 1. 마우스 오른쪽 버튼을 입력받는다
        if (Input.GetMouseButtonDown(1))
        {
            // 무기 오브젝트를 생성한 후 무기의 생성 위치를 발사 위치로 한다
            GameObject weapon = Instantiate(wpFactory);
            weapon.transform.position = throwPosition.transform.position;

            // 무기 오브젝트의 Rigidbody 컴포넌트를 가져온다
            Rigidbody rb = weapon.GetComponent<Rigidbody>();

            // 플레이어의 정면 방향으로 수류탄에 물리적인 힘을 가한다
            rb.AddForce(player.transform.forward * throwPower, ForceMode.Impulse);
        }
        
    }
}
