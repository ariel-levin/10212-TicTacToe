using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;


public class TTT : ITTT
{
    private const int SERVER = 1;

    private static Dictionary<ICallBack, PlayerData> players = new Dictionary<ICallBack, PlayerData>();
    private static Dictionary<ICallBack, Board> players_boards = new Dictionary<ICallBack, Board>();
    private static Board[] boards = new Board[5];


    public void getRegisterFormAdvisorList()
    {
        ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();
        channel.sendRegisterFormAdvisorList(getAllFreeAdvisors());
    }

    public void registerNewPlayer(PlayerData player, int[] advisors)
    {
        ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();

        using (var db = new TTTDataClassesDataContext())
        {
            using (SqlConnection con = new SqlConnection(db.Connection.ConnectionString))
            {
                string sql = string.Format("Insert into Players(FirstName, LastName, City, Country, Phone, IsAdvisor) "
                    + "values('{0}', '{1}', '{2}', '{3}', '{4}', {5})", player.FirstName, player.LastName, player.City,
                    player.Country, player.Phone, player.IsAdvisor);

                SqlCommand cmd = new SqlCommand(sql, con);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();

                    if (advisors != null)
                    {
                        player.Id = (from p in db.Players
                                     select p.Id).Max();

                        for (var i = 0; i < advisors.Length; i++)
                        {
                            cmd.CommandText = string.Format("update Players set AdviseTo = {0} where Id = {1}", player.Id, advisors[i]);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    con.Close();
                    channel.showPlayerRegisterSuccess();
                }
                catch (Exception e)
                {
                    channel.showException(e);
                }
            }

        }
    }

    public void registerNewChampionship(ChampionshipData champ)
    {
        ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();

        using (var db = new TTTDataClassesDataContext())
        {
            using (SqlConnection con = new SqlConnection(db.Connection.ConnectionString))
            {
                string sql;
                
                if (champ.EndDate == null)
                {
                    sql = string.Format("Insert into Championships(City, StartDate, Picture) "
                     + "values('{0}', '{1}', '{2}')", champ.City, champ.StartDate, champ.Picture);
                }
                else
                {
                    sql = string.Format("Insert into Championships(City, StartDate, EndDate, Picture) "
                     + "values('{0}', '{1}', '{2}', '{3}')", champ.City, champ.StartDate, champ.EndDate, champ.Picture);
                }
                
                SqlCommand cmd = new SqlCommand(sql, con);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    channel.showNewChampSuccess();
                }
                catch (Exception e)
                {
                    channel.showException(e);
                }
            }

        }
    }

    public void getAllUsers(string caller)
    {
        ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();
        channel.sendPlayers(getAllPlayersFromDB(), caller);
    }

    public void login(PlayerData user)
    {
        ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();

        if (players.ContainsKey(channel))
        {
            channel.loginError("You are already connected, please logout first", user);
        }
        else
        {
            if (isUserAlreadyLogged(user))
            {
                channel.loginError("The following user is already connected, please select another", user);
            }
            else
            {
                players.Add(channel, user);
                channel.loginSuccess(user);
            }
        }
    }

    public void logout(bool waitingForResponse)
    {
        ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();

        if (players.ContainsKey(channel) )
        {
            players.Remove(channel);
            if (waitingForResponse)
                channel.logoutSuccess();
        }
        else
        {
            if (waitingForResponse)
                channel.logoutError("User is not logged in");
        }
    }

    public void wake()
    {
        ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();
        channel.response();
    }

    public void getAllChampionships(int playerId, string caller)
    {
        ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();
        channel.sendChampionships(getAllChampionships(playerId), caller);
    }

