using System;
using System.Data;
using System.Data.SqlClient;

namespace ADOProject
{
    class Program
    {
        string constring;
        SqlConnection con;
        SqlCommand cmd;
        public Program()
        {
            constring = @"server=LAPTOP-AUGUV4DQ;Integrated security =true;Initial catalog=pubs";
            con = new SqlConnection(constring);
        }
        void DisplayMovies()
        {
            string strCmd = "select * from tblMovie";
            cmd = new SqlCommand(strCmd, con);
            try
            {
                con.Open();
                SqlDataReader drMovies = cmd.ExecuteReader();
                while (drMovies.Read())
                {
                    Console.WriteLine("Movie Id:" + drMovies[0]);
                    Console.WriteLine("Movie Name: " + drMovies[1]);
                    Console.WriteLine("Movie dduration: " + drMovies[2]);
                    Console.WriteLine("---------------------------------------------------------------------------------------");
                }
            }
            catch (SqlException sqlException)
            {
                Console.WriteLine(sqlException.Message);
            }
            finally
            {
                con.Close();
            }
        }
        void AddMovie()
        {
            Console.WriteLine("Enter a movie name:");
            string mName = Console.ReadLine();
            Console.WriteLine("Enter the movie duration:");
            float mDuration = (float)Math.Round(float.Parse(Console.ReadLine()), 2);
            string strCmd = "insert into tblMovie(name,duration) values (@mname,@mdur)";
            cmd = new SqlCommand(strCmd, con);
            cmd.Parameters.AddWithValue("@mname", mName);
            cmd.Parameters.AddWithValue("@mdur", mDuration);
            try
            {
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                    Console.WriteLine("Movie Inserted");
                else
                {
                    Console.WriteLine("No not done");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }
        }
        void UpdateMovie()
        {
            Console.WriteLine("Enter the Movie ID:");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the movie duration:");
            float mDuration = (float)Math.Round(float.Parse(Console.ReadLine()), 2);
            string strCmd = "Update tblMovie set duration = @mduration where id=@mid";
            cmd = new SqlCommand(strCmd, con);
            cmd.Parameters.AddWithValue("@mid", id);
            cmd.Parameters.AddWithValue("@mduration", mDuration);
            try
            {
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                    Console.WriteLine("Movie Updated");
                else
                {
                    Console.WriteLine("No not done");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }
        }
        void DeleteMovie()
        {
            Console.WriteLine("Enter the Movie ID:");
            int id = Convert.ToInt32(Console.ReadLine());
            string strCmd = "delete from tblMovie where id=@mid";
            cmd = new SqlCommand(strCmd, con);
            cmd.Parameters.AddWithValue("@mid", id);
            try
            {
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                    Console.WriteLine("Movie Deleted");
                else
                {
                    Console.WriteLine("No not done");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }

        }
        void PrintMovieById()
        {
            string strCmd = "select * from tblMovie where id=@mid";
            cmd = new SqlCommand(strCmd, con);
            try
            {
                con.Open();
                Console.WriteLine("Please enter the id");
                int id = Convert.ToInt32(Console.ReadLine());
                cmd.Parameters.Add("@mid", SqlDbType.Int);
                cmd.Parameters[0].Value = id;
                SqlDataReader drMovies = cmd.ExecuteReader();
                while (drMovies.Read())
                {
                    Console.WriteLine("Movie Id:" + drMovies[0].ToString());
                    Console.WriteLine("Movie Name: " + drMovies[1]);
                    Console.WriteLine("Movie Duration: " + drMovies[2].ToString());
                    Console.WriteLine("-------------------------------------");
                }
            }
            catch (SqlException sqlException)
            {
                Console.WriteLine(sqlException.Message);
            }
            finally
            {
                con.Close();
            }
        }
        void PrintMenu()
        {
            Console.WriteLine("Menu");
            int choice = 0;
            do
            {
                Console.WriteLine("1.Add Movie");
                Console.WriteLine("2.Update Movie");
                Console.WriteLine("3.Print single Movie");
                Console.WriteLine("4.Delete Movie");
                Console.WriteLine("5.Display Movies List");
                Console.WriteLine("6.Exit");
                Console.WriteLine("**");
                Console.WriteLine("Select an action need to perform");
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        new Program().AddMovie();
                        break;
                    case 2:
                        new Program().UpdateMovie();
                        break;
                    case 3:
                        new Program().PrintMovieById();
                        break;
                    case 4:
                        new Program().DeleteMovie();
                        break;
                    case 5:
                        new Program().DisplayMovies();
                        break;
                    case 6:
                        Console.WriteLine("Exiting");
                        break;
                    default:
                        Console.WriteLine("Invalid entry");
                        break;
                }
            } while (choice != 6);
        }
        static void Main(string[] args)
        {
            new Program().PrintMenu();
        }
    }
}
