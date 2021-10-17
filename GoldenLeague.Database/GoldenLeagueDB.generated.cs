﻿//---------------------------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated by T4Model template for T4 (https://github.com/linq2db/linq2db).
//    Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//---------------------------------------------------------------------------------------------------

#pragma warning disable 1591

using System;
using System.Collections.Generic;
using System.Linq;

using LinqToDB;
using LinqToDB.Configuration;
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
		public ITable<MatchBetting>  MatchBetting  { get { return this.GetTable<MatchBetting>(); } }
		public ITable<Matches>       Matches       { get { return this.GetTable<Matches>(); } }
		public ITable<Teams>         Teams         { get { return this.GetTable<Teams>(); } }
		public ITable<Users>         Users         { get { return this.GetTable<Users>(); } }
		public ITable<VMatchBetting> VMatchBetting { get { return this.GetTable<VMatchBetting>(); } }

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

	[Table(Schema="dbo", Name="MatchBetting")]
	public partial class MatchBetting
	{
		[PrimaryKey(1), NotNull    ] public Guid UserId        { get; set; } // uniqueidentifier
		[PrimaryKey(2), NotNull    ] public Guid MatchId       { get; set; } // uniqueidentifier
		[Column,           Nullable] public int? HomeTeamScore { get; set; } // int
		[Column,           Nullable] public int? AwayTeamScore { get; set; } // int

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

	[Table(Schema="dbo", Name="Matches")]
	public partial class Matches
	{
		[PrimaryKey, NotNull    ] public Guid     MatchId       { get; set; } // uniqueidentifier
		/// <summary>
		/// Sezon pilkarski (np. 2022 oznacza sezon 2021/22)
		/// </summary>
		[Column,     NotNull    ] public int      SeasonNo      { get; set; } // int
		/// <summary>
		/// Numer kolejki ligowej
		/// </summary>
		[Column,     NotNull    ] public int      GameweekNo    { get; set; } // int
		[Column,     NotNull    ] public DateTime MatchDateTime { get; set; } // datetime
		[Column,     NotNull    ] public Guid     HomeTeamId    { get; set; } // uniqueidentifier
		[Column,        Nullable] public int?     HomeTeamScore { get; set; } // int
		[Column,     NotNull    ] public Guid     AwayTeamId    { get; set; } // uniqueidentifier
		[Column,        Nullable] public int?     AwayTeamScore { get; set; } // int

		#region Associations

		/// <summary>
		/// FK_Matches_Teams_AwayTeam
		/// </summary>
		[Association(ThisKey="AwayTeamId", OtherKey="TeamId", CanBeNull=false, Relationship=LinqToDB.Mapping.Relationship.ManyToOne, KeyName="FK_Matches_Teams_AwayTeam", BackReferenceName="MatchesAwayTeams")]
		public Teams AwayTeam { get; set; }

		/// <summary>
		/// FK_Matches_Teams_HomeTeam
		/// </summary>
		[Association(ThisKey="HomeTeamId", OtherKey="TeamId", CanBeNull=false, Relationship=LinqToDB.Mapping.Relationship.ManyToOne, KeyName="FK_Matches_Teams_HomeTeam", BackReferenceName="MatchesHomeTeams")]
		public Teams HomeTeam { get; set; }

		/// <summary>
		/// FK_MatchBetting_Matches_BackReference
		/// </summary>
		[Association(ThisKey="MatchId", OtherKey="MatchId", CanBeNull=true, Relationship=LinqToDB.Mapping.Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<MatchBetting> MatchBettings { get; set; }

		#endregion
	}

	[Table(Schema="dbo", Name="Teams")]
	public partial class Teams
	{
		[PrimaryKey, NotNull] public Guid   TeamId               { get; set; } // uniqueidentifier
		[Column,     NotNull] public string TeamName             { get; set; } // nvarchar(100)
		[Column,     NotNull] public string TeamNameShort        { get; set; } // nvarchar(15)
		[Column,     NotNull] public string TeamNameAbbreviation { get; set; } // varchar(3)

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

		#endregion
	}

	[Table(Schema="dbo", Name="Users")]
	public partial class Users
	{
		[PrimaryKey, NotNull    ] public Guid   UserId    { get; set; } // uniqueidentifier
		[Column,     NotNull    ] public string Login     { get; set; } // nvarchar(100)
		[Column,        Nullable] public string FullName  { get; set; } // nvarchar(100)
		[Column,     NotNull    ] public string Password  { get; set; } // nvarchar(100)
		[Column,     NotNull    ] public bool   IsAdmin   { get; set; } // bit
		[Column,     NotNull    ] public bool   IsDeleted { get; set; } // bit

		#region Associations

		/// <summary>
		/// FK_MatchBetting_Users_BackReference
		/// </summary>
		[Association(ThisKey="UserId", OtherKey="UserId", CanBeNull=true, Relationship=LinqToDB.Mapping.Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<MatchBetting> MatchBettings { get; set; }

		#endregion
	}

	[Table(Schema="dbo", Name="vMatchBetting", IsView=true)]
	public partial class VMatchBetting
	{
		[Column, NotNull    ] public Guid     UserId              { get; set; } // uniqueidentifier
		[Column, NotNull    ] public Guid     MatchId             { get; set; } // uniqueidentifier
		[Column, NotNull    ] public int      SeasonNo            { get; set; } // int
		[Column, NotNull    ] public int      GameweekNo          { get; set; } // int
		[Column, NotNull    ] public DateTime MatchDateTime       { get; set; } // datetime
		[Column, NotNull    ] public Guid     HomeTeamId          { get; set; } // uniqueidentifier
		[Column, NotNull    ] public string   HomeTeamName        { get; set; } // nvarchar(15)
		[Column,    Nullable] public int?     HomeTeamScoreActual { get; set; } // int
		[Column,    Nullable] public int?     HomeTeamScoreBet    { get; set; } // int
		[Column, NotNull    ] public Guid     AwayTeamId          { get; set; } // uniqueidentifier
		[Column, NotNull    ] public string   AwayTeamName        { get; set; } // nvarchar(15)
		[Column,    Nullable] public int?     AwayTeamScoreActual { get; set; } // int
		[Column,    Nullable] public int?     AwayTeamScoreBet    { get; set; } // int
	}

	public static partial class TableExtensions
	{
		public static MatchBetting Find(this ITable<MatchBetting> table, Guid UserId, Guid MatchId)
		{
			return table.FirstOrDefault(t =>
				t.UserId  == UserId &&
				t.MatchId == MatchId);
		}

		public static Matches Find(this ITable<Matches> table, Guid MatchId)
		{
			return table.FirstOrDefault(t =>
				t.MatchId == MatchId);
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
