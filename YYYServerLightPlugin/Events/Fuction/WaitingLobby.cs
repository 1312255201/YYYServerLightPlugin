using System;
using System.Collections.Generic;
using AdminToys;
using Footprinting;
using MapGeneration;
using MEC;
using Mirror;
using PlayerRoles;
using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using UnityEngine;

namespace YYYServerLightPlugin.Events.Fuction
{
    public class WaitingLobby
    {
        private static List<PrimitiveObjectToy> primitiveObjectToys = new List<PrimitiveObjectToy>();
        public static List<LightSourceToy> Ex_light = new List<LightSourceToy>();
        private string text;
        private GameObject cube;
        private GameObject light;
        private CoroutineHandle lobbyTimer;
        [PluginEvent(ServerEventType.WaitingForPlayers)]
        void WaitingPlayers()
        {
            Create();
            try
            {
                GameObject.Find("StartRound").transform.localScale = Vector3.zero;
            }
            catch
            {
                //ignore
            }
            if (lobbyTimer.IsRunning)
            {
                Timing.KillCoroutines(lobbyTimer);
            }
            lobbyTimer = Timing.RunCoroutine(LobbyTimer());
        }
        [PluginEvent(ServerEventType.PlayerJoined)]
        void OnPlayerJoined(Player player)
        {
            if (!Round.IsRoundStarted && (GameCore.RoundStart.singleton.NetworkTimer > 8 || GameCore.RoundStart.singleton.NetworkTimer == -2))
            {
                Timing.CallDelayed(0.1f, () =>
                {
                   player.SetRole(RoleTypeId.Tutorial);
                });
                Timing.CallDelayed(0.5f, () =>
                {
                    player.Position = new Vector3(-4, 1101, -105f);
                });
            }
        }
        [PluginEvent(ServerEventType.PlayerDying)]
        void OnPlayerDying(Player player, Player attacker, PlayerStatsSystem.DamageHandlerBase damageHandler)
        {
            if (!Round.IsRoundStarted)
            {
                player.ClearInventory();
            }
        }

        [PluginEvent(ServerEventType.WarheadStart)]
        void WarheadStart(bool isAutomatic, Player player, bool isResumed)
        {
            foreach (var vaLightSourceToy in Ex_light)
            {
                vaLightSourceToy.LightColor = Color.red;
            }
        }
        [PluginEvent(ServerEventType.WarheadStop)]
        void WarheadStop(Player player)
        {
            foreach (var vaLightSourceToy in Ex_light)
            {
                vaLightSourceToy.LightColor = Color.white;
            }
        }
        [PluginEvent(ServerEventType.RoundStart)]
        void RoundStart()
        {
            foreach (var objectToy in primitiveObjectToys)
            {
                NetworkServer.Destroy(objectToy.gameObject);
            }
            primitiveObjectToys.Clear();
            foreach (var variablPlayer in Player.GetPlayers())
            {
                variablPlayer.Role = RoleTypeId.Spectator;
            }
            if (lobbyTimer.IsRunning)
            {
                Timing.KillCoroutines(lobbyTimer);
            }
            foreach (var player in Player.GetPlayers())
            {
                player.IsGodModeEnabled = false;
            }
            foreach (var variRoom in Map.Rooms)
            {
                if (variRoom.Zone != FacilityZone.Surface)
                {
                    Ex_light.Add(CreateLight(variRoom.transform.position + Vector3.up * 4,10,Color.white));
                }
            }
        }
        private IEnumerator<float> LobbyTimer()
        {
            while (!Round.IsRoundStarted)
            {
                foreach (var player1 in Player.GetPlayers())
                {
                    if(player1.Position.y <=1028)
                    {
                        player1.ClearInventory();
                        player1.Position = new Vector3(-4, 1101, -105f);
                    }
                    if(Vector3.Distance(player1.Position,new Vector3(10, 1102, -90)) <= 2)
                    {
                        player1.ClearInventory();
                        player1.Position = new Vector3(-5, 1031, 1);
                        player1.AddItem(ItemType.GunFSP9);
                        player1.AddItem(ItemType.GunAK);
                        player1.AddItem(ItemType.Jailbird);
                        player1.AddItem(ItemType.GunCOM18);
                        player1.AddItem(ItemType.GunE11SR);
                        player1.AddItem(ItemType.GunLogicer);
                        player1.AddItem(ItemType.GunCrossvec);
                        player1.AddItem(ItemType.GunCom45);
                        player1.SetAmmo(ItemType.Ammo556x45,100);
                        player1.SetAmmo(ItemType.Ammo9x19,100);
                        player1.SetAmmo(ItemType.Ammo12gauge,100);
                        player1.SetAmmo(ItemType.Ammo44cal,100);
                        player1.SetAmmo(ItemType.Ammo762x39,100);
                    }
                }
                text = string.Empty;
                text +=  "<size=40><color=#FFFF00><b>回合即将开始,剩余等待时间：{seconds}</b></color></size>\n<size=30><i>{players}</i></size>";
                var networkTimer = GameCore.RoundStart.singleton.NetworkTimer;
                switch (networkTimer)
                {
                    case -2: text = text.Replace("{seconds}", "服务器已暂停"); break;

                    case -1: text = text.Replace("{seconds}", "回合已启动"); break;

                    case 0: text = text.Replace("{seconds}", "回合已启动"); break;

                    default: text = text.Replace("{seconds}", networkTimer.ToString()); break;
                }
                text = text.Replace("{players}", Player.GetPlayers().Count+"个玩家已连接");
                foreach (var player in Player.GetPlayers())
                {
                    player.ReceiveHint(text, 2);
                }
                yield return Timing.WaitForSeconds(1f);
            }
            yield break;
        }
        [PluginEvent(ServerEventType.PlayerDropItem)]
        void OnPlayerDropItem(Player player, InventorySystem.Items.ItemBase item)
        {
            if(!Round.IsRoundStarted)
            {
                NetworkServer.Destroy(item.gameObject);
            }
        }

