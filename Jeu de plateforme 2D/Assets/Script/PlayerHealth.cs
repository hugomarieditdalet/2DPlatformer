using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int Maxhealth = 100;
    public int currentHealth;

    public float invicibilityTimeAfterHit = 3f;
    public float invincibilityFLashDelay = 0.15f;
    public bool isInvincible = false;

    public SpriteRenderer graphics;
    public HealthBar healthBar;

    public AudioClip hitSound;

    public static PlayerHealth instance;

    private void Awake()
    { 
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerHealth dans la sc�ne ");
            return;        }
        instance = this;
    }

    void Start()
    {
        currentHealth = Maxhealth;
        healthBar.SetMaxHealth(Maxhealth);
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.H))
        {
            TakeDamage(20);
        }
    }

    public void HealPlayer(int amount)
    {
        if((currentHealth + amount)> Maxhealth)
        {
            currentHealth = Maxhealth;
        }
        else
        {
            currentHealth += amount;
        }
        healthBar.SetHealth(currentHealth);

    }

    public void TakeDamage(int damage)
    {
        if(!isInvincible )
        {
            AudioManager.instance.PlayClipAt(hitSound, transform.position);
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);

            if(currentHealth <= 0)
            {
                Die();
                return;
            }

            isInvincible = true;
            StartCoroutine(InvincibilityFlash());
            StartCoroutine(HandleInvincibilityDelay());

        }
    }

    public void Die()
    {
        PlayerMovement.instance.enabled = false;
        PlayerMovement.instance.animator.SetTrigger("Die");
        PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Kinematic;
        PlayerMovement.instance.rb.linearVelocity = Vector3.zero;
        PlayerMovement.instance.playerCollider.enabled = false;
        GameOverManager.instance.OnPlayerDeath();
    }

    public void Respawn()
    {
        PlayerMovement.instance.enabled = true;
        PlayerMovement.instance.animator.SetTrigger("Respawn");
        PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Dynamic;
        PlayerMovement.instance.playerCollider.enabled = true;
        currentHealth = Maxhealth;
        healthBar.SetHealth(currentHealth);
    }

    public IEnumerator InvincibilityFlash()
    {
        while(isInvincible)
        {
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(invincibilityFLashDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invincibilityFLashDelay);
        }
    }

    public IEnumerator HandleInvincibilityDelay()
    {
        yield return new WaitForSeconds(invicibilityTimeAfterHit);
        isInvincible = false ;
    }
}
