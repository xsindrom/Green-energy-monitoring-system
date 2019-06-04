using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace Network
{
    public class Server : MonoSingleton<Server>
    {
        public void Post<T, U>(T request, UnityAction<U> callback, Dictionary<string, string> headers = null) where T: BaseRequest where U: BaseResponce, new()
        {
            StartCoroutine(PostCoroutine(request, callback, headers));
        }

        private IEnumerator PostCoroutine<T, U>(T request, UnityAction<U> callback, Dictionary<string, string> headers) where T : BaseRequest where U : BaseResponce, new()
        {
            using (UnityWebRequest www = string.IsNullOrEmpty(request.postData) ? UnityWebRequest.Get(request.path) : UnityWebRequest.Post(request.path, request.postData))
            {
                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        www.SetRequestHeader(header.Key, header.Value);
                    }
                }

                yield return www.SendWebRequest();

                var responce = new U();

                if (!string.IsNullOrEmpty(www.error))
                {
                    responce.status = Status.ERROR;
                }
                else
                {
                    try
                    {
                        responce = responce.Parse<U>(www.downloadHandler.text);
                    }
                    catch
                    {
                        responce.status = Status.ERROR;
                    }
                }

                EventHelper.SafeCall(callback, responce);
            }
        }
    }
}