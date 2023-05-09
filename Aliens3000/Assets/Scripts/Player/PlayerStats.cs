
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;
    private float _currentHealth;

    private Healthbar _healthbar;
    private AnimationStateController _animationStateController;
    private AudioManager _audioManager;
    private PlayerMovementController _playerController;
    private WeaponController _playerShooting;
     public bool isPlayerDead= false;
     [SerializeField] private float healthRegenerationRate = 0.01f;

     
     
      void Update()
     {
         if (_currentHealth < _maxHealth && !isPlayerDead)
         {
             _currentHealth += healthRegenerationRate * Time.deltaTime;
             _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
             UpdateHealthBar();
         }
     }
     void Start()
    {
        _healthbar = FindObjectOfType<Healthbar>();
        _animationStateController = GetComponent<AnimationStateController>();
        _audioManager = FindObjectOfType<AudioManager>();
        _playerController = GetComponent<PlayerMovementController>();
        _playerShooting = GetComponent<WeaponController>();
        _currentHealth = _maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        if (_currentHealth <= 0)
        {
            Die();
        }
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        _healthbar.UpdateHealthBar(_maxHealth, _currentHealth);
        CheckHeartBeat();
    }

    private void Die()
    {
        _animationStateController.isCharacterDead = true;
        isPlayerDead = true;

        _audioManager.PlaySound("PlayerDead");
    
        if (_playerController != null)
        {
            _playerController.enabled = false;
        }
        if (_playerShooting != null)
        {
            _playerShooting.enabled = false;
        }
        var playerCollider = GetComponent<Collider>();
        if (playerCollider != null)
        {
            playerCollider.enabled = false;
        }

        var playerColliderInChild = GetComponentInChildren<Collider>();
        if (playerColliderInChild != null)
        {
            playerColliderInChild.enabled = false;
        }
           
    }
    
    private void CheckHeartBeat()
    {
        if (_currentHealth / _maxHealth <= 0.3f)
        {
            _audioManager.PlaySound("PlayerHeartBeat",true);
        }
        else
        {
            _audioManager.StopSound("PlayerHeartBeat");
        }
    }
}
