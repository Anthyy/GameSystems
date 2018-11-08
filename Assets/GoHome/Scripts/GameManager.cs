using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Xml.Serialization;
using System.IO; // To use FileStream

namespace GoHome
{
    [Serializable]
    public class GameData
    {
        public int score;
        public int level;
    }
    public class GameManager : MonoBehaviour
    {
        #region Singleton
        public static GameManager Instance;
        private void Awake()
        {
            Instance = this;
            // dataPath = "C:\Users\Anthy\Documents\Projects\Assets\GoHome\Data\GameSame.xml"       
            fullPath = Application.dataPath + "/GoHome/Data/" + fileName + ".xml";

            // Check if file exists
            if (File.Exists(fullPath))
            {
                // Load the file and contents
                Load();
            }
        }

        private void OnDestroy()
        {
            Instance = null;
            // Save the data on destroy
            Save();
        }
        #endregion

        public int currentLevel = 0;
        public int currentScore = 0;
        public bool isGameRunning = true;
        public Transform levelContainer;
        [Header("UI")]
        public Text scoreText;
        [Header("Game Saves")]
        public string fileName = "GameData";

        private Level[] levels;
        private string fullPath;
        private GameData data = new GameData();

        private void Save()
        {
            // Set data's score to current
            data.score = currentScore;
            data.level = currentLevel;

            // Create a serializer of type GameData
            var serializer = new XmlSerializer(typeof(GameData)); // "var" is a placeholder for whatever you put on the other side
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                serializer.Serialize(stream, data);
            }
        }

        private void Load()
        {
            var serializer = new XmlSerializer(typeof(GameData));
            using (var stream = new FileStream(fullPath, FileMode.Open))
            {
                data = serializer.Deserialize(stream) as GameData; // Converting from xml to variable
            }

            // Update the values from data
            currentScore = data.score;
            currentLevel = data.level;

            AddScore(0);
        }
        // Disable all levels except the levelIndex
        private void SetLevel(int levelIndex)
        {
            // Loop through all levels
            for(int i = 0; i < levels.Length; i++)
            {
                // Get Level GameObject
                GameObject level = levels[i].gameObject;
                level.SetActive(false); // Disable level
                // Is current index (i) the same as levelIndex?
                if(i == levelIndex)
                {
                    // Enable that level instead
                    level.SetActive(true);
                }
            }
        }

        private void Start()
        {
            // Populate levels array with levels in game
            levels = levelContainer.GetComponentsInChildren<Level>(true);
            SetLevel(currentLevel);
        }
        public void GameOver()
        {
            // Stop game from running
            isGameRunning = false;
        }

        public void AddScore(int scoreToAdd)
        {
            currentScore += scoreToAdd;
            scoreText.text = "Score: " + currentScore; // Concatenating is adding two values to create a string

            // AddScore(scoreToAdd * modifier); // You can write this to simplify the 2 statements above
        }

        /*public void AddScore(int scoreToAdd, int modifier) // In case you want to add a modifier to your game
        {
            currentScore += scoreToAdd * modifier;
            scoreText.text = "Score: " + currentScore;

            //AddScore(scoreToAdd * modifier);
        }
        */

        // Call this function to move to the next level
        public void NextLevel()
        {
            // Increase currentLevel
            currentLevel++;
            // If currentLevel exceeds level length 
            if(currentLevel >= levels.Length)
            {
                // GameOver!
            }
            else
            {
                // Update current level
                SetLevel(currentLevel);
            }           
        }

        public void Restart()
        {
            // Get current scene
            Scene currentScene = SceneManager.GetActiveScene();
            // Reload current scene
            SceneManager.LoadScene(currentScene.name);
        }
    }
}

