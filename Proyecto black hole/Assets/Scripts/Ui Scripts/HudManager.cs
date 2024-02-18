using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudManager : MonoBehaviour
{
    public GameObject weaponInfo;

    void Start()
    {
        EventManager.current.NewGunEvent.AddListener(CreateWeaponInfo);
    }

    public void CreateWeaponInfo ()
    {
        Instantiate(weaponInfo, transform);
    }
}
