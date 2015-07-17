using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

public class TTT : ITTT
{

    public void getAllPlayers()
    {
        ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();
        TTTDataClassesDataContext db = new TTTDataClassesDataContext();
        var x =
            from p in db.Players
            select p;
        List<PlayerData> players = new List<PlayerData>();
        foreach (var p in x)
        {
            players.Add(getPlayerData(p));
        }

        channel.returnPlayersList(players);
    }

    public PlayerData getPlayerData(Player p)
    {
        PlayerData player = new PlayerData();
        player.Id = p.Id;
        player.FirstName = p.FirstName;
        player.LastName = p.LastName;
        player.City = p.City;
        player.Country = p.Country;
        player.Phone = p.Phone.Value;
        player.AdviseTo = p.AdviseTo.Value;
        return player;
    }

}
