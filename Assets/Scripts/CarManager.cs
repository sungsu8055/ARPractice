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
        // �ٴ� �� �νĽ� ��Ƽ������ Ȱ���� ���� ��Ȱ�� �ʱ�ȭ
        indicator.SetActive(false);
        // arManager�� ARRaycastManager ������Ʈ ����
        arManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {

    }

    // �ٴ� ���� �� ǥ�� ��¿� �Լ�
    void DetectGround()
    {
        // ��ũ���� �߾� ������ ã�´�. ��ũ�� ��ü ����� ������ ���� ��ġ�� ����
        Vector2 screenSize = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        // ���̿� �ε��� ����� ������ ������ ����Ʈ ���� ����
        List<ARRaycastHit> hitInfos = new List<ARRaycastHit>();

        // ��ũ�� �߾� �������� ���� �߻� �� Plane Ÿ�� ���� ����� ������
        if(arManager.Raycast(screenSize, hitInfos, TrackableType.All))
        {
            // ǥ�� ������Ʈ Ȱ��
            indicator.SetActive(true);
            // ǥ�� ������Ʈ�� ��ġ �� ȸ������ ���� �浹 ������ ��ġ
            indicator.transform.position = hitInfos[0].pose.position;
            indicator.transform.rotation = hitInfos[0].pose.rotation;
        }
        else
        {
            // ��� ������ ��Ȱ��
            indicator.SetActive(false);
        }
    }
}
