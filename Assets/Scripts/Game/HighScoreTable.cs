using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UIManager;
using static UIManagerMenu;

public class HighScoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highScoreEntryTransformList;
    private AudioSource audioSource;
    [SerializeField] AudioClip newHighScoreSound;

    private void Awake()
    {
        entryContainer = transform.Find("HSEntryContainer");
        entryTemplate = entryContainer.Find("HSEntryTemplate");
        entryTemplate.gameObject.SetActive(false);
        Debug.Log(transform.Find("HSEntryContainer").Find("HSEntryTemplate"));
        audioSource = GetComponent<AudioSource>();

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        HighScores highScores = JsonUtility.FromJson<HighScores>(jsonString);

        if (highScores == null)
        {
            // There's no stored table, initialize
            highScores = new HighScores();
            highScores.highScoreEntryList = new List<HighScoreEntry>();
            highScores.highScoreEntryList.Add(new HighScoreEntry { name = "HEC", score = 250 });
        }

        highScores.highScoreEntryList.Sort((a, b) => b.score - a.score);
        //// Sort the high score entries by score in descending order
        //for (int i = 0; i < highScores.highScoreEntryList.Count; i++)
        //{
        //    for (int j = i + 1; j < highScores.highScoreEntryList.Count; j++)
        //    {
        //        if (highScores.highScoreEntryList[j].score > highScores.highScoreEntryList[i].score)
        //        {
        //            // Swap the entries
        //            HighScoreEntry temp = highScores.highScoreEntryList[i];
        //            highScores.highScoreEntryList[i] = highScores.highScoreEntryList[j];
        //            highScores.highScoreEntryList[j] = temp;
        //        }
        //    }
        //}

        //Keeps only the top 10 scores
        //if (highScores.highScoreEntryList.Count > 10)
        //{
        //    for (int h = highScores.highScoreEntryList.Count; h > 10; h--)
        //    {
        //        highScores.highScoreEntryList.RemoveAt(10);
        //    }
        //}

        highScoreEntryTransformList = new List<Transform>();
        foreach (HighScoreEntry highscoreEntry in highScores.highScoreEntryList)
        {
            CreateHighScoreEntry(highscoreEntry, entryContainer, highScoreEntryTransformList);
            
            //Play new high score sound
            audioSource.volume = 0.125f;
            audioSource.PlayOneShot(newHighScoreSound);
        }
    }

    private void CreateHighScoreEntry(HighScoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 67f;
        Transform entryTransform = Instantiate(entryTemplate, entryContainer);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            case 1:
                rankString = "1ST";
                break;
            case 2:
                rankString = "2ND";
                break;
            case 3:
                rankString = "3RD";
                break;
            default:
                rankString = rank + "TH";
                break;
        }

        Debug.Log("Rank string: " + rankString);
        entryTransform.Find("PosText").GetComponent<TMPro.TextMeshProUGUI>().text = rankString;

        string name = highscoreEntry.name;

        entryTransform.Find("NameText").GetComponent<TMPro.TextMeshProUGUI>().text = name;

        int score = highscoreEntry.score;

        entryTransform.Find("ScoreText").GetComponent<TMPro.TextMeshProUGUI>().text = score.ToString();

        entryTransform.Find("Background").gameObject.SetActive(rank % 2 == 1);

        switch (rank)
        {
            case 1:
                entryTransform.Find("PosText").GetComponent<TMPro.TextMeshProUGUI>().color = Color.yellow;
                entryTransform.Find("NameText").GetComponent<TMPro.TextMeshProUGUI>().color = Color.yellow;
                entryTransform.Find("ScoreText").GetComponent<TMPro.TextMeshProUGUI>().color = Color.yellow;
                break;
            case 2:
                entryTransform.Find("PosText").GetComponent<TMPro.TextMeshProUGUI>().color = Color.white;
                entryTransform.Find("NameText").GetComponent<TMPro.TextMeshProUGUI>().color = Color.white;
                entryTransform.Find("ScoreText").GetComponent<TMPro.TextMeshProUGUI>().color = Color.white;
                break;
            case 3:
                entryTransform.Find("PosText").GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 0.5f, 0f); // Bronze color
                entryTransform.Find("NameText").GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 0.5f, 0f);
                entryTransform.Find("ScoreText").GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 0.5f, 0f);
                break;
        }

        switch (rank)
        {
            default:
                entryTransform.Find("Trophy").gameObject.SetActive(false);
                break;
            case 1:
                entryTransform.Find("Trophy").GetComponent<Image>().color = Color.yellow;
                break;
            case 2:
                entryTransform.Find("Trophy").GetComponent<Image>().color = Color.white;
                break;
            case 3:
                entryTransform.Find("Trophy").GetComponent<Image>().color = new Color(1f, 0.5f, 0f); // Bronze color
                break;
        }

        transformList.Add(entryTransform);
    }

    public static void AddHighScoreEntry(int score, string name)
    {
        // Create high score entry
        HighScoreEntry highScoreEntry = new HighScoreEntry { score = score, name = name };

        // Load saved high scores
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        HighScores highScores = JsonUtility.FromJson<HighScores>(jsonString);

        if (highScores == null)
        {
            highScores = new HighScores();
            highScores.highScoreEntryList = new List<HighScoreEntry>();
        }

        // Add new entry to high score list
        highScores.highScoreEntryList.Add(highScoreEntry);
        highScores.highScoreEntryList.Sort((a, b) => b.score - a.score);

        //Keeps only the top 10 scores
        if (highScores.highScoreEntryList.Count > 10)
        {
            highScores.highScoreEntryList.RemoveRange(10, highScores.highScoreEntryList.Count - 10);
        }

        // Save updated high scores
        string json = JsonUtility.ToJson(highScores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    private class HighScores
    {
        public List<HighScoreEntry> highScoreEntryList;
    }

    // Represents a single high score entry
    [System.Serializable]
    private class HighScoreEntry
    {
        public int score;
        public string name;
    }
}
