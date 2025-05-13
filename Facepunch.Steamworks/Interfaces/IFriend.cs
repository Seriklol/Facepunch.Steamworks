using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Steamworks;

public interface IFriend
{
	SteamId Id { get; set; }
	/// <summary>
	/// Returns true if this is the local user
	/// </summary>
	bool IsMe { get; }
	/// <summary>
	/// Return true if this is a friend
	/// </summary>
	bool IsFriend { get; }
	/// <summary>
	/// Returns true if you have this user blocked
	/// </summary>
	bool IsBlocked { get; }
	/// <summary>
	/// Return true if this user is playing the game we're running
	/// </summary>
	bool IsPlayingThisGame { get; }
	/// <summary>
	/// Returns true if this friend is online
	/// </summary>
	bool IsOnline { get; }
	/// <summary>
	/// Returns true if this friend is marked as away
	/// </summary>
	bool IsAway { get; }
	/// <summary>
	/// Returns true if this friend is marked as busy
	/// </summary>
	bool IsBusy { get; }
	/// <summary>
	/// Returns true if this friend is marked as snoozing
	/// </summary>
	bool IsSnoozing { get; }
	Relationship Relationship { get; }
	FriendState State { get; }
	/// <summary>
	/// Returns the player's current Steam name.
	/// <remarks>
	///   Steam returns nicknames here if "Append nicknames to friends' names" is disabled in the Steam client.
	/// </remarks> 
	/// </summary>
	string Name { get; }
	/// <summary>
	/// Returns the nickname that was set for this Steam player, if any.
	/// <remarks>
	///   Steam will never return nicknames if "Append nicknames to friends' names" is disabled in the Steam client.
	/// </remarks>
	/// </summary>
	string Nickname { get; }
	/// <summary>
	/// Returns the player's Steam name history.
	/// </summary>
	IEnumerable<string> NameHistory { get; }
	int SteamLevel { get; }
	Friend.FriendGameInfo? GameInfo { get; }
	string ToString();
	/// <summary>
	/// Sometimes we don't know the user's name. This will wait until we have
	/// downloaded the information on this user.
	/// </summary>
	Task RequestInfoAsync();
	bool IsIn( SteamId group_or_room );
	Task<Data.Image?> GetSmallAvatarAsync();
	Task<Data.Image?> GetMediumAvatarAsync();
	Task<Data.Image?> GetLargeAvatarAsync();
	string GetRichPresence( string key );
	/// <summary>
	/// Invite this friend to the game that we are playing
	/// </summary>
	bool InviteToGame( string Text );
	/// <summary>
	/// Sends a message to a Steam friend. Returns true if success
	/// </summary>
	bool SendMessage( string message );
	/// <summary>
	/// Tries to get download the latest user stats
	/// </summary>
	/// <returns>True if successful, False if failure</returns>
	Task<bool> RequestUserStatsAsync();
	/// <summary>
	/// Gets a user stat. Must call RequestUserStats first.
	/// </summary>
	/// <param name="statName">The name of the stat you want to get</param>
	/// <param name="defult">Will return this value if not available</param>
	/// <returns>The value, or defult if not available</returns>
	float GetStatFloat( string statName, float defult = 0 );
	/// <summary>
	/// Gets a user stat. Must call RequestUserStats first.
	/// </summary>
	/// <param name="statName">The name of the stat you want to get</param>
	/// <param name="defult">Will return this value if not available</param>
	/// <returns>The value, or defult if not available</returns>
	int GetStatInt( string statName, int defult = 0 );
	/// <summary>
	/// Gets a user achievement state. Must call RequestUserStats first.
	/// </summary>
	/// <param name="statName">The name of the achievement you want to get</param>
	/// <param name="defult">Will return this value if not available</param>
	/// <returns>The value, or defult if not available</returns>
	bool GetAchievement( string statName, bool defult = false );
	/// <summary>
	/// Gets a the time this achievement was unlocked.
	/// </summary>
	/// <param name="statName">The name of the achievement you want to get</param>
	/// <returns>The time unlocked. If it wasn't unlocked, or you haven't downloaded the stats yet - will return DateTime.MinValue</returns>
	DateTime GetAchievementUnlockTime( string statName );
}
