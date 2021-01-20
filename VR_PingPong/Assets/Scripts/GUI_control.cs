using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUI_control : MonoBehaviour
{
    public panel current = null;

    public Animator transition;

    private void Start()
    {
        GetPanels();
    }

    private void GetPanels()
    {
        panel[] panels = GetComponentsInChildren<panel>();

        foreach (panel p in panels)
        {
            p.Setup(this);
        }

        current.show();
    }

    public void SetCurrent(panel newpanel)
    {
        current.hide();
        current = newpanel;
        current.show();
    }

    public void ChangeScene(string scene_name)
    {
        SceneManager.LoadScene(scene_name);
    }


    public void ExitApplication()
    {
        Application.Quit();
    }
}
