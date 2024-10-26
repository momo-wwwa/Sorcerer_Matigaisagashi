using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource; // AudioSourceを追加
    [SerializeField] private float musicPlayTime = 0.5f; // 30秒間音楽を再生
    private GameObject window;
    [SerializeField]
    [Tooltip("発生させるエフェクト(パーティクル)")]
    private ParticleSystem particle;
    [SerializeField]
    [Tooltip("発生させるエフェクト(パーティクル)")] private ParticleSystem particle2;
    int count = 0; 


    private void Start()
    {
        window = GameObject.Find("WindowManager");
            particle.Stop();
            particle2.Stop();
            audioSource.Stop(); // スタート時に音声を停止
    }



    public void OnClick()
    {
        Debug.Log("クリック");
        // すでに音楽が再生中であれば停止
        if (audioSource.isPlaying)
        {
            audioSource.Stop(); // 音楽を停止
        }

        audioSource.Play(); // 音楽を再生する
        StartCoroutine(StopMusicAfterTime(musicPlayTime)); // 音楽を停止するコルーチンを開始

        if (count == 0)
        {
            // 子オブジェクトのパーティクルシステムをアクティブにして再生する
            particle.Play();
            particle2.Play(); // パーティクルを再生
                          


            window.GetComponent<WindowManager>().Count();
        }

        count++;
    }
        private IEnumerator StopMusicAfterTime(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait); // 指定した時間待つ
        audioSource.Stop(); // 音楽を停止
    }

}

