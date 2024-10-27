using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; // SceneManager��ǉ�

public class WindowManager : MonoBehaviour
{
    [SerializeField] GameObject nokori;
    [SerializeField] GameObject time;
    [SerializeField] GameObject clear;
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject fieldObject;
    [SerializeField] GameObject gameCamera;
    [SerializeField] GameObject startCamera;
    [SerializeField] GameObject start;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource BGM;
    [SerializeField] AudioSource Clear;
    [SerializeField] AudioSource Gameover;
    [SerializeField] ParticleSystem[] particleSystems; // �����̃p�[�e�B�N���V�X�e�����Ǘ�

    private int quantity = 0;
    private float downTime = 30.0f;
    private bool gameActive = false;

    public void Start()
    {
        BGM.Play();
        fieldObject.SetActive(true);
        nokori.GetComponent<TextMeshProUGUI>().text = quantity.ToString();
        start.SetActive(true);
        clear.SetActive(false);
        gameOver.SetActive(false);
        startCamera.SetActive(true);
        gameCamera.SetActive(false);
    }

    void Update()
    {
        if (gameActive)
        {
            gameCamera.SetActive(true);
            downTime -= Time.deltaTime;
            time.GetComponent<TextMeshProUGUI>().text = downTime.ToString("F2");

            if (downTime <= 0)
            {
                gameActive = false;
                GameOver();
            }
        }
    }

    public void Count()
    {
        if (quantity < 11)
        {
            quantity += 1;
            nokori.GetComponent<TextMeshProUGUI>().text = quantity.ToString();

            if (quantity == 10)
            {
                gameCamera.SetActive(false);
                startCamera.SetActive(true);
                clear.SetActive(true);
                HideOtherObjects();
                StopGame();
                BGM.Stop();
                Clear.Play();
            }
        }
    }

    public void StartGame()
    {
        gameActive = true;
        gameCamera.SetActive(true);
        startCamera.SetActive(false);
        start.SetActive(false);
        downTime = 30.0f;
        quantity = 0;
        nokori.SetActive(true);
        time.SetActive(true);
        nokori.GetComponent<TextMeshProUGUI>().text = quantity.ToString();
        audioSource.Play();
    }

    private IEnumerator StopMusicAfterTime(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        audioSource.Stop();
    }

    private void HideOtherObjects()
    {
        fieldObject.SetActive(false);
        nokori.SetActive(false);
        time.SetActive(false);
    }

    private void StopGame()
    {
        gameActive = false;

        // ���ׂẴp�[�e�B�N���V�X�e�����~���ăN���A
        foreach (var particle in particleSystems)
        {
            if (particle.isPlaying)
            {
                particle.Stop();
                particle.Clear(); // �p�[�e�B�N�����N���A
            }
        }
    }

    private void GameOver()
    {
        gameOver.SetActive(true);
        gameCamera.SetActive(false);
        startCamera.SetActive(true);
        Gameover.Play();
        BGM.Stop();
        HideOtherObjects();

        // �Q�[���I�[�o�[���ɂ����ׂẴp�[�e�B�N���V�X�e�����~
        foreach (var particle in particleSystems)
        {
            if (particle.isPlaying)
            {
                particle.Stop();
                particle.Clear(); // �p�[�e�B�N�����N���A
            }
        }
    }

    // �Q�[�������Z�b�g���郁�\�b�h�i�V�[���������[�h�j
    public void ResetGame()
    {
        // ���݂̃V�[�����ă��[�h
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
