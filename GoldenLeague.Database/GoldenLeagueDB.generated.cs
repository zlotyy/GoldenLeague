﻿//---------------------------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated by T4Model template for T4 (https://github.com/linq2db/linq2db).
//    Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//---------------------------------------------------------------------------------------------------

#pragma warning disable 1591

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using LinqToDB;
using LinqToDB.Common;
using LinqToDB.Configuration;
using LinqToDB.Data;
using LinqToDB.Mapping;

namespace GoldenLeague.Database
{
	/// <summary>
	/// Database       : GoldenLeague
	/// Data Source    : (LocalDb)\MSSQLLocalDB
	/// Server Version : 13.00.4001
	/// </summary>
	public partial class GoldenLeagueDB : LinqToDB.Data.DataConnection
	{
		public ITable<BookmakerBets>                 BookmakerBets                 { get { return this.GetTable<BookmakerBets>(); } }
		public ITable<BookmakerLeagues>              BookmakerLeagues              { get { return this.GetTable<BookmakerLeagues>(); } }
		public ITable<BookmakerLeaguesLCompetitions> BookmakerLeaguesLCompetitions { get { return this.GetTable<BookmakerLeaguesLCompetitions>(); } }
		public ITable<Competitions>                  Competitions                  { get { return this.GetTable<Competitions>(); } }
		public ITable<ConfigDictionary>              ConfigDictionary              { get { return this.GetTable<ConfigDictionary>(); } }
		public ITable<Matches>                       Matches                       { get { return this.GetTable<Matches>(); } }
		public ITable<PremierLeagueTable>            PremierLeagueTable            { get { return this.GetTable<PremierLeagueTable>(); } }
		public ITable<Teams>                         Teams                         { get { return this.GetTable<Teams>(); } }
		public ITable<Users>                         Users                         { get { return this.GetTable<Users>(); } }
		public ITable<VBookmakerBet>                 VBookmakerBet                 { get { return this.GetTable<VBookmakerBet>(); } }
		public ITable<VMatch>                        VMatch                        { get { return this.GetTable<VMatch>(); } }

		public GoldenLeagueDB()
		{
			InitDataContext();
			InitMappingSchema();
		}

		public GoldenLeagueDB(string configuration)
			: base(configuration)
		{
			InitDataContext();
			InitMappingSchema();
		}

		public GoldenLeagueDB(LinqToDbConnectionOptions options)
			: base(options)
		{
			InitDataContext();
			InitMappingSchema();
		}

		public GoldenLeagueDB(LinqToDbConnectionOptions<GoldenLeagueDB> options)
			: base(options)
		{
			InitDataContext();
			InitMappingSchema();
		}

		partial void InitDataContext  ();
		partial void InitMappingSchema();
	}

	[Table(Schema="dbo", Name="BookmakerBets")]
	public partial class BookmakerBets
	{
		[Column(DataType=LinqToDB.DataType.Guid),  PrimaryKey(1), NotNull] public Guid UserId        { get; set; } // uniqueidentifier
		[Column(DataType=LinqToDB.DataType.Guid),  PrimaryKey(2), NotNull] public Guid MatchId       { get; set; } // uniqueidentifier
		[Column(DataType=LinqToDB.DataType.Int32),    Nullable           ] public int? HomeTeamScore { get; set; } // int
		[Column(DataType=LinqToDB.DataType.Int32),    Nullable           ] public int? AwayTeamScore { get; set; } // int
		/// <summary>
		/// Liczba punktów za wytypowany wynik meczu (0 - nietrafiony, 1 - trafiony zwyciezca, 3 - trafiony wynik)
		/// </summary>
		[Column(DataType=LinqToDB.DataType.Int32),    Nullable           ] public int? BettingPoints { get; set; } // int

		#region Associations

		/// <summary>
		/// FK_MatchBetting_Matches
		/// </summary>
		[Association(ThisKey="MatchId", OtherKey="MatchId", CanBeNull=false, Relationship=LinqToDB.Mapping.Relationship.ManyToOne, KeyName="FK_MatchBetting_Matches", BackReferenceName="MatchBettings")]
		public Matches Match { get; set; }

