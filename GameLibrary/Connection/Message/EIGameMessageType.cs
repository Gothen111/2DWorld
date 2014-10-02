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
        RequestBlockMessage,
        RequestChunkMessage,
        RequestLivingObjectMessage,
        RequestPlayerMessage,
        RequestRegionMessage,
        RequestWorldMessage,
        UpdateAnimatedObjectBodyMessage,
        UpdateBlockMessage,
        UpdateChunkMessage,
        UpdateCreatureInventoryMessage,
        UpdateFactionsMessage,
        UpdateObjectMessage,
        UpdateObjectMovementMessage,
        UpdateObjectHealthMessage,
        UpdateObjectPositionMessage,
        UpdatePlayerMessage,
        UpdateRacesMessage,
        UpdateRegionMessage,
        UpdateWorldMessage,
    }
}
