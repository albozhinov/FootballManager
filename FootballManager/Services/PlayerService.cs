namespace FootballManager.Services
{
    using FootballManager.Data.Common;
    using FootballManager.Data.Models;
    using FootballManager.Services.Contracts;
    using FootballManager.ViewModels.Players;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PlayerService : IPlayerService
    {
        private readonly IValidatorService validatorService;
        private readonly IRepository repo;

        public PlayerService(IRepository _repository, IValidatorService _validatorService)
        {
            this.repo = _repository;
            this.validatorService = _validatorService;
        }

        public (bool isRegister, string errors) CreatePlayer(AddPlayerViewModel model)
        {
            bool isValid = true;
            string errorResult = null;

            (isValid, errorResult) = validatorService.ValidateModel(model);

            if (!isValid)
            {
                return (isValid, errorResult);
            }

            if (model.ImageUrl == null)
            {
                model.ImageUrl = " ";
            }
            
            
            var player = new Player()
            {
                FullName = model.FullName,
                ImageUrl = model.ImageUrl,
                Description = model.Description,
                Position = model.Position,
                Endurance = model.Endurance,
                Speed = model.Speed,
            };

            try
            {
                repo.Add(player);
                repo.SaveChanges();
            }
            catch (Exception)
            {
                errorResult = "Cloud not create player.";
                isValid = false;
            }

            return (isValid, errorResult);
        }

        public ICollection<AllPlayersViewModel> All()
        {
            var allPlayers = repo.All<Player>()
                                    .Select(p => new AllPlayersViewModel()
                                    {
                                       Id = p.Id,
                                       ImageUrl = p.ImageUrl,
                                       Speed = p.Speed,
                                       FullName = p.FullName,
                                       Position = p.Position,
                                       Description = p.Description,
                                       Endurance = p.Endurance
                                    }).ToList();

            return allPlayers;
        }       

        public ICollection<AllPlayersViewModel> Collection(string usedId)
        {
            var userPlayers = repo.All<UserPlayer>()
                                    .Where(up => up.UserId == usedId)
                                    .Include(p => p.Player)
                                    .Select(p => new AllPlayersViewModel()
                                    {
                                        Id = p.Player.Id,
                                        ImageUrl = p.Player.ImageUrl,
                                        Speed = p.Player.Speed,
                                        FullName = p.Player.FullName,
                                        Position = p.Player.Position,
                                        Description = p.Player.Description,
                                        Endurance = p.Player.Endurance
                                    }).ToList();

            return userPlayers;
        }

        public void RemoveFromCollection(string userId, int playerId)
        {
            var user = repo.All<User>()
                                    .Where(u => u.Id == userId)
                                    .Include(u => u.Players)
                                    .FirstOrDefault();

            var player = repo.All<UserPlayer>().Where(p => p.PlayerId == playerId).FirstOrDefault();

            user.Players.Remove(player);

            repo.SaveChanges();

            return;
        }

        public void AddToCollection(string userId, int playerId)
        {
            var isExists = repo.All<UserPlayer>().FirstOrDefault(up => up.UserId == userId && up.PlayerId == playerId);

            if (isExists != null)
            {
                return;
            }
            var user = repo.All<User>()
                                    .Where(u => u.Id == userId)
                                    .Include(u => u.Players)
                                    .ThenInclude(p => p.Player)
                                    .FirstOrDefault();

            var player = repo.All<Player>().Where(p => p.Id == playerId).FirstOrDefault();

            var userPlayer = new UserPlayer()
            {
                User = user,
                Player = player,
                PlayerId = player.Id,
                UserId = user.Id
            };            

            user.Players.Add(userPlayer);

            repo.SaveChanges();
            return;
        }
    }
}
