using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLibrary.Connection.Message
{
    public enum EIGameMessageType
    {
        CreatureEquipmentToInventoryMessage,
        CreatureInventoryItemPositionChangeMessage,
        CreatureInventoryToEquipmentMessage,
        PlayerCommandMessage,
        RemoveObjectMessage,
        RequestChunkMessage,
        RequestLivingObjectMessage,
        RequestPlayerMessage,
        RequestRegionMessage,
        RequestWorldMessage,
        UpdateAnimatedObjectBodyMessage,
        UpdateBehaviourMessage,
        UpdateChunkMessage,
        UpdateCreatureInventoryMessage,
        UpdateObjectHealthMessage,
        UpdateObjectPositionMessage,
        UpdatePlayerMessage,
        UpdateRegionMessage,
        UpdateWorldMessage,
    }
}
