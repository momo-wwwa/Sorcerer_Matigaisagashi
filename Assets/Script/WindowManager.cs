using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; // SceneManagerを追加

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
    [SerializeField] ParticleSystem[] particleSystems; // 複数のパーティクルシステムを管理

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

        // すべてのパーティクルシステムを停止してクリア
        foreach (var particle in particleSystems)
        {
            if (particle.isPlaying)
            {
                particle.Stop();
                particle.Clear(); // パーティクルをクリア
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

        // ゲームオーバー時にもすべてのパーティクルシステムを停止
        foreach (var particle in particleSystems)
        {
            if (particle.isPlaying)
            {
                particle.Stop();
                particle.Clear(); // パーティクルをクリア
            }
        }
    }

    // ゲームをリセットするメソッド（シーンをリロード）
    public void ResetGame()
    {
        // 現在のシーンを再ロード
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
