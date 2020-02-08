﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpScript : MonoBehaviour {

    public int add = 1;
    public string type = "hitPoints";

    void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerScript hero = collision.gameObject.GetComponent<PlayerScript>();
        if (hero != null)
        {
            switch (type)
            {
                case "hitPoints":
                    HealthScript life = hero.gameObject.GetComponent<HealthScript>();
                    life.Restore(add);
                    Destroy(gameObject);
                    // updating the Hero's HealthBar
                    Canvas.FindObjectOfType<Slider>().value = life.hp;
                    break;
                // Immunity power-up
                case "immunity":
                    HealthScript immunity = hero.gameObject.GetComponent<HealthScript>();
                    immunity.GetImmunity();
                    Destroy(gameObject);
                    break;
            }            
        }
        else
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        }       
    }
}
