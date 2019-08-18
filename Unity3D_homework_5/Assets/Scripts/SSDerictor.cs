using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//the director class
public class SSDirector : System.Object
{
    private static SSDirector _instance;

    public SceneController currentScenceController { get; set; }

    public static SSDirector getInstance()
    {
        if (_instance == null)
        {
            _instance = new SSDirector();
        }
        return _instance;
    }

    public int getFPS()
    {
        return Application.targetFrameRate;
    }

    public void setFPS(int fps)
    {
        Application.targetFrameRate = fps;
    }
}