using System.Collections;
using System.Collections.Generic;


namespace MikrocosmosDatabase
{
    public class UserTableManager : TableBaseManager<User> {
        /// <summary>
        /// Search the User object from the database, given the username. (Null of not found)
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public User SearchUsername(string username) {
            return SearchByFieldNameUniqueResult("Username", username);
        }

        /// <summary>
        /// Search the User object from the database, given the playfabid. (Null of not found)
        /// </summary>
        /// <param name="playfabid"></param>
        /// <returns></returns>
        public User SearchPlayfabid(string playfabid) {
            return SearchByFieldNameUniqueResult("Playfabid", playfabid);
        }

        /// <summary>
        /// Authenticate the username, playfabid, and password of a player. Add them to the database if not found.
        /// Return the added User database object. Return null if username and playfabid do not match the same user.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="playfabid"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User AuthenticateUsernamePlayfabid(string username, string playfabid,string password) {
            if (SearchUsername(username) == null && SearchPlayfabid(playfabid) == null) {
                //new User
                Add(new User() {Username = username, Playfabid = playfabid, Password = password});
                return SearchUsername(username);
            }


            return SearchByFieldNamesUniqueResult(new string[] {"Username", "Playfabid"}, new[] {username, playfabid});
        }
    }

}
