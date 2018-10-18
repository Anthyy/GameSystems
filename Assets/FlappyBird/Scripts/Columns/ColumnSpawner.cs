using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird
{
    public class ColumnSpawner : MonoBehaviour
    {
        public GameObject prefab;
        public int maxPoolSize = 5;
        public float spawnRate = 3f;
        public float minY = -1f;
        public float maxY = 3.5f;
        public Vector3 standbyPos = new Vector3(-15, -25);
        public float startX = 10f;

        private Transform[] columns;
        private int currentColumn = 0;
        private float spawnTimer = 0f;

        // Use this for initialization
        void Start()
        {
            SpawnColumns();
        }

        // Update is called once per frame
        void Update()
        {
            // Increase timer
            spawnTimer += Time.deltaTime;
            // Is the game NOT(!) over AND(&&) has spawnTimer reached the spawnRate?
            if (!GameManager.Instance.isGameOver && spawnTimer >= spawnRate)
            {
                // Reset timer
                spawnTimer = 0f;
                // Reposition a column (i.e. Reuse a column)
                RepositionColumn();
            }
        }

        void SpawnColumns()
        {
            // Create pool to store columns 
            columns = new Transform[maxPoolSize];
            // Loop through the pool
            for (int i = 0; i < columns.Length; i++)
            {
                // Fill each element with a new column
                GameObject clone = Instantiate(prefab, transform); // Cloning prefab
                Transform column = clone.transform; // Getting transform
                column.position = standbyPos; // Setting position
                columns[i] = column; // Adding to pool
            }
        }

        void RepositionColumn()
        {
            // Get random y pos
            float randomY = Random.Range(minY, maxY);
            // Get current column
            Transform column = columns[currentColumn];
            // Reposition that column
            column.position = new Vector3(startX, randomY);
            // Increment currentColumn
            currentColumn++;
            // If currentColumn exceeds pool size
            if (currentColumn >= maxPoolSize)
            {
                // Set currentColumn back to zero
                currentColumn = 0;
            }             
        }
    }
}

