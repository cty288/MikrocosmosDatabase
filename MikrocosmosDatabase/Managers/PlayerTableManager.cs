using System;
using System.Collections.Generic;


namespace MikrocosmosDatabase
{
    class PlayerTableManager:TableBaseManager<Player> {
        /// <summary>
        /// Search the Player database object from the database, given the display name. (Null of not found)
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Player SearchByDisplayName(string displayName) {
            return SearchByFieldNameUniqueResult("DisplayName", displayName);
        }

        /// <summary>
        /// Search the Player database object from the database, given the user id name. (Null of not found)
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public IList<Player> SearchByUser(User user) {
            return SearchByFieldName("Users", user);
        }

        /// <summary>
        /// Authenticate the displayname of the user. Create a new Player for the user if not found the displayname and returns true
        /// Also returns true of the displayname belongs to the user (which means the user already have this Player)
        /// returns false if the Player of this displayname does not belong to the user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="displayName"></param>
        /// <returns></returns>
        public bool AuthenticateDisplayName(User user, string displayName) {
            Player searchResult = SearchByDisplayName(displayName);
            if (searchResult == null) {
                Add(new Player() {DisplayName = displayName, Users = user});
                return true;
            }

            if (searchResult.Users.Id == user.Id) {
                return true;
            }

            return false;
        }
    }
}
