using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FlappyBird
{
    public class UINumber : MonoBehaviour
    {
        public int number = 0;
        public Sprite[] numbers; // Stores all the flappy digits
        public GameObject scoreTextPrefab; // Score Prefab text element to create
        public Vector3 standbyPos = new Vector3(-15, 15); // Position offscreen for standby
        public int maxDigits = 5; // The amount of digits to store offscreen for reuse

        private GameObject[] scoreTextPool;
        private int[] digits;

        public int Value 
        {
            get
            {
                return number; 
            }
            set
            {
                number = value;
                RefreshText(value);
            }
        }

        // Use this for initialization
        void Start()
        {
            SpawnPool();
            // Subscribe to scoreAdded function in GameManager
            GameManager.Instance.scoreAdded += RefreshText;
            // Update score to start on zero
            RefreshText(number);
        }

        void RefreshText(int num)
        {
            // Convert score into array of digits
            int[] digits = GetDigits(num);
            // Loop through all digits
            for (int i = 0; i // Initialisation
                < digits.Length; // Condition
                i++) // Increment
            {
                // Get value of each digit
                int value = digits[i];
                // Get corresponding text element in pool
                GameObject textElement = scoreTextPool[i];
                // Get image component attached to it
                Image img = textElement.GetComponent<Image>();
                // Assign sprite to number using value
                img.sprite = numbers[value];
                // Activate text element
                textElement.SetActive(true);
            }    

            // Loop through all remaining elements in the pool
            for (int i = digits.Length; i < scoreTextPool.Length; i++)
            {
                // Deactivate that element
                scoreTextPool[i].SetActive(false);
            }
        }

        void SpawnPool()
        {
            // Allocate memory for the score text pool
            scoreTextPool = new GameObject[maxDigits];
            // Loop through all available digits
            for (int i = 0; i < maxDigits; i++)
            {
                // Create a new GameObject offscreen
                GameObject clone = Instantiate(scoreTextPrefab, standbyPos, Quaternion.identity);
                // Get the image component attached to the clone
                Image img = clone.GetComponent<Image>();
                // Set sprite to corresponding number sprite
                img.sprite = numbers[i];
                // Attach to self
                clone.transform.SetParent(transform);
                // Set name of text to index
                clone.name = i.ToString();
                // Add it to pool
                scoreTextPool[i] = clone;
            }
        }

        // Converts numbers into an array of single digits
        int[] GetDigits(int number)
        {
            List<int> digits = new List<int>();
            // While numbers is greater than 10
            while (number >= 10)
            {
                // Modulus by 10 and return remainder
                digits.Add(number % 10);
                // Divide total number by 10
                number /= 10;
            }
            // Add last number to digit
            digits.Add(number);
            // Flip the array around (it's backwards)
            digits.Reverse();
            // Return to array
            return digits.ToArray();
        }
    }
}

