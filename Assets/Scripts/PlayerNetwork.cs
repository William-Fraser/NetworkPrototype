using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    private NetworkVariable<CustomData> randomNum = new NetworkVariable<CustomData>(
        new CustomData { 
            x = 0,
            y = 0,
            z = 0,
        }, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    private NetworkVariable<Vector3> Movement = new NetworkVariable<Vector3>(default,
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public struct CustomData : INetworkSerializable
    {
        public float x;
        public float y;
        public float z;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref x);
            serializer.SerializeValue(ref y);
            serializer.SerializeValue(ref z);
        }
    }

    public override void OnNetworkSpawn()
    {
        Movement.OnValueChanged += (Vector3 previousValue, Vector3 newValue) => { 
        Debug.Log($"{OwnerClientId}; Movement: {Movement.Value}");
        };
    }

    void Update()
    {

        if (!IsOwner) return;

        Vector3 MoveDir = new Vector3(0, 0, 0);

        if(Input.GetKey(KeyCode.W)) MoveDir.z = +1f;
        if(Input.GetKey(KeyCode.A)) MoveDir.x = -1f;
        if(Input.GetKey(KeyCode.S)) MoveDir.z = -1f;
        if(Input.GetKey(KeyCode.D)) MoveDir.x = +1f;

        Movement.Value = MoveDir;

        MovementServerRpc();
    }

    [ServerRpc]
    private void MovementServerRpc()
    {
        float moveSpeed = 5f;
        transform.position += Movement.Value * moveSpeed * Time.deltaTime;
    }
}