    public void registerPlayerToChamp(PlayerData player, ChampionshipData[] chmps)
    {
        ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();

        if (player == null || chmps == null)
        {
            channel.registerPlayerToChampError("Error: data is corrupted");
            return;
        }

        using (var db = new TTTDataClassesDataContext())
        {
            using (SqlConnection con = new SqlConnection(db.Connection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("", con);
                try
                {
                    con.Open();

                    for (var i = 0; i < chmps.Length; i++)
                    {
                        if (!isPlayerRegisteredToChamp(player, chmps[i], db))
                        {
                            cmd.CommandText = string.Format("Insert into PlayerChampionships(PlayerId, ChampionshipId) "
                                + "values({0}, {1})", player.Id, chmps[i].Id);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    con.Close();
                    channel.registerPlayerToChampSuccess();
                }
                catch (Exception e)
                {
                    channel.showException(e);
                }
            }

        }
    }

    public void startGameRequest(int dim, bool singlePlayer)
    {
        ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();

        if (!players.ContainsKey(channel))
        {
            channel.gameError("Something went wrong...\nYou are not logged in on the server\n"
                + "Please try to logout and login again");
            return;
        }

        if (!singlePlayer)
        {
            Board b = tryFindOpponent(dim);
            if (b != null)
            {
                b.addPlayer(channel, dim);
                players_boards.Add(channel, b);
                channel.addedSuccessfully(false);
                b.startMultiplayerGame();
                return;
            }
        }

        int index = findAvailableBoard();
        if (index == -1)
        {
            channel.gameEnded("Sorry, all boards are taken at the moment..");
        }
        else
        {
            boards[index] = new Board(dim, singlePlayer, channel, this);
            players_boards.Add(channel, boards[index]);
            channel.addedSuccessfully(true);
            if (singlePlayer)
                channel.startGame(true);
        }
    }

    public void playerPressed(int row, int col)
    {
        ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();

        if (!players_boards.ContainsKey(channel))
        {
            channel.gameError("Something went wrong...\nCouldn't find player's board");
            return;
        }

        players_boards[channel].playerPressed(channel, row, col);
    }

    public void playerExitGame()
    {
        ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();

        if (players_boards.ContainsKey(channel))
        {
            players_boards[channel].playerExit(channel);
            players_boards.Remove(channel);
        }
    }

    public PlayerData getPlayerData(ICallBack channel)
    {
        if (players.ContainsKey(channel))
            return players[channel];
        else
            return null;
    }

    public void insertGameToDB(Game game)
    {
        using (var db = new TTTDataClassesDataContext())
        {
            using (SqlConnection con = new SqlConnection(db.Connection.ConnectionString))
            {
                string sql = "";
                if (game.Winner.HasValue)
                    sql = string.Format("Insert into Games(Player1, Player2, Winner, BoardSize, Moves, StartTime, EndTime) "
                         + "values({0}, {1}, {2}, {3}, '{4}', '{5}', '{6}')", game.Player1, game.Player2, game.Winner,
                         game.BoardSize, game.Moves, game.StartTime, game.EndTime);
                else
                    sql = string.Format("Insert into Games(Player1, Player2, Moves, StartTime, EndTime) "
                         + "values({0}, {1}, {2}, '{3}', '{4}', '{5}')", game.Player1, game.Player2, game.BoardSize,
                         game.Moves, game.StartTime, game.EndTime);

                SqlCommand cmd = new SqlCommand(sql, con);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception) { }
            }
        }
    }

    public void getAllGames(bool withPlayersNames, int playerId, string caller)
    {
        ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();
        channel.sendGames(getAllGamesFromDB(withPlayersNames, playerId), caller);
    }

    public void getGamePlayers(GameData game)
    {
        ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();

        using (var db = new TTTDataClassesDataContext())
        {
            PlayerData[] players = new PlayerData[2];
            players[0] = getPlayerDataById(game.Player1, db);
            players[1] = getPlayerDataById(game.Player2, db);
            channel.sendPlayers(players, "Q");
        }
    }

    public void getGameAdvisors(GameData game)
    {
        ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();

        using (var db = new TTTDataClassesDataContext())
        {
            PlayerData player1 = getPlayerDataById(game.Player1, db);
            PlayerData player2 = getPlayerDataById(game.Player2, db);
            var a1 = getPlayerAdvisors(player1.Id, db);
            var a2 = getPlayerAdvisors(player2.Id, db);
            PlayerData[] advisors = new PlayerData[a1.Count() + a2.Count()];
            int i = 0;
            foreach (var a in a1)
            {
                advisors[i] = getPlayerData(a);
                advisors[i++].AdviseToName = player1.Id + " : " + player1.FirstName;
            }
            foreach (var a in a2)
            {
                advisors[i] = getPlayerData(a);
                advisors[i++].AdviseToName = player2.Id + " : " + player2.FirstName;
            }
            channel.sendGameAdvisors(advisors);
        }
    }

    public void getChampionshipPlayers(ChampionshipData chmp)
    {
        ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();

        using (var db = new TTTDataClassesDataContext())
        {
            var x = db.PlayerChampionships.Where(pc => pc.ChampionshipId == chmp.Id);
            PlayerData[] players = new PlayerData[x.Count()];
            int i = 0;
            foreach (var pc in x)
            {
                players[i++] = getPlayerDataById(pc.PlayerId, db);
            }
            channel.sendPlayers(players, "Q");
        }
    }

    public void getPlayersGamesNum()
    {
        ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();

        using (var db = new TTTDataClassesDataContext())
        {
            PlayerGames[] playersGames = new PlayerGames[db.Players.Count()-1];
            int i = 0;
            foreach (var p in db.Players)
            {
                if (p.Id != SERVER)
                {
                    playersGames[i] = new PlayerGames();
                    playersGames[i].Name = p.Id + " : " + p.FirstName;
                    var x = db.Games.Where(g => g.Player1 == p.Id || g.Player2 == p.Id);
                    playersGames[i++].NumberOfGames = x.Count();
                }
            }
            channel.sendPlayersGamesNum(playersGames);
        }
    }

    public void getCitiesChampionshipsNum()
    {
        ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();

        using (var db = new TTTDataClassesDataContext())
        {
            var x = db.Championships.GroupBy(c => c.City);
            CityChampionships[] citiesChmps = new CityChampionships[x.Count()];
            int i = 0;
            foreach (var cc in x)
            {
                citiesChmps[i] = new CityChampionships();
                citiesChmps[i].City = cc.Key;
                citiesChmps[i++].NumberOfChampionships = cc.Count();
            }
            channel.sendCitiesChampionshipsNum(citiesChmps);
        }
    }


    /////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////


    private PlayerData[] getAllFreeAdvisors()
    {
        using (var db = new TTTDataClassesDataContext())
        {
            var x = db.Players.Where(p => p.IsAdvisor == 1 && !p.AdviseTo.HasValue);
            PlayerData[] players = new PlayerData[x.Count()];
            int i = 0;
            foreach (var p in x)
            {
                players[i++] = getPlayerData(p);
            }
            return players;
        }
    }

    private PlayerData getPlayerData(Player p)
    {
        PlayerData player = new PlayerData();
        player.Id = p.Id;
        player.FirstName = p.FirstName;
        player.LastName = p.LastName;
        player.City = p.City;
        player.Country = p.Country;
        player.Phone = p.Phone;
        player.IsAdvisor = p.IsAdvisor;
        if (p.AdviseTo.HasValue)
            player.AdviseTo = p.AdviseTo.Value;
        return player;
    }

    private GameData getGameData(Game g)
    {
        GameData game = new GameData();
        game.Id = g.Id;
        game.Player1 = g.Player1;
        game.Player2 = g.Player2;
        if (g.Winner.HasValue)
            game.Winner = g.Winner;
        game.BoardSize = g.BoardSize;
        game.Moves = g.Moves;
        game.StartTime = g.StartTime;
        game.EndTime = g.EndTime;
        return game;
    }

    private ChampionshipData[] getAllChampionships(int playerId = -1)
    {
        using (var db = new TTTDataClassesDataContext())
        {
            IQueryable<Championship> x = null;

            if (playerId == -1)
                x = db.Championships;
            else
            {
                var y = db.PlayerChampionships.Where(pc => pc.PlayerId == playerId);
                x = db.Championships.Where(c => y.Any(pc => pc.ChampionshipId == c.Id));
            }

            ChampionshipData[] chmps = new ChampionshipData[x.Count()];
            int i = 0;
            foreach (var c in x)
            {
                chmps[i++] = getChampionshipData(c);
            }
            return chmps;
        }
    }

    private ChampionshipData getChampionshipData(Championship c)
    {
        ChampionshipData champ = new ChampionshipData();
        champ.Id = c.Id;
        champ.City = c.City;
        champ.StartDate = c.StartDate;
        if (c.EndDate.HasValue)
            champ.EndDate = c.EndDate;
        champ.Picture = c.Picture;
        return champ;
    }

    private PlayerData[] getAllPlayersFromDB()
    {
        using (var db = new TTTDataClassesDataContext())
        {
            var x = db.Players.Where(p => p.Id != SERVER);
            PlayerData[] players = new PlayerData[x.Count()];
            int i = 0;
            foreach (var p in x)
            {
                players[i++] = getPlayerData(p);
            }
            return players;
        }
    }

    private GameData[] getAllGamesFromDB(bool withPlayersNames, int playerId = -1)
    {
        using (var db = new TTTDataClassesDataContext())
        {
            IQueryable<Game> x = null;

            if (playerId == -1)
                x = db.Games;
            else
                x = db.Games.Where(g => g.Player1 == playerId || g.Player2 == playerId);

            GameData[] games = new GameData[x.Count()];
            int i = 0;
            foreach (var g in x)
            {
                games[i] = getGameData(g);

                if (withPlayersNames)
                {
                    PlayerData p = getPlayerDataById(games[i].Player1, db);
                    games[i].Player1_Name = p.Id + " : " + p.FirstName;
                    p = getPlayerDataById(games[i].Player2, db);
                    games[i].Player2_Name = p.Id + " : " + p.FirstName;
                    if (g.Winner.HasValue)
                    {
                        p = getPlayerDataById((int)games[i].Winner, db);
                        games[i].Winner_Name = p.Id + " : " + p.FirstName;
                    }
                }
                i++;
            }
            return games;
        }
    }

    private bool isUserAlreadyLogged(PlayerData user) 
    {
        return players.Values.Any(p => p.Id == user.Id);
    }

    private bool isPlayerRegisteredToChamp(PlayerData player, ChampionshipData champ, TTTDataClassesDataContext db)
    {
        var x =
            from pc in db.PlayerChampionships
            where pc.PlayerId == player.Id && pc.ChampionshipId == champ.Id
            select pc;

        return x.Count() > 0;
    }

    private Board tryFindOpponent(int dim)
    {
        foreach (var b in boards)
        {
            if (b != null && b.isWaitingForPlayer2() && dim == b.getDimension())
                return b;
        }
        return null;
    }

    private int findAvailableBoard()
    {
        for (var i = 0; i < boards.Length; i++)
        {
            if (boards[i] == null || boards[i].isGameEnded())
                return i;
        }

        return -1;
    }

    private PlayerData getPlayerDataById(int id, TTTDataClassesDataContext db)
    {
        Player player = db.Players.Where(p => p.Id == id).First();
        return getPlayerData(player);
    }

    private IQueryable<Player> getPlayerAdvisors(int id, TTTDataClassesDataContext db)
    {
        return db.Players.Where(p => p.AdviseTo == id);
    }

}
