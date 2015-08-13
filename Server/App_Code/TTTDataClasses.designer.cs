﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;



[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="TicTacToeDB")]
public partial class TTTDataClassesDataContext : System.Data.Linq.DataContext
{
	
	private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
	
  #region Extensibility Method Definitions
  partial void OnCreated();
  partial void InsertPlayer(Player instance);
  partial void UpdatePlayer(Player instance);
  partial void DeletePlayer(Player instance);
  partial void InsertChampionship(Championship instance);
  partial void UpdateChampionship(Championship instance);
  partial void DeleteChampionship(Championship instance);
  partial void InsertGame(Game instance);
  partial void UpdateGame(Game instance);
  partial void DeleteGame(Game instance);
  #endregion
	
	public TTTDataClassesDataContext() : 
			base(global::System.Configuration.ConfigurationManager.ConnectionStrings["TicTacToeDBConnectionString"].ConnectionString, mappingSource)
	{
		OnCreated();
	}
	
	public TTTDataClassesDataContext(string connection) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public TTTDataClassesDataContext(System.Data.IDbConnection connection) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public TTTDataClassesDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public TTTDataClassesDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public System.Data.Linq.Table<Player> Players
	{
		get
		{
			return this.GetTable<Player>();
		}
	}
	
	public System.Data.Linq.Table<Championship> Championships
	{
		get
		{
			return this.GetTable<Championship>();
		}
	}
	
	public System.Data.Linq.Table<PlayerChampionship> PlayerChampionships
	{
		get
		{
			return this.GetTable<PlayerChampionship>();
		}
	}
	
	public System.Data.Linq.Table<Game> Games
	{
		get
		{
			return this.GetTable<Game>();
		}
	}
}

