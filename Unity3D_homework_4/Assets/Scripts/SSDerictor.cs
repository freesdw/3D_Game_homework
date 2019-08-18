/** 
 * 这个文件是用来场景控制的，负责各个场景的切换， 
 * 虽然目前只有一个场景 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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