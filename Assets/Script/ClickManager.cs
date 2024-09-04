using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    private GameObject window;
    [SerializeField]
    [Tooltip("����������G�t�F�N�g(�p�[�e�B�N��)")]
    private ParticleSystem particle;
    [SerializeField]
    [Tooltip("����������G�t�F�N�g(�p�[�e�B�N��)")] private ParticleSystem particle2;
    int count = 0; 


    private void Start()
    {
        window = GameObject.Find("WindowManager");
            particle.Stop();
            particle2.Stop();
        
    }

    public void OnClick()
    {
        Debug.Log("�N���b�N");
        if (count == 0)
        {
            // �q�I�u�W�F�N�g�̃p�[�e�B�N���V�X�e�����A�N�e�B�u�ɂ��čĐ�����
            particle.Play();
            particle2.Play(); // �p�[�e�B�N�����Đ�

            window.GetComponent<WindowManager>().Count();
        }
        
        count++;
    }
}

