using Opencraft.NetCode;
using UnityEngine;

public interface INetworking
{
    void SendToServer(ToServer message);
}