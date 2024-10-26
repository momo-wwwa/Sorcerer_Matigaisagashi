using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WindowManager : MonoBehaviour
{
    [SerializeField] GameObject nokori;
    [SerializeField] GameObject time;
    [SerializeField] GameObject clear;
    [SerializeField] GameObject clear2;
    [SerializeField] GameObject gameOverPanel1; // ゲームオーバーパネル
    [SerializeField] GameObject gameOverPanel2; // ゲームオーバーパネル
    [SerializeField] GameObject fieldObeject;
    [SerializeField] GameObject gameCamera;
    [SerializeField] GameObject startCamera; // スタートカメラ
    [SerializeField] GameObject start;
    [SerializeField] AudioSource audioSource; // AudioSourceを追加
    [SerializeField] AudioSource BGM; // AudioSourceを追加
    [SerializeField] AudioSource Clear;
    [SerializeField] AudioSource Gameover;

    private int quantity = 0;
    private float downTime = 30.0f;
    private bool gameActive = false; // 初期状態は非アクティブ

    // Start is called before the first frame update
    void Start()
    {
        nokori.GetComponent<TextMeshProUGUI>().text = quantity.ToString();
        gameOverPanel1.SetActive(false); // ゲームオーバーパネルは最初は非表示
        gameOverPanel2.SetActive(false);
        gameCamera.SetActive(false);
        BGM.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameActive) // ゲームがアクティブな場合にのみダウンタイムを減少
        {
            gameCamera.SetActive(true);
            startCamera.SetActive(false);
            start.SetActive(false);
            downTime -= Time.deltaTime;
            time.GetComponent<TextMeshProUGUI>().text = downTime.ToString("F2"); // 小数点以下2桁まで表示

            // ダウンタイムが0以下になったら、ゲームオーバー処理を追加
            if (downTime == 0)
            {
                GameOver();
            }
        }
    }

    public void Count()
    {
        if (quantity < 10)
        {
            quantity += 1;
            nokori.GetComponent<TextMeshProUGUI>().text = quantity.ToString();

            if (quantity == 9)
            {
                clear.SetActive(true);
                clear2.SetActive(true);
                HideOtherObjects();
                StopGame(); // ゲームを停止するメソッドを呼び出す
                BGM.Stop();
                Clear.Play();
            }
        }
    }

    public void StartGame() // ゲーム開始メソッド
    {
        gameActive = true; // ゲームをアクティブにする
        gameCamera.SetActive(true);
        startCamera.SetActive(false);
        downTime = 30.0f; // ダウンタイムをリセット
        quantity = 0; // 数量をリセット
        nokori.GetComponent<TextMeshProUGUI>().text = quantity.ToString(); // 数量表示を更新
        // 音楽を再生
        audioSource.Play(); // 音楽を再生する
    }

    private IEnumerator StopMusicAfterTime(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait); // 指定した時間待つ
        audioSource.Stop(); // 音楽を停止
    }

    private void HideOtherObjects()
    {
        // フィールドのオブジェクトを非表示にする
        fieldObeject.SetActive(false);

        // 他のオブジェクトを非表示にする
        nokori.SetActive(false);
        time.SetActive(false);
    }

    private void StopGame()
    {
        gameActive = false; // ゲームを非アクティブにする
    }

    private void GameOver()
    {
        // ゲームオーバーの処理をここに追加
        Debug.Log("Game Over!");

        // ゲームオーバーパネルを表示
        gameOverPanel1.SetActive(true);
        gameOverPanel2.SetActive(true);
        Gameover.Play();
        BGM.Stop();

        // フィールドのオブジェクトを非表示にする
        HideOtherObjects();
    }
}
