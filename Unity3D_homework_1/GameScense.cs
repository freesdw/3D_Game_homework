using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScense : MonoBehaviour {
    //地图。 0表示未被占据，1 表示蓝色方占据， 2表示红色方占据
    private int[,] map = new int[3, 3];
    private int player = 0;
    public AudioClip audio;	//开始的音乐
    private AudioSource audioSource;
	//玩家图片
    public Texture2D img_0;
    public Texture2D img_1;

    /// <summary>
    /// 重置地图（全为0）
    /// </summary>
    void ResetMap()
    {
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                map[i, j] = 0;
            }
        }

        audioSource.Play();
        //startAudio.gameObject.GetComponent<AudioSource>().Play();
    }
    private void Awake()
    {//设置AudioSource组件
        audioSource = this.gameObject.GetComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.volume = 1.0f;
        audioSource.clip = audio;
    }

    // Use this for initialization
    void Start () {
        ResetMap();    
	}

    /// <summary>
    /// 判断游戏是否结束
    /// </summary>
    /// <returns>1表示蓝方赢，2表示红方赢</returns>
    private int WhetherWin()
    {
        for(int i = 0; i < 3; i++)
        {
            if(map[i, 0] == map[i, 1] && map[i, 1] == map[i, 2] && map[i, 0] != 0)
            {
                return map[i, 0];
            }
        }
        for(int j = 0; j < 3; j++)
        {
            if(map[0, j] == map[1, j] && map[1, j] == map[2, j] && map[0, j] != 0)
            {
                return map[0, j];
            }
        }
        if(map[0, 0] == map[1, 1] && map[1, 1] == map[2, 2] && map[0, 0] != 0)
        {
            return map[0, 0];
        }
        if(map[0, 2] == map[1, 1] && map[1, 1] == map[2, 0] && map[1, 1] != 0)
        {
            return map[1, 1];
        }
        return 0;
    }
	//用屏幕的高度作为度量单位来处理屏幕大小不同而导致的问题
    private void OnGUI()
    {
        int midWidth = Screen.width / 2;
        int midHeight = Screen.height / 2;
        int buttonEdge = Screen.height / 5;
        GUIStyle fontStyle_0 = new GUIStyle();
        fontStyle_0.fontSize = buttonEdge / 2;
        fontStyle_0.fontStyle = FontStyle.Bold;
        fontStyle_0.normal.textColor = Color.black;

        GUIStyle fontStyle_1 = new GUIStyle();
        fontStyle_1.fontSize = buttonEdge /2;
        fontStyle_1.fontStyle = FontStyle.Bold;
        fontStyle_1.normal.textColor = Color.blue;

        GUIStyle fontStyle_2 = new GUIStyle();
        fontStyle_2.fontSize = buttonEdge / 2;
        fontStyle_2.fontStyle = FontStyle.Bold;
        fontStyle_2.normal.textColor = Color.red;
        
        GUI.Label(new Rect(midWidth - buttonEdge * 4, midHeight - 1.5f * buttonEdge, 1.5f * buttonEdge, 1.5f * buttonEdge), img_0);
        GUI.Label(new Rect(midWidth - buttonEdge * 4, midHeight, 1.5f * buttonEdge, buttonEdge), "Blue", fontStyle_1);
        GUI.Label(new Rect(midWidth + buttonEdge * 3, midHeight - 1.5f * buttonEdge, 1.5f * buttonEdge, 1.5f * buttonEdge), img_1);
        GUI.Label(new Rect(midWidth + buttonEdge * 3, midHeight, 1.5f * buttonEdge, buttonEdge), "Red", fontStyle_2);

        int winner = WhetherWin();
    //    Debug.Log(winner);
        if(winner == 1)
        {
            if (GUI.Button(new Rect(midWidth - buttonEdge * 1.5f, 0.5f * buttonEdge, 3 * buttonEdge, 3.5f * buttonEdge), "Blue Win", fontStyle_1))
            {
                ResetMap();
            }
        }
        else if(winner == 2)
        {
            if (GUI.Button(new Rect(midWidth - buttonEdge * 1.5f,0.5f * buttonEdge, 3 * buttonEdge, 3.5f * buttonEdge), "Red Win", fontStyle_2))
            {
                ResetMap();
            }
        }
        else
        {
            GUI.Label(new Rect(midWidth - buttonEdge * 2.5f, buttonEdge / 4, buttonEdge * 2, buttonEdge), "Welcome To Tictactoe", fontStyle_0);
        }

        if(GUI.Button(new Rect(midWidth - 0.5f*buttonEdge, midHeight + 1.75f*buttonEdge, buttonEdge, 0.5f*buttonEdge), "Reset"))
        {
            ResetMap();
        }

        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                float x = midWidth - 1.5f * buttonEdge + j * buttonEdge;
                float y = midHeight - 1.5f * buttonEdge + i * buttonEdge;
                if ( map[i, j] == 1)
                {
                    GUI.Button(new Rect(x, y, buttonEdge, buttonEdge), img_0);
                }
                else if(map[i, j] == 2)
                {
                    GUI.Button(new Rect(x, y, buttonEdge, buttonEdge), img_1);
                }
                else
                {
                    if(GUI.Button(new Rect(x, y, buttonEdge, buttonEdge), ""))
                    {
                        if(player % 2 == 0)
                        {
                            map[i, j] = 1;
                        }
                        else
                        {
                            map[i, j] = 2;
                        }
                        player = (player + 1) % 2;//改变玩家角色
                    }
                }
            }
        }
    }
}
