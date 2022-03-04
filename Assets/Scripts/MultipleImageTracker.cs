using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class MultipleImageTracker : MonoBehaviour
{
    ARTrackedImageManager imageManager;

    void Start()
    {
        imageManager = GetComponent<ARTrackedImageManager>();

        // 이미지 인식 델리게이트에 실행 함수 연결
        imageManager.trackedImagesChanged += OntracedImage;
    }

    void Update()
    {
        
    }

    void OntracedImage(ARTrackedImagesChangedEventArgs args)
    {
        // 새로 인식한 이미지 모두 순회
        foreach(ARTrackedImage trackedImage in args.added)
        {
            // 이미지 라이브러리에서 인식한 이미지의 이름을 읽어온다.
            string imageName = trackedImage.referenceImage.name;

            // Resources 폴더에서 인식한 이미지와 동일한 이름의 프리팹 서치
            GameObject imagePrefab = Resources.Load<GameObject>(imageName);

            // 검색된 프리팹이 존재한다면
            if(imagePrefab != null)
            {
                // 이미지에 등록된 자식 오브젝트가 없다면
                if(trackedImage.transform.childCount < 1)
                {
                    // 이미지 위치에 프리팹 생성
                    GameObject go = Instantiate(imagePrefab, trackedImage.transform.position, trackedImage.transform.rotation);

                    // 자식 오브젝트 등록
                    go.transform.SetParent(trackedImage.transform);
                }                
            }
        }
        // 생성된 이미지 순회
        foreach (ARTrackedImage trackedImage in args.updated)
        {
            // 이미지에 등록된 자식 오브젝트가 있다면
            if (trackedImage.transform.childCount > 0)
            {
                // 자식 오브젝트의 위치를 이미지의 위치와 동기화
                trackedImage.transform.GetChild(0).position = trackedImage.transform.position;
                trackedImage.transform.GetChild(0).rotation = trackedImage.transform.rotation;
            }
        }
    }
}
