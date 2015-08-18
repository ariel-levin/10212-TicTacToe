using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;


public interface ICallBack
{

    [OperationContract(IsOneWay = true)]
    void sendRegisterFormAdvisorList(PlayerData[] players);

    [OperationContract(IsOneWay = true)]
    void showException(Exception e);

    [OperationContract(IsOneWay = true)]
    void showPlayerRegisterSuccess();

    [OperationContract(IsOneWay = true)]
    void sendChampionships(ChampionshipData[] chmps, string caller);

    [OperationContract(IsOneWay = true)]
    void showNewChampSuccess();

    [OperationContract(IsOneWay = true)]
    void sendPlayers(PlayerData[] users, string caller);

    [OperationContract(IsOneWay = true)]
    void loginSuccess(PlayerData user);

    [OperationContract(IsOneWay = true)]
    void logoutSuccess();

    [OperationContract(IsOneWay = true)]
    void loginError(string error, PlayerData user);

    [OperationContract(IsOneWay = true)]
    void logoutError(string error);

    [OperationContract(IsOneWay = true)]
    void response();

    [OperationContract(IsOneWay = true)]
    void registerPlayerToChampSuccess();

    [OperationContract(IsOneWay = true)]
    void registerPlayerToChampError(string error);

    [OperationContract(IsOneWay = true)]
    void startGame(bool yourTurn);

    [OperationContract(IsOneWay = true)]
    void gameError(string error);

    [OperationContract(IsOneWay = true)]
    void gameMessage(string msg);

    [OperationContract(IsOneWay = true)]
    void gameEnded(string msg);

    [OperationContract(IsOneWay = true)]
    void opponentPressed(int row, int col);

    [OperationContract(IsOneWay = true)]
    void addedSuccessfully(bool firstPlayer);

    [OperationContract(IsOneWay = true)]
    void playerTurn();

    [OperationContract(IsOneWay = true)]
    void sendGames(GameData[] games, string caller);

    [OperationContract(IsOneWay = true)]
    void sendGameAdvisors(PlayerData[] advisors);

    [OperationContract(IsOneWay = true)]
    void sendPlayersGamesNum(PlayerGames[] playersGames);

    [OperationContract(IsOneWay = true)]
    void sendCitiesChampionshipsNum(CityChampionships[] citiesChmps);

    [OperationContract(IsOneWay = true)]
    void updateSuccess(string msg = null);

    [OperationContract(IsOneWay = true)]
    void updateError(string err);

}