		/// <summary>
		/// FK_MatchBetting_Users
		/// </summary>
		[Association(ThisKey="UserId", OtherKey="UserId", CanBeNull=false, Relationship=LinqToDB.Mapping.Relationship.ManyToOne, KeyName="FK_MatchBetting_Users", BackReferenceName="MatchBettings")]
		public Users User { get; set; }

		#endregion
	}

	[Table(Schema="dbo", Name="BookmakerLeagues")]
	public partial class BookmakerLeagues
	{
		[Column(DataType=LinqToDB.DataType.Guid),                PrimaryKey, NotNull] public Guid   LeagueId   { get; set; } // uniqueidentifier
		[Column(DataType=LinqToDB.DataType.NVarChar, Length=50),             NotNull] public string LeagueName { get; set; } // nvarchar(50)
		[Column(DataType=LinqToDB.DataType.Char,     Length=1),              NotNull] public char   IsPrivate  { get; set; } // char(1)
	}

	[Table(Schema="dbo", Name="BookmakerLeagues_l_Competitions")]
	public partial class BookmakerLeaguesLCompetitions
	{
		[Column(DataType=LinqToDB.DataType.Guid), PrimaryKey(1), NotNull] public Guid LeagueId      { get; set; } // uniqueidentifier
		[Column(DataType=LinqToDB.DataType.Guid), PrimaryKey(2), NotNull] public Guid CompetitionId { get; set; } // uniqueidentifier
	}

	[Table(Schema="dbo", Name="Competitions")]
	public partial class Competitions
	{
		[Column(DataType=LinqToDB.DataType.Guid),                PrimaryKey,  NotNull] public Guid   CompetitionId     { get; set; } // uniqueidentifier
		[Column(DataType=LinqToDB.DataType.NVarChar, Length=50),              NotNull] public string CompetitionName   { get; set; } // nvarchar(50)
		[Column(DataType=LinqToDB.DataType.NVarChar, Length=50),    Nullable         ] public string CompetitionIcon   { get; set; } // nvarchar(50)
		[Column(DataType=LinqToDB.DataType.NVarChar, Length=50),    Nullable         ] public string CountryIcon       { get; set; } // nvarchar(50)
		/// <summary>
		/// Aktualny sezon pilkarski (np. 2022 oznacza sezon 2021/22)
		/// </summary>
		[Column(DataType=LinqToDB.DataType.Int32),                            NotNull] public int    CurrentSeasonNo   { get; set; } // int
		/// <summary>
		/// Aktualny sezon pilkarski (np. 2022 oznacza sezon 2021/22)
		/// </summary>
		[Column(DataType=LinqToDB.DataType.Int32),                            NotNull] public int    CurrentGameweekNo { get; set; } // int

		#region Associations

