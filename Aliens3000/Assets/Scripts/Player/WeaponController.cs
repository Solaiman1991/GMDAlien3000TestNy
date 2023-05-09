using System.Collections;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private CinemachineBrain cinemachineBrain;
    public Collider playerCollider;
    [SerializeField] private int maxAmmo = 100;
    [SerializeField] private int currentAmmo;
    [SerializeField] private float reloadTime = 3.0f;
    [SerializeField] private AmmoBar ammoBar;
    [SerializeField] private TextMeshProUGUI reloadText;
    [SerializeField] private float shootRate;
    [SerializeField] private Transform playerTransform;
    private bool isReloading = false;
    public GameObject m_shotPrefab;
    private float m_shootRateTimeStamp;
    RaycastHit hit;
    float range = 1000.0f;
    private InputAction shootAction;
    private InputAction reloadAction;

    private void Awake()
    {
        shootAction = FindObjectOfType<PlayerInput>().actions["Shoot"];
        reloadAction = FindObjectOfType<PlayerInput>().actions["Reload"];
        Cursor.lockState = CursorLockMode.Locked;

        currentAmmo = maxAmmo;
        ammoBar.UpdateAmmoBar(maxAmmo, currentAmmo);
        reloadText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isReloading)
            return;

        if (reloadAction.triggered && currentAmmo < maxAmmo)
        {
            StartCoroutine(Reload());
            return;
        }

        if (shootAction.ReadValue<float>() > 0)
        {
            ShootRay();
        }
    }

    private Camera GetActiveCamera()
    {
        return cinemachineBrain.OutputCamera;
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading");

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
        ammoBar.UpdateAmmoBar(maxAmmo, currentAmmo);
        Debug.Log("Reloaded");
    }

    void ShootRay()
    {
        PlayerStats playerStats = FindObjectOfType<PlayerStats>();
        if (playerStats.isPlayerDead)
        {
            return;
        }

        if (Time.time > m_shootRateTimeStamp && currentAmmo > 0)
        {
            Ray ray = GetActiveCamera().ScreenPointToRay(Input.mousePosition);
            Vector3 target;

            if (Physics.Raycast(ray, out hit, range) && hit.collider != playerCollider)
            {
                target = hit.point;
            }
            else
            {
                target = ray.GetPoint(range);
            }

            GameObject laser =
                GameObject.Instantiate(m_shotPrefab, transform.position, transform.rotation) as GameObject;
            laser.GetComponent<BulletBehavior>().setTarget(target);
            GameObject.Destroy(laser, 4f);

            m_shootRateTimeStamp = Time.time + shootRate;

            currentAmmo--;
            ammoBar.UpdateAmmoBar(maxAmmo, currentAmmo);

            AudioManager audioManager = FindObjectOfType<AudioManager>();

            if (!audioManager.IsSoundPlaying("PlayerLaserGun"))
            {
                audioManager.PlaySound("PlayerLaserGun");
            }
        }

        if (currentAmmo <= 0 && !isReloading)
        {
            reloadText.gameObject.SetActive(true);
        }
        else if (reloadText.gameObject.activeSelf && currentAmmo > 0)
        {
            reloadText.gameObject.SetActive(false);
        }
    }
}
