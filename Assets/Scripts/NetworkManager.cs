using System.Collections;
using UnityEngine.Networking;
using UnityEngine;

using static Constants;

public class NetworkManager : MonoBehaviour
{
    //Todo: to constants file
    const string serverURL = "https://dev3r02.elysium.today/inventory/status";

    const string authParam = "auth";
    const string authKey = "BMeHG5xqJeB4qCjpuJCTQLsqNGaqkfB6";

    const string itemIdParam = "itemId";
    const string itemEventParam = "eventType";

    public void SendEquipEvent(EquipEventType eventType, int itemId)
    {
        StartCoroutine(Post(eventType, itemId));
    }

    IEnumerator Post(EquipEventType eventType, int itemId)
    {
        WWWForm form = new WWWForm();
        form.AddField(itemIdParam, itemId.ToString());
        form.AddField(itemEventParam, eventType.ToString());

        UnityWebRequest www = UnityWebRequest.Post(serverURL, form);
        www.SetRequestHeader(authParam, authKey);

        Debug.LogError("Post: " + eventType + " " + itemId);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }

        Debug.Log(www.downloadHandler.text);
        Debug.Log(www.downloadHandler.data.Length);

        string resultUTF8 = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
        string resultASCII = System.Text.Encoding.ASCII.GetString(www.downloadHandler.data);

        Debug.Log("resultUTF8 " + resultUTF8);
        Debug.Log("resultASCII " + resultASCII);

        www.Dispose();
    }
}
