using UnityEngine;

public class SlotItemMove : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // 마우스 오른쪽 버튼을 눌렀을 때
        {
            StartDragging();
        }

        if (Input.GetMouseButtonUp(1)) // 마우스 오른쪽 버튼을 뗐을 때
        {
            StopDragging();
        }

        if (isDragging)
        {
            // 마우스의 현재 위치를 월드 좌표로 변환
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;

            // 오브젝트를 새로운 위치로 이동
            transform.position = curPosition;
        }
    }

    void StartDragging()
    {
        isDragging = true;
        // 현재 마우스 위치와 오브젝트의 위치 사이의 차이를 계산하여 이동할 때 사용
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void StopDragging()
    {
        isDragging = false;
    }
}

