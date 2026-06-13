using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

public class UdpTestReceiver : MonoBehaviour
{
    private UdpClient udpClient;
    public int port = 12351;
    private bool isReceiving = false;

    async void Start()
    {
        try
        {
            udpClient = new UdpClient(port);
            isReceiving = true;
            Debug.Log($"<color=green>ポート {port} で受信待機を開始しました...</color>");

            // 非同期で受信を待機
            await ReceiveDataAsync();
        }
        catch (System.Exception e)
        {
            Debug.LogError($"ポート開放エラー（すでにポートが使われている可能性があります）: {e.Message}");
        }
    }

    private async Task ReceiveDataAsync()
    {
        while (isReceiving)
        {
            try
            {
                UdpReceiveResult result = await udpClient.ReceiveAsync();
                Debug.Log($"データ受信成功！ 送信元: {result.RemoteEndPoint} / バイト数: {result.Buffer.Length}");
            }
            catch (System.Exception e)
            {
                if (isReceiving) Debug.LogWarning($"受信エラー: {e.Message}");
            }
        }
    }

    void OnApplicationQuit()
    {
        isReceiving = false;
        if (udpClient != null)
        {
            udpClient.Close();
        }
    }
}