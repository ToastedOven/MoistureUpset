using R2API.Networking;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoistureUpset.NetMessages
{
    public static class Register
    {
        public static void Init()
        {

            NetworkingAPI.RegisterMessageType<InteractReplacements.SyncFidget>();

            NetworkingAPI.RegisterMessageType<SyncAudio>();
            NetworkingAPI.RegisterMessageType<SyncAudioWithJotaroSubtitles>();
            NetworkingAPI.RegisterMessageType<SyncDamage>();
            NetworkingAPI.RegisterMessageType<SyncItems>();
            NetworkingAPI.RegisterMessageType<SyncSuicide>();
            NetworkingAPI.RegisterMessageType<SyncChance>();
            NetworkingAPI.RegisterMessageType<SyncShrine>();
            NetworkingAPI.RegisterMessageType<SyncBroke>();
            NetworkingAPI.RegisterMessageType<SyncBonziApproach>();
            NetworkingAPI.RegisterMessageType<SyncLunarReRoll>();
            NetworkingAPI.RegisterMessageType<SendSoda>();
            NetworkingAPI.RegisterMessageType<RequestSoda>();
            NetworkingAPI.RegisterMessageType<RequestRoblox>();
            NetworkingAPI.RegisterMessageType<SendRoblox>();
            NetworkingAPI.RegisterMessageType<SyncFanFare>();
            NetworkingAPI.RegisterMessageType<SendBlock>();
            NetworkingAPI.RegisterMessageType<RequestBlock>();
        }
    }
}
