using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponsScript : MonoBehaviour
{
    public List<WeaponController> startingWeapons = new List<WeaponController>();
    public Transform weaponParentSocket;
    public Transform defaultWeaponPosition;
    public Transform aimingPosition;

    public int activeWeaponIndex { get; private set; }

    private WeaponController[] weaponSlots = new WeaponController[5];

    void Start()
    {
        activeWeaponIndex = 1;
        foreach (WeaponController startingWeapon in startingWeapons)
        {
            AddWeapon(startingWeapon);
        }
        SwitchWeapon(0);
    }

    void Update()
    {
        // Verifica las teclas numéricas del 1 al 5
        for (int i = 0; i < 5; i++)
        {
            // Comprueba si se ha presionado la tecla numérica correspondiente
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SwitchWeapon(i);
                break; // Si se ha encontrado y procesado la tecla, no es necesario seguir iterando
            }
        }
    }

    private void SwitchWeapon(int p_weaponIndex)
    {
        if (p_weaponIndex != activeWeaponIndex && p_weaponIndex >= 0 && p_weaponIndex < weaponSlots.Length)
        {
            // Desactivar el arma activa actual
            if (activeWeaponIndex >= 0 && activeWeaponIndex < weaponSlots.Length)
            {
                weaponSlots[activeWeaponIndex].gameObject.SetActive(false);
            }

            // Activar el arma seleccionada
            weaponSlots[p_weaponIndex].gameObject.SetActive(true);
            activeWeaponIndex = p_weaponIndex;
            EventManager.current.NewGunEvent.Invoke();
        }
    }

    private void AddWeapon(WeaponController p_weaponPrefab)
    {
        weaponParentSocket.position = defaultWeaponPosition.position;

        //Añadir arma al jugador pero no mostrarla
        for (int i = 0; i<weaponSlots.Length; i++)
        {
            if (weaponSlots[i] == null)
            {
                WeaponController weaponClone = Instantiate(p_weaponPrefab, weaponParentSocket);
                weaponClone.gameObject.SetActive(false);

                weaponSlots[i] = weaponClone;
                return;
            }
        }
    }
}