        public void Create()
        {
            InitLate();
            primitiveObjectToys.Add(CreateCubeAPI(new Vector3(1, 1030, -8), Color.white, new Vector3(100, 1, 50), PrimitiveType.Cube));
            primitiveObjectToys.Add(CreateCubeAPI(new Vector3(1, 1100, -100), Color.white, new Vector3(50, 1, 50), PrimitiveType.Cube));
            primitiveObjectToys.Add(CreateCubeAPI(new Vector3(10, 1101, -90), Color.red, new Vector3(1, 1, 1), PrimitiveType.Cube));
            CreateLight(new Vector3(44, 1035, -8), 80, Color.white);
            CreateLight(new Vector3(-2, 1035, -8), 90, Color.white);
            CreateLight(new Vector3(-46, 1035, -8), 80, Color.white);
            CreateLight(new Vector3(10, 1105, -100), 150, Color.white);
        }
        public PrimitiveObjectToy CreateCubeAPI(Vector3 position, Color color, Vector3 size, PrimitiveType primitiveType)
        {
            cube.TryGetComponent<AdminToyBase>(out var component);
            AdminToyBase adminToyBase = UnityEngine.Object.Instantiate(component, position, Quaternion.identity);
            PrimitiveObjectToy Base = (PrimitiveObjectToy)adminToyBase;
            NetworkServer.Spawn(Base.gameObject);
            Base.NetworkPrimitiveType = primitiveType;
            Base.NetworkMaterialColor = color;
            var transform = Base.transform;
            transform.position = position;
            transform.rotation = Quaternion.identity;
            transform.localScale = size;
            Base.NetworkScale = transform.localScale;
            Base.NetworkPosition = transform.position;
            Base.NetworkRotation = new LowPrecisionQuaternion(transform.rotation);
            return Base;
        }
        private LightSourceToy CreateLight(Vector3 vector3 ,int lightrange,Color color)
        {
            light.TryGetComponent<LightSourceToy>(out var component2);
            LightSourceToy Base2 = UnityEngine.Object.Instantiate(component2);
            Base2.transform.position = vector3;
            Base2.transform.localScale = Vector3.one;
            Base2.LightColor =color;
            Base2.LightRange = lightrange;
            Base2.LightIntensity = lightrange;
            NetworkServer.Spawn(Base2.gameObject);
            GameObject awa2 = Base2.gameObject;
            awa2.transform.localPosition = vector3;
            return Base2;
        }
        private void InitLate()
        {
            foreach (var prefab in NetworkClient.prefabs)
            {
                if (prefab.Value.TryGetComponent<PrimitiveObjectToy>(out var primitiveObjectToy))
                {
                    cube = prefab.Value;
                    Log.Info("找到方块了");
                }
                if (prefab.Value.TryGetComponent<LightSourceToy>(out LightSourceToy lightSourceToy))
                {
                    light = prefab.Value;
                    Log.Info("找到光源了");
                }
            }
        }
    }
}