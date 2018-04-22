using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Radar : MonoBehaviour
{
    EnemyMovement enemy;
    GameObject pointer;

    public Sprite pointerSprite;

    public float distance = 50f;

    Slider slider;
    float sliderValue = 0.5f;

    void Start()
    {
        Cursor.visible = false;
        enemy = FindObjectOfType<EnemyMovement>();
        slider = FindObjectOfType<Slider>();
        
        pointer = new GameObject("Pointer");
        pointer.AddComponent<SpriteRenderer>().sprite = pointerSprite;
        slider.value = sliderValue;
    }


    void Update()
    {
        Vector3 direction = enemy.transform.position - transform.position;
        pointer.transform.position = transform.position + direction.normalized * 2;

        if (Vector3.Distance(transform.position, enemy.transform.position) < distance)
        {
            sliderValue += Time.deltaTime / 8;
        }
        else if (Vector3.Distance(transform.position, enemy.transform.position) < distance * 1.5f)
        {
            // NIE ZMIENIAJ PASKA
        }
        else
        {
            sliderValue -= Time.deltaTime / 32;
        }

        slider.value = sliderValue;

        if (sliderValue < 0)
        {
            SceneManager.LoadScene("LOSE");
        }
        else if (sliderValue > 1)
        {
            SceneManager.LoadScene("LOSE");
        }
        pointer.transform.LookAt(enemy.transform.position);
    }
}
