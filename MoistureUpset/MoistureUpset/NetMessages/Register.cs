﻿using R2API.Networking;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoistureUpset.NetMessages
{
    public static class Register
    {
        public static void Init()
        {
            NetworkingAPI.RegisterMessageType<SyncAnimationToServer>();
            NetworkingAPI.RegisterMessageType<SyncAnimationToClients>();

            NetworkingAPI.RegisterMessageType<InteractReplacements.SyncFidget>();

            NetworkingAPI.RegisterMessageType<SyncAudio>();
            NetworkingAPI.RegisterMessageType<SyncAudioWithJotaroSubtitles>();
        }
    }
}
