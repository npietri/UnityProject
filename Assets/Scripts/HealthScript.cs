using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour {

    /// <summary>
    /// Total hitpoints and health
    /// </summary>
    /// 
    public int maxHealth = 1;
    public int hp = 1;
    public bool immunity = false;

    public Slider HealthBar;

    public Image DamageImg;
    public float flashSpeed = 20f;
    public Color flashColour = new Color (1f, 0f, 0f, 0.9f);
    public Image ImmunityImg;
    public Color immunityColour = new Color(1f, 1f, 1f, 0.6f);
    bool damaged;

    /// <summary>
    /// Enemy or player?
    /// </summary>
    public bool isEnemy = true;

    void Update () {
        OnHeroDamage ();
    }

    /// <summary>
    /// Inflicts damage and check if the object should be destroyed
    /// </summary>
    /// <param name="damageCount"></param>
    public void Damage (int damageCount) {

        if (! immunity || isEnemy)
        {
            hp -= damageCount;
        }

        if (hp <= 0) {
            

            // Dead!
            Destroy (gameObject);

            // Explosion (smoke effect)
            SpecialEffects.Instance.Explosion(transform.position);

            if (isEnemy == true)
            {
                ManageScore.score +=10;
            }
        }

        // updating the Hero's HealthBar
        if (isEnemy == false) {
            damaged = true;
            HealthBar.value = hp;
        }
    }

    // Restore hero's health
    public void Restore (int healCount) {
        if (hp < maxHealth) {
            if (hp + healCount < maxHealth) {
                hp += healCount;
            } else {
                hp = maxHealth;
            }
        }
    }

    // Get Immunity for 5 seconds
    public void GetImmunity()
    {
        immunity = true;
        StartCoroutine(Wait5Seconds());
        StartCoroutine(BlinkBackground());
    }

    // Changing immunity property after 5 seconds
    IEnumerator Wait5Seconds()
    {
        yield return new WaitForSecondsRealtime(5);
        immunity = false;
        ImmunityImg.color = Color.clear;
    }

    // Blinking white background while hero is immune
    IEnumerator BlinkBackground()
    {
        bool isBackgroundDisplayed = false;
        while (immunity)
        {
            isBackgroundDisplayed = !isBackgroundDisplayed;
            ChangeBackground(isBackgroundDisplayed);
            yield return new WaitForSecondsRealtime(0.05f);
        }
    }

    // Change the background color: immunity or none
    private void ChangeBackground(bool isBackgroundDisplayed)
    {
        if (isBackgroundDisplayed)
        {
            ImmunityImg.color = immunityColour;
        } else
        {
            ImmunityImg.color = Color.clear;
        }
    }

    void OnTriggerEnter2D (Collider2D otherCollider) {
        // Is this a shot?
        ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript> ();
        if (shot != null) {
            // Avoid friendly fire
            if (shot.isEnemyShot != isEnemy) {
                Damage (shot.damage);

                SoundEffectsHelper.Instance.MakeExplosionSound ();
                // Destroy the shot
                Destroy (shot.gameObject); // Remember to always target the game object, otherwise you will just remove the script
            }
        }
    }

    // changing the colour for a moment to show that the hero was damaged
    void OnHeroDamage () {
        if (isEnemy == false) {
            if (damaged) {
                DamageImg.color = flashColour;
                DamageImg.color = Color.Lerp (DamageImg.color, Color.clear, flashSpeed * Time.deltaTime);
            } else {
                DamageImg.color = Color.clear;
            }
            damaged = false;
        }
    }
}