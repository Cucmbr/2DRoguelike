using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomsPlacer : MonoBehaviour
{
    public Room[] RoomPrefabs;
    public Room StartingRoom;

    public int chestRooms;
    public int shops;
    public int ladders;
    bool fixC = false;
    bool fixS = false;
    bool fixL = false;

    private Room[,] spawnedRooms;

    private IEnumerator Start()
    {
        spawnedRooms = new Room[5, 5];
        spawnedRooms[2, 2] = StartingRoom;

        for (int i = 0; i < 8; i++)
        {
            yield return new WaitForSecondsRealtime(0.1f);
            PlaceOneRoom();
        }
        if (chestRooms != 0)
        {
            fixC = true;
            PlaceOneRoom();
        }
        if (shops != 0)
        {
            fixS = true;
            PlaceOneRoom();
        }
        if (ladders != 0)
        {
            fixL = true;
            PlaceOneRoom();
        }
    }

    private void PlaceOneRoom()
    {
        HashSet<Vector2Int> vacantPlaces = new HashSet<Vector2Int>();
        for (int x = 0; x < spawnedRooms.GetLength(0); x++)
        {
            for (int y = 0; y < spawnedRooms.GetLength(1); y++)
            {
                if (spawnedRooms[x, y] == null) continue;

                int maxX = spawnedRooms.GetLength(0) - 1;
                int maxY = spawnedRooms.GetLength(1) - 1;

                if (x > 0 && spawnedRooms[x - 1, y] == null) vacantPlaces.Add(new Vector2Int(x - 1, y));
                if (y > 0 && spawnedRooms[x, y - 1] == null) vacantPlaces.Add(new Vector2Int(x, y - 1));
                if (x < maxX && spawnedRooms[x + 1, y] == null) vacantPlaces.Add(new Vector2Int(x + 1, y));
                if (y < maxY && spawnedRooms[x, y + 1] == null) vacantPlaces.Add(new Vector2Int(x, y + 1));
            }
        }

        int rd = Random.Range(0, 4);
        int rb;
        if ((rd == 0 && chestRooms != 0) || fixC == true)
        {
            rb = RoomPrefabs.Length - 3;
            chestRooms -= 1;
            fixC = false;
        }
        else if ((rd == 1 && shops != 0) || fixS == true)
        {
            rb = RoomPrefabs.Length - 2;
            shops -= 1;
            fixS = false;
        }
        else if ((rd == 2 && ladders != 0) || fixL == true)
        {
            rb = RoomPrefabs.Length - 1;
            ladders -= 1;
            fixL = false;
        }
        else
        {
            rb = Random.Range(0, RoomPrefabs.Length - 3);
        }

        Room newRoom = Instantiate(RoomPrefabs[rb]);

        int limit = 500;
        while (limit-- > 0)
        {           
            Vector2Int position = vacantPlaces.ElementAt(Random.Range(0, vacantPlaces.Count));
            if (ConnectToSomething(newRoom, position))
            {
                newRoom.transform.position = new Vector3(position.x - 2, position.y - 2) * 20;
                spawnedRooms[position.x, position.y] = newRoom;
                return;
            }
        }

        Destroy(newRoom.gameObject);
    }

    private bool ConnectToSomething(Room room, Vector2Int p)
    {
        int maxX = spawnedRooms.GetLength(0) - 1;
        int maxY = spawnedRooms.GetLength(1) - 1;

        List<Vector2Int> neighbours = new List<Vector2Int>();

        if (room.DoorU != null && p.y < maxY && spawnedRooms[p.x, p.y + 1]?.DoorD != null) neighbours.Add(Vector2Int.up);
        if (room.DoorD != null && p.y > 0 && spawnedRooms[p.x, p.y - 1]?.DoorU != null) neighbours.Add(Vector2Int.down);
        if (room.DoorR != null && p.x < maxX && spawnedRooms[p.x + 1, p.y]?.DoorL != null) neighbours.Add(Vector2Int.right);
        if (room.DoorL != null && p.x > 0 && spawnedRooms[p.x - 1, p.y]?.DoorR != null) neighbours.Add(Vector2Int.left);

        if (neighbours.Count == 0) return false;

        Vector2Int selectedDirection = neighbours[Random.Range(0, neighbours.Count)];
        Room selectedRoom = spawnedRooms[p.x + selectedDirection.x, p.y + selectedDirection.y];

        if (selectedDirection == Vector2Int.up)
        {
            room.DoorU.SetActive(false);
            selectedRoom.DoorD.SetActive(false);
        }
        else if (selectedDirection == Vector2Int.down)
        {
            room.DoorD.SetActive(false);
            selectedRoom.DoorU.SetActive(false);
        }
        else if (selectedDirection == Vector2Int.right)
        {
            room.DoorR.SetActive(false);
            selectedRoom.DoorL.SetActive(false);
        }
        else if (selectedDirection == Vector2Int.left)
        {
            room.DoorL.SetActive(false);
            selectedRoom.DoorR.SetActive(false);
        }

        return true;
    }
}