using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    public Camera mainCamera; // メインカメラ
    public Camera secondaryCamera; // セカンダリカメラ
    public GameObject mainFieldObject; // メインカメラ用のフィールドオブジェクト
    public GameObject secondaryFieldObject; // セカンダリカメラ用のフィールドオブジェクト
    public float rotateSpeed = 1.0f;
    public float zoomSpeed = 5.0f; // ズーム速度
    public float minZoom = 1.0f; // 最小ズーム値
    public float maxZoom = 10.0f; // 最大ズーム値

    private Vector3 lastMousePosition; // 位置記録
    private Vector3 newAngle = new Vector3(0, 0, 0); // 位置更新

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

        // メインカメラの回転
        mainCamera.transform.RotateAround(mainFieldObject.transform.position, Vector3.up, angle.x);

        // セカンダリカメラの回転
        secondaryCamera.transform.RotateAround(secondaryFieldObject.transform.position, Vector3.up, angle.x);
    }

    public void ZoomCameras(float scroll, float speed)
    {
        // メインカメラのズーム処理
        ZoomCamera(mainCamera, mainFieldObject, scroll, speed);

        // セカンダリカメラのズーム処理
        ZoomCamera(secondaryCamera, secondaryFieldObject, scroll, speed);
    }

    private void ZoomCamera(Camera camera, GameObject fieldObject, float scroll, float speed)
    {
        // マウスの位置をスクリーン座標からワールド座標に変換
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, fieldObject.transform.position);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            Vector3 mouseWorldPosition = ray.GetPoint(distance);

            // カメラとフィールドオブジェクト間の方向ベクトルを取得
            Vector3 direction = camera.transform.position - mouseWorldPosition;

            // 現在の距離を計算
            float currentDistance = direction.magnitude;

            // ズーム後の新しい距離を計算
            float newDistance = Mathf.Clamp(currentDistance - scroll * speed, minZoom, maxZoom);

            // カメラの位置を新しい距離に基づいて調整
            Vector3 targetPosition = mouseWorldPosition + direction.normalized * newDistance;

            // DOTweenを使用してカメラの位置をスムーズに移動
            camera.transform.DOMove(targetPosition, 0.5f);
        }
    }
}
