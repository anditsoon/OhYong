using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerItem : MonoBehaviour
{
    GameObject nearObject;
    GameObject equipWeapon;

    bool iDown;
    bool sDown1; // 1�� ���
    bool sDown2;
    bool sDown3;

    // ����ڰ� � ���⸦ ������ �ִ���
    public List<GameObject> weapons = new List<GameObject>();
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
        Debug.Log(nearObject.name);

   
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
        sDown2 = Input.GetButton("Swap2");
        sDown3 = Input.GetButton("Swap3");

    }

    void Swap()
    {
        int weaponIndex = -1;
        if (sDown1) weaponIndex = 0;
        if (sDown2) weaponIndex = 1;
        if (sDown3) weaponIndex = 2;

        if (sDown1 || sDown2 || sDown3) //   �׸��� && !isJumping ����
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
        if(iDown && nearObject != null) // ���߿� PlayerMove �� ��ġ�� && !isJumping �ؾߵ�
        {
            if (nearObject.tag == "Weapon")
            {
                // Item item = nearObject.GetComponent<Item>();
                weapons.Add(nearObject);
                //hasWeapons[weaponIndex] = true;

                Destroy(nearObject);
            }
        }
    }
}
