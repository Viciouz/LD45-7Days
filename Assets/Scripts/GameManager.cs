using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameState { Active, Gameover }

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }

    public int level = 0;
    public List<Transform> levelSunPositions;
    public List<Color> levelColors;

    public Transform sunTransform;
    public SpriteRenderer sunSprite;
    public SpriteRenderer innerSunSprite;

    public float levelTime = 30;
    public float timeLeft;

    public Image timeImage;

    public GameState state;

    public RectTransform scorePanel;
    public RectTransform timePanel;
    public RectTransform startButton;

    public TextMeshProUGUI dayText;
    public Color completeColor;

    public AudioClip levelCompleteSFX;
    public AudioClip gameOverSFX;

    public List<float> cameraYPosition;

    public GameObject rain;

    public bool isLoading = false;

    public GameObject rootBranch;


    public void Awake() {
        Time.timeScale = 0f;
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(this);
        }
    }

    public void StartGame() {
        Camera.main.DOColor(levelColors[level], 0.3f);
        Time.timeScale = 1f;
        StartCoroutine(Timer());
        scorePanel.DOScale(1, 0.3f);
        timePanel.DOScale(1, 0.3f);
        startButton.DOScale(0, 0.3f);
        state = GameState.Active;
        SoundManager.Instance.musicSource.Play();
        rain.SetActive(true);
        StartCoroutine(Timer());
    }


    public IEnumerator Timer() {
        while (state == GameState.Active) {
            yield return new WaitForSeconds(1f);
            timeLeft--;
            timeImage.DOFillAmount(timeLeft / levelTime, 0.3f);
            if (timeLeft <= 0 || !rootBranch.activeInHierarchy) {
                state = GameState.Gameover;
                StartCoroutine(GameOver());
            }
        }
    }

    public void GameOverClick() {
        StartCoroutine(GameOver());
    }

    public IEnumerator GameOver() {
        state = GameState.Gameover;
        sunTransform.DOMove(new Vector3(0f,3f,0f), 0.5f);
        Camera.main.DOColor(levelColors[6], 1f);
        Camera.main.transform.position = new Vector3(0f, 3f, -20f);
        Camera.main.orthographicSize = 3;
        Camera.main.transform.DOShakePosition(3f);
        SoundManager.PlayRandomSfx(gameOverSFX);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void CompleteLevel() {
        if (!isLoading && state != GameState.Gameover) {
            StartCoroutine(SetUpLevel());
        }
    }

    public IEnumerator SetUpLevel() {   
        if(level >= 6) {
            isLoading = true;
            timeImage.DOColor(completeColor, 0.3f);
            timeImage.DOFillAmount(1, 0.3f);
            dayText.text = "7";
            Camera.main.DOColor(levelColors[2], 0.3f);
            sunSprite.DOColor(completeColor, 0.3f);
            innerSunSprite.DOColor(completeColor, 0.3f);
     
            state = GameState.Gameover;
            StopAllCoroutines();
        } else {
            isLoading = true;
            levelTime += 20;
            timeLeft = levelTime;
            timeImage.DOFillAmount(timeLeft / levelTime, 0.3f);
            timeImage.rectTransform.DOPunchScale(new Vector3(1.1f, 1.1f), 0.3f, 1, 1);
            level++;
            dayText.text = (level + 1).ToString();
            SoundManager.PlayRandomSfx(levelCompleteSFX);
            sunTransform.DOShakeScale(0.5f);
            sunTransform.DOMove(levelSunPositions[level].position, 0.5f);
            Camera.main.orthographicSize = Camera.main.orthographicSize <= 13 ? Camera.main.orthographicSize += 2 : 13;
            Camera.main.transform.DOMove(new Vector3(0f, cameraYPosition[level], -20f), 0.3f);
            Camera.main.DOColor(levelColors[level], 0.3f);
            yield return new WaitForSeconds(1f);
            isLoading = false;
        }
       
    }

}
