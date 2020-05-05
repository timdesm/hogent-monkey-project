using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace HoGent_Monkey_Project
{
    class DatabaseManager
    {
        public static int getNewWoodID()
        {
            int ID = 1;
            MySqlConnection con = Program.db.getConnection();
            using var cmd = DatabaseUtil.CommandExecutor(con, "SELECT * FROM woods_trees ORDER BY id DESC LIMIT 1");
            con.Open();
            using MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                ID = rdr.GetInt32("woodID") + 1;
            }
            return ID;
        }

        public static void logTrees(int woodID, List<Tree> trees)
        {
            StringBuilder sb = new StringBuilder("INSERT INTO woods_trees (woodID, treeID, x, y) VALUES ");
            List<string> rows = new List<string>();
            foreach(Tree tree in trees)
            {
                rows.Add(string.Format("('{0}','{1}','{2}','{3}')", woodID, tree.ID, tree.X, tree.Y));
            }
            sb.Append(string.Join(",", rows));
            sb.Append(";");

            MySqlConnection con = Program.db.getConnection();
            con.Open();
            using var cmd = DatabaseUtil.CommandExecutor(con, sb.ToString());
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static void logMonkeys(int woodID, Monkey monkey)
        {
            StringBuilder sb = new StringBuilder("INSERT INTO woods_monkeys (monkeyID, monkeyName, woodID, seqnr, treeID, x, y) VALUES ");
            List<string> rows = new List<string>();
            for (int i = 0; i < monkey.Path.Count; i++)
            {
                Tree tree = monkey.Path[i];
                rows.Add(string.Format("('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", monkey.ID, monkey.Name, woodID, i, tree.ID, tree.X, tree.Y));
            }
            sb.Append(string.Join(",", rows));
            sb.Append(";");

            MySqlConnection con = Program.db.getConnection();
            con.Open();
            using var cmd = DatabaseUtil.CommandExecutor(con, sb.ToString());
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static void log(int woodID, List<String> log)
        {
            StringBuilder sb = new StringBuilder("INSERT INTO woods_log (woodID, monkeyID, message) VALUES ");
            List<string> rows = new List<string>();
            foreach (String line in log)
            {
                rows.Add(string.Format("('{0}','{1}','{2}')", woodID, line.Split(";")[0], line.Split(";")[1]));
            }
            sb.Append(string.Join(",", rows));
            sb.Append(";");

            MySqlConnection con = Program.db.getConnection();
            con.Open();
            using var cmd = DatabaseUtil.CommandExecutor(con, sb.ToString());
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            con.Close();
        }

    }
}