[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Players")]
public partial class Player : INotifyPropertyChanging, INotifyPropertyChanged
{
	
	private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
	
	private int _Id;
	
	private string _FirstName;
	
	private string _LastName;
	
	private string _City;
	
	private string _Country;
	
	private string _Phone;
	
	private byte _IsAdvisor;
	
	private System.Nullable<int> _AdviseTo;
	
	private EntitySet<Player> _Players;
	
	private EntitySet<Game> _Games;
	
	private EntitySet<Game> _Games1;
	
	private EntitySet<Game> _Games2;
	
	private EntityRef<Player> _Player1;
	
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnFirstNameChanging(string value);
    partial void OnFirstNameChanged();
    partial void OnLastNameChanging(string value);
    partial void OnLastNameChanged();
    partial void OnCityChanging(string value);
    partial void OnCityChanged();
    partial void OnCountryChanging(string value);
    partial void OnCountryChanged();
    partial void OnPhoneChanging(string value);
    partial void OnPhoneChanged();
    partial void OnIsAdvisorChanging(byte value);
    partial void OnIsAdvisorChanged();
    partial void OnAdviseToChanging(System.Nullable<int> value);
    partial void OnAdviseToChanged();
    #endregion
	
	public Player()
	{
		this._Players = new EntitySet<Player>(new Action<Player>(this.attach_Players), new Action<Player>(this.detach_Players));
		this._Games = new EntitySet<Game>(new Action<Game>(this.attach_Games), new Action<Game>(this.detach_Games));
		this._Games1 = new EntitySet<Game>(new Action<Game>(this.attach_Games1), new Action<Game>(this.detach_Games1));
		this._Games2 = new EntitySet<Game>(new Action<Game>(this.attach_Games2), new Action<Game>(this.detach_Games2));
		this._Player1 = default(EntityRef<Player>);
		OnCreated();
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
	public int Id
	{
		get
		{
			return this._Id;
		}
		set
		{
			if ((this._Id != value))
			{
				this.OnIdChanging(value);
				this.SendPropertyChanging();
				this._Id = value;
				this.SendPropertyChanged("Id");
				this.OnIdChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FirstName", DbType="NChar(20) NOT NULL", CanBeNull=false)]
	public string FirstName
	{
		get
		{
			return this._FirstName;
		}
		set
		{
			if ((this._FirstName != value))
			{
				this.OnFirstNameChanging(value);
				this.SendPropertyChanging();
				this._FirstName = value;
				this.SendPropertyChanged("FirstName");
				this.OnFirstNameChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LastName", DbType="NChar(20)")]
	public string LastName
	{
		get
		{
			return this._LastName;
		}
		set
		{
			if ((this._LastName != value))
			{
				this.OnLastNameChanging(value);
				this.SendPropertyChanging();
				this._LastName = value;
				this.SendPropertyChanged("LastName");
				this.OnLastNameChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_City", DbType="NChar(20)")]
	public string City
	{
		get
		{
			return this._City;
		}
		set
		{
			if ((this._City != value))
			{
				this.OnCityChanging(value);
				this.SendPropertyChanging();
				this._City = value;
				this.SendPropertyChanged("City");
				this.OnCityChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Country", DbType="NChar(20)")]
	public string Country
	{
		get
		{
			return this._Country;
		}
		set
		{
			if ((this._Country != value))
			{
				this.OnCountryChanging(value);
				this.SendPropertyChanging();
				this._Country = value;
				this.SendPropertyChanged("Country");
				this.OnCountryChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Phone", DbType="NChar(20)")]
	public string Phone
	{
		get
		{
			return this._Phone;
		}
		set
		{
			if ((this._Phone != value))
			{
				this.OnPhoneChanging(value);
				this.SendPropertyChanging();
				this._Phone = value;
				this.SendPropertyChanged("Phone");
				this.OnPhoneChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsAdvisor", DbType="TinyInt NOT NULL")]
	public byte IsAdvisor
	{
		get
		{
			return this._IsAdvisor;
		}
		set
		{
			if ((this._IsAdvisor != value))
			{
				this.OnIsAdvisorChanging(value);
				this.SendPropertyChanging();
				this._IsAdvisor = value;
				this.SendPropertyChanged("IsAdvisor");
				this.OnIsAdvisorChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AdviseTo", DbType="Int")]
	public System.Nullable<int> AdviseTo
	{
		get
		{
			return this._AdviseTo;
		}
		set
		{
			if ((this._AdviseTo != value))
			{
				if (this._Player1.HasLoadedOrAssignedValue)
				{
					throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
				}
				this.OnAdviseToChanging(value);
				this.SendPropertyChanging();
				this._AdviseTo = value;
				this.SendPropertyChanged("AdviseTo");
				this.OnAdviseToChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Player_Player", Storage="_Players", ThisKey="Id", OtherKey="AdviseTo")]
	public EntitySet<Player> Players
	{
		get
		{
			return this._Players;
		}
		set
		{
			this._Players.Assign(value);
		}
	}
	
	[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Player_Game", Storage="_Games", ThisKey="Id", OtherKey="Player1")]
	public EntitySet<Game> Games
	{
		get
		{
			return this._Games;
		}
		set
		{
			this._Games.Assign(value);
		}
	}
	
	[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Player_Game1", Storage="_Games1", ThisKey="Id", OtherKey="Player2")]
	public EntitySet<Game> Games1
	{
		get
		{
			return this._Games1;
		}
		set
		{
			this._Games1.Assign(value);
		}
	}
	
	[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Player_Game2", Storage="_Games2", ThisKey="Id", OtherKey="Winner")]
	public EntitySet<Game> Games2
	{
		get
		{
			return this._Games2;
		}
		set
		{
			this._Games2.Assign(value);
		}
	}
	
	[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Player_Player", Storage="_Player1", ThisKey="AdviseTo", OtherKey="Id", IsForeignKey=true)]
	public Player Player1
	{
		get
		{
			return this._Player1.Entity;
		}
		set
		{
			Player previousValue = this._Player1.Entity;
			if (((previousValue != value) 
						|| (this._Player1.HasLoadedOrAssignedValue == false)))
			{
				this.SendPropertyChanging();
				if ((previousValue != null))
				{
					this._Player1.Entity = null;
					previousValue.Players.Remove(this);
				}
				this._Player1.Entity = value;
				if ((value != null))
				{
					value.Players.Add(this);
					this._AdviseTo = value.Id;
				}
				else
				{
					this._AdviseTo = default(Nullable<int>);
				}
				this.SendPropertyChanged("Player1");
			}
		}
	}
	
	public event PropertyChangingEventHandler PropertyChanging;
	
	public event PropertyChangedEventHandler PropertyChanged;
	
	protected virtual void SendPropertyChanging()
	{
		if ((this.PropertyChanging != null))
		{
			this.PropertyChanging(this, emptyChangingEventArgs);
		}
	}
	
	protected virtual void SendPropertyChanged(String propertyName)
	{
		if ((this.PropertyChanged != null))
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
	
	private void attach_Players(Player entity)
	{
		this.SendPropertyChanging();
		entity.Player1 = this;
	}
	
	private void detach_Players(Player entity)
	{
		this.SendPropertyChanging();
		entity.Player1 = null;
	}
	
	private void attach_Games(Game entity)
	{
		this.SendPropertyChanging();
		entity.Player = this;
	}
	
	private void detach_Games(Game entity)
	{
		this.SendPropertyChanging();
		entity.Player = null;
	}
	
	private void attach_Games1(Game entity)
	{
		this.SendPropertyChanging();
		entity.Player3 = this;
	}
	
	private void detach_Games1(Game entity)
	{
		this.SendPropertyChanging();
		entity.Player3 = null;
	}
	
	private void attach_Games2(Game entity)
	{
		this.SendPropertyChanging();
		entity.Player4 = this;
	}
	
	private void detach_Games2(Game entity)
	{
		this.SendPropertyChanging();
		entity.Player4 = null;
	}
}

[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Championships")]
public partial class Championship : INotifyPropertyChanging, INotifyPropertyChanged
{
	
	private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
	
	private int _Id;
	
	private string _City;
	
	private System.DateTime _StartDate;
	
	private System.Nullable<System.DateTime> _EndDate;
	
	private string _Picture;
	
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnCityChanging(string value);
    partial void OnCityChanged();
    partial void OnStartDateChanging(System.DateTime value);
    partial void OnStartDateChanged();
    partial void OnEndDateChanging(System.Nullable<System.DateTime> value);
    partial void OnEndDateChanged();
    partial void OnPictureChanging(string value);
    partial void OnPictureChanged();
    #endregion
	
	public Championship()
	{
		OnCreated();
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
	public int Id
	{
		get
		{
			return this._Id;
		}
		set
		{
			if ((this._Id != value))
			{
				this.OnIdChanging(value);
				this.SendPropertyChanging();
				this._Id = value;
				this.SendPropertyChanged("Id");
				this.OnIdChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_City", DbType="NChar(20) NOT NULL", CanBeNull=false)]
	public string City
	{
		get
		{
			return this._City;
		}
		set
		{
			if ((this._City != value))
			{
				this.OnCityChanging(value);
				this.SendPropertyChanging();
				this._City = value;
				this.SendPropertyChanged("City");
				this.OnCityChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_StartDate", DbType="Date NOT NULL")]
	public System.DateTime StartDate
	{
		get
		{
			return this._StartDate;
		}
		set
		{
			if ((this._StartDate != value))
			{
				this.OnStartDateChanging(value);
				this.SendPropertyChanging();
				this._StartDate = value;
				this.SendPropertyChanged("StartDate");
				this.OnStartDateChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EndDate", DbType="Date")]
	public System.Nullable<System.DateTime> EndDate
	{
		get
		{
			return this._EndDate;
		}
		set
		{
			if ((this._EndDate != value))
			{
				this.OnEndDateChanging(value);
				this.SendPropertyChanging();
				this._EndDate = value;
				this.SendPropertyChanged("EndDate");
				this.OnEndDateChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Picture", DbType="NChar(500)")]
	public string Picture
	{
		get
		{
			return this._Picture;
		}
		set
		{
			if ((this._Picture != value))
			{
				this.OnPictureChanging(value);
				this.SendPropertyChanging();
				this._Picture = value;
				this.SendPropertyChanged("Picture");
				this.OnPictureChanged();
			}
		}
	}
	
	public event PropertyChangingEventHandler PropertyChanging;
	
	public event PropertyChangedEventHandler PropertyChanged;
	
	protected virtual void SendPropertyChanging()
	{
		if ((this.PropertyChanging != null))
		{
			this.PropertyChanging(this, emptyChangingEventArgs);
		}
	}
	
	protected virtual void SendPropertyChanged(String propertyName)
	{
		if ((this.PropertyChanged != null))
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}

[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.PlayerChampionships")]
public partial class PlayerChampionship
{
	
	private int _PlayerId;
	
	private int _ChampionshipId;
	
	public PlayerChampionship()
	{
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PlayerId", DbType="Int NOT NULL")]
	public int PlayerId
	{
		get
		{
			return this._PlayerId;
		}
		set
		{
			if ((this._PlayerId != value))
			{
				this._PlayerId = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ChampionshipId", DbType="Int NOT NULL")]
	public int ChampionshipId
	{
		get
		{
			return this._ChampionshipId;
		}
		set
		{
			if ((this._ChampionshipId != value))
			{
				this._ChampionshipId = value;
			}
		}
	}
}

[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Games")]
public partial class Game : INotifyPropertyChanging, INotifyPropertyChanged
{
	
	private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
	
	private int _Id;
	
	private int _Player1;
	
	private int _Player2;
	
	private System.Nullable<int> _Winner;
	
	private int _BoardSize;
	
	private string _Moves;
	
	private System.DateTime _StartTime;
	
	private System.DateTime _EndTime;
	
	private EntityRef<Player> _Player;
	
	private EntityRef<Player> _Player3;
	
	private EntityRef<Player> _Player4;
	
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnPlayer1Changing(int value);
    partial void OnPlayer1Changed();
    partial void OnPlayer2Changing(int value);
    partial void OnPlayer2Changed();
    partial void OnWinnerChanging(System.Nullable<int> value);
    partial void OnWinnerChanged();
    partial void OnBoardSizeChanging(int value);
    partial void OnBoardSizeChanged();
    partial void OnMovesChanging(string value);
    partial void OnMovesChanged();
    partial void OnStartTimeChanging(System.DateTime value);
    partial void OnStartTimeChanged();
    partial void OnEndTimeChanging(System.DateTime value);
    partial void OnEndTimeChanged();
    #endregion
	
	public Game()
	{
		this._Player = default(EntityRef<Player>);
		this._Player3 = default(EntityRef<Player>);
		this._Player4 = default(EntityRef<Player>);
		OnCreated();
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
	public int Id
	{
		get
		{
			return this._Id;
		}
		set
		{
			if ((this._Id != value))
			{
				this.OnIdChanging(value);
				this.SendPropertyChanging();
				this._Id = value;
				this.SendPropertyChanged("Id");
				this.OnIdChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Player1", DbType="Int NOT NULL")]
	public int Player1
	{
		get
		{
			return this._Player1;
		}
		set
		{
			if ((this._Player1 != value))
			{
				if (this._Player.HasLoadedOrAssignedValue)
				{
					throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
				}
				this.OnPlayer1Changing(value);
				this.SendPropertyChanging();
				this._Player1 = value;
				this.SendPropertyChanged("Player1");
				this.OnPlayer1Changed();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Player2", DbType="Int NOT NULL")]
	public int Player2
	{
		get
		{
			return this._Player2;
		}
		set
		{
			if ((this._Player2 != value))
			{
				if (this._Player3.HasLoadedOrAssignedValue)
				{
					throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
				}
				this.OnPlayer2Changing(value);
				this.SendPropertyChanging();
				this._Player2 = value;
				this.SendPropertyChanged("Player2");
				this.OnPlayer2Changed();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Winner", DbType="Int")]
	public System.Nullable<int> Winner
	{
		get
		{
			return this._Winner;
		}
		set
		{
			if ((this._Winner != value))
			{
				if (this._Player4.HasLoadedOrAssignedValue)
				{
					throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
				}
				this.OnWinnerChanging(value);
				this.SendPropertyChanging();
				this._Winner = value;
				this.SendPropertyChanged("Winner");
				this.OnWinnerChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BoardSize", DbType="Int NOT NULL")]
	public int BoardSize
	{
		get
		{
			return this._BoardSize;
		}
		set
		{
			if ((this._BoardSize != value))
			{
				this.OnBoardSizeChanging(value);
				this.SendPropertyChanging();
				this._BoardSize = value;
				this.SendPropertyChanged("BoardSize");
				this.OnBoardSizeChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Moves", DbType="VarChar(500) NOT NULL", CanBeNull=false)]
	public string Moves
	{
		get
		{
			return this._Moves;
		}
		set
		{
			if ((this._Moves != value))
			{
				this.OnMovesChanging(value);
				this.SendPropertyChanging();
				this._Moves = value;
				this.SendPropertyChanged("Moves");
				this.OnMovesChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_StartTime", DbType="DateTime NOT NULL")]
	public System.DateTime StartTime
	{
		get
		{
			return this._StartTime;
		}
		set
		{
			if ((this._StartTime != value))
			{
				this.OnStartTimeChanging(value);
				this.SendPropertyChanging();
				this._StartTime = value;
				this.SendPropertyChanged("StartTime");
				this.OnStartTimeChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EndTime", DbType="DateTime NOT NULL")]
	public System.DateTime EndTime
	{
		get
		{
			return this._EndTime;
		}
		set
		{
			if ((this._EndTime != value))
			{
				this.OnEndTimeChanging(value);
				this.SendPropertyChanging();
				this._EndTime = value;
				this.SendPropertyChanged("EndTime");
				this.OnEndTimeChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Player_Game", Storage="_Player", ThisKey="Player1", OtherKey="Id", IsForeignKey=true)]
	public Player Player
	{
		get
		{
			return this._Player.Entity;
		}
		set
		{
			Player previousValue = this._Player.Entity;
			if (((previousValue != value) 
						|| (this._Player.HasLoadedOrAssignedValue == false)))
			{
				this.SendPropertyChanging();
				if ((previousValue != null))
				{
					this._Player.Entity = null;
					previousValue.Games.Remove(this);
				}
				this._Player.Entity = value;
				if ((value != null))
				{
					value.Games.Add(this);
					this._Player1 = value.Id;
				}
				else
				{
					this._Player1 = default(int);
				}
				this.SendPropertyChanged("Player");
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Player_Game1", Storage="_Player3", ThisKey="Player2", OtherKey="Id", IsForeignKey=true)]
	public Player Player3
	{
		get
		{
			return this._Player3.Entity;
		}
		set
		{
			Player previousValue = this._Player3.Entity;
			if (((previousValue != value) 
						|| (this._Player3.HasLoadedOrAssignedValue == false)))
			{
				this.SendPropertyChanging();
				if ((previousValue != null))
				{
					this._Player3.Entity = null;
					previousValue.Games1.Remove(this);
				}
				this._Player3.Entity = value;
				if ((value != null))
				{
					value.Games1.Add(this);
					this._Player2 = value.Id;
				}
				else
				{
					this._Player2 = default(int);
				}
				this.SendPropertyChanged("Player3");
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Player_Game2", Storage="_Player4", ThisKey="Winner", OtherKey="Id", IsForeignKey=true)]
	public Player Player4
	{
		get
		{
			return this._Player4.Entity;
		}
		set
		{
			Player previousValue = this._Player4.Entity;
			if (((previousValue != value) 
						|| (this._Player4.HasLoadedOrAssignedValue == false)))
			{
				this.SendPropertyChanging();
				if ((previousValue != null))
				{
					this._Player4.Entity = null;
					previousValue.Games2.Remove(this);
				}
				this._Player4.Entity = value;
				if ((value != null))
				{
					value.Games2.Add(this);
					this._Winner = value.Id;
				}
				else
				{
					this._Winner = default(Nullable<int>);
				}
				this.SendPropertyChanged("Player4");
			}
		}
	}
	
	public event PropertyChangingEventHandler PropertyChanging;
	
	public event PropertyChangedEventHandler PropertyChanged;
	
	protected virtual void SendPropertyChanging()
	{
		if ((this.PropertyChanging != null))
		{
			this.PropertyChanging(this, emptyChangingEventArgs);
		}
	}
	
	protected virtual void SendPropertyChanged(String propertyName)
	{
		if ((this.PropertyChanged != null))
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
#pragma warning restore 1591
