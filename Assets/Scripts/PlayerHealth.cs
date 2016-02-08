using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    public float maxHealth = 100f;
    public float currentHealth = 100f;
    public Slider healthSlider;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    public void TakeDamage()
    {
        TakeDamage(10f);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthSlider.value = currentHealth;
    }
}
