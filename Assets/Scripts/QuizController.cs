using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizController : MonoBehaviour
{
    public Button[] answerButtons;
    private LessonManager manager;

    void Start()
    {
        // Using the modern method to avoid the obsolete warning
        manager = Object.FindFirstObjectByType<LessonManager>();
    }

    public void SetupQuiz(TextMeshProUGUI questionField, string questionText, string metadata)
    {
        questionField.text = questionText;

        string[] sections = metadata.Split('|');
        string[] options = sections[0].Split(';');
        int correctIndex = int.Parse(sections[1]);

        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < options.Length)
            {
                answerButtons[i].gameObject.SetActive(true);
                answerButtons[i].interactable = true; // Ensure buttons are reset to interactable
                answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = options[i];

                int index = i;
                answerButtons[i].onClick.RemoveAllListeners();
                answerButtons[i].onClick.AddListener(() => CheckAnswer(index, correctIndex));
            }
            else
            {
                answerButtons[i].gameObject.SetActive(false);
            }
        }
    }

    void CheckAnswer(int selected, int correct)
    {
        string hexGreenBg = "#3CAEA3";
        string hexRedBg = "#EA543A";
        string hexTextSelected = "#FFFFFF";
        string hexDefaultBg = "#FFFFFF";
        string hexDefaultText = "#000000";

        foreach (Button btn in answerButtons)
        {
            if (ColorUtility.TryParseHtmlString(hexDefaultBg, out Color defBg))
                btn.image.color = defBg;

            TextMeshProUGUI btnText = btn.GetComponentInChildren<TextMeshProUGUI>();
            if (btnText != null && ColorUtility.TryParseHtmlString(hexDefaultText, out Color defText))
                btnText.color = defText;
        }

        bool isCorrect = (selected == correct);
        string targetBgHex = isCorrect ? hexGreenBg : hexRedBg;

        if (ColorUtility.TryParseHtmlString(targetBgHex, out Color finalBg))
            answerButtons[selected].image.color = finalBg;

        TextMeshProUGUI selectedText = answerButtons[selected].GetComponentInChildren<TextMeshProUGUI>();
        if (selectedText != null && ColorUtility.TryParseHtmlString(hexTextSelected, out Color finalText))
            selectedText.color = finalText;

        if (isCorrect)
        {
            Debug.Log("<color=green>Correct!</color>");

            if (manager != null)
                manager.EnableFinishButton();

            foreach (Button btn in answerButtons)
            {
                btn.interactable = false;
            }
        }
        else
        {
            Debug.Log("<color=red>Wrong!</color>");
        }
    }
}