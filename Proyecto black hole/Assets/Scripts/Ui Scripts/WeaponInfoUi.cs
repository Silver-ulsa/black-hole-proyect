using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponInfoUi : MonoBehaviour
{
    public TMP_Text currentBulletText;
    public TMP_Text totalBulletText;

    void OnEnable()
    {
        EventManager.current.updateBulletsEvent.AddListener(UpdateBulletText);
    }

    void OnDisable()
    {
        EventManager.current.updateBulletsEvent.RemoveListener(UpdateBulletText);
    }

    public void UpdateBulletText(int currentBullets, int totalBullets)
    {
        if (currentBullets <= 0)
        {
            currentBulletText.color = Color.red;
        }
        else
        {
            currentBulletText.color = Color.white;
        }
        currentBulletText.text = currentBullets.ToString();
        totalBulletText.text = totalBullets.ToString();
    }
}
