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
    public float returnDuration = 0.5f; // 元の位置に戻す時間

    private Vector3 lastMousePosition; // 位置記録
    private Vector3 newAngle = new Vector3(0, 0, 0); // 位置更新
    private float currentDistanceMain; // メインカメラの現在のズーム距離
    private float currentDistanceSecondary; // セカンダリカメラの現在のズーム距離
    private float initialDistanceMain; // メインカメラの初期ズーム距離
    private float initialDistanceSecondary; // セカンダリカメラの初期ズーム距離

    void Start()
    {
        // 初期ズーム距離を記録
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

        // メインカメラのフィールドオブジェクトを中心に回転
        mainCamera.transform.RotateAround(mainFieldObject.transform.position, Vector3.up, angle.x);

        // セカンダリカメラもセカンダリフィールドオブジェクトを中心に同じ量だけ回転
        secondaryCamera.transform.RotateAround(secondaryFieldObject.transform.position, Vector3.up, angle.x);
    }

    public void ZoomCameras(float scroll, float speed)
    {
        // メインカメラとセカンダリカメラのズーム処理を同じズーム量で行う
        ZoomCamera(mainCamera, mainFieldObject, scroll, ref currentDistanceMain, speed, initialDistanceMain);
        ZoomCamera(secondaryCamera, secondaryFieldObject, scroll, ref currentDistanceSecondary, speed, initialDistanceSecondary);
    }

    private void ZoomCamera(Camera camera, GameObject fieldObject, float scroll, ref float currentDistance, float speed, float initialDistance)
    {
        Vector3 direction = camera.transform.position - fieldObject.transform.position;

        // 新しい距離を計算
        float newDistance = currentDistance - scroll * speed;

        // ズーム距離が制限を超えた場合、自動的に初期距離に戻す
        if (newDistance < minZoom || newDistance > maxZoom)
        {
            newDistance = initialDistance; // 初期距離にリセット
        }
        currentDistance = Mathf.Clamp(newDistance, minZoom, maxZoom); // 制限内にクランプ

        // 新しいカメラの位置を計算
        Vector3 targetPosition = fieldObject.transform.position + direction.normalized * currentDistance;

        // DOTweenを使用してカメラを移動
        camera.transform.DOMove(targetPosition, returnDuration);
    }
}
