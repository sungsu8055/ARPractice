using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARCore;
using Unity.Collections;
using UnityEngine.UI;

public class FindDetection : MonoBehaviour
{
    NativeArray<ARCoreFaceRegionData> regionData;

    public ARFaceManager afm;
    public GameObject smallCube;
    public Text vertexIndex;

    List<GameObject> faceCubes = new List<GameObject>();
    ARCoreFaceSubsystem subSys;


    void Start()
    {
        // 위치 표시용 큐브 3개를 생성
        for(int i = 0; i < 3; i++)
        {
            GameObject go = Instantiate(smallCube);
            faceCubes.Add(go);
            go.SetActive(false);
        }

        // ARFaceManager 가 얼굴 인식 델리게이트(facesChanged) 실행 시 실행할 함수 연결
        // afm.facesChanged += OnDetectThreePoints;
        afm.facesChanged += OnDetectFaceAll;

        // ARFoundation의 XRFaceSubsystem 클래스 변수를 ARCore의 ARCoreFaceSubsystem 클래스 변수로 캐스팅
        subSys = (ARCoreFaceSubsystem)afm.subsystem;
    }

    void Update()
    {
        
    }

    // facesChanged 델리게이트에 연결할 함수
    void OnDetectThreePoints(ARFacesChangedEventArgs args)
    {
        // 인식 정보를 담는 updated, removed 리스트의 크기가 한 개인 것을 활용 각 리스트의 크기가 한 개 이상일 경우 상태를 도출할 수 있음
        // 얼굴 인식 정보가 갱신된 것이 있다면
        if (args.updated.Count > 0)
        {
            // 인식된 얼굴에서 특정 위치값 가져옴
            subSys.GetRegionPoses(args.updated[0].trackableId, Allocator.Persistent, ref regionData);
            // 특정 위치에 오브젝트 위치(0: 코 끝, 1: 이마 좌측, 2: 이마 우측)
            for(int i = 0; i < regionData.Length; i++)
            {
                faceCubes[i].transform.position = regionData[i].pose.position;
                faceCubes[i].transform.rotation = regionData[i].pose.rotation;
                faceCubes[i].SetActive(true);
            }
        }
        // 얼굴 인식 정보가 없거나 잃게 되면
        else if(args.removed.Count > 0)
        {
            for(int i =0; i < regionData.Length; i++)
            {
                faceCubes[i].SetActive(false);
            }
        }
    }

    void OnDetectFaceAll(ARFacesChangedEventArgs args)
    {
        if(args.updated.Count > 0)
        {
            // 텍스트 UI의 문자열 데이터를 정수형 데이터로 변환
            int num = int.Parse(vertexIndex.text);

            // 얼굴 버텍스 배열에서 지정한 인덱스에 해당하는 좌표를 가져온다.
            // Vector3 vertPosition = args.updated[0].vertices[100];
            Vector3 vertPosition = args.updated[0].vertices[num];

            // 버텍스 좌표를 월드 좌표로 변환
            vertPosition = args.updated[0].transform.TransformPoint(vertPosition);

            // 준비된 큐브 하나를 활성화 후 버텍스 위치에 가져다 놓음
            faceCubes[0].SetActive(true);
            faceCubes[0].transform.position = vertPosition;
        }
        else if(args.removed.Count > 0)
        {
            faceCubes[0].SetActive(false);
        }
    }
}
