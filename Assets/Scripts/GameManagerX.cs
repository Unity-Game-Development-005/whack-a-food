﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManagerX : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI timeText;

    public TextMeshProUGUI gameOverText;

    public GameObject titleScreen;

    public Button restartButton; 

    public List<GameObject> targetPrefabs;

    private int score;

    private float time;

    private float spawnRate = 1.5f;

    public bool isGameActive;


    private float spaceBetweenSquares = 2.5f; 

    private float minValueX = -3.75f; //  x value of the center of the left-most square

    private float minValueY = -3.75f; //  y value of the center of the bottom-most square




    private void Update()
    {
        if (isGameActive)
        {
            UpdateTime();
        }
    }



    // Start the game, remove title screen, reset score, and adjust spawnRate based on difficulty button clicked
    //public void StartGame()
    public void StartGame(int difficulty)
    {
        //spawnRate /= 5;
        spawnRate /= difficulty;

        isGameActive = true;

        StartCoroutine(SpawnTarget());

        score = 0;

        time = 60f;

        UpdateScore(0);

        titleScreen.SetActive(false);
    }


    // While game is active spawn a random target
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);

            int index = Random.Range(0, targetPrefabs.Count);

            if (isGameActive)
            {
                Instantiate(targetPrefabs[index], RandomSpawnPosition(), targetPrefabs[index].transform.rotation);
            }
            
        }
    }


    // Generate a random spawn position based on a random index from 0 to 3
    Vector3 RandomSpawnPosition()
    {
        float spawnPosX = minValueX + (RandomSquareIndex() * spaceBetweenSquares);

        float spawnPosY = minValueY + (RandomSquareIndex() * spaceBetweenSquares);

        Vector3 spawnPosition = new Vector3(spawnPosX, spawnPosY, 0);

        return spawnPosition;

    }


    // Generates random square index from 0 to 3, which determines which square the target will appear in
    int RandomSquareIndex()
    {
        return Random.Range(0, 4);
    }


    // Update score with value from target clicked
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;

        //scoreText.text = "score";
        scoreText.text = "Score: " + score.ToString();
    }


    private void UpdateTime()
    {
        time -= Time.deltaTime;

        timeText.text = "Time: " + time.ToString("0");

        if (time <= 0)
        {
            time = 0f;

            GameOver();
        }
    }


    // Stop game, bring up game over text and restart button
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);

        //restartButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(true);

        isGameActive = false;
    }


    // Restart game by reloading the scene
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


} // end of class
