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

    public void getAllUsers()
    {
        ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();
        channel.sendAllUsers(getAllPlayersFromDB());
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

    public void getRegToChampList()
    {
        ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();
        channel.sendRegToChampList(getAllChampionships());
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
                    sql = string.Format("Insert into Games(Player1, Player2, Winner, Moves, StartTime, EndTime) "
                         + "values({0}, {1}, {2}, '{3}', '{4}', '{5}')", game.Player1, game.Player2, game.Winner,
                         game.Moves, game.StartTime, game.EndTime);
                else
                    sql = string.Format("Insert into Games(Player1, Player2, Moves, StartTime, EndTime) "
                         + "values({0}, {1}, '{2}', '{3}', '{4}')", game.Player1, game.Player2, game.Moves, 
                         game.StartTime, game.EndTime);

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


    /////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////


    private PlayerData[] getAllFreeAdvisors()
    {
        using (var db = new TTTDataClassesDataContext())
        {
            var x =
                from p in db.Players
                where p.IsAdvisor == 1 && !p.AdviseTo.HasValue
                select p;
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

    private ChampionshipData[] getAllChampionships()
    {
        using (var db = new TTTDataClassesDataContext())
        {
            var x =
                from c in db.Championships
                select c;
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
            var x =
                from p in db.Players
                where p.Id != 1
                select p;
            PlayerData[] players = new PlayerData[x.Count()];
            int i = 0;
            foreach (var p in x)
            {
                players[i++] = getPlayerData(p);
            }
            return players;
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

}
