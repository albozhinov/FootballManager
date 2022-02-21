namespace FootballManager.Controllers
{
    using BasicWebServer.Server.Attributes;
    using BasicWebServer.Server.Controllers;
    using BasicWebServer.Server.HTTP;
    using FootballManager.Services.Contracts;
    using FootballManager.ViewModels.Players;

    public class PlayersController : Controller
    {
        private readonly IPlayerService playerService;
        public PlayersController(Request request,IPlayerService _playerService)
            : base(request)
        {
            playerService = _playerService;
        }

        [Authorize]
        public Response Add()
        {
            return View(new { IsAuthenticated = true });
        }

        [Authorize]
        [HttpPost]
        public Response Add(AddPlayerViewModel model)
        {
            model.UserId = User.Id;
            var (isAdded, errors) = playerService.CreatePlayer(model);

            if (!isAdded)
            {
                return View(new { IsAuthenticated = true });
            }

            return Redirect("/Players/All");
        }

        [Authorize]
        public Response Collection()
        {
            var userCollection = playerService.Collection(User.Id);

            var model = new
            {
                IsAuthenticated = true,
                Players = userCollection
            };

            return View(model);
        }

        [Authorize]
        public Response AddToCollection(int playerId)
        {
            playerService.AddToCollection(User.Id, playerId);

            return Redirect("/Players/All");
        }

        [Authorize]
        public Response RemoveFromCollection(int playerId)
        {
            playerService.RemoveFromCollection(User.Id, playerId);

            return Redirect("/Players/Collection");
        }

        [Authorize]
        public Response All()
        {
            var allPlayers = playerService.All();
            var model = new 
            { 
                IsAuthenticated = true,
                Players = allPlayers
            };

            return View(model);
        }
    }
}
