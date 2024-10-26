using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    public Camera mainCamera; // ���C���J����
    public Camera secondaryCamera; // �Z�J���_���J����
    public GameObject mainFieldObject; // ���C���J�����p�̃t�B�[���h�I�u�W�F�N�g
    public GameObject secondaryFieldObject; // �Z�J���_���J�����p�̃t�B�[���h�I�u�W�F�N�g
    public float rotateSpeed = 1.0f;
    public float zoomSpeed = 5.0f; // �Y�[�����x
    public float minZoom = 1.0f; // �ŏ��Y�[���l
    public float maxZoom = 10.0f; // �ő�Y�[���l
    public float returnDuration = 0.5f; // ���̈ʒu�ɖ߂�����

    private Vector3 lastMousePosition; // �ʒu�L�^
    private Vector3 newAngle = new Vector3(0, 0, 0); // �ʒu�X�V
    private float currentDistanceMain; // ���C���J�����̌��݂̃Y�[������
    private float currentDistanceSecondary; // �Z�J���_���J�����̌��݂̃Y�[������
    private float initialDistanceMain; // ���C���J�����̏����Y�[������
    private float initialDistanceSecondary; // �Z�J���_���J�����̏����Y�[������

    void Start()
    {
        // �����Y�[���������L�^
        currentDistanceMain = initialDistanceMain = (mainCamera.transform.position - mainFieldObject.transform.position).magnitude;
        currentDistanceSecondary = initialDistanceSecondary = (secondaryCamera.transform.position - secondaryFieldObject.transform.position).magnitude;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            newAngle = mainCamera.transform.localEulerAngles;
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            rotateCameras();
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            ZoomCameras(scroll, zoomSpeed);
        }
    }

    private void rotateCameras()
    {
        Vector3 angle = new Vector3(
            Input.GetAxis("Mouse X") * rotateSpeed,
            0,
            0
        );

        // ���C���J�����̃t�B�[���h�I�u�W�F�N�g�𒆐S�ɉ�]
        mainCamera.transform.RotateAround(mainFieldObject.transform.position, Vector3.up, angle.x);

        // �Z�J���_���J�������Z�J���_���t�B�[���h�I�u�W�F�N�g�𒆐S�ɓ����ʂ�����]
        secondaryCamera.transform.RotateAround(secondaryFieldObject.transform.position, Vector3.up, angle.x);
    }

    public void ZoomCameras(float scroll, float speed)
    {
        // ���C���J�����ƃZ�J���_���J�����̃Y�[�������𓯂��Y�[���ʂōs��
        ZoomCamera(mainCamera, mainFieldObject, scroll, ref currentDistanceMain, speed, initialDistanceMain);
        ZoomCamera(secondaryCamera, secondaryFieldObject, scroll, ref currentDistanceSecondary, speed, initialDistanceSecondary);
    }

    private void ZoomCamera(Camera camera, GameObject fieldObject, float scroll, ref float currentDistance, float speed, float initialDistance)
    {
        Vector3 direction = camera.transform.position - fieldObject.transform.position;

        // �V�����������v�Z
        float newDistance = currentDistance - scroll * speed;

        // �Y�[�������������𒴂����ꍇ�A�����I�ɏ��������ɖ߂�
        if (newDistance < minZoom || newDistance > maxZoom)
        {
            newDistance = initialDistance; // ���������Ƀ��Z�b�g
        }
        currentDistance = Mathf.Clamp(newDistance, minZoom, maxZoom); // �������ɃN�����v

        // �V�����J�����̈ʒu���v�Z
        Vector3 targetPosition = fieldObject.transform.position + direction.normalized * currentDistance;

        // DOTween���g�p���ăJ�������ړ�
        camera.transform.DOMove(targetPosition, returnDuration);
    }
}
