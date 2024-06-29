using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerItem : MonoBehaviour
{
    GameObject nearObject;
    GameObject equipWeapon;

    bool iDown;
    bool sDown1; // 1번 장비

    // 사용자가 어떤 무기를 가지고 있느냐
    public GameObject[] weapons;
    public bool[] hasWeapons;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Interaction();
        Swap();
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Weapon")
        {
            nearObject = other.gameObject;

        }

   
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Weapon")
        {
            nearObject = null;
        }

 
    }

    void GetInput()
    {
        iDown = Input.GetButtonDown("Interaction");
        sDown1 = Input.GetButtonDown("Swap1");
    }

    void Swap()
    {
        int weaponIndex = -1;
        if (sDown1) weaponIndex = 0;

        if (sDown1) // || sDown2 || sDown3 그리고 && !isJumping 까지
        {
            if (equipWeapon != null)
            {
                equipWeapon.SetActive(false);
            }
            equipWeapon = weapons[weaponIndex];
            equipWeapon.SetActive(true);
        }
    }

    void Interaction()
    {
        if(iDown && nearObject != null) // 나중에 PlayerMove 랑 합치고 && !isJumping 해야됨
        {
            if (nearObject.tag == "Weapon")
            {
                Item item = nearObject.GetComponent<Item>();
                int weaponIndex = item.value;
                hasWeapons[weaponIndex] = true;

                Destroy(nearObject);
            }
        }
    }
}
