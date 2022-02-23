using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCtrl : MonoBehaviour
{
    public GameObject[] bodyObjects;
    public Color32[] colors;

    Material[] carMats;

    // 자동차 회전값
    public float rotSpeed = 0.1f;
    float axisRotAdj = -0.1f;

    void Start()
    {
        // carMAts의 배열을 자동차 오브젝트 수만큼 초기화
        carMats = new Material[bodyObjects.Length];
        // 자동차 보디 오브젝트 매터리얼을 각각 carMats에 지정
        for(int i = 0; i < carMats.Length; i++)
        {
            carMats[i] = bodyObjects[i].GetComponent<MeshRenderer>().material;
        }
        // 색상 배열 0번에는 매터리얼의 초기 색상을 저장
        colors[0] = carMats[0].color;
    }

    void Update()
    {
        // 터치 부위가 1개 이상일 시
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            // 터치가 움직이는 상태일 시
            if(touch.phase == TouchPhase.Moved)
            {
                // 카메라 위치에서 정면 방향으로 레이 발사 후 레이에 부딪힌 물체가 8번 레이어에 해당 시
                Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                RaycastHit hitInfo;

                if(Physics.Raycast(ray, out hitInfo, Mathf.Infinity, 1 << 8))
                {
                    // 터치 이동값 저장
                    Vector3 delPos = touch.deltaPosition;

                    // 한 프레임 간의 터치 이동량에 따라 로컬 Y축으로 자동차 회전
                    this.transform.Rotate(transform.up, delPos.x * axisRotAdj * rotSpeed);
                }
            }
        }
    }

    public void ChangeColor(int num)
    {
        // 각 LOD 매터리얼의 색상을 버튼에 지정된 색상으로 변경
        for(int i = 0; i < carMats.Length; i++)
        {
            carMats[i].color = colors[num];
        }
    }
}
