using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class UI_Manager : MonoBehaviour
{
    public ARFaceManager faceManager;
    public Material[] faceMats;

    // 얼굴 인식 마커
    public Image faceTrackingState;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // 버튼 클릭 시 실행 함수
    public void ToggleMaskImage()
    {
        // faceManager 컴포넌트에서 현재 생성된 Face 오브젝트를 모두 순회한다
        foreach(ARFace face in faceManager.trackables)
        {
            // face 오브젝트가 얼굴을 인식할 경우
            if(face.trackingState == TrackingState.Tracking)
            {
                // face 오브젝트의 활성화 상태를 반대로 변경
                face.gameObject.SetActive(!face.gameObject.activeSelf);
            }
        }
    }

    public void SwitchFaceMaterial(int num)
    {
        // faceManager 컴포넌트에서 생성된 face 오브젝트를 모두 순회
        foreach (ARFace face in faceManager.trackables)
        {
            // 만일 페이스 오브젝트가 얼굴을 인식한 상태일 시
            if(face.trackingState == TrackingState.Tracking)
            {
                // face 오브젝트의 mesh renderer에 접근
                MeshRenderer mr = face.gameObject.GetComponent<MeshRenderer>();

                // 각 버튼에 설정된 번호로 매터리얼 변경 (int 파라미터에 배열 번호를 대입)
                mr.material = faceMats[num];
            }
        }
    }

    /*/
    public void IsFaceTracking()
    {
        foreach (ARFace face in faceManager.trackables)
        {
            // face 오브젝트가 얼굴을 인식할 경우
            if (face.trackingState == TrackingState.Tracking)
            {
                faceTrackingState.gameObject.SetActive(true);
            }
            else
            {
                faceTrackingState.gameObject.SetActive(false);
            }
        }
    }
    //*/
}
