using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using KeepThingsAPI.Models;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KeepThingsAPI.Controllers
{
    public class SqlConnectionController
    {
        private MySqlConnection cnn;
        private MySqlDataReader reader;
        public void InitSqlConnection()
        {
            string connectionString;
            connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=keepthings;";
            this.cnn = new MySqlConnection(connectionString);
        }
        #region User
        public string User_getUser(string id)
        {
            if (cnn == null) InitSqlConnection();
            string query = "SELECT * FROM user Where Auth0_ID = '" + id + "'";;
            MySqlCommand command = new MySqlCommand(query, cnn);
            User user = new User();
            cnn.Open();

            reader = command.ExecuteReader();
            if (!reader.HasRows) return null;
            while (reader.Read())
            {
                user.id = reader.GetInt32(0);
                user.Auth0_id = reader.GetString(1);
                user.name = reader.GetString(2);
                user.first_name = reader.GetString(3);
                user.password = reader.GetString(4);
                user.email = reader.GetString(5);
                user.tel_nr = reader.GetString(6);
                user.username = reader.GetString(7);
                user.type = reader.GetString(8);
                user.verified = reader.GetBoolean(9);
            }
            var json = JsonConvert.SerializeObject(user);
            cnn.Close();
            return json.ToString();
        }
        public string User_getUsers()
        {
            if (cnn == null) InitSqlConnection();
            string query = "SELECT * FROM user";
            MySqlCommand command = new MySqlCommand(query, cnn);
            List<User> users = new List<User>();
            cnn.Open();
            reader = command.ExecuteReader();
            if (!reader.HasRows) return null;
            while (reader.Read())
            {
                User user = new User();
                user.id = reader.GetInt32(0);
                user.Auth0_id = reader.GetString(1);
                user.name = reader.GetString(2);
                user.first_name = reader.GetString(3);
                user.password = reader.GetString(4);
                user.email = reader.GetString(5);
                user.tel_nr = reader.GetString(6);
                user.username = reader.GetString(7);
                user.type = reader.GetString(8);
                user.verified = reader.GetBoolean(9);
                users.Add(user);
            }
            var json = JsonConvert.SerializeObject(users);
            cnn.Close();
            return json.ToString();
        }
        public string User_postUser(User user)
        {
            if (cnn == null) InitSqlConnection();           
            string query = "INSERT INTO user (Auth0_ID,name,first_name,password,email,tel_nr,username,type,verified) " +
                "VALUES ('" + user.Auth0_id + "','" + user.name + "','" + user.first_name + "','" + user.password + "','" + user.email + "','" + user.tel_nr + "','" + user.username + "','" + user.type + "','" + user.verified + "');";
            MySqlCommand command = new MySqlCommand(query, cnn);
            cnn.Open();
            try
            {
                command.ExecuteNonQuery();
                command = new MySqlCommand("SELECT * FROM user WHERE id = LAST_INSERT_ID()", cnn);
                user = new User();
                if (cnn.State == System.Data.ConnectionState.Closed) cnn.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    user.id = reader.GetInt32(0);
                    user.Auth0_id = reader.GetString(1);
                    user.name = reader.GetString(2);
                    user.first_name = reader.GetString(3);
                    user.password = reader.GetString(4);
                    user.email = reader.GetString(5);
                    user.tel_nr = reader.GetString(6);
                    user.username = reader.GetString(7);
                    user.type = reader.GetString(8);
                    user.verified = reader.GetBoolean(9);
                }
                var userJson = JsonConvert.SerializeObject(user);
                return userJson.ToString();
            }
            catch (Exception ex)
            {
                return "Error with execuing the insert: " + ex.Message;
            }
            finally
            {
                if (cnn.State == System.Data.ConnectionState.Open) cnn.Close();
            }
        }
        public string User_putUser(string id, User user)
        {
            if (cnn == null) InitSqlConnection();
            string query = "UPDATE user " +
                " SET Auth0_ID = '" + user.Auth0_id + "', name = '" + user.name + "', first_name = '" + user.first_name + "', password = '" + user.password + "', email = '" + user.email + "',tel_nr = '" + user.tel_nr + "', username = '" + user.username + "', type = '" + user.type + "',verified = '" + user.verified + "'" +
                " WHERE Auth0_ID = '" + id + "'";
            MySqlCommand command = new MySqlCommand(query, cnn);
            cnn.Open();
            try
            {
                command.ExecuteNonQuery();
                command = new MySqlCommand("SELECT * FROM user WHERE Auth0_ID = '" + user.Auth0_id + "'", cnn);
                user = new User();
                if (cnn.State == System.Data.ConnectionState.Closed) cnn.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    user.id = reader.GetInt32(0);
                    user.Auth0_id = reader.GetString(1);
                    user.name = reader.GetString(2);
                    user.first_name = reader.GetString(3);
                    user.password = reader.GetString(4);
                    user.email = reader.GetString(5);
                    user.tel_nr = reader.GetString(6);
                    user.username = reader.GetString(7);
                    user.type = reader.GetString(8);
                    user.verified = reader.GetBoolean(9);
                }
                var userJson = JsonConvert.SerializeObject(user);
                return userJson.ToString();
            }
            catch (Exception ex)
            {
                return "Error with execuing the insert: " + ex.Message;
            }
            finally
            {
                if (cnn.State == System.Data.ConnectionState.Open) cnn.Close();
            }
        }
        public string User_deleteUser(int id)
        {
            if (cnn == null) InitSqlConnection();
            string query = "DELETE FROM user WHERE id = " + id;
            MySqlCommand command = new MySqlCommand(query, cnn);
            cnn.Open();
            try
            {
                command.ExecuteNonQuery();
                return "done";
            }
            catch (Exception ex)
            {
                return "Error with execuing the delete: " + ex.Message;
            }
            finally
            {
                if (cnn.State == System.Data.ConnectionState.Open) cnn.Close();
            }
        }
        #endregion




        #region Useritem
        public String UserItem_getUserItem(int id)
        {
            if (cnn == null) InitSqlConnection();
            string query = "SELECT * FROM user_items Where id = " + id;
            MySqlCommand command = new MySqlCommand(query, cnn);
            UserItem userItem = new UserItem();
            if (cnn.State == System.Data.ConnectionState.Closed) cnn.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                userItem.id = reader.GetInt32(0);
                userItem.item_name = reader.GetString(1);
                userItem.item_desc = reader.GetString(2);
                userItem.user_id = reader.GetInt32(3);
                userItem.borrower = reader.GetString(4);
                userItem.date_from = reader.GetMySqlDateTime(5).Year + "-" + reader.GetMySqlDateTime(5).Month + "-" + reader.GetMySqlDateTime(5).Day;
                userItem.date_to = reader.GetMySqlDateTime(6).Year + "-" + reader.GetMySqlDateTime(6).Month + "-" + reader.GetMySqlDateTime(6).Day;
            }
            var json = JsonConvert.SerializeObject(userItem);
            cnn.Close();
            return json.ToString();
        }
        public String UserItem_getUserItems()
        {
            if (cnn == null) InitSqlConnection();
            string query = "SELECT * FROM user_items";
            MySqlCommand command = new MySqlCommand(query, cnn);
            List<UserItem> userItems = new List<UserItem>();
            cnn.Open();

            reader = command.ExecuteReader();
            while (reader.Read())
            {
                UserItem userItem = new UserItem();
                userItem.id = reader.GetInt32(0);
                userItem.item_name = reader.GetString(1);
                userItem.item_desc = reader.GetString(2);
                userItem.user_id = reader.GetInt32(3);
                userItem.borrower = reader.GetString(4);
                userItem.date_from = reader.GetMySqlDateTime(5).Year + "-" + reader.GetMySqlDateTime(5).Month + "-" + reader.GetMySqlDateTime(5).Day;
                userItem.date_to = reader.GetMySqlDateTime(6).Year + "-" + reader.GetMySqlDateTime(6).Month + "-" + reader.GetMySqlDateTime(6).Day;
                userItems.Add(userItem);
            }
            var json = JsonConvert.SerializeObject(userItems);
            cnn.Close();
            return json.ToString();
        }
        public string UserItem_postUserItem(UserItem userItem)
        {
            if (cnn == null) InitSqlConnection();
            string query = "INSERT INTO user_items (item_name,item_desc,owner,borrower,date_from,date_to) " +
                "VALUES ('" + userItem.item_name + "','" + userItem.item_desc + "'," + userItem.user_id + ",'" + userItem.borrower + "','" + userItem.date_from + "','" + userItem.date_to + "');";
            MySqlCommand command = new MySqlCommand(query, cnn);
            cnn.Open();
            try
            {
                command.ExecuteNonQuery();
                command = new MySqlCommand("SELECT * FROM user_items WHERE id = LAST_INSERT_ID()", cnn);
                userItem = new UserItem();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    userItem.id = reader.GetInt32(0);
                    userItem.item_name = reader.GetString(1);
                    userItem.item_desc = reader.GetString(2);
                    userItem.user_id = reader.GetInt32(3);
                    userItem.borrower = reader.GetString(4);
                    userItem.date_from = reader.GetMySqlDateTime(5).Year + "-" + reader.GetMySqlDateTime(5).Month + "-" + reader.GetMySqlDateTime(5).Day;
                    userItem.date_to = reader.GetMySqlDateTime(6).Year + "-" + reader.GetMySqlDateTime(6).Month + "-" + reader.GetMySqlDateTime(6).Day;
                }
                var json = JsonConvert.SerializeObject(userItem);
                return json.ToString();
            }
            catch (Exception ex)
            {
                return "Error with execuing the insert: " + ex.Message;
            }
            finally
            {
                if (cnn.State == System.Data.ConnectionState.Open) cnn.Close();
            }
        }
        public string UserItem_putUserItem(int id, UserItem userItem)
        {
            if (cnn == null) InitSqlConnection();
            string query = "UPDATE user_items " +
                " SET item_name = '" + userItem.item_name + "',item_desc = '" + userItem.item_desc + "',owner = " + userItem.user_id + ",borrower = '" + userItem.borrower + "',date_from = '" + userItem.date_from + "',date_to = '" + userItem.date_to + "' " +
                " WHERE id = " + id;
            MySqlCommand command = new MySqlCommand(query, cnn);
            cnn.Open();
            try
            {
                command.ExecuteNonQuery();
                command = new MySqlCommand("SELECT * FROM user_items WHERE id = " + id, cnn);
                userItem = new UserItem();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    userItem.id = reader.GetInt32(0);
                    userItem.item_name = reader.GetString(1);
                    userItem.item_desc = reader.GetString(2);
                    userItem.user_id = reader.GetInt32(3);
                    userItem.borrower = reader.GetString(4);
                    userItem.date_from = reader.GetMySqlDateTime(5).Year + "-" + reader.GetMySqlDateTime(5).Month + "-" + reader.GetMySqlDateTime(5).Day;
                    userItem.date_to = reader.GetMySqlDateTime(6).Year + "-" + reader.GetMySqlDateTime(6).Month + "-" + reader.GetMySqlDateTime(6).Day;
                }
                var json = JsonConvert.SerializeObject(userItem);
                return json.ToString();
            }
            catch (Exception ex)
            {
                return "Error with execuing the insert: " + ex.Message;
            }
            finally
            {
                if (cnn.State == System.Data.ConnectionState.Open) cnn.Close();
            }
        }
        public string UserItem_deleteUserItem(int id)
        {
            if (cnn == null) InitSqlConnection();
            string query = "DELETE FROM user_items WHERE id = " + id;
            MySqlCommand command = new MySqlCommand(query, cnn);
            cnn.Open();
            try
            {
                command.ExecuteNonQuery();
                return "done";
            }
            catch (Exception ex)
            {
                return "Error with execuing the delete: " + ex.Message;
            }
            finally
            {
                if (cnn.State == System.Data.ConnectionState.Open) cnn.Close();
            }
        }
        #endregion




        #region MarketplaceItem
        public String MarketplaceItem_getMarketplaceItem(int id)
        {
            if (cnn == null) InitSqlConnection();
            string query = "SELECT * FROM marketplace_items Where id = " + id;
            MySqlCommand command = new MySqlCommand(query, cnn);
            MarketplaceItem marketplaceItem = new MarketplaceItem();
            if (cnn.State == System.Data.ConnectionState.Closed) cnn.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                marketplaceItem.id = reader.GetInt32(0);
                marketplaceItem.item_name = reader.GetString(1);
                marketplaceItem.item_desc = reader.GetString(2);
                marketplaceItem.user_id = reader.GetInt32(3);
                marketplaceItem.borrower = reader.GetString(4);
                marketplaceItem.date_from = reader.GetMySqlDateTime(5).Year + "-" + reader.GetMySqlDateTime(5).Month + "-" + reader.GetMySqlDateTime(5).Day;
                marketplaceItem.date_to = reader.GetMySqlDateTime(6).Year + "-" + reader.GetMySqlDateTime(6).Month + "-" + reader.GetMySqlDateTime(6).Day;
            }
            var json = JsonConvert.SerializeObject(marketplaceItem);
            cnn.Close();
            return json.ToString();
        }
        public String MarketplaceItem_getMarketplaceItems()
        {
            if (cnn == null) InitSqlConnection();
            string query = "SELECT * FROM marketplace_items";
            MySqlCommand command = new MySqlCommand(query, cnn);
            List<MarketplaceItem> marketplaceItems = new List<MarketplaceItem>();
            cnn.Open();

            reader = command.ExecuteReader();
            while (reader.Read())
            {
                MarketplaceItem marketplaceItem = new MarketplaceItem();
                marketplaceItem.id = reader.GetInt32(0);
                marketplaceItem.item_name = reader.GetString(1);
                marketplaceItem.item_desc = reader.GetString(2);
                marketplaceItem.user_id = reader.GetInt32(3);
                marketplaceItem.borrower = reader.GetString(4);
                marketplaceItem.date_from = reader.GetMySqlDateTime(5).Year + "-" + reader.GetMySqlDateTime(5).Month + "-" + reader.GetMySqlDateTime(5).Day;
                marketplaceItem.date_to = reader.GetMySqlDateTime(6).Year + "-" + reader.GetMySqlDateTime(6).Month + "-" + reader.GetMySqlDateTime(6).Day;
                marketplaceItems.Add(marketplaceItem);
            }
            var json = JsonConvert.SerializeObject(marketplaceItems);
            cnn.Close();
            return json.ToString();
        }
        public string MarketplaceItem_postMarketplaceItem(MarketplaceItem marketplaceItem)
        {
            if (cnn == null) InitSqlConnection();
            string query = "INSERT INTO marketplace_items (item_name,item_desc,owner,borrower,date_from,date_to) " +
                "VALUES ('" + marketplaceItem.item_name + "','" + marketplaceItem.item_desc + "'," + marketplaceItem.user_id + ",'" + marketplaceItem.borrower + "','" + marketplaceItem.date_from + "','" + marketplaceItem.date_to + "');";
            MySqlCommand command = new MySqlCommand(query, cnn);
            cnn.Open();
            try
            {
                command.ExecuteNonQuery();
                command = new MySqlCommand("SELECT * FROM marketplace_items WHERE id = LAST_INSERT_ID()", cnn);
                marketplaceItem = new MarketplaceItem();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    marketplaceItem.id = reader.GetInt32(0);
                    marketplaceItem.item_name = reader.GetString(1);
                    marketplaceItem.item_desc = reader.GetString(2);
                    marketplaceItem.user_id = reader.GetInt32(3);
                    marketplaceItem.borrower = reader.GetString(4);
                    marketplaceItem.date_from = reader.GetMySqlDateTime(5).Year + "-" + reader.GetMySqlDateTime(5).Month + "-" + reader.GetMySqlDateTime(5).Day;
                    marketplaceItem.date_to = reader.GetMySqlDateTime(6).Year + "-" + reader.GetMySqlDateTime(6).Month + "-" + reader.GetMySqlDateTime(6).Day;
                }
                var json = JsonConvert.SerializeObject(marketplaceItem);
                return json.ToString();
            }
            catch (Exception ex)
            {
                return "Error with execuing the insert: " + ex.Message;
            }
            finally
            {
                if (cnn.State == System.Data.ConnectionState.Open) cnn.Close();
            }
        }
        public string MarketplaceItem_putMarketplaceItem(int id, MarketplaceItem marketplaceItem)
        {
            if (cnn == null) InitSqlConnection();
            string query = "UPDATE marketplace_items " +
                " SET item_name = '" + marketplaceItem.item_name + "',item_desc = '" + marketplaceItem.item_desc + "',owner = " + marketplaceItem.user_id + ",borrower = '" + marketplaceItem.borrower + "',date_from = '" + marketplaceItem.date_from + "',date_to = '" + marketplaceItem.date_to + "' " +
                " WHERE id = " + id;
            MySqlCommand command = new MySqlCommand(query, cnn);
            cnn.Open();
            try
            {
                command.ExecuteNonQuery();
                command = new MySqlCommand("SELECT * FROM marketplace_items WHERE id = " + id, cnn);
                marketplaceItem = new MarketplaceItem();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    marketplaceItem.id = reader.GetInt32(0);
                    marketplaceItem.item_name = reader.GetString(1);
                    marketplaceItem.item_desc = reader.GetString(2);
                    marketplaceItem.user_id = reader.GetInt32(3);
                    marketplaceItem.borrower = reader.GetString(4);
                    marketplaceItem.date_from = reader.GetMySqlDateTime(5).Year + "-" + reader.GetMySqlDateTime(5).Month + "-" + reader.GetMySqlDateTime(5).Day;
                    marketplaceItem.date_to = reader.GetMySqlDateTime(6).Year + "-" + reader.GetMySqlDateTime(6).Month + "-" + reader.GetMySqlDateTime(6).Day;
                }
                var json = JsonConvert.SerializeObject(marketplaceItem);
                return json.ToString();
            }
            catch (Exception ex)
            {
                return "Error with execuing the insert: " + ex.Message;
            }
            finally
            {
                if (cnn.State == System.Data.ConnectionState.Open) cnn.Close();
            }
        }
        public string MarketplaceItem_deleteMarketplaceItem(int id)
        {
            if (cnn == null) InitSqlConnection();
            string query = "DELETE FROM marketplace_items WHERE id = " + id;
            MySqlCommand command = new MySqlCommand(query, cnn);
            cnn.Open();
            try
            {
                command.ExecuteNonQuery();
                return "done";
            }
            catch (Exception ex)
            {
                return "Error with execuing the delete: " + ex.Message;
            }
            finally
            {
                if (cnn.State == System.Data.ConnectionState.Open) cnn.Close();
            }
        }
        #endregion




        #region Messages
        
        public String Message_getMessages(int chat_id)
        {
            if (cnn == null) InitSqlConnection();
            string query = "SELECT * FROM messages WHERE chat_ID = " + chat_id;
            MySqlCommand command = new MySqlCommand(query, cnn);
            List<Message> messages = new List<Message>();
            cnn.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Message message = new Message();
                message.id = reader.GetInt32(0);
                message.chat_id = reader.GetInt32(1);
                message.message = reader.GetString(2);
                message.sender_id = reader.GetInt32(3);
                message.timestamp = reader.GetMySqlDateTime(4).Year + "-" + reader.GetMySqlDateTime(4).Month + "-" + reader.GetMySqlDateTime(4).Day + " " + reader.GetMySqlDateTime(4).Hour + ":" + reader.GetMySqlDateTime(4).Minute + ":" + reader.GetMySqlDateTime(4).Second;
                messages.Add(message);
            }
            var json = JsonConvert.SerializeObject(messages);
            cnn.Close();
            return json.ToString();
        }
        public string Message_postMessages(Message message)
        {
            if (cnn == null) InitSqlConnection();
            string query = "INSERT INTO messages (chat_ID,message,sender_ID) " +
                "VALUES (" + message.chat_id + ",'" + message.message + "'," + message.sender_id + ");";
            MySqlCommand command = new MySqlCommand(query, cnn);
            cnn.Open();
            try
            {
                command.ExecuteNonQuery();
                command = new MySqlCommand("SELECT * FROM messages WHERE ID = LAST_INSERT_ID()", cnn);
                message = new Message();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    message.id = reader.GetInt32(0);
                    message.chat_id = reader.GetInt32(1);
                    message.message = reader.GetString(2);
                    message.sender_id = reader.GetInt32(3);
                    message.timestamp = reader.GetMySqlDateTime(4).Year + "-" + reader.GetMySqlDateTime(4).Month + "-" + reader.GetMySqlDateTime(4).Day + " " + reader.GetMySqlDateTime(4).Hour + ":" + reader.GetMySqlDateTime(4).Minute + ":" + reader.GetMySqlDateTime(4).Second;
                }
                var json = JsonConvert.SerializeObject(message);
                return json.ToString();
            }
            catch (Exception ex)
            {
                return "Error with execuing the insert: " + ex.Message;
            }
            finally
            {
                if (cnn.State == System.Data.ConnectionState.Open) cnn.Close();
            }
        }
        public string Message_putMessage(int id, Message message)
        {
            if (cnn == null) InitSqlConnection();
            string query = "UPDATE messages " +
                " SET chat_ID = " + message.chat_id + ",message = '" + message.message + "',sender_ID = " + message.sender_id + ", sent_timestamp = '" + message.timestamp + "'" + 
                " WHERE id = " + id;
            MySqlCommand command = new MySqlCommand(query, cnn);
            cnn.Open();
            try
            {
                command.ExecuteNonQuery();
                command = new MySqlCommand("SELECT * FROM messages WHERE id = " + id, cnn);
                message = new Message();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    message.id = reader.GetInt32(0);
                    message.chat_id = reader.GetInt32(1);
                    message.message = reader.GetString(2);
                    message.sender_id = reader.GetInt32(3);
                    message.timestamp = reader.GetMySqlDateTime(4).Year + "-" + reader.GetMySqlDateTime(4).Month + "-" + reader.GetMySqlDateTime(4).Day + " " + reader.GetMySqlDateTime(4).Hour + ":" + reader.GetMySqlDateTime(4).Minute + ":" + reader.GetMySqlDateTime(4).Second;
                }
                var json = JsonConvert.SerializeObject(message);
                return json.ToString();
            }
            catch (Exception ex)
            {
                return "Error with execuing the insert: " + ex.Message;
            }
            finally
            {
                if (cnn.State == System.Data.ConnectionState.Open) cnn.Close();
            }
        }
        public string Message_deleteMessage(int id)
        {
            if (cnn == null) InitSqlConnection();
            string query = "DELETE FROM messages WHERE ID = " + id;
            MySqlCommand command = new MySqlCommand(query, cnn);
            cnn.Open();
            try
            {
                command.ExecuteNonQuery();
                return "doen";
            }
            catch (Exception ex)
            {
                return "Error with execuing the delete: " + ex.Message;
            }
            finally
            {
                if (cnn.State == System.Data.ConnectionState.Open) cnn.Close();
            }
        }
        #endregion




        #region Chat

        public String Chat_getChats(int id)
        {
            if (cnn == null) InitSqlConnection();
            string query = "SELECT * FROM chat WHERE sender_ID = " + id + " OR receiver_ID = " + id;
            MySqlCommand command = new MySqlCommand(query, cnn);
            List<Chat> chats = new List<Chat>();
            cnn.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Chat chat = new Chat();
                chat.id = reader.GetInt32(0);
                chat.sender_id = reader.GetInt32(1);
                chat.receiver_id = reader.GetInt32(2);
                chat.topic = reader.GetString(3); ;
                chats.Add(chat);
            }
            var json = JsonConvert.SerializeObject(chats);
            cnn.Close();
            return json.ToString();
        }
        public string Chat_postChat(Chat chat)
        {
            if (cnn == null) InitSqlConnection();
            string query = "INSERT INTO chat (sender_ID,receiver_ID,topic) " +
                "VALUES (" + chat.sender_id + "," + chat.receiver_id + ",'" + chat.topic + "');";
            MySqlCommand command = new MySqlCommand(query, cnn);
            cnn.Open();
            try
            {
                command.ExecuteNonQuery();
                command = new MySqlCommand("SELECT * FROM chat WHERE id = LAST_INSERT_ID()", cnn);
                chat = new Chat();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    chat.id = reader.GetInt32(0);
                    chat.sender_id = reader.GetInt32(1);
                    chat.receiver_id = reader.GetInt32(2);
                    chat.topic = reader.GetString(3);
                }
                var json = JsonConvert.SerializeObject(chat);
                return json.ToString();
            }
            catch (Exception ex)
            {
                return "Error with execuing the insert: " + ex.Message;
            }
            finally
            {
                if (cnn.State == System.Data.ConnectionState.Open) cnn.Close();
            }
        }
        public string Chat_putChat(int id, Chat chat)
        {
            if (cnn == null) InitSqlConnection();
            string query = "UPDATE chat " +
                " SET sender_ID = " + chat.sender_id + ",receiver_ID = " + chat.receiver_id + ",topic = '" + chat.topic + "' " +
                " WHERE id = "+ id;
            MySqlCommand command = new MySqlCommand(query, cnn);
            cnn.Open();
            try
            {
                command.ExecuteNonQuery();
                command = new MySqlCommand("SELECT * FROM chat WHERE id = " + id, cnn);
                chat = new Chat();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    chat.id = reader.GetInt32(0);
                    chat.sender_id = reader.GetInt32(1);
                    chat.receiver_id = reader.GetInt32(2);
                    chat.topic = reader.GetString(3);
                }
                var json = JsonConvert.SerializeObject(chat);
                return json.ToString();
            }
            catch (Exception ex)
            {
                return "Error with execuing the insert: " + ex.Message;
            }
            finally
            {
                if (cnn.State == System.Data.ConnectionState.Open) cnn.Close();
            }
        }
        public string Chat_deleteChat(int id)
        {
            if (cnn == null) InitSqlConnection();
            string query = "DELETE FROM chat WHERE id = " + id;
            MySqlCommand command = new MySqlCommand(query, cnn);
            cnn.Open();
            try
            {
                command.ExecuteNonQuery();
                return "done";
            }
            catch (Exception ex)
            {
                return "Error with execuing the delete: " + ex.Message;
            }
            finally
            {
                if (cnn.State == System.Data.ConnectionState.Open) cnn.Close();
            }
        }
        #endregion
    }

}
