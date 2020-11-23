using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float scrollingSpeed;
    public GroundScroll groundScroll;
    public ObstacleSpawner obstacleSpawner;
    public TextMeshProUGUI countdownTextMeshProUgui;
    public GameObject loseScreen;

    public event EventHandler GameStarted;
    public event EventHandler GameEnded;

    public bool IsPlaying { get; private set; }

    public float startWaitTime;
    private float currentStartWaitTime;

    private void OnEnable()
    {
        StartGame();
    }

    public void StartGame()
    {
        foreach (Obstacle obstacle in FindObjectsOfType<Obstacle>())
        {
            Destroy(obstacle.gameObject);
        }
        loseScreen.SetActive(false);
        currentStartWaitTime = startWaitTime;
        StartCoroutine(ICountdown(() =>
        {
            IsPlaying = true;
            groundScroll.scrollingSpeed = scrollingSpeed;
            obstacleSpawner.scrollingSpeed = scrollingSpeed;
            OnGameStarted();
        }));
    }

    private void OnGameStarted()
    {
        EventHandler handler = GameStarted;
        handler?.Invoke(this, EventArgs.Empty);
    }
    private void OnGameEnded()
    {
        EventHandler handler = GameEnded;
        handler?.Invoke(this, EventArgs.Empty);
    }

    private IEnumerator ICountdown(Action callback = null)
    {
        while (currentStartWaitTime > 0)
        {
            countdownTextMeshProUgui.SetText(currentStartWaitTime.ToString("#"));
            yield return null;
            currentStartWaitTime -= Time.deltaTime;
        }

        countdownTextMeshProUgui.SetText(string.Empty);

        callback?.Invoke();
    }

    public void KillPlayer()
    {
        IsPlaying = false;
        loseScreen.SetActive(true);
        OnGameEnded();
    }
}
