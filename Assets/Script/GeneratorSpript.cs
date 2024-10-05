using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorSpript : MonoBehaviour
{
    //cac phong cho san
    [SerializeField] private GameObject[] availableRooms;
    //cac phong dang co tren man hinh hien tai
    [SerializeField] private List<GameObject> currentRooms;
    private float screenWidthInPoints;

    // Start is called before the first frame update
    void Start()
    {
        float height = 2.0f * Camera.main.orthographicSize;
        screenWidthInPoints = height * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void AddRoom(float farthestRoomEndX)
    {
        //1. Chon chi so 1 phong bat ky trong danh sach cac phong co san
        int randomRoomIndex = Random.Range(0, availableRooms.Length);

        //2. Khoi tao 1 phong moi ung voi chi so da tim duoc trong danh sach cac phong
        GameObject room = (GameObject)Instantiate(availableRooms[randomRoomIndex]);

        //3. Tim chieu rong cua phong moi bang cach tim gia tri scale x cua floor
        float roomWidth = room.transform.Find("floor").localScale.x;

        //4.Tim trung tam cua phong moi bang cach lay diem xa nhat cua phong truoc cong voi mot nua chieu rong cua phong
        float roomCenter = farthestRoomEndX + roomWidth * 0.5f;

        //5. Vi tri cua phong moi la vi tri trung tam da tim duoc
        room.transform.position = new Vector3(roomCenter,0,0);

        //6. Them phong moi vao trong cac phong hien tai tren man hinh
        currentRooms.Add(room);
    }
    private void GenerateRoomIfRequired()
    {
        //1. Tao danh sach cac phong can duoc loai bo
        List<GameObject> roomsToRemove = new List<GameObject>();

        //2. boolean kiem tra co can them phong moi khong
        bool addRooms = true;

        //3. Theo doi vi tri hien tai cua nhan vat theo chieu x
        float playerX = transform.position.x;

        //4. Vi tri ma sau cho do thi mot phong se
        float removeRoomX = playerX - screenWidthInPoints;

        //5
        float addRoomX = playerX + screenWidthInPoints;

        //6
        float farthestRoomEndX = 0;
        foreach (var room in currentRooms) {

            //7
            float roomWidth = room.transform.Find("floor").localScale.x;
            float roomStartX = room.transform.position.x - (roomWidth * 0.5f);
            float roomEndX = roomStartX + roomWidth;

            //8
            if (roomStartX > addRoomX)
            {
                addRooms = false;
            }

            //9
            if (roomEndX < removeRoomX)
            {
                roomsToRemove.Add(room);
            }

            //10
            farthestRoomEndX = Mathf.Max(farthestRoomEndX, roomEndX);
        }

        //11
        foreach (var room in roomsToRemove)
        {
            currentRooms.Remove(room);
            Destroy(room);
        }

        //12
        if (addRooms)
        {
            AddRoom(farthestRoomEndX);
        }
        
    }
}
