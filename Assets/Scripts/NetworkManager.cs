using System.Collections;
using UnityEngine.Networking;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    //Todo: to constants file
    const string serverURL = "https://dev3r02.elysium.today/inventory/status";

    const string authParam = "auth";
    const string authKey = "BMeHG5xqJeB4qCjpuJCTQLsqNGaqkfB6";

    const string itemIdParam = "itemId";
    const string itemEventParam = "eventType";

    // ToDo: verify message sending sequence
    public void SendEquipEvent(int itemId, bool equipped)
    {
        StartCoroutine(Send(itemId, equipped));
    }

    IEnumerator Send(int itemId, bool equipped)
    {
        WWWForm form = new WWWForm();
        form.AddField(itemIdParam, itemId.ToString());
        form.AddField(itemEventParam, equipped.ToString());

        UnityWebRequest Request = UnityWebRequest.Post(serverURL, form);
        Request.SetRequestHeader(authParam, authKey);

        yield return Request.SendWebRequest();

        if (Request.isNetworkError || Request.isHttpError)
        {
            Debug.Log(Request.error);
        }

        Request.Dispose();
    }
}
