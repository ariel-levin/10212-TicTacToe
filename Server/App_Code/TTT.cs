/*********************************  
 *  Ariel Levin
 *  ariel.lvn89@gmail.com
 *  http://about.me/ariel.levin
 *********************************/

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


public class TTT : ITTT
{
    private const int SERVER = 1;           // the server's player id
    private const int SLEEP_TIME = 3000;    // sleep time for queries delay
    private const int NUM_OF_BOARDS = 5;    // number of boards available simultaneously

    // players mapped by callback channel
    private static Dictionary<ICallBack, PlayerData> players = new Dictionary<ICallBack, PlayerData>();
    
    // boards mapped by callback channel
    private static Dictionary<ICallBack, Board> players_boards = new Dictionary<ICallBack, Board>();

    // boards that the server offers
    private static Board[] boards = new Board[NUM_OF_BOARDS];



    /* request advisors list from database */
    public void getAdvisorList()
    {
        ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();
        channel.sendRegisterFormAdvisorList(getAllFreeAdvisors());
    }

    /* register new player to database */
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

    /* register new championship to database */
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

    /* request all users from database */
    public async void getAllUsers(string caller, bool delay)
    {
        ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();

        if (delay)
            await Task<int>.Factory.StartNew(sleep);

        channel.sendPlayers(getAllPlayersFromDB(), caller);
    }

