using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardPositionTextSetter : MonoBehaviour
{
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text scoreText;
    public void UpdateText(string Name, float Score)
    {
        nameText.text = Name;
        scoreText.text = Score.ToString();
    }
}
