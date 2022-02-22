using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCtrl : MonoBehaviour
{
    public GameObject[] bodyObjects;
    public Color32[] colors;

    Material[] carMats;

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
        
    }

    public void ChangeColor(int num)
    {
        // 각 LOD 매터리얼의 색상을 버튼에 지정된 색상으로 변경
        for(int i = 0; i < carMats.Length; i++)
        {

        }
    }
}
