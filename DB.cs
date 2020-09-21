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
