using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverWindow : MonoBehaviour
{
    private Text scoreText;
    private Button retryBtn;

    private void Awake()
    {
        scoreText = transform.Find("scoreText").GetComponent<Text>();

        retryBtn = transform.Find("retryBtn").GetComponent<Button>();
        retryBtn.onClick.AddListener(Retry);

    }

    private void Start()
    {
        Uni.GetInstance().OnDied += Uni_OnDied;
        Hide();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Loader.Load(Loader.Scene.SampleScene);
        }
    }

    private void Uni_OnDied(object sender, System.EventArgs e)
    {
        scoreText.text = Level.GetInstance().GetPipesPassedCount().ToString();

        Show();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Retry()
    {
        Loader.Load(Loader.Scene.SampleScene);
    }
}