    /* login a player */
    public void login(PlayerData user)
    {
        ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();

        if (players.ContainsKey(channel))
        {
            channel.loginError("You are already connected, please logout first", user);
        }
        else
        {
            if (isUserLogged(user.Id))
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

    /* logout a player */
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

    /* flase method to wake the server at the beginning of the program */
    public void wake()
    {
        ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();
        channel.response();
    }

    /* request all championships from database for a client query */
    public async void getAllChampionships(int playerId, string caller, bool delay)
    {
        ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();

        if (delay)
            await Task<int>.Factory.StartNew(sleep);

        channel.sendChampionships(getAllChampionships(playerId), caller);
    }

    /* register a player to championship(s) */
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

    /* player request to start a singleplayer / multiplayer game */
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
            Board b = tryFindOpponent(dim, channel);
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

    /* inform the server of a player move */
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

    /* inform the server that a player left the game */
    public void playerExitGame()
    {
        ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();

        if (players_boards.ContainsKey(channel))
        {
            players_boards[channel].playerExit(channel);
            players_boards.Remove(channel);
        }
    }

    /* get player data of the requesting player */
    public PlayerData getPlayerData(ICallBack channel)
    {
        if (players.ContainsKey(channel))
            return players[channel];
        else
            return null;
    }

    /* request to insert a game to database */
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
                    sql = string.Format("Insert into Games(Player1, Player2, BoardSize, Moves, StartTime, EndTime) "
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

    /* request all games from database for a client query */
    public async void getAllGames(bool withPlayersNames, int playerId, string caller, bool delay)
    {
        ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();

        if (delay)
            await Task<int>.Factory.StartNew(sleep);

        channel.sendGames(getAllGamesFromDB(withPlayersNames, playerId), caller);
    }

    /* request all players of a specific game from database for a client query */
    public async void getGamePlayers(GameData game, bool delay)
    {
        ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();

        if (delay)
            await Task<int>.Factory.StartNew(sleep);

        using (var db = new TTTDataClassesDataContext())
        {
            PlayerData[] players = new PlayerData[2];
            players[0] = getPlayerDataById(game.Player1, db);
            players[1] = getPlayerDataById(game.Player2, db);
            channel.sendPlayers(players, "Q");
        }
    }

    /* request all advisors of a specific game from database for a client query */
    public void getGameAdvisors(GameData game, bool delay)
    {
        ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();

        if (delay)
        {
            ManualResetEvent delayEvent = new ManualResetEvent(false);
            ThreadPool.QueueUserWorkItem(new WaitCallback((_) =>
            {
                sleep();
                delayEvent.Set();
            }));
            delayEvent.WaitOne();
        }

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
                advisors[i] = getPlayerData(a, db);
                advisors[i++].AdviseTo_Name = player1.Id + " : " + player1.FirstName;
            }
            foreach (var a in a2)
            {
                advisors[i] = getPlayerData(a, db);
                advisors[i++].AdviseTo_Name = player2.Id + " : " + player2.FirstName;
            }
            channel.sendGameAdvisors(advisors);
        }
    }

    /* request all players of a specific championship from database for a client query */
    public void getChampionshipPlayers(ChampionshipData chmp, bool delay)
    {
        ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();

        if (delay)
        {
            ManualResetEvent delayEvent = new ManualResetEvent(false);
            ThreadPool.QueueUserWorkItem(new WaitCallback((_) =>
            {
                sleep();
                delayEvent.Set();
            }));
            delayEvent.WaitOne();
        }

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

    /* request the number of games for each player in database for a client query */
    public void getPlayersGamesNum(bool delay)
    {
        ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();

        if (delay)
        {
            ManualResetEvent delayEvent = new ManualResetEvent(false);
            ThreadPool.QueueUserWorkItem(new WaitCallback((_) =>
            {
                sleep();
                delayEvent.Set();
            }));
            delayEvent.WaitOne();
        }

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

    /* request the number of championships for each city in database for a client query */
    public void getCitiesChampionshipsNum(bool delay)
    {
        ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();

        if (delay)
        {
            ManualResetEvent delayEvent = new ManualResetEvent(false);
            ThreadPool.QueueUserWorkItem(new WaitCallback((_) =>
            {
                sleep();
                delayEvent.Set();
            }));
            delayEvent.WaitOne();
        }

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

    /* update the players database with the changes received in the array */
    public void updatePlayers(PlayerData[] players)
    {
        ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();

        bool allAdviseToChangesSuccess = true;
        bool userLoggedIn = false;

        using (var db = new TTTDataClassesDataContext())
        {
            using (SqlConnection con = new SqlConnection(db.Connection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("", con);
                try
                {
                    con.Open();

                    foreach (var p in players)
                    {
                        userLoggedIn = isUserLogged(p.Id);

                        if (!userLoggedIn)
                        {
                            string sql = "";
                            p.IsAdvisor = (p.Is_Advisor.Equals("Yes")) ? (byte)1 : (byte)0;

                            if (p.IsAdvisor == 1) 
                            {
                                bool adviseToChanged = isAdviseToChanged(p, db);
                                bool updateAdviseTo = adviseToChanged;

                                if (adviseToChanged)
                                {
                                    if (p.AdviseTo_Name.Equals(""))
                                    {
                                        sql = string.Format("Update Players SET FirstName='{0}', "
                                            + "LastName='{1}', City='{2}', Country='{3}', Phone='{4}', IsAdvisor={5}, AdviseTo=NULL "
                                            + "where Id={6}", p.FirstName, p.LastName, p.City, p.Country, p.Phone, p.IsAdvisor, p.Id);
                                    }
                                    else
                                    {
                                        try
                                        {
                                            int id = int.Parse(p.AdviseTo_Name);
                                            p.AdviseTo = id;
                                        }
                                        catch (Exception)
                                        {
                                            allAdviseToChangesSuccess = false;
                                            updateAdviseTo = false;
                                        }

                                        if (updateAdviseTo)
                                        {
                                            sql = string.Format("Update Players SET FirstName='{0}', "
                                                + "LastName='{1}', City='{2}', Country='{3}', Phone='{4}', IsAdvisor={5}, AdviseTo={6} "
                                                + "where Id={7}", p.FirstName, p.LastName, p.City, p.Country, p.Phone, p.IsAdvisor, p.AdviseTo, p.Id);
                                        }
                                    }
                                }
                            
                                if (!adviseToChanged || sql.Equals(""))
                                {
                                    sql = string.Format("Update Players SET FirstName='{0}', "
                                        + "LastName='{1}', City='{2}', Country='{3}', Phone='{4}', IsAdvisor={5} "
                                        + "where Id={6}", p.FirstName, p.LastName, p.City, p.Country, p.Phone, p.IsAdvisor, p.Id);
                                }
                            }
                            else
                            {
                                sql = string.Format("Update Players SET FirstName='{0}', "
                                    + "LastName='{1}', City='{2}', Country='{3}', Phone='{4}', IsAdvisor={5}, AdviseTo=NULL "
                                    + "where Id={6}", p.FirstName, p.LastName, p.City, p.Country, p.Phone, p.IsAdvisor, p.Id);
                            }

                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                        }
                    }

                    con.Close();
                }
                catch (SqlException)
                {
                    channel.updateError("Database updated partially with some errors\nPerhaps"
                        + " one or more 'AdviseTo' references didn't match a Player Id");
                }
                catch (Exception)
                {
                    channel.updateError("Some error occured while updating the database");
                }
            }
        }
        if (!allAdviseToChangesSuccess)
            channel.updateError("Database updated, but one or more 'AdviseTo' references that didn't match a Player Id");

        if (userLoggedIn)
            channel.updateError("Can't update a logged in user");

        if (!userLoggedIn && allAdviseToChangesSuccess)
            channel.updateSuccess();
    }

    /* update the championships database with the changes received in the array */
    public void updateChampionships(ChampionshipData[] chmps)
    {
        ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();

        using (var db = new TTTDataClassesDataContext())
        {
            using (SqlConnection con = new SqlConnection(db.Connection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("", con);
                try
                {
                    con.Open();

                    foreach (var c in chmps)
                    {
                        string sql = "Update Championships SET City='" + c.City + "', StartDate='" + c.StartDate + "'";

                        if (c.EndDate != null)
                            sql += ", EndDate='" + c.EndDate + "'";
                        else
                            sql += ", EndDate=NULL";

                        if (c.Picture != null && c.Picture.Replace(" ", String.Empty).Length > 0)
                            sql += ", Picture='" + c.Picture + "'";
                        else
                            sql += ", Picture=NULL";

                        sql += " where Id=" + c.Id;
                                
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                    }

                    con.Close();

                    channel.updateSuccess();
                }
                catch (Exception)
                {
                    channel.updateError("Some error occured while updating the database");
                }
            }
        }
    }

    /* delete the specific player from database */
    public void deletePlayer(PlayerData player)
    {
        ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();

        if (isUserLogged(player.Id))
        {
            channel.updateError("Can't delete an online user");
            return;
        }

        using (var db = new TTTDataClassesDataContext())
        {
            using (SqlConnection con = new SqlConnection(db.Connection.ConnectionString))
            {
                try
                {
                    bool success = deletePlayerDependencies(player.Id, db, con);

                    if (!success)
                        throw new Exception();

                    string sql = string.Format("delete from Players where Id={0}", player.Id);
                    SqlCommand cmd = new SqlCommand(sql, con);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    channel.updateSuccess();
                }
                catch (Exception)
                {
                    channel.updateError("Some error occured while deleting from database");
                }
            }
        }
    }

    /* delete the players from database that matches the specific value in the specific column */
    public void deletePlayers(PlayerData player, string title, string value)
    {
        ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();

        List<int> playerIds = getPlayerMatches(player, title, value);

        if (playerIds == null || playerIds.Count() < 1)
        {
            channel.updateError("There's no players with value '" + value + "' in field '" + title + "'");
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
                    int loggedInCount = 0;
                    foreach (var id in playerIds)
                    {
                        if (isUserLogged(id))
                        {
                            channel.updateError("Can't delete an online user [id=" + id + "]");
                            loggedInCount++;
                        }
                        else
                        {
                            bool success = deletePlayerDependencies(id, db, con);
                            if (success)
                            {
                                cmd.CommandText = string.Format("delete from Players where Id={0}", id);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    con.Close();
                    channel.updateSuccess(playerIds.Count() - loggedInCount + " players deleted");
                }
                catch (Exception)
                {
                    channel.updateError("Some error occured while deleting from database");
                }
            }
        }
    }

    /* delete the specific championship from database */
    public void deleteChampionship(ChampionshipData chmp)
    {
        ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();

        using (var db = new TTTDataClassesDataContext())
        {
            using (SqlConnection con = new SqlConnection(db.Connection.ConnectionString))
            {
                try
                {
                    bool success = deleteChampionshipDependencies(chmp.Id, db, con);

                    if (!success)
                        throw new Exception();

                    string sql = string.Format("delete from Championships where Id={0}", chmp.Id); ;
                    SqlCommand cmd = new SqlCommand(sql, con);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    channel.updateSuccess();
                }
                catch (Exception)
                {
                    channel.updateError("Some error occured while deleting from database");
                }
            }
        }
    }

    /* delete the championships from database that matches the specific value in the specific column */
    public void deleteChampionships(string title, string value)
    {
        ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();

        List<int> chmpsIds = getChampionshipMatches(title, value);

        if (chmpsIds == null || chmpsIds.Count() < 1)
        {
            channel.updateError("There's no championships with value '" + value + "' in field '" + title + "'");
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

                    foreach (var id in chmpsIds)
                    {
                        bool success = deleteChampionshipDependencies(id, db, con);
                        if (success)
                        {
                            cmd.CommandText = string.Format("delete from Championships where Id={0}", id);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    con.Close();
                    channel.updateSuccess(chmpsIds.Count() + " championships deleted");
                }
                catch (Exception)
                {
                    channel.updateError("Some error occured while deleting from database");
                }
            }
        }
    }


    /////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////


    #region Private Methods

    private PlayerData[] getAllFreeAdvisors()
    {
        using (var db = new TTTDataClassesDataContext())
        {
            var x = db.Players.Where(p => p.IsAdvisor == 1 && !p.AdviseTo.HasValue);
            PlayerData[] players = new PlayerData[x.Count()];
            int i = 0;
            foreach (var p in x)
            {
                players[i++] = getPlayerData(p, db);
            }
            return players;
        }
    }

    private PlayerData getPlayerData(Player p, TTTDataClassesDataContext db)
    {
        PlayerData player = new PlayerData();
        player.Id = p.Id;
        player.FirstName = p.FirstName;
        player.LastName = p.LastName;
        player.City = p.City;
        player.Country = p.Country;
        player.Phone = p.Phone;
        player.IsAdvisor = p.IsAdvisor;
        player.Is_Advisor = (p.IsAdvisor == 1) ? "Yes" : "No";
        if (p.AdviseTo.HasValue)
        {
            player.AdviseTo = p.AdviseTo.Value;
            Player adviseTo = db.Players.Where(pl => pl.Id == player.AdviseTo).First();
            player.AdviseTo_Name = adviseTo.Id + " : " + adviseTo.FirstName;
        }
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
            IEnumerable<Championship> x = null;

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
                players[i++] = getPlayerData(p, db);
            }
            return players;
        }
    }

    private GameData[] getAllGamesFromDB(bool withPlayersNames, int playerId = -1)
    {
        using (var db = new TTTDataClassesDataContext())
        {
            IEnumerable<Game> x = null;

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

    private bool isUserLogged(int id) 
    {
        return players.Values.Any(p => p.Id == id);
    }

    private bool isPlayerRegisteredToChamp(PlayerData player, ChampionshipData champ, TTTDataClassesDataContext db)
    {
        var x =
            from pc in db.PlayerChampionships
            where pc.PlayerId == player.Id && pc.ChampionshipId == champ.Id
            select pc;

        return x.Count() > 0;
    }

    private Board tryFindOpponent(int dim, ICallBack player)
    {
        foreach (var b in boards)
        {
            if (b != null && !b.isGameEnded() && dim == b.getDimension() && b.isWaitingForPlayer2())
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

    private PlayerData getPlayerDataById(System.Nullable<int> id, TTTDataClassesDataContext db)
    {
        if (!id.HasValue || id < 1)
            return null;
        var x = db.Players.Where(p => p.Id == id.Value);
        if (x.Count() < 1)
            return null;
        else
            return getPlayerData(x.First(), db);
    }

    private IEnumerable<Player> getPlayerAdvisors(int id, TTTDataClassesDataContext db)
    {
        return db.Players.Where(p => p.AdviseTo == id);
    }

    private bool isAdviseToChanged(PlayerData player, TTTDataClassesDataContext db)
    {
        PlayerData adviseTo = getPlayerDataById(player.AdviseTo, db);
        if (adviseTo == null)
        {
            if (player.AdviseTo_Name != null && !player.AdviseTo_Name.Equals(""))
                return true;
        }
        else
        {
            if (!player.AdviseTo_Name.Equals(adviseTo.Id + " : " + adviseTo.FirstName))
                return true;
        }
        return false;
    }

    private bool deletePlayerDependencies(int playerId, TTTDataClassesDataContext db, SqlConnection con)
    {
        if (removePlayerAdvisors(playerId, db, con)
                && deletePlayerGames(playerId, db, con)
                    && deletePlayerChampionships(playerId, db, con))
            return true;
        else
            return false;
    }

    private bool removePlayerAdvisors(int playerId, TTTDataClassesDataContext db, SqlConnection con)
    {
        String sql = string.Format("Update Players SET AdviseTo=NULL where AdviseTo={0}", playerId);
        SqlCommand cmd = new SqlCommand(sql, con);
        try
        {
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private bool deletePlayerGames(int playerId, TTTDataClassesDataContext db, SqlConnection con)
    {
        string sql = string.Format("delete from Games where Player1={0} or Player2={0} or Winner={0}", playerId);
        SqlCommand cmd = new SqlCommand(sql, con);
        try
        {
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private bool deletePlayerChampionships(int playerId, TTTDataClassesDataContext db, SqlConnection con)
    {
        string sql = string.Format("delete from PlayerChampionships where PlayerId={0}", playerId);
        SqlCommand cmd = new SqlCommand(sql, con);
        try
        {
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private bool deleteChampionshipDependencies(int chmpId, TTTDataClassesDataContext db, SqlConnection con)
    {
        string sql = string.Format("delete from PlayerChampionships where ChampionshipId={0}", chmpId);
        SqlCommand cmd = new SqlCommand(sql, con);
        try
        {
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private List<int> getPlayerMatches(PlayerData player, string title, string value)
    {
        try
        {        
            IEnumerable<Player> players = null;

            using (var db = new TTTDataClassesDataContext())
            {
                switch (title)
                {
                    case "Id":
                        players = db.Players.Where(p => p.Id == int.Parse(value));
                        break;
                    case "FirstName":
                        players = db.Players.Where(p => 
                            p.FirstName.Replace(" ", string.Empty).Equals(value.Replace(" ", string.Empty)));
                        break;
                    case "LastName":
                        players = db.Players.Where(p => 
                            p.LastName.Replace(" ", string.Empty).Equals(value.Replace(" ", string.Empty)));
                        break;
                    case "City":
                        players = db.Players.Where(p => 
                            p.City.Replace(" ", string.Empty).Equals(value.Replace(" ", string.Empty)));
                        break;
                    case "Country":
                        players = db.Players.Where(p => 
                            p.Country.Replace(" ", string.Empty).Equals(value.Replace(" ", string.Empty)));
                        break;
                    case "Phone":
                        players = db.Players.Where(p => 
                            p.Phone.Replace(" ", string.Empty).Equals(value.Replace(" ", string.Empty)));
                        break;
                    case "Is_Advisor":
                        int IsAdvisor = -1;
                        if (value.Equals("Yes"))
                            IsAdvisor = 1;
                        else if (value.Equals("No"))
                            IsAdvisor = 0;
                        if (IsAdvisor != -1)
                            players = db.Players.Where(p => p.IsAdvisor == IsAdvisor);
                        break;
                    case "AdviseTo_Name":
                        players = db.Players.Where(p => p.AdviseTo == player.AdviseTo);
                        break;
                }

                if (players != null && players.Count() > 0)
                {
                    List<int> lst = new List<int>();
                    foreach (var p in players)
                        lst.Add(p.Id);
                    return lst;
                }
                else
                    return null;
            }
        }
        catch (Exception)
        {
            return null;
        }
    }

    private List<int> getChampionshipMatches(string title, string value)
    {
        try
        {
            IEnumerable<Championship> chmps = null;

            using (var db = new TTTDataClassesDataContext())
            {
                switch (title)
                {
                    case "Id":
                        chmps = db.Championships.Where(c =>
                            c.Id == int.Parse(value.Replace(" ", string.Empty)));
                        break;
                    case "City":
                        chmps = db.Championships.Where(c =>
                            c.City.Replace(" ", string.Empty).Equals(value.Replace(" ", string.Empty)));
                        break;
                    case "StartDate":
                        chmps = db.Championships.Where(c => c.StartDate.Equals(value));
                        break;
                    case "EndDate":
                        chmps = db.Championships.Where(c => c.EndDate.Equals(value));
                        break;
                    case "Picture":
                        chmps = db.Championships.Where(c =>
                            c.Picture.Replace(" ", string.Empty).Equals(value.Replace(" ", string.Empty)));
                        break;
                }

                if (chmps != null && chmps.Count() > 0)
                {
                    List<int> lst = new List<int>();
                    foreach (var p in chmps)
                        lst.Add(p.Id);
                    return lst;
                }
                else
                    return null;
            }
        }
        catch (Exception)
        {
            return null;
        }
    }

    private int sleep()
    {
        Thread.Sleep(SLEEP_TIME);
        return 1;
    }

    #endregion

}
