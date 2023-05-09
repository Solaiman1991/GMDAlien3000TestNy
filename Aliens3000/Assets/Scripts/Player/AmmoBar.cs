using UnityEngine;
using UnityEngine.UI;

public class AmmoBar : MonoBehaviour
{
    [SerializeField] private Image _ammoBarSprite;

    public void UpdateAmmoBar(int maxAmmo, int currentAmmo)
    {
        _ammoBarSprite.fillAmount = (float)currentAmmo / maxAmmo;
    }
}