using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedRPC("{\"types\":[[\"float\"]]")]
	[GeneratedRPCVariableNames("{\"types\":[[\"Damage\"]]")]
	public abstract partial class PlayerCubeBehavior : NetworkBehavior
	{
		public const byte RPC_ATTACK = 0 + 5;
		
		public PlayerCubeNetworkObject networkObject = null;

		public override void Initialize(NetworkObject obj)
		{
			// We have already initialized this object
			if (networkObject != null && networkObject.AttachedBehavior != null)
				return;
			
			networkObject = (PlayerCubeNetworkObject)obj;
			networkObject.AttachedBehavior = this;

			base.SetupHelperRpcs(networkObject);
			networkObject.RegisterRpc("Attack", Attack, typeof(float));

			MainThreadManager.Run(() =>
			{
				NetworkStart();
				networkObject.Networker.FlushCreateActions(networkObject);
			});

			networkObject.onDestroy += DestroyGameObject;

			if (!obj.IsOwner)
			{
				if (!skipAttachIds.ContainsKey(obj.NetworkId))
					ProcessOthers(gameObject.transform, obj.NetworkId + 1);
				else
					skipAttachIds.Remove(obj.NetworkId);
			}

			if (obj.Metadata == null)
				return;

			byte transformFlags = obj.Metadata[0];

			if (transformFlags == 0)
				return;

			BMSByte metadataTransform = new BMSByte();
			metadataTransform.Clone(obj.Metadata);
			metadataTransform.MoveStartIndex(1);

			if ((transformFlags & 0x01) != 0 && (transformFlags & 0x02) != 0)
			{
				MainThreadManager.Run(() =>
				{
					transform.position = ObjectMapper.Instance.Map<Vector3>(metadataTransform);
					transform.rotation = ObjectMapper.Instance.Map<Quaternion>(metadataTransform);
				});
			}
			else if ((transformFlags & 0x01) != 0)
			{
				MainThreadManager.Run(() => { transform.position = ObjectMapper.Instance.Map<Vector3>(metadataTransform); });
			}
			else if ((transformFlags & 0x02) != 0)
			{
				MainThreadManager.Run(() => { transform.rotation = ObjectMapper.Instance.Map<Quaternion>(metadataTransform); });
			}
		}

		protected override void CompleteRegistration()
		{
			base.CompleteRegistration();
			networkObject.ReleaseCreateBuffer();
		}

		public override void Initialize(NetWorker networker, byte[] metadata = null)
		{
			Initialize(new PlayerCubeNetworkObject(networker, createCode: TempAttachCode, metadata: metadata));
		}

		private void DestroyGameObject(NetWorker sender)
		{
			MainThreadManager.Run(() => { try { Destroy(gameObject); } catch { } });
			networkObject.onDestroy -= DestroyGameObject;
		}

		public override NetworkObject CreateNetworkObject(NetWorker networker, int createCode, byte[] metadata = null)
		{
			return new PlayerCubeNetworkObject(networker, this, createCode, metadata);
		}

		protected override void InitializedTransform()
		{
			networkObject.SnapInterpolations();
		}

		/// <summary>
		/// Arguments:
		/// float Damage
		/// </summary>
		public abstract void Attack(RpcArgs args);

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}