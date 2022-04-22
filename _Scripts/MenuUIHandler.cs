using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuUIHandler : MonoBehaviour
{
    public PlayerController playerController;

    public AudioSource audioSource;

    [Header("Scene UI Objects")]
    public GameObject menuSceneUI;
    public GameObject menuSceneHeaderRow;
    public GameObject menuSceneMidRow;
    public GameObject inGameSceneUI;
    public GameObject gameOverSceneUI;
    public GameObject soundOn;
    public GameObject soundOff;

    private Animator m_headerRowAnimator;
    private Animator m_midRowAnimator;
    

    private void Awake()
    {
        m_headerRowAnimator = menuSceneHeaderRow.GetComponent<Animator>();
        m_midRowAnimator = menuSceneMidRow.GetComponent<Animator>();
    }
    private void Start()
    {
        playerController.enabled = false;
    }

    public void StartButton()
    {
        GameManager.isGameStarted = true;
        StartCoroutine(StartMenuCoroutine());
        m_headerRowAnimator.SetTrigger("disappear_trig");
        m_midRowAnimator.SetTrigger("disappear_trig");
        inGameSceneUI.SetActive(true);
        playerController.enabled = true;
    }
    private IEnumerator StartMenuCoroutine()
    {
        yield return new WaitForSecondsRealtime(1);
        menuSceneUI.SetActive(false);
    }
    public void RetryButton()
    {
        SceneManager.LoadScene(0);
    }
    public void DisableSoundsButton()
    {
        audioSource.enabled = false;
        soundOn.SetActive(true);
        soundOff.SetActive(false);
    }
    public void EnableSoundsButton()
    {

        audioSource.enabled = true;
        soundOn.SetActive(false);
        soundOff.SetActive(true);
    }
}
