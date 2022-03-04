using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallCtrl : MonoBehaviour
{
    public float resetTime = 3.0f;
    // 포획 확률 30%
    public float captureRate = 0.3f;
    public Text result;
    public GameObject effect;

    Rigidbody rb;
    bool isReady = true;
    Vector2 startPos;

    void Start()
    {
        // 포획 결과 텍스트를 공백 상태로 초기화
        result.text = "";

        // 리지드바디 물리 능력 비활성화
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    void Update()
    {
        // 공이 날아가는 중일 때에는 카메라 전방 배치 해제
        if (!isReady)
        {
            return;
        }

        // 공을 카메라 전방 하단에 배치
        SetBallPosition(Camera.main.transform);

        // 드래그 거리에 비례, 공 발사
        if(Input.touchCount > 0 && isReady)
        {
            Touch touch = Input.GetTouch(0);

            // 터치 시작 시
            if(touch.phase == TouchPhase.Began)
            {
                // 터치한 픽셀 저장
                startPos = touch.position;
            }
            // 터치 종료 시
            else if(touch.phase == TouchPhase.Ended)
            {
                // 손가락이 드래그한 픽셀의 Y축 거리를 구한다
                float dragDistance = touch.position.y - startPos.y;
                // ar 카메라 기준 던질 방향(전방 45도 위쪽) 설정
                Vector3 throwAngle = (Camera.main.transform.forward + Camera.main.transform.up).normalized;

                // 물리 능력 활성화, 준비 상태 비활성화
                rb.isKinematic = false;
                isReady = false;

                // rigidbody를 이용 던질 방향 * 드래그 거리만큼 물리적 힘을 가한다
                rb.AddForce(throwAngle * dragDistance * 0.005f, ForceMode.VelocityChange);

                // 3초 후 공의 위치 및 속도 초기화
                Invoke("ResetBall", resetTime);
            }
        }
    }

    void SetBallPosition(Transform anchor)
    {
        // 카메라의 위치에서 일정 거리만큼 떨어진 특정 위치 설정
        Vector3 offset = anchor.forward * 0.5f + anchor.up * -0.2f;
        // 공의 위치를 카메라 위치에서 즉정 위치만큼 이동된 거리로 설정
        transform.position = anchor.position + offset;
    }

    void ResetBall()
    {
        // 물리 능력 비활성, 속도 초기화
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        // 준비 상태로 변경
        isReady = true;

        gameObject.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isReady)
        {
            return;
        }

        // 포획 확률을 추첨
        float draw = Random.Range(0, 1.0f);

        if(draw <= captureRate)
        {
            result.text = "포획 성공!";
        }
        else
        {
            result.text = "포획에 실패해 도망쳤습니다...";
        }
        // 이펙트 생성
        Instantiate(effect, collision.transform.position, Camera.main.transform.rotation);

        // 고양이 캐릭터를 제거하고 공을 비활성
        Destroy(collision.gameObject);
        gameObject.SetActive(false);
    }
}
