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
    private Dictionary<ICallBack, PlayerData> channels = new Dictionary<ICallBack, PlayerData>();


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

                    player.Id = (from p in db.Players
                                 select p.Id).Max();

                    if (advisors != null)
                    {
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

    public void getChampionships()
    {
        ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();
        channel.sendChampionships(getAllChampionships());
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

        if (isUserAlreadyLogged(user))
        {
            channel.userAlreadyConnected(user);
        }
        else
        {
            channels.Add(channel, user);
            channel.loginSuccess(user);
        }
    }

    public void logout()
    {
        ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();

        if (channels.ContainsKey(channel) )
        {
            channels.Remove(channel);
            channel.logoutSuccess();
        }
        else
        {
            channel.logoutFail();
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
        return channels.Values.Any(p => p.Id == user.Id);
    }

}
