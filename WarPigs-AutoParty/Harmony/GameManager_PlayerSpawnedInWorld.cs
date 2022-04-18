using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarPigs.AutoParty.Harmony
{
    [HarmonyPatch(typeof(GameManager))]
    [HarmonyPatch(nameof(GameManager.PlayerSpawnedInWorld))]
    public class GameManager_PlayerSpawnedInWorld
    {
        private static void Postfix(GameManager __instance, ClientInfo _cInfo, RespawnType _respawnReason, Vector3i _pos, int _entityId)
        {
            //Make sure the game is running
            if (!__instance.gameStateManager.IsGameStarted()) return;

            //Make sure we're a dedicated server
            if (!GameManager.IsDedicatedServer) return;

            //Make sure we have players logged in
            if (GameManager.Instance.World.Players.list.Count == 0) return;

            Entity entity;
            if (!__instance.World.Entities.dict.TryGetValue(_entityId, out entity))
            {
                return;
            }
            EntityPlayer entityPlayer = entity as EntityPlayer;
            if (entityPlayer == null)
            {
                return;
            }

            Log.Out($"FPHI: PlayerSpawnedInWorld");
            Party party = null;
            foreach (EntityPlayer player in GameManager.Instance.World.Players.list)// this list is only of active players
            {
                if (party == null && player.IsInParty() && player.Party != null) party = player.Party;
            }

            if(party == null)
            {
                party = PartyManager.Current.CreateParty();
                Log.Out($"FPHI: Created Party {party.PartyID}");
            }

            foreach (EntityPlayer player in GameManager.Instance.World.Players.list)// this list is only of active players
            {
                Log.Out($"FPHI: Adding {player.GetDebugName()} to Party {party.PartyID}");
                party.AddPlayer(player);
                SingletonMonoBehaviour<ConnectionManager>.Instance.SendPackage(NetPackageManager.GetPackage<NetPackagePartyData>().Setup(party, player.entityId, NetPackagePartyData.PartyActions.AcceptInvite, false), false, -1, -1, -1, -1);
            }

        }
    }
}