		/// <summary>
		/// FK_Matches_Competitions_BackReference
		/// </summary>
		[Association(ThisKey="CompetitionId", OtherKey="CompetitionId", CanBeNull=true, Relationship=LinqToDB.Mapping.Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<Matches> Matches { get; set; }

		#endregion
	}

	[Table(Schema="dbo", Name="ConfigDictionary")]
	public partial class ConfigDictionary
	{
		[Column(DataType=LinqToDB.DataType.Int32),                 PrimaryKey, Identity] public int    ConfigId          { get; set; } // int
		[Column(DataType=LinqToDB.DataType.VarChar,  Length=50),   NotNull             ] public string ConfigKey         { get; set; } // varchar(50)
		[Column(DataType=LinqToDB.DataType.NVarChar, Length=4000), NotNull             ] public string ConfigValue       { get; set; } // nvarchar(4000)
		[Column(DataType=LinqToDB.DataType.NVarChar, Length=4000), NotNull             ] public string ConfigDescription { get; set; } // nvarchar(4000)
	}

	[Table(Schema="dbo", Name="Matches")]
	public partial class Matches
	{
		[Column(DataType=LinqToDB.DataType.Guid,     SkipOnInsert=true), PrimaryKey,  NotNull] public Guid     MatchId       { get; set; } // uniqueidentifier
		[Column(DataType=LinqToDB.DataType.Guid),                                     NotNull] public Guid     CompetitionId { get; set; } // uniqueidentifier
		/// <summary>
		/// Sezon pilkarski (np. 2022 oznacza sezon 2021/22)
		/// </summary>
		[Column(DataType=LinqToDB.DataType.Int32),                                    NotNull] public int      SeasonNo      { get; set; } // int
		/// <summary>
		/// Numer kolejki ligowej
		/// </summary>
		[Column(DataType=LinqToDB.DataType.Int32),                                    NotNull] public int      GameweekNo    { get; set; } // int
		[Column(DataType=LinqToDB.DataType.DateTime),                                 NotNull] public DateTime MatchDateTime { get; set; } // datetime
		[Column(DataType=LinqToDB.DataType.Guid),                                     NotNull] public Guid     HomeTeamId    { get; set; } // uniqueidentifier
		[Column(DataType=LinqToDB.DataType.Int32),                          Nullable         ] public int?     HomeTeamScore { get; set; } // int
		[Column(DataType=LinqToDB.DataType.Guid),                                     NotNull] public Guid     AwayTeamId    { get; set; } // uniqueidentifier
		[Column(DataType=LinqToDB.DataType.Int32),                          Nullable         ] public int?     AwayTeamScore { get; set; } // int
		/// <summary>
		/// Element key in external API, to get element statistics
		/// </summary>
		[Column(DataType=LinqToDB.DataType.Int32),                          Nullable         ] public int?     ForeignKey    { get; set; } // int

		#region Associations

		/// <summary>
		/// FK_Matches_Teams_AwayTeam
		/// </summary>
		[Association(ThisKey="AwayTeamId", OtherKey="TeamId", CanBeNull=false, Relationship=LinqToDB.Mapping.Relationship.ManyToOne, KeyName="FK_Matches_Teams_AwayTeam", BackReferenceName="MatchesAwayTeams")]
		public Teams AwayTeam { get; set; }

		/// <summary>
		/// FK_Matches_Competitions
		/// </summary>
		[Association(ThisKey="CompetitionId", OtherKey="CompetitionId", CanBeNull=false, Relationship=LinqToDB.Mapping.Relationship.ManyToOne, KeyName="FK_Matches_Competitions", BackReferenceName="Matches")]
		public Competitions Competition { get; set; }

		/// <summary>
		/// FK_Matches_Teams_HomeTeam
		/// </summary>
		[Association(ThisKey="HomeTeamId", OtherKey="TeamId", CanBeNull=false, Relationship=LinqToDB.Mapping.Relationship.ManyToOne, KeyName="FK_Matches_Teams_HomeTeam", BackReferenceName="MatchesHomeTeams")]
		public Teams HomeTeam { get; set; }

		/// <summary>
		/// FK_MatchBetting_Matches_BackReference
		/// </summary>
		[Association(ThisKey="MatchId", OtherKey="MatchId", CanBeNull=true, Relationship=LinqToDB.Mapping.Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<BookmakerBets> MatchBettings { get; set; }

		#endregion
	}

	[Table(Schema="dbo", Name="PremierLeagueTable")]
	public partial class PremierLeagueTable
	{
		[Column(DataType=LinqToDB.DataType.Guid),  PrimaryKey, NotNull] public Guid TeamId        { get; set; } // uniqueidentifier
		[Column(DataType=LinqToDB.DataType.Int32),             NotNull] public int  SeasonNo      { get; set; } // int
		[Column(DataType=LinqToDB.DataType.Int32),             NotNull] public int  MatchesPlayed { get; set; } // int
		[Column(DataType=LinqToDB.DataType.Int32),             NotNull] public int  Wins          { get; set; } // int
		[Column(DataType=LinqToDB.DataType.Int32),             NotNull] public int  Draws         { get; set; } // int
		[Column(DataType=LinqToDB.DataType.Int32),             NotNull] public int  Defeats       { get; set; } // int
		[Column(DataType=LinqToDB.DataType.Int32),             NotNull] public int  Points        { get; set; } // int
		[Column(DataType=LinqToDB.DataType.Int32),             NotNull] public int  GoalsScored   { get; set; } // int
		[Column(DataType=LinqToDB.DataType.Int32),             NotNull] public int  GoalsConceded { get; set; } // int

		#region Associations

		/// <summary>
		/// FK_PremierLeagueTable_Teams
		/// </summary>
		[Association(ThisKey="TeamId", OtherKey="TeamId", CanBeNull=false, Relationship=LinqToDB.Mapping.Relationship.OneToOne, KeyName="FK_PremierLeagueTable_Teams", BackReferenceName="PremierLeagueTable")]
		public Teams Team { get; set; }

		#endregion
	}

	[Table(Schema="dbo", Name="Teams")]
	public partial class Teams
	{
		[Column(DataType=LinqToDB.DataType.Guid),                 PrimaryKey,  NotNull] public Guid   TeamId               { get; set; } // uniqueidentifier
		[Column(DataType=LinqToDB.DataType.NVarChar, Length=100),              NotNull] public string TeamName             { get; set; } // nvarchar(100)
		[Column(DataType=LinqToDB.DataType.NVarChar, Length=15),               NotNull] public string TeamNameShort        { get; set; } // nvarchar(15)
		[Column(DataType=LinqToDB.DataType.VarChar,  Length=3),                NotNull] public string TeamNameAbbreviation { get; set; } // varchar(3)
		/// <summary>
		/// Element key in external API, to get element statistics
		/// </summary>
		[Column(DataType=LinqToDB.DataType.Int32),                   Nullable         ] public int?   ForeignKey           { get; set; } // int

		#region Associations

		/// <summary>
		/// FK_Matches_Teams_AwayTeam_BackReference
		/// </summary>
		[Association(ThisKey="TeamId", OtherKey="AwayTeamId", CanBeNull=true, Relationship=LinqToDB.Mapping.Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<Matches> MatchesAwayTeams { get; set; }

		/// <summary>
		/// FK_Matches_Teams_HomeTeam_BackReference
		/// </summary>
		[Association(ThisKey="TeamId", OtherKey="HomeTeamId", CanBeNull=true, Relationship=LinqToDB.Mapping.Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<Matches> MatchesHomeTeams { get; set; }

		/// <summary>
		/// FK_PremierLeagueTable_Teams_BackReference
		/// </summary>
		[Association(ThisKey="TeamId", OtherKey="TeamId", CanBeNull=true, Relationship=LinqToDB.Mapping.Relationship.OneToOne, IsBackReference=true)]
		public PremierLeagueTable PremierLeagueTable { get; set; }

		#endregion
	}

	[Table(Schema="dbo", Name="Users")]
	public partial class Users
	{
		[Column(DataType=LinqToDB.DataType.Guid),                 PrimaryKey,  NotNull] public Guid   UserId    { get; set; } // uniqueidentifier
		[Column(DataType=LinqToDB.DataType.NVarChar, Length=100),              NotNull] public string Login     { get; set; } // nvarchar(100)
		[Column(DataType=LinqToDB.DataType.NVarChar, Length=100),    Nullable         ] public string FullName  { get; set; } // nvarchar(100)
		[Column(DataType=LinqToDB.DataType.NVarChar, Length=100),              NotNull] public string Password  { get; set; } // nvarchar(100)
		[Column(DataType=LinqToDB.DataType.Boolean),                           NotNull] public bool   IsAdmin   { get; set; } // bit
		[Column(DataType=LinqToDB.DataType.Boolean),                           NotNull] public bool   IsDeleted { get; set; } // bit

		#region Associations

		/// <summary>
		/// FK_MatchBetting_Users_BackReference
		/// </summary>
		[Association(ThisKey="UserId", OtherKey="UserId", CanBeNull=true, Relationship=LinqToDB.Mapping.Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<BookmakerBets> MatchBettings { get; set; }

		#endregion
	}

	[Table(Schema="dbo", Name="vBookmakerBet", IsView=true)]
	public partial class VBookmakerBet
	{
		[Column(DataType=LinqToDB.DataType.Guid),                 NotNull    ] public Guid     UserId                   { get; set; } // uniqueidentifier
		[Column(DataType=LinqToDB.DataType.Guid),                 NotNull    ] public Guid     MatchId                  { get; set; } // uniqueidentifier
		[Column(DataType=LinqToDB.DataType.Int32),                NotNull    ] public int      SeasonNo                 { get; set; } // int
		[Column(DataType=LinqToDB.DataType.Int32),                NotNull    ] public int      GameweekNo               { get; set; } // int
		[Column(DataType=LinqToDB.DataType.DateTime),             NotNull    ] public DateTime MatchDateTime            { get; set; } // datetime
		[Column(DataType=LinqToDB.DataType.Guid),                 NotNull    ] public Guid     HomeTeamId               { get; set; } // uniqueidentifier
		[Column(DataType=LinqToDB.DataType.Int32),                   Nullable] public int?     HomeForeignKey           { get; set; } // int
		[Column(DataType=LinqToDB.DataType.NVarChar, Length=100), NotNull    ] public string   HomeTeamName             { get; set; } // nvarchar(100)
		[Column(DataType=LinqToDB.DataType.NVarChar, Length=15),  NotNull    ] public string   HomeTeamNameShort        { get; set; } // nvarchar(15)
		[Column(DataType=LinqToDB.DataType.VarChar,  Length=3),   NotNull    ] public string   HomeTeamNameAbbreviation { get; set; } // varchar(3)
		[Column(DataType=LinqToDB.DataType.Guid),                 NotNull    ] public Guid     AwayTeamId               { get; set; } // uniqueidentifier
		[Column(DataType=LinqToDB.DataType.Int32),                   Nullable] public int?     AwayForeignKey           { get; set; } // int
		[Column(DataType=LinqToDB.DataType.NVarChar, Length=100), NotNull    ] public string   AwayTeamName             { get; set; } // nvarchar(100)
		[Column(DataType=LinqToDB.DataType.NVarChar, Length=15),  NotNull    ] public string   AwayTeamNameShort        { get; set; } // nvarchar(15)
		[Column(DataType=LinqToDB.DataType.VarChar,  Length=3),   NotNull    ] public string   AwayTeamNameAbbreviation { get; set; } // varchar(3)
		[Column(DataType=LinqToDB.DataType.Int32),                   Nullable] public int?     HomeTeamScoreActual      { get; set; } // int
		[Column(DataType=LinqToDB.DataType.Int32),                   Nullable] public int?     HomeTeamScoreBet         { get; set; } // int
		[Column(DataType=LinqToDB.DataType.Int32),                   Nullable] public int?     AwayTeamScoreActual      { get; set; } // int
		[Column(DataType=LinqToDB.DataType.Int32),                   Nullable] public int?     AwayTeamScoreBet         { get; set; } // int
		[Column(DataType=LinqToDB.DataType.Int32),                   Nullable] public int?     BettingPoints            { get; set; } // int
	}

	[Table(Schema="dbo", Name="vMatch", IsView=true)]
	public partial class VMatch
	{
		[Column(DataType=LinqToDB.DataType.Guid),                 NotNull    ] public Guid     MatchId                  { get; set; } // uniqueidentifier
		[Column(DataType=LinqToDB.DataType.Int32),                   Nullable] public int?     ForeignKey               { get; set; } // int
		[Column(DataType=LinqToDB.DataType.Int32),                NotNull    ] public int      SeasonNo                 { get; set; } // int
		[Column(DataType=LinqToDB.DataType.Int32),                NotNull    ] public int      GameweekNo               { get; set; } // int
		[Column(DataType=LinqToDB.DataType.DateTime),             NotNull    ] public DateTime MatchDateTime            { get; set; } // datetime
		[Column(DataType=LinqToDB.DataType.Guid),                 NotNull    ] public Guid     HomeTeamId               { get; set; } // uniqueidentifier
		[Column(DataType=LinqToDB.DataType.Int32),                   Nullable] public int?     HomeForeignKey           { get; set; } // int
		[Column(DataType=LinqToDB.DataType.NVarChar, Length=100), NotNull    ] public string   HomeTeamName             { get; set; } // nvarchar(100)
		[Column(DataType=LinqToDB.DataType.NVarChar, Length=15),  NotNull    ] public string   HomeTeamNameShort        { get; set; } // nvarchar(15)
		[Column(DataType=LinqToDB.DataType.VarChar,  Length=3),   NotNull    ] public string   HomeTeamNameAbbreviation { get; set; } // varchar(3)
		[Column(DataType=LinqToDB.DataType.Guid),                 NotNull    ] public Guid     AwayTeamId               { get; set; } // uniqueidentifier
		[Column(DataType=LinqToDB.DataType.Int32),                   Nullable] public int?     AwayForeignKey           { get; set; } // int
		[Column(DataType=LinqToDB.DataType.NVarChar, Length=100), NotNull    ] public string   AwayTeamName             { get; set; } // nvarchar(100)
		[Column(DataType=LinqToDB.DataType.NVarChar, Length=15),  NotNull    ] public string   AwayTeamNameShort        { get; set; } // nvarchar(15)
		[Column(DataType=LinqToDB.DataType.VarChar,  Length=3),   NotNull    ] public string   AwayTeamNameAbbreviation { get; set; } // varchar(3)
		[Column(DataType=LinqToDB.DataType.Int32),                   Nullable] public int?     HomeTeamScore            { get; set; } // int
		[Column(DataType=LinqToDB.DataType.Int32),                   Nullable] public int?     AwayTeamScore            { get; set; } // int
	}

	public static partial class GoldenLeagueDBStoredProcedures
	{
		#region CreateBookmakerBetRecords

		public static int CreateBookmakerBetRecords(this GoldenLeagueDB dataConnection)
		{
			return dataConnection.ExecuteProc("[dbo].[CreateBookmakerBetRecords]");
		}

		#endregion

		#region GetUserBookmakerBet

		public static IEnumerable<VBookmakerBet> GetUserBookmakerBet(this GoldenLeagueDB dataConnection, Guid? @UserId, int? @SeasonNo)
		{
			return dataConnection.QueryProc<VBookmakerBet>("[dbo].[GetUserBookmakerBet]",
				new DataParameter("@UserId",   @UserId,   LinqToDB.DataType.Guid),
				new DataParameter("@SeasonNo", @SeasonNo, LinqToDB.DataType.Int32));
		}

		#endregion

		#region SetBookmakerBetPointsForEmptyBetting

		public static int SetBookmakerBetPointsForEmptyBetting(this GoldenLeagueDB dataConnection)
		{
			return dataConnection.ExecuteProc("[dbo].[SetBookmakerBetPointsForEmptyBetting]");
		}

		#endregion

		#region SpAlterdiagram

		public static int SpAlterdiagram(this GoldenLeagueDB dataConnection, string @diagramname, int? @owner_id, int? @version, byte[] @definition)
		{
			return dataConnection.ExecuteProc("[dbo].[sp_alterdiagram]",
				new DataParameter("@diagramname", @diagramname, LinqToDB.DataType.NVarChar),
				new DataParameter("@owner_id",    @owner_id,    LinqToDB.DataType.Int32),
				new DataParameter("@version",     @version,     LinqToDB.DataType.Int32),
				new DataParameter("@definition",  @definition,  LinqToDB.DataType.VarBinary));
		}

		#endregion

		#region SpCreatediagram

		public static int SpCreatediagram(this GoldenLeagueDB dataConnection, string @diagramname, int? @owner_id, int? @version, byte[] @definition)
		{
			return dataConnection.ExecuteProc("[dbo].[sp_creatediagram]",
				new DataParameter("@diagramname", @diagramname, LinqToDB.DataType.NVarChar),
				new DataParameter("@owner_id",    @owner_id,    LinqToDB.DataType.Int32),
				new DataParameter("@version",     @version,     LinqToDB.DataType.Int32),
				new DataParameter("@definition",  @definition,  LinqToDB.DataType.VarBinary));
		}

		#endregion

		#region SpDropdiagram

		public static int SpDropdiagram(this GoldenLeagueDB dataConnection, string @diagramname, int? @owner_id)
		{
			return dataConnection.ExecuteProc("[dbo].[sp_dropdiagram]",
				new DataParameter("@diagramname", @diagramname, LinqToDB.DataType.NVarChar),
				new DataParameter("@owner_id",    @owner_id,    LinqToDB.DataType.Int32));
		}

		#endregion

		#region SpHelpdiagramdefinition

		public static IEnumerable<SpHelpdiagramdefinitionResult> SpHelpdiagramdefinition(this GoldenLeagueDB dataConnection, string @diagramname, int? @owner_id)
		{
			return dataConnection.QueryProc<SpHelpdiagramdefinitionResult>("[dbo].[sp_helpdiagramdefinition]",
				new DataParameter("@diagramname", @diagramname, LinqToDB.DataType.NVarChar),
				new DataParameter("@owner_id",    @owner_id,    LinqToDB.DataType.Int32));
		}

		public partial class SpHelpdiagramdefinitionResult
		{
			public int?   version    { get; set; }
			public byte[] definition { get; set; }
		}

		#endregion

		#region SpHelpdiagrams

		public static IEnumerable<SpHelpdiagramsResult> SpHelpdiagrams(this GoldenLeagueDB dataConnection, string @diagramname, int? @owner_id)
		{
			return dataConnection.QueryProc<SpHelpdiagramsResult>("[dbo].[sp_helpdiagrams]",
				new DataParameter("@diagramname", @diagramname, LinqToDB.DataType.NVarChar),
				new DataParameter("@owner_id",    @owner_id,    LinqToDB.DataType.Int32));
		}

		public partial class SpHelpdiagramsResult
		{
			public string Database { get; set; }
			public string Name     { get; set; }
			public int    ID       { get; set; }
			public string Owner    { get; set; }
			public int    OwnerID  { get; set; }
		}

		#endregion

		#region SpRenamediagram

		public static int SpRenamediagram(this GoldenLeagueDB dataConnection, string @diagramname, int? @owner_id, string @new_diagramname)
		{
			return dataConnection.ExecuteProc("[dbo].[sp_renamediagram]",
				new DataParameter("@diagramname",     @diagramname,     LinqToDB.DataType.NVarChar),
				new DataParameter("@owner_id",        @owner_id,        LinqToDB.DataType.Int32),
				new DataParameter("@new_diagramname", @new_diagramname, LinqToDB.DataType.NVarChar));
		}

		#endregion
	}

	public static partial class SqlFunctions
	{
		#region FnDiagramobjects

		[Sql.Function(Name="dbo.fn_diagramobjects", ServerSideOnly=true)]
		public static int? FnDiagramobjects()
		{
			throw new InvalidOperationException();
		}

		#endregion
	}

	public static partial class TableExtensions
	{
		public static BookmakerBets Find(this ITable<BookmakerBets> table, Guid UserId, Guid MatchId)
		{
			return table.FirstOrDefault(t =>
				t.UserId  == UserId &&
				t.MatchId == MatchId);
		}

		public static BookmakerLeagues Find(this ITable<BookmakerLeagues> table, Guid LeagueId)
		{
			return table.FirstOrDefault(t =>
				t.LeagueId == LeagueId);
		}

		public static BookmakerLeaguesLCompetitions Find(this ITable<BookmakerLeaguesLCompetitions> table, Guid LeagueId, Guid CompetitionId)
		{
			return table.FirstOrDefault(t =>
				t.LeagueId      == LeagueId &&
				t.CompetitionId == CompetitionId);
		}

		public static Competitions Find(this ITable<Competitions> table, Guid CompetitionId)
		{
			return table.FirstOrDefault(t =>
				t.CompetitionId == CompetitionId);
		}

		public static ConfigDictionary Find(this ITable<ConfigDictionary> table, int ConfigId)
		{
			return table.FirstOrDefault(t =>
				t.ConfigId == ConfigId);
		}

		public static Matches Find(this ITable<Matches> table, Guid MatchId)
		{
			return table.FirstOrDefault(t =>
				t.MatchId == MatchId);
		}

		public static PremierLeagueTable Find(this ITable<PremierLeagueTable> table, Guid TeamId)
		{
			return table.FirstOrDefault(t =>
				t.TeamId == TeamId);
		}

		public static Teams Find(this ITable<Teams> table, Guid TeamId)
		{
			return table.FirstOrDefault(t =>
				t.TeamId == TeamId);
		}

		public static Users Find(this ITable<Users> table, Guid UserId)
		{
			return table.FirstOrDefault(t =>
				t.UserId == UserId);
		}
	}
}

#pragma warning restore 1591
