using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TaskHandler : MonoBehaviour
{
    public Sprite[] Tex;

    public Color32[] BColor;

    public Color32[] BgColor;

    public Camera Cam;

    public GameObject Patterns;

    public Text Level;

    public Material GMat;

    public Material WMat;

    public GameObject GPanel;

    public GameObject Extras;

    public GameObject TapToStart;

    private List<GameObject> PatList;

    private int Index;

    private string LevelPref = "Level";

    private string PreLevel = "PreLevel";

    private float width = 16f;

    private float height = 9f;

    private void Awake()
    {
        //this.Cam.aspect = this.height / this.width;
        this.Index = UnityEngine.Random.Range(0, 2);
        this.GMat.mainTexture = this.Tex[this.Index].texture;
        this.Cam.backgroundColor = this.BgColor[GameManager.Instance.ColorIndex];
        this.WMat.color = this.BColor[GameManager.Instance.ColorIndex];
        GameManager.Instance.ColorIndex++;
        if (GameManager.Instance.ColorIndex == 10)
        {
            GameManager.Instance.ColorIndex = 0;
        }

        this.PatList = new List<GameObject>();
        for (int i = 0; i < this.Patterns.transform.childCount; i++)
        {
            this.PatList.Add(this.Patterns.transform.GetChild(i).gameObject);
        }
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey(this.LevelPref))
        {
            PlayerPrefs.SetInt(this.LevelPref, 1);
        }

        if (!PlayerPrefs.HasKey(this.PreLevel))
        {
            PlayerPrefs.SetInt(this.PreLevel, 1);
        }

        this.GPanel.SetActive(true);
        this.Extras.SetActive(true);
        this.SetScene();
    }

    private void SetScene()
    {
        GameManager.Instance.LevelCount = PlayerPrefs.GetInt(this.LevelPref);
        this.Level.text = GameManager.Instance.LevelCount.ToString();
        if (GameManager.Instance.LevelCount > 53)
        {
            if (GameManager.Instance.ChangeStatus)
            {
                do
                {
                    GameManager.Instance.PatternCount = UnityEngine.Random.Range(0, 53);
                } while (GameManager.Instance.PatternCount == PlayerPrefs.GetInt(this.PreLevel));
            }
            else
            {
                GameManager.Instance.PatternCount = PlayerPrefs.GetInt(this.PreLevel);
            }
        }
        else
        {
            GameManager.Instance.PatternCount = GameManager.Instance.LevelCount - 1;
        }

        this.PatList[GameManager.Instance.PatternCount].SetActive(true);
        GameManager.Instance.GrassCount = this.PatList[GameManager.Instance.PatternCount].transform.GetChild(0)
            .gameObject.transform.childCount;
    }

    public void NextLevel()
    {
        GameManager.Instance.PlayStatus = false;
        PlayerPrefs.SetInt(this.PreLevel, GameManager.Instance.PatternCount);
        GameManager.Instance.ChangeStatus = true;
        PlayerPrefs.SetInt(this.LevelPref, GameManager.Instance.LevelCount);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Restart()
    {
        GameManager.Instance.adcount++;
        GameManager.Instance.PlayStatus = false;
        PlayerPrefs.SetInt(this.PreLevel, GameManager.Instance.PatternCount);
        GameManager.Instance.ChangeStatus = false;
        AdsControl.Instance.showAds();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public GameObject _subPanel;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !GameManager.Instance.PlayStatus &&
            !EventSystem.current.IsPointerOverGameObject())
        {
            if (GameDataManager.Instance.playerData.IsRegister)
            {
                GameManager.Instance.PlayStatus = true;
                this.TapToStart.SetActive(false);
                this.Extras.SetActive(false);
            }
            else
            {
                
                _subPanel.SetActive(true);
            }
        }
    }
}