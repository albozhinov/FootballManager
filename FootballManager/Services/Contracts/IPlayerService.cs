namespace FootballManager.Services.Contracts
{
    using FootballManager.ViewModels.Players;
    using System.Collections.Generic;

    public interface IPlayerService
    {
        (bool isRegister, string errors) CreatePlayer(AddPlayerViewModel model);

        ICollection<AllPlayersViewModel> All();

        ICollection<AllPlayersViewModel> Collection(string userId);

        void RemoveFromCollection(string userId, int playerId);

        void AddToCollection(string userId, int playerId);
    }
}
