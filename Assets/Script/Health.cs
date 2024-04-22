using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public delegate void HitEvent(GameObject source);
    public HitEvent OnHit;

    public delegate void ResetEvent();
    public ResetEvent OnHitReset;
    public Transform respawnPoint;

    public float MaxHealth = 10f;
    public float InvulnerabilityDuration = 1f; // Duration of invulnerability after being hit
    private bool _canDamage = true;
    private float _currentHealth;

    private void Start()
    {
        ResetHealthToMax();
    }

    private void Update()
    {
        // Only for testing, remove this in actual gameplay
        if (Input.GetKeyDown(KeyCode.K))
            Damage(1f, this.gameObject);
    }

    private void ResetInvulnerability()
    {
        _canDamage = true;
        OnHitReset?.Invoke();
    }

    public void Damage(float damage, GameObject source)
    {
        if (!_canDamage)
            return;

        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            _currentHealth = 0f;
            
            Respawn();
        }

        // Start invulnerability period
        _canDamage = false;
        Invoke(nameof(ResetInvulnerability), InvulnerabilityDuration);

        OnHit?.Invoke(source);
    }
    void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        transform.position = respawnPoint.position;
        GameManager.Instance.ResetUIText();
        Debug.Log("Respawn Manager Reset UI Text");
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public void ResetHealthToMax()
    {
        _currentHealth = MaxHealth;
    }

    // Expose canDamage state to other scripts
    public bool CanDamage => _canDamage;

    // Define CurrentHealth property
    public float CurrentHealth => _currentHealth;
}

