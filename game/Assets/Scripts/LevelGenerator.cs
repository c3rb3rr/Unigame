using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{
    public GameObject roomLayout;
    public int distance;
    public Color startColor, endColor;
    public Transform generatorPoint;
    public int xOffSet = 18;
    public int yOffSet = 10;
    public LayerMask whatIsRoom;
    private GameObject _endRoom;
    private List<GameObject> Rooms = new List<GameObject>();
    public RoomPrefabs rooms;
    private List<GameObject> _generatedOutlines = new List<GameObject>();

    public RoomFloor floorStart, floorEnd;
    public RoomFloor[] potentialFloors;
    
    public enum Direction
    {
        Left,
        Up,
        Right,
        Down
    };

    public Direction selectedDir;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(roomLayout, generatorPoint.position, generatorPoint.rotation).GetComponent<SpriteRenderer>().color = startColor;
        selectedDir = (Direction)Random.Range(0, 4);
        MoveGenerationPoint();

        for (int i = 0; i < distance; i++)
        {
            GameObject newRoom = Instantiate(roomLayout, generatorPoint.position, generatorPoint.rotation);
            Rooms.Add(newRoom);
            
            if (i + 1 == distance)
            {
                newRoom.GetComponent<SpriteRenderer>().color = endColor;
                Rooms.RemoveAt(Rooms.Count - 1);
                _endRoom = newRoom;
            }
            
            selectedDir = (Direction)Random.Range(0, 4);
            MoveGenerationPoint();

            while (Physics2D.OverlapCircle(generatorPoint.position, 1f, whatIsRoom))
            {
                MoveGenerationPoint();
            }
        }
        
        // create room outlines
        CreateRoomOutline(Vector3.zero);
        foreach (GameObject room in Rooms)
        {
            CreateRoomOutline(room.transform.position);
        }
        CreateRoomOutline(_endRoom.transform.position);

        // adding floors to the outlines
        foreach (GameObject outline in _generatedOutlines)
        {
            bool generateFloor = true;
            
            // if this room is start room
            if (outline.transform.position == Vector3.zero)
            {
                Instantiate(floorStart, outline.transform.position, transform.rotation).theRoom = outline.GetComponent<Room>();
                generateFloor = false;
            }
            if (outline.transform.position == _endRoom.transform.position)
            {
                Instantiate(floorEnd, outline.transform.position, transform.rotation).theRoom = outline.GetComponent<Room>();
                generateFloor = false;
            }

            if (generateFloor)
            {
                int floor = Random.Range(0, potentialFloors.Length);
                Instantiate(potentialFloors[floor], outline.transform.position, transform.rotation).theRoom =
                    outline.GetComponent<Room>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR        
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
#endif
    }

    public void MoveGenerationPoint()
    {
        switch (selectedDir)
        {
            case Direction.Up:
                generatorPoint.position += new Vector3(0f, yOffSet, 0f);
                break;
            
            case Direction.Down:
                generatorPoint.position += new Vector3(0f, -yOffSet, 0f);
                break;
            
            case Direction.Left:
                generatorPoint.position += new Vector3(-xOffSet, 0f, 0f);
                break;
            
            case Direction.Right:
                generatorPoint.position += new Vector3(xOffSet, 0f, 0f);
                break;
        }
    }

    public void CreateRoomOutline(Vector3 roomPosition)
    {
        bool roomAbove = Physics2D.OverlapCircle(roomPosition + new Vector3(0f, yOffSet, 0f), 1f, whatIsRoom);
        bool roomBelow = Physics2D.OverlapCircle(roomPosition + new Vector3(0f, -yOffSet, 0f), 1f, whatIsRoom);
        bool roomLeft = Physics2D.OverlapCircle(roomPosition + new Vector3(-xOffSet, 0f, 0f), 1f, whatIsRoom);
        bool roomRight = Physics2D.OverlapCircle(roomPosition + new Vector3(xOffSet, 0f, 0f), 1f, whatIsRoom);
        int directionCount = 0;
        if (roomAbove)
        {
            directionCount++;
        }
        if (roomBelow)
        {
            directionCount++;
        }
        if (roomLeft)
        {
            directionCount++;
        }
        if (roomRight)
        {
            directionCount++;
        }

        switch (directionCount)
        {
            case 0:
                Debug.LogError("No rooms - directionCount 0");
                break;
            case 1:
                if (roomAbove)
                {
                    _generatedOutlines.Add(Instantiate(rooms.roomU, roomPosition, transform.rotation));
                }
                if (roomBelow)
                {
                    _generatedOutlines.Add(Instantiate(rooms.roomD, roomPosition, transform.rotation));
                }
                if (roomLeft)
                {
                    _generatedOutlines.Add(Instantiate(rooms.roomL, roomPosition, transform.rotation));
                }
                if (roomRight)
                {
                    _generatedOutlines.Add(Instantiate(rooms.roomR, roomPosition, transform.rotation));
                }
                break;
            case 2:
                if (roomAbove && roomBelow)
                {
                    _generatedOutlines.Add(Instantiate(rooms.roomUD, roomPosition, transform.rotation));
                }
                if (roomAbove && roomLeft)
                {
                    _generatedOutlines.Add(Instantiate(rooms.roomUL, roomPosition, transform.rotation));
                }
                if (roomAbove && roomRight)
                {
                    _generatedOutlines.Add(Instantiate(rooms.roomUR, roomPosition, transform.rotation));
                }
                if (roomLeft && roomRight)
                {
                    _generatedOutlines.Add(Instantiate(rooms.roomLR, roomPosition, transform.rotation));
                }
                if (roomBelow && roomLeft)
                {
                    _generatedOutlines.Add(Instantiate(rooms.roomDL, roomPosition, transform.rotation));
                }
                if (roomBelow && roomRight)
                {
                    _generatedOutlines.Add(Instantiate(rooms.roomDR, roomPosition, transform.rotation));
                }
                break;
            case 3:
                if (roomAbove && roomLeft && roomRight)
                {
                    _generatedOutlines.Add(Instantiate(rooms.roomULR, roomPosition, transform.rotation));
                }
                if (roomAbove && roomBelow && roomRight)
                {
                    _generatedOutlines.Add(Instantiate(rooms.roomUDR, roomPosition, transform.rotation));
                }
                if (roomAbove && roomBelow && roomLeft)
                {
                    _generatedOutlines.Add(Instantiate(rooms.roomUDL, roomPosition, transform.rotation));
                }
                if (roomBelow && roomLeft && roomRight)
                {
                    _generatedOutlines.Add(Instantiate(rooms.roomDLR, roomPosition, transform.rotation));
                }
                break;
            case 4:
                if (roomAbove && roomLeft && roomRight && roomBelow)
                {
                    _generatedOutlines.Add(Instantiate(rooms.roomUDLR, roomPosition, transform.rotation));
                }
                break;
        }
        
    }
}

//unity can process this as a data object that can be stored 
[System.Serializable]
public class RoomPrefabs
{
    public GameObject roomU, roomD, roomL, roomR, roomUD, roomUL, roomUR, roomLR, roomDL, roomDR,
        roomUDR, roomDLR, roomUDL, roomULR, roomUDLR;
}