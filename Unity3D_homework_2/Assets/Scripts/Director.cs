using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//导演类。不多说了
public class Director : System.Object {
    private static Director _instance;
    public SceneController currentSceneController { set; get; }

    public static Director getInstance()
    {
        if(_instance == null)
        {
            _instance = new Director();
        }
        return _instance;
    }
}
