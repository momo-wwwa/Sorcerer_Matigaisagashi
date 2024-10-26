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
    [SerializeField] GameObject gameOverPanel1; // �Q�[���I�[�o�[�p�l��
    [SerializeField] GameObject gameOverPanel2; // �Q�[���I�[�o�[�p�l��
    [SerializeField] GameObject fieldObeject;
    [SerializeField] GameObject gameCamera;
    [SerializeField] GameObject startCamera; // �X�^�[�g�J����
    [SerializeField] GameObject start;
    [SerializeField] AudioSource audioSource; // AudioSource��ǉ�
    [SerializeField] AudioSource BGM; // AudioSource��ǉ�
    [SerializeField] AudioSource Clear;
    [SerializeField] AudioSource Gameover;

    private int quantity = 0;
    private float downTime = 30.0f;
    private bool gameActive = false; // ������Ԃ͔�A�N�e�B�u

    // Start is called before the first frame update
    void Start()
    {
        nokori.GetComponent<TextMeshProUGUI>().text = quantity.ToString();
        gameOverPanel1.SetActive(false); // �Q�[���I�[�o�[�p�l���͍ŏ��͔�\��
        gameOverPanel2.SetActive(false);
        gameCamera.SetActive(false);
        BGM.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameActive) // �Q�[�����A�N�e�B�u�ȏꍇ�ɂ̂݃_�E���^�C��������
        {
            gameCamera.SetActive(true);
            startCamera.SetActive(false);
            start.SetActive(false);
            downTime -= Time.deltaTime;
            time.GetComponent<TextMeshProUGUI>().text = downTime.ToString("F2"); // �����_�ȉ�2���܂ŕ\��

            // �_�E���^�C����0�ȉ��ɂȂ�����A�Q�[���I�[�o�[������ǉ�
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
                StopGame(); // �Q�[�����~���郁�\�b�h���Ăяo��
                BGM.Stop();
                Clear.Play();
            }
        }
    }

    public void StartGame() // �Q�[���J�n���\�b�h
    {
        gameActive = true; // �Q�[�����A�N�e�B�u�ɂ���
        gameCamera.SetActive(true);
        startCamera.SetActive(false);
        downTime = 30.0f; // �_�E���^�C�������Z�b�g
        quantity = 0; // ���ʂ����Z�b�g
        nokori.GetComponent<TextMeshProUGUI>().text = quantity.ToString(); // ���ʕ\�����X�V
        // ���y���Đ�
        audioSource.Play(); // ���y���Đ�����
    }

    private IEnumerator StopMusicAfterTime(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait); // �w�肵�����ԑ҂�
        audioSource.Stop(); // ���y���~
    }

    private void HideOtherObjects()
    {
        // �t�B�[���h�̃I�u�W�F�N�g���\���ɂ���
        fieldObeject.SetActive(false);

        // ���̃I�u�W�F�N�g���\���ɂ���
        nokori.SetActive(false);
        time.SetActive(false);
    }

    private void StopGame()
    {
        gameActive = false; // �Q�[�����A�N�e�B�u�ɂ���
    }

    private void GameOver()
    {
        // �Q�[���I�[�o�[�̏����������ɒǉ�
        Debug.Log("Game Over!");

        // �Q�[���I�[�o�[�p�l����\��
        gameOverPanel1.SetActive(true);
        gameOverPanel2.SetActive(true);
        Gameover.Play();
        BGM.Stop();

        // �t�B�[���h�̃I�u�W�F�N�g���\���ɂ���
        HideOtherObjects();
    }
}
