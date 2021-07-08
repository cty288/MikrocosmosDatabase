using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace MikrocosmosDatabase
{
    public class PlayerTableManager:TableBaseManager<Player> {
        /// <summary>
        /// Search the Player database object from the database, given the display name. (Null of not found)
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<Player> SearchByDisplayName(string displayName) {
            return await SearchByFieldNameUniqueResult("DisplayName", displayName);
        }

        /// <summary>
        /// Search the Player database object from the database, given the user id name. (Null of not found)
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<IList<Player>> SearchByUser(User user) {
            return await SearchByFieldName("Users", user);
        }

        /// <summary>
        /// Authenticate the displayname of the user. Create a new Player for the user if not found the displayname and returns true
        /// Also returns true of the displayname belongs to the user (which means the user already have this Player)
        /// returns false if the Player of this displayname does not belong to the user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="displayName"></param>
        /// <returns></returns>
        public async Task<bool> AuthenticateDisplayName(User user, string displayName) {
            Debug.Log($"Authenticating username {displayName}...");
            Player searchResult = await SearchByDisplayName(displayName);
            if (searchResult == null) {
                await Add(new Player() {DisplayName = displayName, Users = user});
                Debug.Log($"Authenticating new player name {displayName} success! ");
                return true;
            }

            if (searchResult.Users.Id == user.Id) {
                Debug.Log($"Authenticating existing player name {displayName} success! ");
                return true;
            }
            Debug.Log($"Authenticating display name {displayName} failed! It doesn't belong to {user.Username}! ");
            return false;
        }
    }
}
