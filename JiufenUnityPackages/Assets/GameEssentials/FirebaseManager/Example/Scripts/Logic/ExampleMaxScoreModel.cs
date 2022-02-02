using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class ExampleMaxScoreModel : MonoBehaviour
{
    private const string baseUrl = "{firebaseURl}/MaxScore";
    public void GetMaxScore()
    {
        FirebaseRequest.FirebaseObjectRequestPetiton<int>(baseUrl,
            null,
            (success, maxScore) =>
            {
                Debug.Log(maxScore);
            },
            RequestType.GET);
    }

    public void SetMaxScore()
    {
        FirebaseRequest.FirebaseObjectRequestPetiton<MaxScorePayload>(baseUrl,
            new MaxScorePayload(){MaxScore = 3 },
            (success, maxScore) =>
            {
                Debug.Log(maxScore.MaxScore);
            },
            RequestType.PATCH);
    }

    public void DeleteMaxScore()
    {
        FirebaseRequest.FirebaseObjectRequestPetiton<int>(baseUrl,
            null,
            (success, maxScore) =>
            {
                Debug.Log(maxScore);
            },
            RequestType.DELETE);
    }
}
public class MaxScorePayload
{
    public int MaxScore;
}
