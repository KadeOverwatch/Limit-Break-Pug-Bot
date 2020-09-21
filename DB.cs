using DSharpPlus.Entities;
using Limit_Break_Pug_Bot.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Limit_Break_Pug_Bot
{
    public class DB
    {
        public string connStr = @"Server=.\SQLEXPRESS;Database=LimitBreakPugs;Trusted_Connection=True;";
        public List<Player> Players = new List<Player>();
        public List<PugEvent> Events = new List<PugEvent>();

        public async Task<Player> GetPlayer(DiscordUser mbr)
        {
            Player player = new Player();
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand($"SELECT TOP 1 * FROM dbo.Players WHERE Discord_Tag = '{mbr.Username}#{mbr.Discriminator}'", conn))  // rpl w sp
            {
                await conn.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();

                if (!reader.Read())
                    throw new InvalidOperationException("No user records were returned.");

                player.ID = reader.GetGuid(reader.GetOrdinal("ID"));
                player.Battle_Tag = reader.GetString(reader.GetOrdinal("Battle_Tag"));
                player.Discord_Tag = reader.GetString(reader.GetOrdinal("Discord_Tag"));
                player.Player_Rank = reader.GetInt32(reader.GetOrdinal("Player_Rank"));
                player.Player_Team = reader.GetString(reader.GetOrdinal("Player_Team"));
                player.Created_On = reader.GetDateTime(reader.GetOrdinal("Created_On"));

                if (reader.Read())
                    throw new InvalidOperationException("Multiple user records were returned.");

                return await Task.FromResult(player);
            }
        }

        public async Task<PugEvent> GetEvent(DiscordMessage msg)
        {
            PugEvent eve = new PugEvent();
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand($"SELECT TOP 1 * FROM dbo.Events WHERE Discord_Message_ID = '{msg.Id}'", conn))  // rpl w sp
            {
                await conn.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();

                if (!reader.Read())
                    throw new InvalidOperationException("No event records were returned.");

                eve.ID = reader.GetGuid(reader.GetOrdinal("ID"));
                eve.Scheduled_Date = reader.GetDateTime(reader.GetOrdinal("Scheduled_Date"));
                eve.Created_On = reader.GetDateTime(reader.GetOrdinal("Created_On"));
                eve.Discord_Message_ID = reader.GetString(reader.GetOrdinal("Discord_Message_ID"));

                if (reader.Read())
                    throw new InvalidOperationException("Multiple event records were returned.");

                return await Task.FromResult(eve);
            }
        }

        public async Task UpdateRegistration(Player player, PugEvent e)
        {
            await Query("UPDATE dbo.Registrations SET Player_Cancelled = '1' WHERE Event_ID = '61683a91-66f2-47e1-9912-2ea4f1410ff9' AND Player_ID = 'ebb8b6ce-4592-42ca-9e7e-d34483245dca'");
            await Task.CompletedTask;
        }

        public async Task CreateRegistration(Player player, PugEvent e)
        {
            await Query($"DELETE FROM dbo.Registrations WHERE Event_ID = '{e.ID}' AND Player_ID = '{player.ID}'");
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand($"INSERT INTO dbo.Registrations (Event_ID, Player_ID, Player_Cancelled) VALUES ('{e.ID}','{player.ID}', null)", conn))  // rpl w sp
            {
                await conn.OpenAsync().ConfigureAwait(false);
                try
                {
                    Console.WriteLine(cmd.CommandText);
                    await cmd.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            await Task.CompletedTask;
        }

        public async Task Query(string query)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(query, conn))  // rpl w sp
            {
                await conn.OpenAsync().ConfigureAwait(false);
                try
                {
                    Console.WriteLine(cmd.CommandText);
                    await cmd.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            await Task.CompletedTask;
        }

        public async Task<List<PugEvent>> GetCurrentEvents()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Events WHERE Scheduled_Date >= GETDATE()", conn))  // rpl w sp
            {
                await conn.OpenAsync().ConfigureAwait(false);
                try
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                PugEvent e = new PugEvent();

                                e.ID = reader.GetGuid(reader.GetOrdinal("ID"));
                                e.Scheduled_Date = reader.GetDateTime(reader.GetOrdinal("Scheduled_Date"));
                                e.Created_On = reader.GetDateTime(reader.GetOrdinal("Created_On"));
                                e.Discord_Message_ID = reader.GetString(reader.GetOrdinal("Discord_Message_ID"));

                                Events.Add(e);
                            }
                        }
                        return await Task.FromResult(Events);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return await Task.FromResult(Events);
                }
            }
        }

        public async Task CreateEvent(PugEvent e)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand($"INSERT INTO dbo.Events (Scheduled_Date, Discord_Message_ID) VALUES ('{e.Scheduled_Date}','{e.Discord_Message_ID}')", conn))  // rpl w sp
            {
                await conn.OpenAsync().ConfigureAwait(false);
                try
                {
                    Console.WriteLine(cmd.CommandText);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            await Task.CompletedTask;
        }

        public async Task<List<Player>> GetPlayers()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Players", conn))  // rpl w sp
            {
                await conn.OpenAsync().ConfigureAwait(false);
                try
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            Players.Clear();
                            while (reader.Read())
                            {
                                Player player = new Player();

                                player.ID = reader.GetGuid(reader.GetOrdinal("ID"));
                                player.Battle_Tag = reader.GetString(reader.GetOrdinal("Battle_Tag"));
                                player.Discord_Tag = reader.GetString(reader.GetOrdinal("Discord_Tag"));
                                player.Player_Rank = reader.GetInt32(reader.GetOrdinal("Player_Rank"));
                                player.Player_Team = reader.GetString(reader.GetOrdinal("Player_Team"));
                                player.Created_On = reader.GetDateTime(reader.GetOrdinal("Created_On"));

                                int modifiedOnIndex = reader.GetOrdinal("Modified_On");
                                if (!reader.IsDBNull(modifiedOnIndex))
                                {
                                    player.Modified_On = reader.GetDateTime(reader.GetOrdinal("Modified_On"));
                                } else
                                {
                                    player.Modified_On = DateTime.MinValue;
                                }

                                Players.Add(player);
                            }
                        }
                        return await Task.FromResult(Players);
                    }
                } catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return await Task.FromResult(Players);
                }
            }
        }
    }
}
