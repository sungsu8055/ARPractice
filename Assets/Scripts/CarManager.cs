using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CarManager : MonoBehaviour
{
    public GameObject indicator;

    ARRaycastManager arManager;


    void Start()
    {
        // 바닥 면 인식시 인티케이터 활성을 위해 비활성 초기화
        indicator.SetActive(false);
        // arManager에 ARRaycastManager 컴포넌트 연결
        arManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {

    }

    // 바닥 감지 및 표식 출력용 함수
    void DetectGround()
    {
        // 스크린의 중앙 지점을 찾는다. 스크린 전체 사이즈를 반으로 만든 수치를 도출
        Vector2 screenSize = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        // 레이에 부딪힌 대상의 정보를 저장할 리스트 변수 생성
        List<ARRaycastHit> hitInfos = new List<ARRaycastHit>();

        // 스크린 중앙 지점에서 레이 발사 시 Plane 타입 추적 대상이 있으면
        if(arManager.Raycast(screenSize, hitInfos, TrackableType.All))
        {
            // 표식 오브젝트 활성
            indicator.SetActive(true);
            // 표식 오브젝트의 위치 및 회전값을 레이 충돌 지점에 일치
            indicator.transform.position = hitInfos[0].pose.position;
            indicator.transform.rotation = hitInfos[0].pose.rotation;
        }
        else
        {
            // 대상 없으면 비활성
            indicator.SetActive(false);
        }
    }
}
