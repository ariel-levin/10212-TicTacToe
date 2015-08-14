using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;


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
    [DataMember]
    public string AdviseToName { get; set; }
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

[DataContract]
public class PlayerGames
{
    [DataMember]
    public string Name { get; set; }
    [DataMember]
    public int NumberOfGames { get; set; }
}

[DataContract]
public class CityChampionships
{
    [DataMember]
    public string City { get; set; }
    [DataMember]
    public int NumberOfChampionships { get; set; }
}
