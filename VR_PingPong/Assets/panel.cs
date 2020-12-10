using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panel : MonoBehaviour
{
    private Canvas canvas = null;
    private GUI_control gui = null;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    public void Setup(GUI_control gui)
    {
        this.gui = gui;
        hide();
    }

    public void show()
    {
        canvas.enabled = true;
    }

    public void hide()
    {
        canvas.enabled = false;
    }
}
