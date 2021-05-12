﻿using System;
using System.Collections.Generic;
using System.Linq;
using ActionCommandGame.Model;
using ActionCommandGame.Repository;
using ActionCommandGame.Services.Abstractions;
using ActionCommandGame.Services.Extensions;
using ActionCommandGame.Services.Model.Core;

namespace ActionCommandGame.Services
{
    public class PlayerItemService : IPlayerItemService
    {
        private readonly ActionButtonGameUiDbContext _database;

        public PlayerItemService(ActionButtonGameUiDbContext database)
        {
            _database = database;
        }

        public PlayerItem Get(Guid id)
        {
            return _database.PlayerItems.Find(id);
        }

        public IList<PlayerItem> Find(Guid? playerId = null)
        {
            var query = _database.PlayerItems.AsQueryable();

            if (playerId.HasValue)
            {
                query = query
                    .Where(pi => pi.PlayerId == playerId);

            }
            
            return query.ToList();
        }

        public ServiceResult<PlayerItem> Create(Guid playerId, Guid itemId)
        {
            var player = _database.Players.SingleOrDefault(p => p.Id == playerId);
            if (player == null)
            {
                return new ServiceResult<PlayerItem>().PlayerNotFound();
            }

            var item = _database.Items.SingleOrDefault(i => i.Id == itemId);
            if (item == null)
            {
                return new ServiceResult<PlayerItem>().ItemNotFound();
            }

            var playerItem = new PlayerItem
            {
                ItemId = itemId,
                Item = item,
                PlayerId = playerId,
                Player = player
            };
            _database.PlayerItems.Add(playerItem);
            player.Inventory.Add(playerItem);
            item.PlayerItems.Add(playerItem);

            //Auto Equip the item you bought
            if (item.Fuel > 0)
            {
                playerItem.RemainingFuel = item.Fuel;
                player.CurrentFuelPlayerItemId = playerItem.Id;
                player.CurrentFuelPlayerItem = playerItem;
            }
            if (item.Attack > 0)
            {
                playerItem.RemainingAttack = item.Attack;
                player.CurrentAttackPlayerItemId = playerItem.Id;
                player.CurrentAttackPlayerItem = playerItem;
            }
            if (item.Defense > 0)
            {
                playerItem.RemainingDefense = item.Defense;
                player.CurrentDefensePlayerItemId = playerItem.Id;
                player.CurrentDefensePlayerItem = playerItem;
            }

            _database.SaveChanges();

            return new ServiceResult<PlayerItem>(playerItem);
        }

        public PlayerItem Update(Guid id, PlayerItem playerItem)
        {
            var dbItem = _database.PlayerItems.Find(id);
            if (dbItem == null)
            {
                return null;
            }

            playerItem.Id = dbItem.Id;
            var updatedItem = _database.PlayerItems.Update(playerItem).Entity;
            _database.SaveChanges();
            return updatedItem;
        }

        public ServiceResult Delete(Guid id)
        {
            var playerItem = _database.PlayerItems.SingleOrDefault(pi => pi.Id == id);

            if (playerItem == null)
            {
                return new ServiceResult().NotFound();
            }
            
            var player = playerItem.Player;
            player.Inventory.Remove(playerItem);
            
            var item = playerItem.Item;
            item.PlayerItems.Remove(playerItem);

            //Clear up equipment
            if (player.CurrentFuelPlayerItemId == id)
            {
                player.CurrentFuelPlayerItemId = null;
                player.CurrentFuelPlayerItem = null;
            }
            if (player.CurrentAttackPlayerItemId == id)
            {
                player.CurrentAttackPlayerItemId = null;
                player.CurrentAttackPlayerItem = null;
            }
            if (player.CurrentDefensePlayerItemId == id)
            {
                player.CurrentDefensePlayerItemId = null;
                player.CurrentDefensePlayerItem = null;
            }

            _database.PlayerItems.Remove(playerItem);

            //Save Changes
            _database.SaveChanges();

            return new ServiceResult();
        }
        
    }
}
