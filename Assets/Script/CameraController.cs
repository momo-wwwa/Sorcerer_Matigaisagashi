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

    private Vector3 lastMousePosition; // �ʒu�L�^
    private Vector3 newAngle = new Vector3(0, 0, 0); // �ʒu�X�V

    void Start()
    {

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

        // ���C���J�����̉�]
        mainCamera.transform.RotateAround(mainFieldObject.transform.position, Vector3.up, angle.x);

        // �Z�J���_���J�����̉�]
        secondaryCamera.transform.RotateAround(secondaryFieldObject.transform.position, Vector3.up, angle.x);
    }

    public void ZoomCameras(float scroll, float speed)
    {
        // ���C���J�����̃Y�[������
        ZoomCamera(mainCamera, mainFieldObject, scroll, speed);

        // �Z�J���_���J�����̃Y�[������
        ZoomCamera(secondaryCamera, secondaryFieldObject, scroll, speed);
    }

    private void ZoomCamera(Camera camera, GameObject fieldObject, float scroll, float speed)
    {
        // �}�E�X�̈ʒu���X�N���[�����W���烏�[���h���W�ɕϊ�
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, fieldObject.transform.position);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            Vector3 mouseWorldPosition = ray.GetPoint(distance);

            // �J�����ƃt�B�[���h�I�u�W�F�N�g�Ԃ̕����x�N�g�����擾
            Vector3 direction = camera.transform.position - mouseWorldPosition;

            // ���݂̋������v�Z
            float currentDistance = direction.magnitude;

            // �Y�[����̐V�����������v�Z
            float newDistance = Mathf.Clamp(currentDistance - scroll * speed, minZoom, maxZoom);

            // �J�����̈ʒu��V���������Ɋ�Â��Ē���
            Vector3 targetPosition = mouseWorldPosition + direction.normalized * newDistance;

            // DOTween���g�p���ăJ�����̈ʒu���X���[�Y�Ɉړ�
            camera.transform.DOMove(targetPosition, 0.5f);
        }
    }
}
