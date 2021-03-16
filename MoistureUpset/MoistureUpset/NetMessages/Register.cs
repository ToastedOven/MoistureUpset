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
            NetworkingAPI.RegisterMessageType<SyncAnimation>();
            NetworkingAPI.RegisterMessageType<InteractReplacements.SyncFidget>();
            NetworkingAPI.RegisterMessageType<SyncAudio>();
            NetworkingAPI.RegisterMessageType<SyncAudioWithJotaroSubtitles>();
            NetworkingAPI.RegisterMessageType<SyncDamage>();
            NetworkingAPI.RegisterMessageType<SyncItems>();
            NetworkingAPI.RegisterMessageType<SyncSuicide>();
        }
    }
}
