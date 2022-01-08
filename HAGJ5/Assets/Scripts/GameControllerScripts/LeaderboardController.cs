using TMPro;
using LootLocker.Requests;
using UnityEngine;

public class LeaderboardController : MonoBehaviour
{
    public TMP_InputField memberID;
    private int score = 0;
    public int ID = 1235;

    public int MaxScores = 7;
    public TextMeshProUGUI[] _score;
    public TextMeshProUGUI[] _names;

    public GameObject leaderboardObj;
    public GameObject enterNameObj;

    // Start is called before the first frame update
    void Start()
    {
        enterNameObj.SetActive(true);
        leaderboardObj.SetActive(false);

        score = PlayerPrefs.GetInt("Reward");
        LootLockerSDKManager.StartSession("Player", (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully logged in");
            }
            else
            {
                Debug.Log("Failed to login");
            }

        });
    }

    public void SubmitScore()
    {
        LootLockerSDKManager.SubmitScore(memberID.text, score, ID, (response) => 
        {
            if (response.success)
            {
                Debug.Log("Successfully submitted");
            }
            else
            {
                Debug.Log("Failed in submitting");
            }
        });

        enterNameObj.SetActive(false);
        leaderboardObj.SetActive(true);
        ShowScores();
    }

    public void ShowScores()
    {
        LootLockerSDKManager.GetScoreList(ID, MaxScores, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully obtained");

                LootLockerLeaderboardMember[] scores = response.items;
                for (int i = 0; i<scores.Length; i++)
                {
                    _names[i].text = (scores[i].rank + ".   " + scores[i].member_id);
                    _score[i].text = (scores[i].score.ToString());
                }

                if (scores.Length < MaxScores)
                {
                    for (int i = scores.Length; i< MaxScores; i++)
                    {
                        _names[i].text = (scores[i].rank + ".   none");
                        _score[i].text = "none";
                    }
                }
            }
            else
            {
                Debug.Log("Failed to obtain");
            }
        });
        
    }
}
