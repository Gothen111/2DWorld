﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLibrary.Connection.Message
{
    public enum EIGameMessageType
    {
        UpdateChunkMessage,
        RequestPlayerMessage,
        UpdatePlayerMessage,
        PlayerCommandMessage,
        UpdateLivingObjectMessage
    }
}