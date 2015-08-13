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
    void getAllUsers(char caller);

    [OperationContract(IsOneWay = true)]
    void login(PlayerData user);

    [OperationContract(IsOneWay = true)]
    void logout(bool waitingForResponse);

    [OperationContract(IsOneWay = true)]
    void wake();

    [OperationContract(IsOneWay = true)]
    void getAllChampionships(char caller);

    [OperationContract(IsOneWay = true)]
    void registerPlayerToChamp(PlayerData player, ChampionshipData[] chmps);

    [OperationContract(IsOneWay = true)]
    void startGameRequest(int dim, bool singlePlayer);

    [OperationContract(IsOneWay = true)]
    void playerPressed(int row, int col);

    [OperationContract(IsOneWay = true)]
    void playerExitGame();

    [OperationContract(IsOneWay = true)]
    void getAllGames(bool withPlayersNames);

}


public interface ICallBack
{

    [OperationContract(IsOneWay = true)]
    void sendRegisterFormAdvisorList(PlayerData[] players);

    [OperationContract(IsOneWay = true)]
    void showException(Exception e);

    [OperationContract(IsOneWay = true)]
    void showPlayerRegisterSuccess();

    [OperationContract(IsOneWay = true)]
    void sendAllChampionships(ChampionshipData[] chmps, char caller);

    [OperationContract(IsOneWay = true)]
    void showNewChampSuccess();

    [OperationContract(IsOneWay = true)]
    void sendAllUsers(PlayerData[] users, char caller);

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
    void sendAllGames(GameData[] games);

}

[DataContract]
public class PlayerData
{
    [DataMember]
    public int Id { get; set; }
    [DataMember]
    public string FirstName { get; set; }
    [DataMember]
    public string LastName { get; set; }
    [DataMember]
    public string City { get; set; }
    [DataMember]
    public string Country { get; set; }
    [DataMember]
    public string Phone { get; set; }
    [DataMember]
    public byte IsAdvisor { get; set; }
    [DataMember]
    public System.Nullable<int> AdviseTo { get; set; }
}

[DataContract]
public class ChampionshipData
{
    [DataMember]
    public int Id { get; set; }
    [DataMember]
    public string City { get; set; }
    [DataMember]
    public System.DateTime StartDate { get; set; }
    [DataMember]
    public System.Nullable<System.DateTime> EndDate { get; set; }
    [DataMember]
    public string Picture { get; set; }
}

[DataContract]
public class GameData
{
    [DataMember]
    public int Id { get; set; }
    [DataMember]
    public int Player1 { get; set; }
    [DataMember]
    public string Player1_Name { get; set; }
    [DataMember]
    public int Player2 { get; set; }
    [DataMember]
    public string Player2_Name { get; set; }
    [DataMember]
    public System.Nullable<int> Winner { get; set; }
    [DataMember]
    public string Winner_Name { get; set; }
    [DataMember]
    public int BoardSize { get; set; }
    [DataMember]
    public string Moves { get; set; }
    [DataMember]
    public System.DateTime StartTime { get; set; }
    [DataMember]
    public System.DateTime EndTime { get; set; }

}
