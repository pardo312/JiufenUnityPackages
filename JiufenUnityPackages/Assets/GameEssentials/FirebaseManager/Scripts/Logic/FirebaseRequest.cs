using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;
using UnityEngine;

public static class FirebaseRequest
{
    #region  Methods
    #region SingleObject Request 
    public static async void FirebaseObjectRequestPetiton<T>(string _url, object _payload = null, Action<bool, T> _callback = null, RequestType _type = RequestType.GET)
    {
        StringContent data = null;
        if (_payload != null)
        {
            string json = JsonConvert.SerializeObject(_payload);
            data = new StringContent(json, Encoding.UTF8, "application/json");


            if (_type == RequestType.PATCH)
            {
                _url = _url.Remove(_url.LastIndexOf("/")+1, _url.Length - (_url.LastIndexOf("/")+1));
            }

            _url += ".json";
            if (_type == RequestType.GET)
                _url += SetGetParameters(json);
        }
        else
        {
            _url += ".json";
        }


        using (var httpClient = new HttpClient())
        {
            HttpResponseMessage response = new HttpResponseMessage();
            switch (_type)
            {
                case RequestType.GET:
                    response = await httpClient.GetAsync(_url);
                    break;
                case RequestType.POST:
                    response = await httpClient.PostAsync(_url, data);
                    break;
                case RequestType.PUT:
                    response = await httpClient.PutAsync(_url, data);
                    break;
                case RequestType.PATCH:
                    var request = new HttpRequestMessage(new HttpMethod("PATCH"), _url);
                    request.Content = data;
                    response = await httpClient.SendAsync(request);
                    break;
                case RequestType.DELETE:
                    response = await httpClient.DeleteAsync(_url);
                    break;
            }
            string result = response.Content.ReadAsStringAsync().Result;
            result = result.Replace("\\", string.Empty);

            T responseR = default(T);
            if (!String.IsNullOrEmpty(result) && result != "null")
                responseR = JsonConvert.DeserializeObject<T>(result);

            if (_callback != null)
            {
                if (IsAnyNotNullOrEmpty(responseR))
                    _callback(responseR != null, responseR);
                else
                    _callback(false, default(T));
            }
        }
    }
    #endregion SingleObject Request 

    #region List Request Petiton
    public static async void FirebaseListRequestPetiton<T>(string _url, object _payload = null, Action<bool, FirebaseListDto<T>> _callback = null, RequestType _type = RequestType.GET, bool patchWithFinalPayload = false)
    {
        StringContent data = null;
        if (_payload != null)
        {
            string json = "";
            if (_payload.GetType() == typeof(string))
                json = (string)_payload;
            else
                json = JsonConvert.SerializeObject(_payload);


            if (_type == RequestType.PATCH)
            {
                if (!patchWithFinalPayload)
                {
                    SetPayloadPatchRequest(_url, json, (finalJson) =>
                      {
                          FirebaseListRequestPetiton<T>(_url, finalJson, _callback, _type, true);
                      });
                    return;
                }
                else
                {
                    if (_payload.GetType() != typeof(ChildCountPayload))
                        _url += "/List";
                }
            }
            else if (_type == RequestType.DELETE)
            {
                LowerChildCount(_url);
                _url += $"/List/{(int)_payload}";
            }


            _url += ".json";
            data = new StringContent(json, Encoding.UTF8, "application/json");
            if (_type == RequestType.GET)
                _url += SetGetParameters(json);
        }
        else
        {
            _url += ".json";
        }


        using (var httpClient = new HttpClient())
        {
            HttpResponseMessage response = new HttpResponseMessage();
            switch (_type)
            {
                case RequestType.GET:
                    response = await httpClient.GetAsync(_url);
                    break;
                case RequestType.POST:
                    response = await httpClient.PostAsync(_url, data);
                    break;
                case RequestType.PUT:
                    response = await httpClient.PutAsync(_url, data);
                    break;
                case RequestType.PATCH:
                    var request = new HttpRequestMessage(new HttpMethod("PATCH"), _url);
                    request.Content = data;
                    response = await httpClient.SendAsync(request);
                    break;
                case RequestType.DELETE:
                    response = await httpClient.DeleteAsync(_url);
                    break;
            }
            string result = response.Content.ReadAsStringAsync().Result;
            result = result.Replace("\\", string.Empty);

            FirebaseListDto<T> responseR = default(FirebaseListDto<T>);
            if (!String.IsNullOrEmpty(result) && result != "null")
            {
                responseR = JsonConvert.DeserializeObject<FirebaseListDto<T>>(result);
                if (responseR != null && responseR.List != null)
                    responseR.List.RemoveAll(item => item == null);
            }


            if (_callback != null)
            {
                if (_type == RequestType.PATCH && patchWithFinalPayload && _payload.GetType() != typeof(ChildCountPayload))
                {
                    if (!String.IsNullOrEmpty(result) && result != "null")
                        _callback(true, null);
                    else
                        _callback(false,null);
                }
                else
                {
                    if (IsAnyNotNullOrEmpty(responseR))
                        _callback(responseR != null, responseR);
                    else
                        _callback(false, default(FirebaseListDto<T>));
                }
            }
        }
    }

    #region Patch Payload Set
    private static void SetPayloadPatchRequest(string _url, string _originalJson, Action<string> _finalPayloadCallback)
    {
        string dq = ('"' + "");
        GetNumberOfListChilds(_url, (number) =>
         {
             SetNumberOfListChilds(_url, number + 1);

             string finalJson = "{" + dq + number + dq + ":" + _originalJson + "}";
             _finalPayloadCallback?.Invoke(finalJson);
         });
    }

    public static void GetNumberOfListChilds(string _url, Action<int> _response)
    {
        FirebaseObjectRequestPetiton<int>(_url + "/ChildCount", null, (success, data) =>
          {
              if (success)
              {
                  _response?.Invoke(data);
              }
              else
              {
                  SetNumberOfListChilds(_url, 0);
                  _response.Invoke(0);
              }
          }, RequestType.GET);
    }

    private static void SetNumberOfListChilds(string _url, int _newChildCount)
    {
        FirebaseListRequestPetiton<ChildCountPayload>(
            _url,
            new ChildCountPayload() { ChildCount = _newChildCount }, null,
            RequestType.PATCH, true
        );
    }
    #endregion Patch Payload Set

    #region Delete Set
    public static void LowerChildCount(string _url)
    {
        GetNumberOfListChilds(_url, (number) =>
         {
             SetNumberOfListChilds(_url, number - 1);
         });
    }
    #endregion Delete Set
    #endregion List Request Petiton
    #region Helpers
    private static string SetGetParameters(string json)
    {
        string paramsUrl = "";
        Dictionary<string, object> dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        foreach (KeyValuePair<string, object> entry in dictionary)
        {
            if (paramsUrl.Length > 0)
                paramsUrl += "&";
            paramsUrl += $"{entry.Key}={entry.Value}";
        }
        return $"?{paramsUrl}";
    }

    private static bool IsAnyNotNullOrEmpty(object myObject)
    {
        bool anyParamNotNull = true;
        foreach (FieldInfo pi in myObject.GetType().GetRuntimeFields())
        {
            if (pi.GetValue(myObject) == null)
            {
                anyParamNotNull = false;
            }
        }
        return anyParamNotNull;
    }
    #endregion Helpers
    #endregion  Methods
}
public class FirebaseListDto<T>
{
    public int ChildCount;
    public List<T> List;
}
