using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
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
        
    }

    public void OnClick()
    {
        Debug.Log("クリック");
        if (count == 0)
        {
            // 子オブジェクトのパーティクルシステムをアクティブにして再生する
            particle.Play();
            particle2.Play(); // パーティクルを再生

            window.GetComponent<WindowManager>().Count();
        }
        
        count++;
    }
}

