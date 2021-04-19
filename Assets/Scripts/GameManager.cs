using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float totalTime = 30f;
    public float clickTime = 1f;

    public GameObject startScreen;
    public GameObject endScreen;
    public GameObject timeBoard;
    public GameObject scoreBoard;

    private bool end;
    private int score;
    private float gameTimer;
    private float virusTimer;
    private Transform activeVirus;
    private Transform[] viruses;

    // Start is called before the first frame update
    void Start()
    {
        Screen.showCursor = true;
        Screen.lockCursor = false;

        viruses = GameObject.Find("Coronaviruses").GetComponentsInChildren<Transform>();
        activeVirus = null;

        end = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (end)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startScreen.SetActive(false);
                RestartGame();
            }
        }
        else
        {
            gameTimer += Time.deltaTime;
            virusTimer += Time.deltaTime;

            timeBoard.transform.GetChild(1).GetComponent<Text>().text = string.Format("Time Left: {0:F0} s", totalTime-gameTimer);
            scoreBoard.transform.GetChild(1).GetComponent<Text>().text = string.Format("Score: {0:D}", score);

            if (gameTimer >= totalTime)
            {
                EndGame();
            }
            else if (virusTimer >= clickTime)
            {
                ActivateRandom();
            }
            else if (!activeVirus.GetComponent<Virus>().active)
            {
                score++;
                ActivateRandom();
            }
        }
    }

    // Activate a virus by random selection
    void ActivateRandom()
    {
        activeVirus.GetComponent<Virus>().Deactivate();
        Transform thisVirus = viruses[(int)Random.Range(1, viruses.Length)];
        while (activeVirus == thisVirus)
        {
            thisVirus = viruses[(int)Random.Range(1, viruses.Length)];
        }
        activeVirus = thisVirus;
        activeVirus.GetComponent<Virus>().Activate();
        virusTimer = 0f;
    }

    void RestartGame()
    {
        endScreen.SetActive(false);
        timeBoard.SetActive(true);
        scoreBoard.SetActive(true);

        end = false;

        score = 0;
        gameTimer = 0f;
        virusTimer = 0f;
        Time.timeScale = 1f;

        activeVirus = viruses[(int)Random.Range(1, viruses.Length)];
        activeVirus.GetComponent<Virus>().Activate();
    }

    void EndGame()
    {
        activeVirus.GetComponent<Virus>().Deactivate();

        end = true;

        Time.timeScale = 0f;
        endScreen.SetActive(true);
        timeBoard.SetActive(false);
        scoreBoard.SetActive(false);

        endScreen.transform.GetChild(1).GetComponent<Text>().text = string.Format("Score: {0:D}", score);
    }
}
