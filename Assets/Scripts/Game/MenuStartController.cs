using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuStartController : MonoBehaviour
{
    public Button startBtn, exitBtn, mainModeBtn, timeModeBtn;
    public Sprite startBtnHover, exitBtnHover, mainModeHover, timeModeHover;
    Sprite defaultStartBtnSprite, defaultExitBtnSprite, defaultMainModeBtnSprite, defaultTimeModeBtnSprite;
    public GameObject blackScene, startObj, mode;
    Animator blackSceneAnim, startBtnAnim, exitBtnAnim, mainModeBtnAnim, timeModeBtnAnim;

    public AudioClip buttonClip;
    AudioSource music;

    public ParticleSystem clickEffect;

    private void Awake()
    {
        music = gameObject.GetComponent<AudioSource>();
        blackSceneAnim = blackScene.GetComponent<Animator>();
        startBtnAnim = startBtn.GetComponent<Animator>();
        exitBtnAnim = exitBtn.GetComponent<Animator>();
        mainModeBtnAnim = mainModeBtn.GetComponent<Animator>();
        timeModeBtnAnim = timeModeBtn.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        blackSceneAnim.Play("BlackSceneBegin");
        Time.timeScale = 1;
        defaultStartBtnSprite = startBtn.image.sprite;
        defaultExitBtnSprite = exitBtn.image.sprite;
        defaultMainModeBtnSprite = mainModeBtn.image.sprite;
        defaultTimeModeBtnSprite = timeModeBtn.image.sprite;
        music.clip = buttonClip;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //Hieu ung khi click chuot
        {
            music.Play();
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z += 10;
            Instantiate(clickEffect, mousePos, Quaternion.identity);
        }
    }
    public void onHoverStartBtn()
    {
        startBtnAnim.Play("BtnEnter");
        music.Play();
        startBtn.image.sprite = startBtnHover;
    }

    public void onExitStartBtn()
    {
        startBtnAnim.Play("BtnExit");
        startBtn.image.sprite = defaultStartBtnSprite;
    }

    public void onHoverExitBtn()
    {
        exitBtnAnim.Play("BtnEnter");
        music.Play();
        exitBtn.image.sprite = exitBtnHover;
    }
    public void onExitExitBtn()
    {
        exitBtnAnim.Play("BtnExit");
        exitBtn.image.sprite = defaultExitBtnSprite;
    }

    public void onHoverMainModeBtn()
    {
        mainModeBtnAnim.Play("BtnEnter");
        music.Play();
        mainModeBtn.image.sprite = mainModeHover;
    }

    public void onExitMainModeBtn()
    {
        mainModeBtnAnim.Play("BtnExit");
        mainModeBtn.image.sprite = defaultMainModeBtnSprite;
    }
    public void onHoverTimeModeBtn()
    {
        timeModeBtnAnim.Play("BtnEnter");
        music.Play();
        timeModeBtn.image.sprite = timeModeHover;
    }

    public void onExitTimeModeBtn()
    {
        timeModeBtnAnim.Play("BtnExit");
        timeModeBtn.image.sprite = defaultTimeModeBtnSprite;
    }

    public void ExitBtn()
    {
        Debug.Log("Exit");
        Application.Quit();
    }    
    public void StartBtn()
    {
        startObj.SetActive(false);
        mode.SetActive(true);
        
        //SceneManager.LoadScene(1);
    }

    public void MainModeBtn()
    {
        music.Play();
        blackSceneAnim.Play("BlackSceneEnd");
        Invoke("LoadGameScene", 1);
    }

    public void TimeModeBtn()
    {
        music.Play();
        blackSceneAnim.Play("BlackSceneEnd");
        Invoke("LoadTimeModeScene", 1);
    }

    void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }

    void LoadTimeModeScene()
    {
        SceneManager.LoadScene(2);
    }
}
