using System.Collections.Generic;
using System.Threading.Tasks;

namespace Steamworks.Data;

public interface ILobby
{
	SteamId Id { get; }
	/// <summary>
	/// Gets the number of users in this lobby.
	/// </summary>
	int MemberCount { get; }
	/// <summary>
	/// Returns current members in the lobby. The current user must be in the lobby in order to see the users.
	/// </summary>
	IEnumerable<IFriend> Members { get; }
	/// <summary>
	/// Get all data for this lobby.
	/// </summary>
	IEnumerable<KeyValuePair<string, string>> Data { get; }
	/// <summary>
	/// Max members able to join this lobby. Cannot be over <c>250</c>.
	/// Can only be set by the owner of the lobby.
	/// </summary>
	int MaxMembers { get; set; }
	/// <summary>
	/// Gets or sets the owner of the lobby. You must be the lobby owner to set the owner
	/// </summary>
	IFriend Owner { get; set; }
	/// <summary>
	/// Try to join this room. Will return <see cref="RoomEnter.Success"/> on success,
	/// and anything else is a failure.
	/// </summary>
	Task<RoomEnter> Join();
	/// <summary>
	/// Leave a lobby; this will take effect immediately on the client side
	/// other users in the lobby will be notified by a LobbyChatUpdate_t callback
	/// </summary>
	void Leave();
	/// <summary>
	/// Invite another user to the lobby.
	/// Will return <see langword="true"/> if the invite is successfully sent, whether or not the target responds
	/// returns <see langword="false"/> if the local user is not connected to the Steam servers
	/// </summary>
	bool InviteFriend( SteamId steamid );
	/// <summary>
	/// Get data associated with this lobby.
	/// </summary>
	string GetData( string key );
	/// <summary>
	/// Set data associated with this lobby.
	/// </summary>
	bool SetData( string key, string value );
	/// <summary>
	/// Removes a metadata key from the lobby.
	/// </summary>
	bool DeleteData( string key );
	/// <summary>
	/// Gets per-user metadata for someone in this lobby.
	/// </summary>
	string GetMemberData( IFriend member, string key );
	/// <summary>
	/// Sets per-user metadata (for the local user implicitly).
	/// </summary>
	void SetMemberData( string key, string value );
	/// <summary>
	/// Sends a string to the chat room.
	/// </summary>
	bool SendChatString( string message );
	/// <summary>
	/// Sends bytes to the chat room.
	/// </summary>
	unsafe bool SendChatBytes( byte[] data );
	/// <summary>
	/// Refreshes metadata for a lobby you're not necessarily in right now.
	/// <para>
	/// You never do this for lobbies you're a member of, only if your
	/// this will send down all the metadata associated with a lobby.
	/// This is an asynchronous call.
	/// Returns <see langword="false"/> if the local user is not connected to the Steam servers.
	/// Results will be returned by a LobbyDataUpdate_t callback.
	/// If the specified lobby doesn't exist, LobbyDataUpdate_t::m_bSuccess will be set to <see langword="false"/>.
	/// </para>
	/// </summary>
	bool Refresh();
	/// <summary>
	/// Sets the lobby as public.
	/// </summary>
	bool SetPublic();
	/// <summary>
	/// Sets the lobby as private.
	/// </summary>
	bool SetPrivate();
	/// <summary>
	/// Sets the lobby as invisible.
	/// </summary>
	bool SetInvisible();
	/// <summary>
	/// Sets the lobby as friends only.
	/// </summary>
	bool SetFriendsOnly();
	/// <summary>
	/// Set whether or not the lobby can be joined.
	/// </summary>
	/// <param name="b">Whether or not the lobby can be joined.</param>
	bool SetJoinable( bool b );
	/// <summary>
	/// [SteamID variant]
	/// Allows the owner to set the game server associated with the lobby. Triggers the
	/// Steammatchmaking.OnLobbyGameCreated event.
	/// </summary>
	void SetGameServer( SteamId steamServer );
	/// <summary>
	/// [IP/Port variant]
	/// Allows the owner to set the game server associated with the lobby. Triggers the
	/// Steammatchmaking.OnLobbyGameCreated event.
	/// </summary>
	void SetGameServer( string ip, ushort port );
	/// <summary>
	/// Gets the details of the lobby's game server, if set. Returns true if the lobby is
	/// valid and has a server set, otherwise returns false.
	/// </summary>
	bool GetGameServer( ref uint ip, ref ushort port, ref SteamId serverId );
	/// <summary>
	/// Check if the specified SteamId owns the lobby.
	/// </summary>
	bool IsOwnedBy( SteamId k );
}
