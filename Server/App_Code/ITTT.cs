using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
[ServiceContract(CallbackContract = typeof(ICallBack), SessionMode = SessionMode.Required)]
public interface ITTT
{
    [OperationContract(IsOneWay = true)]
    void getAllPlayers();
}

public interface ICallBack
{
    [OperationContract(IsOneWay = true)]
    void returnPlayersList(List<PlayerData> players);
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
    public System.Nullable<int> Phone { get; set; }
    [DataMember]
    public System.Nullable<int> AdviseTo { get; set; }

}
