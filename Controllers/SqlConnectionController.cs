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
        public string User_getUser(int id)
        {
            if (cnn == null) InitSqlConnection();
            string query = "SELECT * FROM user Where user_ID = " + id;
            MySqlCommand command = new MySqlCommand(query, cnn);
            User user = new User();
            cnn.Open();

            reader = command.ExecuteReader();
            if (!reader.HasRows) return null;
            while (reader.Read())
            {
                user.user_id = reader.GetInt32(0);
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
                user.user_id = reader.GetInt32(0);
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
                command = new MySqlCommand("SELECT * FROM user WHERE user_ID = LAST_INSERT_ID()", cnn);
                user = new User();
                if (cnn.State == System.Data.ConnectionState.Closed) cnn.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    user.user_id = reader.GetInt32(0);
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
            string query = "DELETE FROM user WHERE user_ID = " + id;
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
            string query = "SELECT * FROM user_items Where item_ID = " + id;
            MySqlCommand command = new MySqlCommand(query, cnn);
            UserItem userItem = new UserItem();
            if (cnn.State == System.Data.ConnectionState.Closed) cnn.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                userItem.item_id = reader.GetInt32(0);
                userItem.item_name = reader.GetString(1);
                userItem.item_desc = reader.GetString(2);
                userItem.user_id = reader.GetInt32(3);
                userItem.borrower = reader.GetString(4);
                userItem.date_from = reader.GetMySqlDateTime(5) + "";
                userItem.date_to = reader.GetMySqlDateTime(6) + "";
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
                userItem.item_id = reader.GetInt32(0);
                userItem.item_name = reader.GetString(1);
                userItem.item_desc = reader.GetString(2);
                userItem.user_id = reader.GetInt32(3);
                userItem.borrower = reader.GetString(4);
                userItem.date_from = reader.GetMySqlDateTime(5)+"";
                userItem.date_to = reader.GetMySqlDateTime(6)+"";
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
                command = new MySqlCommand("SELECT * FROM user_items WHERE item_ID = LAST_INSERT_ID()", cnn);
                userItem = new UserItem();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    userItem.item_id = reader.GetInt32(0);
                    userItem.item_name = reader.GetString(1);
                    userItem.item_desc = reader.GetString(2);
                    userItem.user_id = reader.GetInt32(3);
                    userItem.borrower = reader.GetString(4);
                    userItem.date_from = reader.GetMySqlDateTime(5) + "";
                    userItem.date_to = reader.GetMySqlDateTime(6) + "";
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
            string query = "DELETE FROM user_items WHERE item_ID = " + id;
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
            string query = "SELECT * FROM marketplace_items Where item_ID = " + id;
            MySqlCommand command = new MySqlCommand(query, cnn);
            MarketplaceItem marketplaceItem = new MarketplaceItem();
            if (cnn.State == System.Data.ConnectionState.Closed) cnn.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                marketplaceItem.item_id = reader.GetInt32(0);
                marketplaceItem.item_name = reader.GetString(1);
                marketplaceItem.item_desc = reader.GetString(2);
                marketplaceItem.user_id = reader.GetInt32(3);
                marketplaceItem.borrower = reader.GetString(4);
                marketplaceItem.date_from = reader.GetMySqlDateTime(5) + "";
                marketplaceItem.date_to = reader.GetMySqlDateTime(6) + "";
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
                marketplaceItem.item_id = reader.GetInt32(0);
                marketplaceItem.item_name = reader.GetString(1);
                marketplaceItem.item_desc = reader.GetString(2);
                marketplaceItem.user_id = reader.GetInt32(3);
                marketplaceItem.borrower = reader.GetString(4);
                marketplaceItem.date_from = reader.GetMySqlDateTime(5) + "";
                marketplaceItem.date_to = reader.GetMySqlDateTime(6) + "";
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
                command = new MySqlCommand("SELECT * FROM marketplace_items WHERE item_ID = LAST_INSERT_ID()", cnn);
                marketplaceItem = new MarketplaceItem();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    marketplaceItem.item_id = reader.GetInt32(0);
                    marketplaceItem.item_name = reader.GetString(1);
                    marketplaceItem.item_desc = reader.GetString(2);
                    marketplaceItem.user_id = reader.GetInt32(3);
                    marketplaceItem.borrower = reader.GetString(4);
                    marketplaceItem.date_from = reader.GetMySqlDateTime(5) + "";
                    marketplaceItem.date_to = reader.GetMySqlDateTime(6) + "";
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
            string query = "DELETE FROM marketplace_items WHERE item_ID = " + id;
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
                message.timestamp = reader.GetString(4);
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
                    message.timestamp = reader.GetString(4);
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
                chat.chat_id = reader.GetInt32(0);
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
                command = new MySqlCommand("SELECT * FROM chat WHERE chat_ID = LAST_INSERT_ID()", cnn);
                chat = new Chat();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    chat.chat_id = reader.GetInt32(0);
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
            string query = "DELETE FROM chat WHERE chat_ID = " + id;
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
