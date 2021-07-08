using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MikrocosmosDatabase
{
    public class UserTableManager : TableBaseManager<User> {
        /// <summary>
        /// Search the User object from the database, given the username. (Null of not found)
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<User> SearchUsername(string username) {
            return await SearchByFieldNameUniqueResult("Username", username);
        }

        /// <summary>
        /// Search the User object from the database, given the playfabid. (Null of not found)
        /// </summary>
        /// <param name="playfabid"></param>
        /// <returns></returns>
        public async Task<User> SearchPlayfabid(string playfabid) {
            return await SearchByFieldNameUniqueResult("Playfabid", playfabid);
        }

        /// <summary>
        /// Authenticate the username, playfabid, and password of a player. Add them to the database if not found.
        /// Return the added User database object. Return null if username and playfabid do not match the same user.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="playfabid"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<User> AuthenticateUsernamePlayfabid(string username, string playfabid,string password) {
            if (await SearchUsername(username) == null && await SearchPlayfabid(playfabid) == null) {
                //new User
                await Add(new User() {Username = username, Playfabid = playfabid, Password = password, LastLoginTime = DateTime.Now});
                return await SearchUsername(username);
            }


            User oldUserSearchResult = await SearchByFieldNamesUniqueResult(new string[] {"Username", "Playfabid"}, new[] {username, playfabid});
            if (oldUserSearchResult == null) {
                return null;
            }

            oldUserSearchResult.Password = password;
            oldUserSearchResult.LastLoginTime=DateTime.Now;
            
            await Update(oldUserSearchResult);
            return oldUserSearchResult;
        }
    }

}
