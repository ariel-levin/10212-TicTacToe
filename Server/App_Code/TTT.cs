using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;


public class TTT : ITTT
{

    public void getRegisterFormAdvisorList()
    {
        ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();
        channel.sendRegisterFormAdvisorList(getAllAdvisors());
    }


    /////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////


    private PlayerData[] getAllAdvisors()
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

    private PlayerData getPlayerData(Player p)
    {
        PlayerData player = new PlayerData();
        player.Id = p.Id;
        player.FirstName = p.FirstName;
        player.LastName = p.LastName;
        player.City = p.City;
        player.Country = p.Country;
        if (p.Phone.HasValue)
            player.Phone = p.Phone.Value;
        player.IsAdvisor = p.IsAdvisor;
        if (p.AdviseTo.HasValue)
            player.AdviseTo = p.AdviseTo.Value;
        return player;
    }

}
