using System.Collections.Generic;
using UnityEngine;

namespace Workshop.Student
{
    public class MapGenerator : MonoBehaviour
    {
        public int columns = 10;
        public int rows = 10;

        public GameObject[] floorTiles;
        public GameObject[] wallTiles;
        public GameObject[] foodTiles;

        public string[,] saveItemMap = new string[3, 3] {
            { " ", "Soda", " "},
            { " ", " ", " "},
            { " ", " ", "Food"},
        };

        // 1. declare Players variable
        public GameObject[] Players;

        // 7. declare Exit variable 
        public GameObject Exit;

        public void Start()
        {
            // 1. random player at the position <0, 0> map
            Instantiate(Players[UnityEngine.Random.Range(0, Players.Length)], new Vector2(0, 0), Quaternion.identity);

            // 2. create obstacles
            for (int y = 0; y < rows / 2; y++)
            {
                GameObject obstacle = Instantiate(
                    wallTiles[1],
                    new Vector3(columns / 2, y, -0.1f),
                    Quaternion.identity
                );
            }

            // 3. create floor
            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    int r = UnityEngine.Random.Range(0, floorTiles.Length);
                    GameObject floor = Instantiate(
                        floorTiles[r],
                        new Vector2(x, y),
                        Quaternion.identity
                        );
                    floor.name = $"{x}, {y}";
                }
            }

            // 4. create walls
            for (int x = -1; x < columns + 1; x++)
            {
                for (int y = -1; y < rows + 1; y++)
                {
                    if (x == -1 || x == columns || y == -1 || y == rows)
                    {
                        int r = UnityEngine.Random.Range(0, wallTiles.Length);
                        GameObject wall = Instantiate(
                            wallTiles[r],
                            new Vector2(x, y),
                            Quaternion.identity
                            );
                        wall.name = $"{x}, {y}";
                    }
                }
            }

            // 5. random foods
            int numberOfFood = UnityEngine.Random.Range(1, 3);
            for (int i = 0; i < numberOfFood; i++)
            {
                int randomX = UnityEngine.Random.Range(0, columns);
                int randomY = UnityEngine.Random.Range(0, rows);
                GameObject food = Instantiate(
                    foodTiles[0],
                    new Vector2(randomX, randomY),
                    Quaternion.identity
                    );
                food.name = $"Food {i}";
            }

            // 6. generate item along with the saveItemMap
            for (int y = 0; y < saveItemMap.GetLength(0); y++)
            {
                for (int x = 0; x < saveItemMap.GetLength(1); x++)
                {
                    string item = saveItemMap[x, y];
                    if (!string.IsNullOrEmpty(item))
                    {
                        foreach (GameObject foodTile in foodTiles)
                        {
                            if (foodTile.name == item)
                            {
                                GameObject food = Instantiate(
                                    foodTile,
                                    new Vector2(x, y),
                                    Quaternion.identity
                                    );
                                food.name = $"{item} {x}, {y}";
                            }
                        }
                    }
                }
            }

            // 7. place exit
            Instantiate(Exit, new Vector2(columns - 1, rows - 1), Quaternion.identity);
        }
    }

}