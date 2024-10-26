using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource; // AudioSource��ǉ�
    [SerializeField] private float musicPlayTime = 0.5f; // 30�b�ԉ��y���Đ�
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
            audioSource.Stop(); // �X�^�[�g���ɉ������~
    }



    public void OnClick()
    {
        Debug.Log("�N���b�N");
        // ���łɉ��y���Đ����ł���Β�~
        if (audioSource.isPlaying)
        {
            audioSource.Stop(); // ���y���~
        }

        audioSource.Play(); // ���y���Đ�����
        StartCoroutine(StopMusicAfterTime(musicPlayTime)); // ���y���~����R���[�`�����J�n

        if (count == 0)
        {
            // �q�I�u�W�F�N�g�̃p�[�e�B�N���V�X�e�����A�N�e�B�u�ɂ��čĐ�����
            particle.Play();
            particle2.Play(); // �p�[�e�B�N�����Đ�
                          


            window.GetComponent<WindowManager>().Count();
        }

        count++;
    }
        private IEnumerator StopMusicAfterTime(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait); // �w�肵�����ԑ҂�
        audioSource.Stop(); // ���y���~
    }

}

