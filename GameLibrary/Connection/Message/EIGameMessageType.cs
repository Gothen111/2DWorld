using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLibrary.Connection.Message
{
    public enum EIGameMessageType
    {
        UpdateChunkMessage,
        RequestRegionMessage,
        UpdateRegionMessage,
        RequestPlayerMessage,
        UpdatePlayerMessage,
        PlayerCommandMessage,
        UpdateLivingObjectMessage,
        UpdateObjectPositionMessage,
        UpdateObjectHealthMessage,
        RequestChunkMessage,
        RequestWorldMessage,
        UpdateWorldMessage,
        RequestLivingObjectMessage,
        RemoveObjectMessage,
        UpdateCreatureInventoryMessage
    }
}
