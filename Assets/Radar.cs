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
        //pointer.transform.localEulerAngles = new Vector3(180f, -90f, -90f);
        pointer.AddComponent<SpriteRenderer>().sprite = pointerSprite;
        //pointerHandle.transform.LookAt(enemy.transform.position);
        slider.value = sliderValue;
        Quaternion.
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
            Debug.LogError("KURWA, ZGUBIŁEM JĄ");
        }
        else if (sliderValue > 1)
        {
            Debug.LogError("KURWA, ZNALAZŁA MNIE");
        }

        sliderValue = Mathf.Clamp(sliderValue, -0.1f, 1.1f);
    }
}
