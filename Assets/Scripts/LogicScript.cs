using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int PlayerScore;
    public int Countdown;
    private float highscore;
    public TMP_Text CountdownText;
    public TMP_Text CurrentScoreValue;
    public TMP_Text FinalScoreText;
    public TMP_Text HighScoreValue;

    public GameObject GameOverScreen;
    public AudioSource ScoreAudio;
    public AudioSource DeathAudio;

    public GameObject BirdObject;
    public PipeSpawner PipeSpawner;
    private bool isGameOver = false;

    private void Awake()
    {
        StartCoroutine(CountdownStart(Countdown));
        highscore = PlayerPrefs.GetFloat("Flappy");
        HighScoreValue.text = highscore.ToString();
    }


    [ContextMenu("Increase Score")]
    public void AddScore(int scoreToAdd)
    {
        if (GameOverScreen.activeSelf == false)
        {
            PlayerScore = PlayerScore + scoreToAdd;
            CurrentScoreValue.text = PlayerScore.ToString();
            ScoreAudio.Play();
        }
    }

    [ContextMenu("Restart Game")]
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    [ContextMenu("Quit Game")]
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void GameOver()
    {
        string finalScoreString;
        if (PlayerScore > highscore)
        {
            finalScoreString = "New High Score! : ";
            PlayerPrefs.SetFloat("Flappy", PlayerScore);
        }
        else
        {
            finalScoreString = "Score : ";
        }
        GameOverScreen.SetActive(true);
        FinalScoreText.text = "Score : ";

        FinalScoreText.text = finalScoreString + PlayerScore.ToString();
        CurrentScoreValue.text = "";

        isGameOver = true;
        DeathAudio.Play();
    }

    IEnumerator CountdownStart(int seconds)
    {
        int counter = seconds;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
            CountdownText.text = counter.ToString();
            if (counter == 0)
            {
                CountdownText.text = "";

            }
        }
        StartGame();
    }

    void StartGame()
    {
        BirdObject.GetComponent<BirdScript>().enabled = true;
        BirdObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        PipeSpawner.enabled = true;
    }
}