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
    void getChampionships();

    [OperationContract(IsOneWay = true)]
    void registerNewChampionship(ChampionshipData champ);

    [OperationContract(IsOneWay = true)]
    void getAllUsers();

    [OperationContract(IsOneWay = true)]
    void login(PlayerData user);

    [OperationContract(IsOneWay = true)]
    void logoff(PlayerData user);

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
    void sendChampionships(ChampionshipData[] chmps);

    [OperationContract(IsOneWay = true)]
    void showNewChampSuccess();

    [OperationContract(IsOneWay = true)]
    void sendAllUsers(PlayerData[] users);

    [OperationContract(IsOneWay = true)]
    void loginSuccess(PlayerData user);

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
