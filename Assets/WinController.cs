using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinController : MonoBehaviour
{
    public Button start;

    private void Start()
    {
        Cursor.visible = true;
        start.onClick.AddListener(() => SceneManager.LoadScene("MAIN"));
    }
}
