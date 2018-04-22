using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radar : MonoBehaviour
{
    EnemyMovement enemy;
    GameObject pointer;
    GameObject pointerHandle;

    public Sprite pointerSprite;

    public float range = 5;

    public float distance = 50f;

    Slider slider;
    float sliderValue = 0.5f;

    void Start()
    {
        enemy = FindObjectOfType<EnemyMovement>();
        slider = FindObjectOfType<Slider>();

        pointerHandle = new GameObject("PointerHandle");
        pointer = new GameObject("Pointer");
        pointer.transform.SetParent(pointerHandle.transform);
        pointer.transform.localEulerAngles = new Vector3(0f, -90f, 0);
        pointer.AddComponent<SpriteRenderer>().sprite = pointerSprite;
        pointerHandle.transform.LookAt(enemy.transform.position);
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
        else
        {
            sliderValue -= Time.deltaTime / 32;
        }

        slider.value = sliderValue;

        if (sliderValue < 0)
        {
            Debug.LogError("KURWA, ZGUBIŁEM JĄ");
        }
        else if (sliderValue > 1)
        {
            Debug.LogError("KURWA, ZNALAZŁA MNIE");
        }
    }
}
