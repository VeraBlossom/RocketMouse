using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorSpript : MonoBehaviour

    //phía sau: nằm bên trái
    //phia trước: nằm bên phải



{
    //các phòng cho sẵn
    [SerializeField] private GameObject[] availableRooms;
    //các phòng đang có trên màn hình hiện tại
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
        //1. Chọn chỉ số một phòng ngẫu nhiên trong danh sách các phòng cho sẵn
        int randomRoomIndex = Random.Range(0, availableRooms.Length);

        //2. Khởi tạo một phòng mới ứng với chỉ số đã tìm được trong danh sách các phòng
        GameObject room = (GameObject)Instantiate(availableRooms[randomRoomIndex]);

        //3. Tìm chiều rộng của phòng với giá trị là scale x của floor
        float roomWidth = room.transform.Find("floor").localScale.x;

        //4.Tìm trung tâm của phòng mới bằng cách lấy điểm xa nhất của phòng trước cộng với một nửa chiều rộng của phòng mới
        float roomCenter = farthestRoomEndX + roomWidth * 0.5f;

        //5. Vị trí của phòng mới là vị trí trung tâm đã tìm được
        room.transform.position = new Vector3(roomCenter,0,0);

        //6. Thêm phòng mới vào danh sách các phòng hiện tại trên màn hình
        currentRooms.Add(room);
    }
    private void GenerateRoomIfRequired()
    {
        //1. Tạo danh sách các phòng cần được loại bỏ
        List<GameObject> roomsToRemove = new List<GameObject>();

        //2. Boolean kiểm tra có cần thêm phòng mới không
        bool addRooms = true;

        //3. Theo dõi vị trí hiện tại của nhân vật
        float playerX = transform.position.x;

        //4. Vị trí đánh dấu để xóa 1 phòng. Nếu vị trí của phòng nằm ở phía sau điểm này thì sẽ xóa phòng
        float removeRoomX = playerX - screenWidthInPoints;

        //5. Vị trí đánh dấu để thêm 1 phòng. Nếu không có phòng sau khi vượt qua điểm này thì phải thêm phòng
        float addRoomX = playerX + screenWidthInPoints;

        //6. Vị trí đánh dấu điểm kết thúc của 1 level, không nhất thiết phải là điểm kết thúc của phòng
        float farthestRoomEndX = 0;

        //liệt kê tất cả các phòng đang có trên màn hình
        foreach (var room in currentRooms) {

            //7.
            //Tìm chiều rộng của phòng với giá trị là scale x của floor
            float roomWidth = room.transform.Find("floor").localScale.x;
            //Tìm vị trí bắt đầu xa nhất của phòng
            float roomStartX = room.transform.position.x - (roomWidth * 0.5f);
            //Tìm vị trí kết thúc xa nhất của phòng
            float roomEndX = roomStartX + roomWidth;

            //8. Nếu vị trí bắt đầu xa nhất của phòng nằm ở phía trước của điểm đánh dấu để thêm 1 phòng, thì không cần thêm phòng
            if (roomStartX > addRoomX)
            {
                addRooms = false;
            }

            //9. Nếu điểm cuối cùng của phòng nằm ở phía sau vị trí đánh dấu để xóa 1 phòng
            if (roomEndX < removeRoomX)
            {
                //phòng đó đã đi ra màn hình rồi và cần đc xóa
                roomsToRemove.Add(room);//thêm phòng đó vào danh sách các phòng cần xóa
            }

            //10. Tìm vị trí để kết thúc 1 level
            farthestRoomEndX = Mathf.Max(farthestRoomEndX, roomEndX);
        }

        //11. Xóa phòng ra khỏi danh sách và loại bỏ ra khỏi game
        foreach (var room in roomsToRemove)
        {
            currentRooms.Remove(room);
            Destroy(room);
        }

        //12. nếu cần phải thêm phòng thì hãy thêm phòng vào vị trí kết thúc 1 level
        if (addRooms)
        {
            AddRoom(farthestRoomEndX);//farthestRoomEndX chính là (điểm xa nhất của phòng trước) ở hàm AddRoom
        }
        
    }

    //coroutine
    private IEnumerator GeneratorCheck()
    {

    }
}
