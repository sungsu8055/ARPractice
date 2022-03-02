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
        // ��ġ ǥ�ÿ� ť�� 3���� ����
        for(int i = 0; i < 3; i++)
        {
            GameObject go = Instantiate(smallCube);
            faceCubes.Add(go);
            go.SetActive(false);
        }

        // ARFaceManager �� �� �ν� ��������Ʈ(facesChanged) ���� �� ������ �Լ� ����
        // afm.facesChanged += OnDetectThreePoints;
        afm.facesChanged += OnDetectFaceAll;

        // ARFoundation�� XRFaceSubsystem Ŭ���� ������ ARCore�� ARCoreFaceSubsystem Ŭ���� ������ ĳ����
        subSys = (ARCoreFaceSubsystem)afm.subsystem;
    }

    void Update()
    {
        
    }

    // facesChanged ��������Ʈ�� ������ �Լ�
    void OnDetectThreePoints(ARFacesChangedEventArgs args)
    {
        // �ν� ������ ��� updated, removed ����Ʈ�� ũ�Ⱑ �� ���� ���� Ȱ�� �� ����Ʈ�� ũ�Ⱑ �� �� �̻��� ��� ���¸� ������ �� ����
        // �� �ν� ������ ���ŵ� ���� �ִٸ�
        if (args.updated.Count > 0)
        {
            // �νĵ� �󱼿��� Ư�� ��ġ�� ������
            subSys.GetRegionPoses(args.updated[0].trackableId, Allocator.Persistent, ref regionData);
            // Ư�� ��ġ�� ������Ʈ ��ġ(0: �� ��, 1: �̸� ����, 2: �̸� ����)
            for(int i = 0; i < regionData.Length; i++)
            {
                faceCubes[i].transform.position = regionData[i].pose.position;
                faceCubes[i].transform.rotation = regionData[i].pose.rotation;
                faceCubes[i].SetActive(true);
            }
        }
        // �� �ν� ������ ���ų� �Ұ� �Ǹ�
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
            // �ؽ�Ʈ UI�� ���ڿ� �����͸� ������ �����ͷ� ��ȯ
            int num = int.Parse(vertexIndex.text);

            // �� ���ؽ� �迭���� ������ �ε����� �ش��ϴ� ��ǥ�� �����´�.
            // Vector3 vertPosition = args.updated[0].vertices[100];
            Vector3 vertPosition = args.updated[0].vertices[num];

            // ���ؽ� ��ǥ�� ���� ��ǥ�� ��ȯ
            vertPosition = args.updated[0].transform.TransformPoint(vertPosition);

            // �غ�� ť�� �ϳ��� Ȱ��ȭ �� ���ؽ� ��ġ�� ������ ����
            faceCubes[0].SetActive(true);
            faceCubes[0].transform.position = vertPosition;
        }
        else if(args.removed.Count > 0)
        {
            faceCubes[0].SetActive(false);
        }
    }
}
