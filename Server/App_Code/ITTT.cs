/*********************************  
 *  Ariel Levin
 *  ariel.lvn89@gmail.com
 *  http://about.me/ariel.levin
 *********************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;


[ServiceContract(CallbackContract = typeof(ICallBack), SessionMode = SessionMode.Required)]
public interface ITTT
{

    [OperationContract(IsOneWay = true)]
    void getRegisterFormAdvisorList();

    [OperationContract(IsOneWay = true)]
    void registerNewPlayer(PlayerData player, int[] advisors);

    [OperationContract(IsOneWay = true)]
    void registerNewChampionship(ChampionshipData champ);

    [OperationContract(IsOneWay = true)]
    void getAllUsers(string caller);

    [OperationContract(IsOneWay = true)]
    void login(PlayerData user);

    [OperationContract(IsOneWay = true)]
    void logout(bool waitingForResponse);

    [OperationContract(IsOneWay = true)]
    void wake();

    [OperationContract(IsOneWay = true)]
    void getAllChampionships(int playerId, string caller);

    [OperationContract(IsOneWay = true)]
    void registerPlayerToChamp(PlayerData player, ChampionshipData[] chmps);

    [OperationContract(IsOneWay = true)]
    void startGameRequest(int dim, bool singlePlayer);

    [OperationContract(IsOneWay = true)]
    void playerPressed(int row, int col);

    [OperationContract(IsOneWay = true)]
    void playerExitGame();

    [OperationContract(IsOneWay = true)]
    void getAllGames(bool withPlayersNames, int playerId, string caller);

    [OperationContract(IsOneWay = true)]
    void getGamePlayers(GameData game);

    [OperationContract(IsOneWay = true)]
    void getGameAdvisors(GameData game);

    [OperationContract(IsOneWay = true)]
    void getChampionshipPlayers(ChampionshipData chmp);

    [OperationContract(IsOneWay = true)]
    void getPlayersGamesNum();

    [OperationContract(IsOneWay = true)]
    void getCitiesChampionshipsNum();

    [OperationContract(IsOneWay = true)]
    void updatePlayers(PlayerData[] players);

    [OperationContract(IsOneWay = true)]
    void updateChampionships(ChampionshipData[] chmps);

    [OperationContract(IsOneWay = true)]
    void deletePlayer(PlayerData player);

    [OperationContract(IsOneWay = true)]
    void deletePlayers(PlayerData player, string title, string value);

    [OperationContract(IsOneWay = true)]
    void deleteChampionship(ChampionshipData chmp);

    [OperationContract(IsOneWay = true)]
    void deleteChampionships(string title, string value);

}
