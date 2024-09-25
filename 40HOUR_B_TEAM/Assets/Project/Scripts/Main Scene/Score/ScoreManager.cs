using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI[] currentScoreText = new TextMeshProUGUI[4];

    [SerializeField]
    private RoundCounter roundCounter = null;

    [SerializeField]
    private DrawScoreImage drawScoreImage = null;

    [SerializeField]
    private InputButtonManager inputButtonManager = null;

    [SerializeField]
    private HatGenerator hatGenerator = null;

    private int[] playerScores = new int[4];

    private List<int> addScores = new List<int>();

    private const int NoneHat = 0;
    private const int NormalHatScore = 1;
    private const int EaglesHatScore = 2;
    private const int KingHatScore = 3;

    private const string FixedName = "(Clone)";

    private void Start()
    {
        for(int i = 0; i < PlayerData.PlayerMax; i++)
        {
            addScores.Add(i);
        }
    }

    private void SetScoreText(int playerNum, int currentScore)
    {
        currentScoreText[playerNum].text = currentScore.ToString();
    }

    public void AddScore(int playerNum)
    {
        playerScores[playerNum] = playerScores[playerNum] + addScores[playerNum];
        StartCoroutine(PlayImage(playerNum));
        SetScoreText(playerNum, playerScores[playerNum]);
    }

    private IEnumerator PlayImage(int playerNum)
    {
        drawScoreImage.SetActiveImage(playerNum, addScores, true);

        yield return new WaitForSeconds(1.5f);

        drawScoreImage.SetActiveImage(playerNum, addScores, false);
    }

    /// <summary>
    /// �v���C���[���I�������{�^�����X�R�A���_�̂ڂ��������m�F���A�ϐ��ɋL�^���܂��B
    /// </summary>
    public void ConvertButtonNumToScore()
    {
        for(int i = 0; i < PlayerData.PlayerMax; i++)
        {
            if (hatGenerator.locatedHat[inputButtonManager.InputButtonNum[i] - 1].gameObject.name == HatData.EaglesHatName + FixedName)
            {
                addScores[i] = EaglesHatScore;
                continue;
            }

            if (hatGenerator.locatedHat[inputButtonManager.InputButtonNum[i] - 1].gameObject.name == HatData.KingHatName + FixedName)
            {
                addScores[i] = KingHatScore;
                continue;
            }

            if (inputButtonManager.InputButtonNum[i] != 0)
            {
                addScores[i] = NormalHatScore;
                continue;
            }
            
            addScores[i] = NoneHat;
        }
        
    }
}