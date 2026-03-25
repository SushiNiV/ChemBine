using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;

public class LessonManager : MonoBehaviour
{
    [Header("Hierarchy Connections")]
    public RectTransform contentStrip;
    public GameObject panelPrefab;
    public Button nextBT, prevBT;
    public Image progressBar;

    [Header("Prefabs")]
    public GameObject lessonPrefab;
    public GameObject quizPrefab;

    [Header("Settings")]
    public float slideSpeed = 0.4f;
    public float panelWidth = 1000f;

    [Header("Finish Logic")]
    public Button finishBT;

    private int currentIndex = 0;
    private int totalPages = 0;
    private Coroutine progressCoroutine;

    void Start()
    {
        Debug.Log(">>> SCRIPT STARTED <<<");
        LoadLesson(1);
    }

    void LoadLesson(int lessonID)
    {
        string dbName = "TRY-DB.db";
        string dbPath = System.IO.Path.Combine(Application.streamingAssetsPath, dbName);

        if (!System.IO.File.Exists(dbPath))
        {
            Debug.LogError("DATABASE MISSING: Could not find " + dbPath);
            return;
        }

        string connectionString = "URI=file:" + dbPath;
        Debug.Log("Attempting to connect to: " + connectionString);

        using (var connection = new SqliteConnection(connectionString))
        {
            try
            {
                connection.Open();
                Debug.Log("Database State: " + connection.State);

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $"SELECT title_text, content, type, metadata FROM slides WHERE lesson_id = {lessonID} ORDER BY page_no ASC";
                    Debug.Log("Executing Query: " + command.CommandText);

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string title = reader.GetString(0);
                            string content = reader.GetString(1);
                            int type = reader.GetInt32(2);
                            string meta = reader.IsDBNull(3) ? "" : reader.GetString(3);

                            SpawnPanel(title, content, type, meta);
                            totalPages++;
                        }
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("SQL Error: " + e.Message);
            }
        }
        UpdateUI(true);
    }

    void CreatePanel(string title, string body)
    {
        GameObject newPanel = Instantiate(panelPrefab, contentStrip);
        newPanel.transform.Find("Title").GetComponent<TextMeshProUGUI>().text = title;
        newPanel.transform.Find("Content/ContentT").GetComponent<TextMeshProUGUI>().text = body;
    }

    public void MoveNext()
    {
        if (currentIndex < totalPages - 1)
        {
            currentIndex++;
            StartCoroutine(AnimateSlide());
        }
    }

    public void MovePrev()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            StartCoroutine(AnimateSlide());
        }
    }

    IEnumerator AnimateSlide()
    {
        Vector2 startPos = contentStrip.anchoredPosition;
        Vector2 endPos = new Vector2(-currentIndex * panelWidth, 0);

        float elapsed = 0;
        while (elapsed < slideSpeed)
        {
            elapsed += Time.deltaTime;
            contentStrip.anchoredPosition = Vector2.Lerp(startPos, endPos, elapsed / slideSpeed);
            yield return null;
        }
        contentStrip.anchoredPosition = endPos;
        UpdateUI(false);
    }

    IEnumerator AnimateProgress(float targetFill)
    {
        float startFill = progressBar.fillAmount;
        float elapsed = 0;
        float duration = slideSpeed;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            progressBar.fillAmount = Mathf.Lerp(startFill, targetFill, elapsed / duration);
            yield return null;
        }
        progressBar.fillAmount = targetFill;
    }

    void UpdateUI(bool instant)
    {
        prevBT.gameObject.SetActive(currentIndex > 0);

        bool isLastPage = (totalPages > 0 && currentIndex == totalPages - 1);

        GameObject currentPanel = contentStrip.GetChild(currentIndex).gameObject;
        QuizController quiz = currentPanel.GetComponent<QuizController>();

        if (isLastPage)
        {
            nextBT.gameObject.SetActive(false);
            finishBT.transform.SetAsLastSibling();

            finishBT.gameObject.SetActive(quiz == null);
        }
        else
        {
            finishBT.gameObject.SetActive(false);
            nextBT.gameObject.SetActive(quiz == null);
        }

        if (progressBar != null && totalPages > 0)
        {
            float targetFill = (float)currentIndex / totalPages;
            if (quiz == null) targetFill = (float)(currentIndex + 1) / totalPages;
            UpdateProgressBar(targetFill, instant);
        }
    }

    void UpdateProgressBar(float targetFill, bool instant)
    {
        if (instant)
        {
            progressBar.fillAmount = targetFill;
        }
        else
        {
            if (progressCoroutine != null) StopCoroutine(progressCoroutine);
            progressCoroutine = StartCoroutine(AnimateProgress(targetFill));
        }
    }

    void SpawnPanel(string title, string content, int type, string meta)
    {
        GameObject prefabToUse = (type == 1) ? quizPrefab : lessonPrefab;
        GameObject newPanel = Instantiate(prefabToUse, contentStrip);

        newPanel.transform.Find("Title").GetComponent<TextMeshProUGUI>().text = title;

        TextMeshProUGUI mainTextField = newPanel.transform.Find("Content/ContentT").GetComponent<TextMeshProUGUI>();

        if (type == 1)
        {
            QuizController quizScript = newPanel.GetComponent<QuizController>();
            if (quizScript != null)
            {
                quizScript.SetupQuiz(mainTextField, content, meta);
            }
        }
        else
        {
            mainTextField.text = content;
        }
    }

    public void EnableFinishButton()
    {
        bool isLastPage = (currentIndex == totalPages - 1);

        if (isLastPage)
        {
            if (finishBT != null)
            {
                finishBT.gameObject.SetActive(true);
                finishBT.interactable = true;
                finishBT.transform.SetAsLastSibling();
            }
        }
        else
        {
            if (nextBT != null)
            {
                nextBT.gameObject.SetActive(true);
                nextBT.transform.SetAsLastSibling();
            }
        }

        if (progressBar != null && totalPages > 0)
        {
            float targetFill = (float)(currentIndex + 1) / totalPages;
            UpdateProgressBar(targetFill, false);
        }
    }
}