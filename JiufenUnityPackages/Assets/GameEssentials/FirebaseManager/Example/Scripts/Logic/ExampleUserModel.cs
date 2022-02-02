using Newtonsoft.Json;
using UnityEngine;
public class ExampleUserModel : MonoBehaviour
{
    private const string baseUrl = "{firebaseUrl}/Users";

    public void GETUsers()
    {
        FirebaseRequest.FirebaseListRequestPetiton<UserDto>(baseUrl, null, RequestUsersCallback, RequestType.GET);
    }

    public void SENDNewUser()
    {
        FirebaseRequest.FirebaseListRequestPetiton<UserDto>(baseUrl, new UserDto
        {
            nombreUsuario = "Test2",
            scoreMin = "1",
            scoreSeg = "30"
        },
        (success, data) =>
         {
             if (success)
             {
                 Debug.Log(success);
             }
         }
         , RequestType.PATCH);
    }

    public void DELETEUser()
    {
        FirebaseRequest.FirebaseListRequestPetiton<UserDto>(baseUrl, 1, null, RequestType.DELETE);
    }

    public void RequestUsersCallback(bool success, FirebaseListDto<UserDto> data)
    {
        Debug.Log($"Success: {success}");

        if (success)
            Debug.Log(JsonConvert.SerializeObject(data));
    }

}
