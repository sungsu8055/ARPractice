using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;

public class CarManager : MonoBehaviour
{
    public GameObject indicator;
    public GameObject myCar;

    ARRaycastManager arManager;
    GameObject placedObject;

    // 재배치 거리 간격
    public float relocationDistance = 1.0f;

    void Start()
    {
        // 바닥 면 인식 시 인티케이터 활성을 위해 비활성 초기화
        indicator.SetActive(false);
        // arManager에 ARRaycastManager 컴포넌트 연결
        arManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        DetectGround();
        // 터치한 오브젝트가 ui오브젝트이면 업데이트 종료(자동차 생성 안 함)
        if(EventSystem.current.currentSelectedGameObject)
        {
            return;
        }
        // 인디케이터 활성화 중 터치 시 자동차 모델링 생성
        if(indicator.activeInHierarchy && Input.touchCount > 0)
        {
            // 첫번째 터치 상태를 가져옴
            Touch touch = Input.GetTouch(0);
            // 터치가 시작된 상태이면 자동차 프리팹을 가져와 인디케이터 자리에 생성
            if(touch.phase == TouchPhase.Began)
            {
                // 생성된 오브젝트가 없을 시 자동차 생성 후 placedObject에 할당
                if(placedObject == null)
                {
                    placedObject = Instantiate(myCar, indicator.transform.position, indicator.transform.rotation);
                }
                // 오브젝트가 있다면 모델링 위치 조정
                else
                {
                    if(Vector3.Distance(placedObject.transform.position, indicator.transform.position) > relocationDistance)
                    {
                        placedObject.transform.SetPositionAndRotation(indicator.transform.position, indicator.transform.rotation);
                    }
                }
            }
        }
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

            indicator.transform.position += indicator.transform.up * 0.1f;
        }
        else
        {
            // 대상 없으면 비활성
            indicator.SetActive(false);
        }
    }
}
