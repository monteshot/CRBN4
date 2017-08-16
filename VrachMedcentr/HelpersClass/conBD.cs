using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfControls;

namespace VrachMedcentr
{
    class conBD
    {
        public string server;
        public string database;
        public string UserID;
        public string Password;
        private string stat;

        static SynhronyzeClass synhronyze = new SynhronyzeClass();
        #region Constructors

        public conBD()
        {
            server = "shostka.mysql.ukraine.com.ua";
            database = "shostka_odc";
            UserID = "shostka_odc";
            Password = "Cpu1234Pro";
            //server = "shostka.mysql.ukraine.com.ua";
            //database = "shostka_medcen";
            //UserID = "shostka_medcen";
            //Password = "n5t7jzqv";
        }

        public conBD(string _server, string _database, string _UserID, string _Password)
        {
            server = _server;
            database = _database;
            UserID = _UserID;
            Password = _Password;
        }


        #endregion

        #region Helpers Methods
        public Task<string> GetTableHash(string _tablename)
        {

            return Task.Run(() =>
            {
                MySqlConnectionStringBuilder mysqlCSB;
                mysqlCSB = new MySqlConnectionStringBuilder();
                mysqlCSB.Server = server;
                mysqlCSB.Database = database;
                mysqlCSB.UserID = UserID;
                mysqlCSB.Password = Password;
                mysqlCSB.ConvertZeroDateTime = true;

                MySqlConnection con = new MySqlConnection();
                con.ConnectionString = mysqlCSB.ConnectionString;
                MySqlCommand cmd = new MySqlCommand();

                string a = "";




                cmd.CommandText = "CHECKSUM TABLE " + _tablename;


                con.Open();

                cmd.Connection = con;
                cmd.ExecuteNonQuery();

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        a = dr.GetString("Checksum");
                    }
                }
                con.Close();

                return a;
            });





        }


        #endregion


        #region Work with data like DataTable
        /// <summary>
        /// Метод загрузки записей на прием всех врачей
        /// </summary>
        /// <returns>Записи на прием всех врачей</returns>
        public DataTable get_ekfgq_ttfsp_dop()
        {

            MySqlConnectionStringBuilder mysqlCSB;
            mysqlCSB = new MySqlConnectionStringBuilder();
            mysqlCSB.Server = server;
            mysqlCSB.Database = database;
            mysqlCSB.UserID = UserID;
            mysqlCSB.Password = Password;

            mysqlCSB.AllowZeroDateTime = true;

            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = mysqlCSB.ConnectionString;
            MySqlCommand cmd = new MySqlCommand();
            con.Open();
            cmd.CommandText = "SELECT * FROM enx4w_ttfsp_dop";
            cmd.Connection = con;
            cmd.ExecuteNonQuery();


            DataTable dt = new DataTable();

            MySqlDataReader reader = cmd.ExecuteReader();
            dt.Load(reader);

            con.Close();
            return dt;


        }
        /// <summary>
        /// Занимается инсертом в таблицу _записей на прием
        /// </summary>
        /// <param name="DT"></param>
        public void insert_ekfgq_ttfsp_dop(DataTable DT)
        {

            MySqlConnectionStringBuilder mysqlCSB;
            mysqlCSB = new MySqlConnectionStringBuilder();
            mysqlCSB.Server = server;
            mysqlCSB.Database = database;
            mysqlCSB.UserID = UserID;
            mysqlCSB.Password = Password;

            mysqlCSB.AllowZeroDateTime = true;

            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = mysqlCSB.ConnectionString;
            MySqlCommand cmd = new MySqlCommand();

            // cmd.Parameters.Add("", MySqlDbType.VarChar);
            //cmd.CommandText = "INSERT INTO ekfgq_ttfsp_dop(id,idrec,iduser,id_specialist,publshed,ordering,checked_out,checked_out_time,rfio,rphone,info,ipuser,rmail,summa,payment_status,number_order,cdate,date,hours,minutes,office_name,specializations_name) VALUES()";


            //   string tempOrder = GetNumberOrder();
            //string[] tempArray = tempOrder.Split(new char[] { '-' });
            //tempOrder = tempArray[0];

            StringBuilder MegaCom = new StringBuilder("INSERT INTO ekfgq_ttfsp_dop(idrec, iduser, id_specialist, published, ordering,checked_out, checked_out_time, rfio, rphone, info, ipuser, rmail, summa,payment_status, number_order, cdate, date, hours, minutes, office_name, specializations_name, specialist_name, specialist_email,specialist_phone, order_password,office_desc, office_address, number_cabinet) VALUES ");
            List<string> Rw = new List<string>();
            List<string> Rw1 = new List<string>();

            foreach (DataRow z in DT.Rows)
            {
                //дороботать
                string fg = $"(";
                for (int i = 1; i <= 29; i++)
                {
                    if (i == 29)
                    {
                        fg = fg + "'" + MySqlHelper.EscapeString(z[i].ToString()) + "'";
                    }
                    else
                    {
                        fg = fg + "'" + MySqlHelper.EscapeString(z[i].ToString()) + "',";
                    }

                }
                fg = fg + ")";
                Rw1.Add(fg);
                fg = "";
                Rw.Add($"('{MySqlHelper.EscapeString(z[1].ToString())}','{MySqlHelper.EscapeString(z[2].ToString())}'," +
                    $"'{MySqlHelper.EscapeString(z[3].ToString())}','{MySqlHelper.EscapeString(z[4].ToString())}','{MySqlHelper.EscapeString(z[5].ToString())}'," +
                    $"'{MySqlHelper.EscapeString(z[6].ToString())}','{MySqlHelper.EscapeString(z[7].ToString())}','{MySqlHelper.EscapeString(z[8].ToString())}'," +
                    $"'{MySqlHelper.EscapeString(z[9].ToString())}','{MySqlHelper.EscapeString(z[10].ToString())}','{MySqlHelper.EscapeString(z[11].ToString())}'," +
                    $"'{MySqlHelper.EscapeString(z[12].ToString())}','{MySqlHelper.EscapeString(z[13].ToString())}','{MySqlHelper.EscapeString(z[14].ToString())}'," +
                    $"'{MySqlHelper.EscapeString(z[15].ToString())}','{MySqlHelper.EscapeString(z[16].ToString())}','{MySqlHelper.EscapeString(z[17].ToString())}'," +
                    $"'{MySqlHelper.EscapeString(z[18].ToString())}','{MySqlHelper.EscapeString(z[19].ToString())}','{MySqlHelper.EscapeString(z[20].ToString())}'," +
                    $"'{MySqlHelper.EscapeString(z[21].ToString())}','{MySqlHelper.EscapeString(z[22].ToString())}','{MySqlHelper.EscapeString(z[23].ToString())}'," +
                    $"'{MySqlHelper.EscapeString(z[24].ToString())}','{MySqlHelper.EscapeString(z[25].ToString())}','{MySqlHelper.EscapeString(z[26].ToString())}'," +
                    $"'{MySqlHelper.EscapeString(z[27].ToString())}','{MySqlHelper.EscapeString(z[28].ToString())}','{MySqlHelper.EscapeString(z[29].ToString())}')");
            }

            MegaCom.Append(string.Join(",", Rw1));
            MegaCom.Append(";");

            con.Close();
            con.Open();
            cmd.Connection = con;
            // con.OpenAsync();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = MegaCom.ToString();
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            con.Close();
            //  con.Close();

            //string _iduser = DT.Columns["iduser"].ToString();
            //string _idrec = DT.Columns["idrec"].ToString();
            //string _id_specialist = DT.Columns["id_specialist"].ToString();
            //string _published = DT.Columns["published"].ToString();
            //string _ordering = DT.Columns["ordering"].ToString();
            //string _checked_out = DT.Columns["checked_out"].ToString();
            //string _checked_out_time = DT.Columns["checked_out_time"].ToString();
            //string _rfio = DT.Columns["rfio"].ToString();
            //string _rphone = DT.Columns["rfio"].ToString();
            //string _info = DT.Columns["info"].ToString();
            //string _ipuser = DT.Columns["ipuser"].ToString();
            //string _rmail = DT.Columns["rmail"].ToString();
            //string _summa = DT.Columns["summa"].ToString();
            //string _payment_status = DT.Columns["payment_status"].ToString();
            //string _number_order = DT.Columns["number_order"].ToString();
            //string _cdate = DT.Columns["cdate"].ToString();
            //string _date = DT.Columns["date"].ToString();
            //string _hours = DT.Columns["hours"].ToString();
            //string _minutes = DT.Columns["minutes"].ToString();
            //string _office_name = DT.Columns["office_name"].ToString();
            //string _specializations_name = DT.Columns["specializations_name"].ToString();
            //string _specialist_name = DT.Columns["specialist_name"].ToString();
            //string _specialist_email = DT.Columns["specialist_email"].ToString();
            //string _order_password = DT.Columns["order_password"].ToString();
            //string _office_desc = DT.Columns["office_desc"].ToString();
            //string _office_address = DT.Columns["office_address"].ToString();
            //string _sms_send = DT.Columns["sms_send"].ToString();
            //string _number_cabinet = DT.Columns["number_cabinet"].ToString();





            //  cmd.CommandText ="INSERT INTO ekfgq_ttfsp_dop(id,iduser, id_specialist, ordering, rfio, rphone, info, ipuser, rmail, number_order, cdate, date, hours, minutes, office_name, specializations_name, specialist_name, specialist_email, order_password, office_address, number_cabinet) VALUES(null,@iduser, @id_specialist, @ordering, @rfio, @rphone, @info, @ipuser, @rmail, @number_order, @cdate, @date, @hours, @minutes, @office_name, @specializations_name, @specialist_name, @specialist_email, @order_password, @office_address, @number_cabinet)";

            //#region Command Parametrs

            //cmd.Parameters.AddWithValue("@iduser", _iduser);
            //cmd.Parameters.AddWithValue("@id_specialist", _id_specialist);
            //cmd.Parameters.AddWithValue("@ordering", _ordering);
            //cmd.Parameters.AddWithValue("@rfio", _rfio);
            //cmd.Parameters.AddWithValue("@rphone", _rphone);
            //cmd.Parameters.AddWithValue("@info", _info);
            //cmd.Parameters.AddWithValue("@ipuser", _ipuser);
            //cmd.Parameters.AddWithValue("@rmail", _rmail);
            //cmd.Parameters.AddWithValue("@number_order", _number_order);
            //cmd.Parameters.AddWithValue("@cdate", _cdate);
            //cmd.Parameters.AddWithValue("@date", _date);
            //cmd.Parameters.AddWithValue("@hours", _hours);
            //cmd.Parameters.AddWithValue("@minutes", _minutes);
            //cmd.Parameters.AddWithValue("@office_name", _office_name);
            //cmd.Parameters.AddWithValue("@specializations_name", _specializations_name);
            //cmd.Parameters.AddWithValue("@specialist_name", _specialist_name);
            //cmd.Parameters.AddWithValue("@specialist_email", _specialist_email);
            //cmd.Parameters.AddWithValue("@order_password", _order_password);
            //cmd.Parameters.AddWithValue("@office_address", _office_address);
            //cmd.Parameters.AddWithValue("@number_cabinet", _number_cabinet);

            //#endregion

            //int a = 0;
            //DataTable dt = new DataTable();

            //MySqlDataReader reader = cmd.ExecuteReader();
            //dt.Load(reader);




            //using (MySqlDataReader dr = cmd.ExecuteReader())
            //{
            //    while (dr.Read())
            //    {
            //        a = dr.GetInt32("Checksum");

            //    }
            //}

            //return dt;
            // GetDoctrosNames(5);
            //   return temp;
        }
        /// <summary>
        /// Метод загрузки рабочих дней врчаей
        /// </summary>
        /// <returns>Рабочие дни конкретных врачей</returns>
        public DataTable get_ekfgq_ttfsp()
        {

            MySqlConnectionStringBuilder mysqlCSB;
            mysqlCSB = new MySqlConnectionStringBuilder();
            mysqlCSB.Server = server;
            mysqlCSB.Database = database;
            mysqlCSB.UserID = UserID;
            mysqlCSB.Password = Password;

            mysqlCSB.AllowZeroDateTime = true;

            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = mysqlCSB.ConnectionString;
            MySqlCommand cmd = new MySqlCommand();
            con.Open();
            cmd.CommandText = "SELECT * FROM enx4w_ttfsp";
            cmd.Connection = con;
            cmd.ExecuteNonQuery();


            DataTable dt = new DataTable();

            MySqlDataReader reader = cmd.ExecuteReader();
            dt.Load(reader);

            con.Close();
            return dt;


        }
        /// <summary>
        /// Занимается инсертом в таблицу _рабочих дней всех врачей
        /// </summary>
        /// <param name="DT"></param>
        public void insert_ekfgq_ttfsp(DataTable DT)
        {
            //ПЕРЕДЕЛАТЬ НА АПДЕЙТ ИНСЕРТ
            MySqlConnectionStringBuilder mysqlCSB;
            mysqlCSB = new MySqlConnectionStringBuilder();
            mysqlCSB.Server = server;
            mysqlCSB.Database = database;
            mysqlCSB.UserID = UserID;
            mysqlCSB.Password = Password;

            mysqlCSB.AllowZeroDateTime = true;

            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = mysqlCSB.ConnectionString;
            MySqlCommand cmd = new MySqlCommand();



            StringBuilder MegaCom = new StringBuilder("INSERT INTO ekfgq_ttfsp(id,idspec, iduser, reception, published, dttime ,hrtime,mntime,ordering ,checked_out, checked_out_time, rfio, rphone, info, ipuser, ttime, plimit,pricezap ,rmail,sms) VALUES ");
            List<string> Rw = new List<string>();

            foreach (DataRow z in DT.Rows)
            {
                Rw.Add(
                    $"('{z[0].ToString()}','{z[1].ToString()}','{z[2].ToString()}','{z[3].ToString()}','{z[4].ToString()}','{z[5].ToString()}','{z[6].ToString()}','{z[7].ToString()}','{z[8].ToString()}','{z[9].ToString()}','{z[10].ToString()}','{z[11].ToString()}','{z[12].ToString()}','{z[13].ToString()}','{z[14].ToString()}','{z[15].ToString()}','{z[16].ToString()}','{z[17].ToString()}','{z[18].ToString()}','{z[19].ToString()}')");
            }

            MegaCom.Append(string.Join(",", Rw));
            MegaCom.Append(";");

            con.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = MegaCom.ToString();
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            con.Close();


        }
        /// <summary>
        /// Метод загрузки юзверей
        /// </summary>
        /// <returns>Список юзверей</returns>
        public DataTable get_ekfgq_users()
        {

            MySqlConnectionStringBuilder mysqlCSB;
            mysqlCSB = new MySqlConnectionStringBuilder();
            mysqlCSB.Server = server;
            mysqlCSB.Database = database;
            mysqlCSB.UserID = UserID;
            mysqlCSB.Password = Password;

            mysqlCSB.AllowZeroDateTime = true;

            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = mysqlCSB.ConnectionString;
            MySqlCommand cmd = new MySqlCommand();
            con.Open();
            cmd.CommandText = "SELECT * FROM ekfgq_users";
            cmd.Connection = con;
            cmd.ExecuteNonQuery();


            DataTable dt = new DataTable();

            MySqlDataReader reader = cmd.ExecuteReader();
            dt.Load(reader);

            con.Close();
            return dt;


        }
        /// <summary>
        /// Занимается инсертом в таблицу _юзверей
        /// </summary>
        /// <param name="DT"></param>
        public void insert_ekfgq_users(DataTable DT)
        {

            //return Task.Run(() =>
            //{
            MySqlConnectionStringBuilder mysqlCSB;
            mysqlCSB = new MySqlConnectionStringBuilder();
            mysqlCSB.Server = server;
            mysqlCSB.Database = database;
            mysqlCSB.UserID = UserID;
            mysqlCSB.Password = Password;

            mysqlCSB.AllowZeroDateTime = true;

            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = mysqlCSB.ConnectionString;
            MySqlCommand cmd = new MySqlCommand();



            StringBuilder MegaCom = new StringBuilder("INSERT INTO ekfgq_users(name, username, email, password, block ,sendEmail,registerDate,lastvisitDate,activation, params, lastResetTime, resetCount,otpKey,otep,requireReset,fio,phone) VALUES ");
            List<string> Rw = new List<string>();

            foreach (DataRow z in DT.Rows)
            {
                Rw.Add(
                    $"('{MySqlHelper.EscapeString(z[1].ToString())}','{MySqlHelper.EscapeString(z[2].ToString())}','{MySqlHelper.EscapeString(z[3].ToString())}','{MySqlHelper.EscapeString(z[4].ToString())}','{MySqlHelper.EscapeString(z[5].ToString())}','{MySqlHelper.EscapeString(z[6].ToString())}','{MySqlHelper.EscapeString(z[7].ToString())}','{MySqlHelper.EscapeString(z[8].ToString())}','{MySqlHelper.EscapeString(z[9].ToString())}','{MySqlHelper.EscapeString(z[10].ToString())}','{MySqlHelper.EscapeString(z[11].ToString())}','{MySqlHelper.EscapeString(z[12].ToString())}','{MySqlHelper.EscapeString(z[13].ToString())}','{MySqlHelper.EscapeString(z[14].ToString())}','{MySqlHelper.EscapeString(z[15].ToString())}','{MySqlHelper.EscapeString(z[16].ToString())}','{MySqlHelper.EscapeString(z[17].ToString())}')");
            }

            MegaCom.Append(string.Join(",", Rw));
            MegaCom.Append(";");

            con.Open();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = MegaCom.ToString();
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            con.Close();
            // });

        }
        /// <summary>
        /// Метод загрузки расписаний
        /// </summary>
        /// <returns>Расписания конкретных врачей</returns>
        public DataTable get_ekfgq_ttfsp_sprtime()
        {

            MySqlConnectionStringBuilder mysqlCSB;
            mysqlCSB = new MySqlConnectionStringBuilder();
            mysqlCSB.Server = server;
            mysqlCSB.Database = database;
            mysqlCSB.UserID = UserID;
            mysqlCSB.Password = Password;

            mysqlCSB.AllowZeroDateTime = true;

            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = mysqlCSB.ConnectionString;
            MySqlCommand cmd = new MySqlCommand();
            con.Open();
            cmd.CommandText = "SELECT * FROM ekfgq_ttfsp_sprtime";
            cmd.Connection = con;
            cmd.ExecuteNonQuery();


            DataTable dt = new DataTable();

            MySqlDataReader reader = cmd.ExecuteReader();
            dt.Load(reader);

            con.Close();
            return dt;


        }
        /// <summary>
        /// Занимается инсертом в таблицу _расписаний конкретных врачей
        /// </summary>
        /// <param name="DT"></param>
        //public void insert_ekfgq_ttfsp_sprtime(DataTable DT)
        //{

        //    MySqlConnectionStringBuilder mysqlCSB;
        //    mysqlCSB = new MySqlConnectionStringBuilder();
        //    mysqlCSB.Server = server;
        //    mysqlCSB.Database = database;
        //    mysqlCSB.UserID = UserID;
        //    mysqlCSB.Password = Password;

        //    mysqlCSB.AllowZeroDateTime = true;

        //    MySqlConnection con = new MySqlConnection();
        //    con.ConnectionString = mysqlCSB.ConnectionString;
        //    MySqlCommand cmd = new MySqlCommand();



        //    StringBuilder MegaCom = new StringBuilder("INSERT INTO ekfgq_ttfsp_sprtime(id,name, published,desc,timehm,checked_out,checked_out_time,ordering,timeprv) VALUES ");
        //    List<string> Rw = new List<string>();

        //    foreach (DataRowCollection z in DT.Rows)
        //    {
        //        Rw.Add(
        //            $"('{z[0].ToString()}','{z[1].ToString()}','{z[2].ToString()}','{z[3].ToString()}','{z[4].ToString()}','{z[5].ToString()}','{z[6].ToString()}','{z[7].ToString()}','{z[8].ToString()}')");
        //    }

        //    MegaCom.Append(string.Join(",", Rw));
        //    MegaCom.Append(";");

        //    con.Open();
        //    cmd.CommandType = CommandType.Text;
        //    cmd.CommandText = MegaCom.ToString();
        //    cmd.ExecuteNonQuery();
        //    cmd.Parameters.Clear();
        //    con.Close();


        //}
        public void update_ekfgq_ttfsp_sprtime(DataTable DT)
        {

            MySqlConnectionStringBuilder mysqlCSB;
            mysqlCSB = new MySqlConnectionStringBuilder();
            mysqlCSB.Server = server;
            mysqlCSB.Database = database;
            mysqlCSB.UserID = UserID;
            mysqlCSB.Password = Password;

            mysqlCSB.AllowZeroDateTime = true;

            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = mysqlCSB.ConnectionString;
            MySqlCommand cmd = new MySqlCommand();



            StringBuilder MegaCom = new StringBuilder("UPDATE ekfgq_ttfsp_sprtime(name, published,desc,timehm,checked_out,checked_out_time,ordering,timeprv) SET " + $"('{DT.Columns[1].ToString()}','{DT.Columns[2].ToString()}','{DT.Columns[3].ToString()}','{DT.Columns[4].ToString()}','{DT.Columns[5].ToString()}','{DT.Columns[6].ToString()}','{DT.Columns[7].ToString()}','{DT.Columns[8].ToString()}')" + "WHERE id=" + DT.Columns[8].ToString());
            // List<string> Rw = new List<string>();

            //foreach (DataRowCollection z in DT.Rows)
            //{
            //    Rw.Add(
            //        $"('{z[0].ToString()}','{z[1].ToString()}','{z[2].ToString()}','{z[3].ToString()}','{z[4].ToString()}','{z[5].ToString()}','{z[6].ToString()}','{z[7].ToString()}','{z[8].ToString()}')");
            //}

            //MegaCom.Append(string.Join(",", Rw));
            //MegaCom.Append(";");

            con.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = MegaCom.ToString();
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            con.Close();


        }

        /// <summary>
        /// Метод загрузки описаний специализаций
        /// </summary>
        /// <returns>Описание специализаций</returns>
        public DataTable get_ekfgq_ttfsp_sprspec()
        {

            MySqlConnectionStringBuilder mysqlCSB;
            mysqlCSB = new MySqlConnectionStringBuilder();
            mysqlCSB.Server = server;
            mysqlCSB.Database = database;
            mysqlCSB.UserID = UserID;
            mysqlCSB.Password = Password;

            mysqlCSB.AllowZeroDateTime = true;

            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = mysqlCSB.ConnectionString;
            MySqlCommand cmd = new MySqlCommand();
            con.Open();
            cmd.CommandText = "SELECT * FROM ekfgq_ttfsp_sprspec";
            cmd.Connection = con;
            cmd.ExecuteNonQuery();


            DataTable dt = new DataTable();

            MySqlDataReader reader = cmd.ExecuteReader();
            dt.Load(reader);

            con.Close();
            return dt;


        }
        /// <summary>
        /// Занимается инсертом в таблицу _описаний специализаций
        /// </summary>
        /// <param name="DT"></param>
        public void insert_ekfgq_ttfsp_sprspec(DataTable DT)
        {
            try
            {

                //return Task.Run(() =>
                //{
                MySqlConnectionStringBuilder mysqlCSB;
                mysqlCSB = new MySqlConnectionStringBuilder();
                mysqlCSB.Server = server;
                mysqlCSB.Database = database;
                mysqlCSB.UserID = UserID;
                mysqlCSB.Password = Password;

                mysqlCSB.AllowZeroDateTime = true;

                MySqlConnection con = new MySqlConnection();
                con.ConnectionString = mysqlCSB.ConnectionString;
                MySqlCommand cmd = new MySqlCommand();



                StringBuilder MegaCom = new StringBuilder("INSERT INTO ekfgq_ttfsp_sprspec(name, published, photo, offphoto, checked_out_time, ordering) VALUES ");
                List<string> Rw = new List<string>();

                // какаето параша с приведеньеи єтого дерьма /*'{MySqlHelper.EscapeString(z[3].ToString())}',*/ в єтом столбике  desc, 

                foreach (DataRow z in DT.Rows)
                {
                    Rw.Add(
                        $"('{MySqlHelper.EscapeString(z["name"].ToString())}','{MySqlHelper.EscapeString(z["published"].ToString())}'," +
                        $"'{MySqlHelper.EscapeString(z["photo"].ToString())}','{MySqlHelper.EscapeString(z["offphoto"].ToString())}'," +
                        $"'{MySqlHelper.EscapeString(z["checked_out_time"].ToString())}','{MySqlHelper.EscapeString(z["ordering"].ToString())}')");
                }

                MegaCom.Append(string.Join(",", Rw));
                MegaCom.Append(";");


                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = MegaCom.ToString();
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                con.Close();

                // });
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());

            }


        }
        /// <summary>
        /// Метод загрузки "талон/время" параметров
        /// </summary>
        /// <returns>Талон или время параметры</returns>
        public DataTable get_talon_time()
        {

            MySqlConnectionStringBuilder mysqlCSB;
            mysqlCSB = new MySqlConnectionStringBuilder();
            mysqlCSB.Server = server;
            mysqlCSB.Database = database;
            mysqlCSB.UserID = UserID;
            mysqlCSB.Password = Password;

            mysqlCSB.AllowZeroDateTime = true;

            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = mysqlCSB.ConnectionString;
            MySqlCommand cmd = new MySqlCommand();
            con.Open();
            cmd.CommandText = "SELECT * FROM talon_time";
            cmd.Connection = con;
            cmd.ExecuteNonQuery();


            DataTable dt = new DataTable();

            MySqlDataReader reader = cmd.ExecuteReader();
            dt.Load(reader);

            con.Close();
            return dt;


        }
        /// <summary>
        /// Делает инсерт "талон/время"
        /// </summary>
        /// <param name="DT"></param>
        public void insert_talon_time(DataTable DT)
        {

            MySqlConnectionStringBuilder mysqlCSB;
            mysqlCSB = new MySqlConnectionStringBuilder();
            mysqlCSB.Server = server;
            mysqlCSB.Database = database;
            mysqlCSB.UserID = UserID;
            mysqlCSB.Password = Password;

            mysqlCSB.AllowZeroDateTime = true;

            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = mysqlCSB.ConnectionString;
            MySqlCommand cmd = new MySqlCommand();



            StringBuilder MegaCom = new StringBuilder("INSERT INTO talon_time(id,doctor_id, parametr) VALUES ");
            List<string> Rw = new List<string>();

            foreach (DataRowCollection z in DT.Rows)
            {
                Rw.Add(
                    $"('{z[0].ToString()}','{z[1].ToString()}','{z[2].ToString()}')");
            }

            MegaCom.Append(string.Join(",", Rw));
            MegaCom.Append(";");

            con.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = MegaCom.ToString();
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            con.Close();


        }


        //    MySqlConnection con = new MySqlConnection();
        //    con.ConnectionString = mysqlCSB.ConnectionString;
        //    MySqlCommand cmd = new MySqlCommand();
        //    con.Open();
        //    cmd.CommandText = "SELECT * FROM ekfgq_ttfsp_dop";
        //    cmd.Connection = con;
        //    cmd.ExecuteNonQuery();

        //    DataTable dt = new DataTable();

        //    MySqlDataReader reader = cmd.ExecuteReader();
        //    dt.Load(reader);
        //    con.Close();
        //    return dt;

        //}
        #endregion


        #region Methd for work with data like ObsCollection


        /// <summary>
        ///  Поллучение записей на конкретную дату для выбраного доктора
        /// </summary>
        /// <param name="docId">ІD доктора</param>
        /// <param name="TimeAppointments"> Дата для поиска</param>
        /// <returns></returns>
        public ObservableCollection<Appointments> GetAppointments(string docId, DateTime TimeAppointments)
        {
            //synhronyze.SynhronyzeTable("ekfgq_ttfsp_dop", 1);

            MySqlConnectionStringBuilder mysqlCSB;
            mysqlCSB = new MySqlConnectionStringBuilder();
            mysqlCSB.Server = server;
            mysqlCSB.Database = database;
            mysqlCSB.UserID = UserID;
            mysqlCSB.Password = Password;
            mysqlCSB.ConvertZeroDateTime = true;

            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = mysqlCSB.ConnectionString;
            MySqlCommand cmd = new MySqlCommand();

            int i = 0;

            ObservableCollection<Appointments> temp = new ObservableCollection<Appointments>();

            con.Open();
            cmd.CommandText = "SELECT * FROM enx4w_ttfsp_dop WHERE id_specialist = @DocID AND date = @Date";
            cmd.Parameters.AddWithValue("@DocID", docId);
            cmd.Parameters.AddWithValue("@Date", TimeAppointments);
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            using (MySqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    i++;
                    temp.Add(new Appointments
                    {
                        NumberZP = i,
                        IDUser = dr.GetString("iduser"),
                        Pacient = dr.GetString("rfio"),
                        TimeAppomination = dr.GetString("hours") + " : " + dr.GetString("minutes"),
                        Comment = "Коментар відсутній",// добавить коментарий при записис?
                        NotComing = false,//вытащить с базы когда добавит димас
                        NumOrder=dr.GetString("number_order")
                    });
                }
            }
            con.Close();

            return temp;
        }

        public ObservableCollection<Users> GetUsers()
        {
            //synhronyze.SynhronyzeTable("ekfgq_users", 1);
            ObservableCollection<Users> temp = new ObservableCollection<Users>();
            try
            {

                MySqlConnectionStringBuilder mysqlCSB;
                mysqlCSB = new MySqlConnectionStringBuilder();
                mysqlCSB.Server = server;
                mysqlCSB.Database = database;
                mysqlCSB.UserID = UserID;
                mysqlCSB.Password = Password;
                mysqlCSB.ConvertZeroDateTime = true;


                MySqlConnection con = new MySqlConnection();
                con.ConnectionString = mysqlCSB.ConnectionString;
                MySqlCommand cmd = new MySqlCommand();



                con.Open();
                cmd.CommandText = "SELECT * FROM enx4w_users";
                cmd.Connection = con;
                cmd.Prepare();

                cmd.ExecuteNonQuery();



                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {

                        temp.Add(new Users
                        {
                            userId = dr.GetString("id"),
                            userFIO = dr.GetString("name"),
                            userMail = dr.GetString("email"),
                            userPhone = dr.GetString("phone")

                        });

                    }
                }
                con.Close();


            }
            catch
            {
                temp = GetUsers();
            }

            return temp;
        }


        /// <summary>
        ///  Find All Specialization  and make doctor list
        /// (Существует другой метод (с ДатаТейблом))
        /// </summary>
        /// <returns></returns>
        public List<DoctorsList> GetDocSpecification()
        {

            //synhronyze.SynhronyzeTable("ekfgq_ttfsp_sprspec", 1);

            MySqlConnectionStringBuilder mysqlCSB;
            mysqlCSB = new MySqlConnectionStringBuilder();
            mysqlCSB.Server = server;
            mysqlCSB.Database = database;
            mysqlCSB.UserID = UserID;
            mysqlCSB.Password = Password;

            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = mysqlCSB.ConnectionString;
            MySqlCommand cmd = new MySqlCommand();

            List<DoctorsList> temp = new List<DoctorsList>();
            // List<DoctorsList.DocNames> Docname1= new List<DoctorsList.DocNames>();
            try
            {

                con.Open();
                cmd.CommandText = "SELECT * FROM enx4w_ttfsp_sprspec";
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {

                        temp.Add(new DoctorsList
                        {
                            specf = dr.GetString("name"),
                            idspecf = dr.GetInt32("id")
                            // Likar = GetDoctrosNames(dr.GetString("id"))



                        });
                    }
                }
                con.Close();
                // GetDoctrosNames(5);
                return temp;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return temp;
        }

        /// <summary>
        ///  Find DocNames For each specialization
        /// </summary>
        /// <param name="specialization"></param>
        /// <returns></returns>
        public ObservableCollection<DocNames> GetDoctrosNames(string specialization)
        {
            //synhronyze.SynhronyzeTable("ekfgq_ttfsp_spec", 1);

            MySqlConnectionStringBuilder mysqlCSB;
            mysqlCSB = new MySqlConnectionStringBuilder();
            mysqlCSB.Server = server;
            mysqlCSB.Database = database;
            mysqlCSB.UserID = UserID;
            mysqlCSB.Password = Password;


            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = mysqlCSB.ConnectionString;
            MySqlCommand cmd = new MySqlCommand();
            ObservableCollection<DocNames> temp = new ObservableCollection<DocNames>();
            con.Open();
            cmd.CommandText = "SELECT * FROM enx4w_ttfsp_spec WHERE idsprspec = @IDSpecialization";//',9,'
            cmd.Parameters.AddWithValue("@IDSpecialization", "," + specialization + ",");
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            using (MySqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    temp.Add(new DocNames
                    {

                        docName = dr.GetString("name"),
                        docID = dr.GetString("id"),
                        // docBool = GetDocTimeTalonStatus(Convert.ToInt32(dr.GetString("id"))),
                        docEmail = dr.GetString("specmail"),
                        docTimeId = dr.GetString("idsprtime"),
                        docCab = dr.GetString("number_cabinet")


                    });
                }
            }
            //con.Close();

            ////con.Open();
            //cmd.CommandText = "SELECT * FROM talon_time";
            //cmd.Connection = con;
            //cmd.ExecuteNonQuery();
            //DataTable dt = new DataTable();

            //MySqlDataReader reader = cmd.ExecuteReader();
            //dt.Load(reader);

            //con.Close();
            //foreach (var a in temp)
            //{
            //    var currentdoc = dt.AsEnumerable().Single(row => row.Field<int>("doctor_id") == Convert.ToInt32(a.docID));


            //    var param = currentdoc.Field<int>("parametr");
            //    if (param == 0)
            //    {
            //        a.docBool = false;
            //    }
            //    else
            //    {
            //        a.docBool = true;
            //    }

            //}
            return temp;
        }


        //public bool GetDocTimeTalonStatus(int _docid)
        //{

        //    //очень часто вызываеться нужно подумать нужно ли

        //    //synhronyze.SynhronyzeTable("talon_time", 1);

        //    //synhronyze.SynhronyzeTable("talon_time", 2);

        //    MySqlConnectionStringBuilder mysqlCSB;
        //    mysqlCSB = new MySqlConnectionStringBuilder();
        //    mysqlCSB.Server = server;
        //    mysqlCSB.Database = database;
        //    mysqlCSB.UserID = UserID;
        //    mysqlCSB.Password = Password;


        //    MySqlConnection con = new MySqlConnection();
        //    con.ConnectionString = mysqlCSB.ConnectionString;
        //    MySqlCommand cmd = new MySqlCommand();
        //    bool parametr = false;
        //    con.Open();
        //    cmd.CommandText = "SELECT * FROM talon_time WHERE doctor_id = @IDSpecialist";//',9,'
        //    cmd.Parameters.AddWithValue("@IDSpecialist", _docid);
        //    cmd.Connection = con;
        //    cmd.ExecuteNonQuery();
        //    using (MySqlDataReader dr = cmd.ExecuteReader())
        //    {
        //        while (dr.Read())
        //        {
        //            if (dr.GetInt32("parametr") == 0)
        //            {
        //                parametr = false;
        //            }
        //            else
        //            {
        //                parametr = true;
        //            }
        //        }
        //    }
        //    con.Close();
        //    return parametr;
        //}
        #endregion

        #region GET DOCTORS TIMES
        public List<Times> getDocTimes(string docId, DateTime date)
        {

            MySqlConnectionStringBuilder mysqlCSB;
            mysqlCSB = new MySqlConnectionStringBuilder();
            mysqlCSB.Server = server;
            mysqlCSB.Database = database;
            mysqlCSB.UserID = UserID;
            mysqlCSB.Password = Password;
                        

           
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = mysqlCSB.ConnectionString;
            MySqlCommand cmd = new MySqlCommand();

            List<Times> temp = new List<Times>();
            string result = "";

            con.Open();
            cmd.CommandText = "SELECT * FROM enx4w_ttfsp  WHERE idspec = @docId AND dttime = @date";//',9,'
            cmd.Parameters.AddWithValue("@docId", docId);
            cmd.Parameters.AddWithValue("@date", date);

            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            using (MySqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    if(dr.GetInt32("iduser")==0)
                    {
                        result = "Green";
                    }
                    else
                    {
                        result = "Red";
                    }

                    temp.Add(new Times
                    {
                        //вкинуть в класс одну из переменных
                        Time = dr.GetString("hrtime") +":"+ dr.GetString("mntime"),
                        Label = dr.GetString("hrtime") +":"+ dr.GetString("mntime"),
                        Status = result

                    });

                }
            }
            cmd.Parameters.Clear();
            con.Close();
            temp = temp.OrderBy(p => p.Label).ToList();
            return temp;


            //DataTable tempData = get_ekfgq_ttfsp_dop();

            //foreach (var a in getDocPubTime)
            //{
            //    if (a != "" && a != null)
            //    {
            //        string[] parTime = a.Split(new char[] { ':' });
            //        if (tempData.AsEnumerable().Any(row => row.Field<int>("id_specialist") == Convert.ToInt32(docId) &&
            //        row.Field<MySqlDateTime>("date").GetDateTime() == date && row.Field<string>("hours") == parTime[0] &&
            //        row.Field<string>("minutes") == parTime[1]
            //        ))
            //        {
            //            result = "Red";
            //        }
            //        else
            //        {
            //            result = "Green";
            //        }
            //        temp.Add(new Times
            //        {
            //            Time = a,
            //            Label = a,
            //            Status = result,
            //            PublickPrivate = true
            //        });
            //    }



            //    //cmd.CommandText = "SELECT * FROM ekfgq_ttfsp_dop WHERE id_specialist = @docId AND date=@dateDB AND hours=@Hours AND minutes=@Mins";//',9,'
            //}

            //foreach (var a in getDocPrivateTime)
            //{
            //    if (a != "" && a != null)
            //    {
            //        string[] parTime = a.Split(new char[] { ':' });
            //        if (tempData.AsEnumerable().Any(row => row.Field<int>("id_specialist") == Convert.ToInt32(docId) &&
            //        row.Field<MySqlDateTime>("date").GetDateTime() == date && row.Field<string>("hours") == parTime[0] &&
            //        row.Field<string>("minutes") == parTime[1]
            //        ))
            //        {
            //            result = "Red";
            //        }
            //        else
            //        {
            //            result = "Green";
            //        }
            //        temp.Add(new Times
            //        {
            //            Time = a,
            //            Label = a,
            //            Status = result,
            //            PublickPrivate = false
            //        });
            //    }
            //}


        }
        /// <summary>
        /// Function for check aveliable list of doctors shedule
        /// </summary>
        /// <param name="_doctimeid"></param>
        /// <returns></returns>
        public bool CheckDoctorList(string _doctimeid)
        {
            //синхронизациия не нужна я думаю           

            MySqlConnectionStringBuilder mysqlCSB;
            mysqlCSB = new MySqlConnectionStringBuilder();
            mysqlCSB.Server = server;
            mysqlCSB.Database = database;
            mysqlCSB.UserID = UserID;
            mysqlCSB.Password = Password;
            // string getDocTime = null;

            bool temp;
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = mysqlCSB.ConnectionString;
            MySqlCommand cmd = new MySqlCommand();



            con.Open();
            cmd.CommandText = "SELECT EXISTS (SELECT * FROM enx4w_ttfsp_sprtime WHERE id = @docId)";//',9,'
            cmd.Parameters.AddWithValue("@docId", _doctimeid);
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            var i = cmd.ExecuteScalar();
            if (Convert.ToInt32(i) == 1)
            {
                temp = true;
            }
            else
            {
                temp = false;
            }

            con.Close();

            return temp;
        }



        public void updateCurr(string publickTime, string privaTetime, string DocID)
        {
            MySqlConnectionStringBuilder mysqlCSB;
            mysqlCSB = new MySqlConnectionStringBuilder();
            mysqlCSB.Server = server;
            mysqlCSB.Database = database;
            mysqlCSB.UserID = UserID;
            mysqlCSB.Password = Password;

            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = mysqlCSB.ConnectionString;
            MySqlCommand cmd = new MySqlCommand();


            con.Open();
            //Recording publick times to base
            cmd.CommandText = "UPDATE enx4w_ttfsp_sprtime SET timehm=@publickTime WHERE id = @docId";//',9,'
            cmd.Parameters.AddWithValue("@publickTime", publickTime);
            cmd.Parameters.AddWithValue("@docId", DocID);
            cmd.Connection = con;
            cmd.ExecuteNonQuery();

            //Recording  private times to base
            cmd.CommandText = "UPDATE enx4w_ttfsp_sprtime SET timeprv=@privaTetime WHERE id = @docId";//',9,'
            cmd.Parameters.AddWithValue("@privaTetime", privaTetime);
            cmd.Connection = con;
            cmd.ExecuteNonQuery();

            //synhronyze.SynhronyzeTable("ekfgq_ttfsp_sprtime", 2);

        }


        public List<string> getParseTime(string time)
        {
            List<string> tempTime = new List<string>();
            string[] cutTime = time.Split(new char[] { '\r' });
            foreach (var a in cutTime) { tempTime.Add(a); }
            return tempTime;
        }
        public string GetStat(string time, DateTime date, string docId)
        {
            MySqlConnectionStringBuilder mysqlCSB;
            mysqlCSB = new MySqlConnectionStringBuilder();
            mysqlCSB.Server = server;
            mysqlCSB.Database = database;
            mysqlCSB.UserID = UserID;
            mysqlCSB.Password = Password;
            string[] parTime = time.Split(new char[] { ':' });
            string result = "";
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = mysqlCSB.ConnectionString;
            MySqlCommand cmd = new MySqlCommand();
            // List<Times> temp = new List<Times>();
            con.Open();
            cmd.CommandText = "SELECT * FROM enx4w_ttfsp_dop WHERE id_specialist = @docId AND date=@dateDB AND hours=@Hours AND minutes=@Mins";//',9,'
            cmd.Parameters.AddWithValue("@docId", docId);
            cmd.Parameters.AddWithValue("@dateDB", date);
            cmd.Parameters.AddWithValue("@Hours", parTime[0]);
            cmd.Parameters.AddWithValue("@Mins", parTime[1]);
            cmd.Connection = con;
            cmd.ExecuteNonQuery();

            using (MySqlDataReader dr = cmd.ExecuteReader())
            {
                //while (dr.Read())
                //{

                if (dr.HasRows == true) { result = "Red"; }
                if (dr.HasRows == false) { result = "Green"; }
                // a = dr.GetString("rfio");
                //  }
            }
            con.Close();


            return result;
        }
        #endregion


        public ObservableCollection<DateTime> GetListOfWorkingDays(int _docId)
        {
            //synhronyze.SynhronyzeTable("ekfgq_ttfsp", 1);

            MySqlConnectionStringBuilder mysqlCSB;
            mysqlCSB = new MySqlConnectionStringBuilder();
            mysqlCSB.Server = server;
            mysqlCSB.Database = database;
            mysqlCSB.UserID = UserID;
            mysqlCSB.Password = Password;
             mysqlCSB.ConvertZeroDateTime = true;

            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = mysqlCSB.ConnectionString;
            MySqlCommand cmd = new MySqlCommand();

            int i = 0;

            ObservableCollection<DateTime> temp = new ObservableCollection<DateTime>();

            con.Open();
            cmd.CommandText = "SELECT dttime FROM enx4w_ttfsp WHERE idspec = @DocID";
            cmd.Parameters.AddWithValue("@DocID", _docId);

            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            using (MySqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    i++;
                    temp.Add(dr.GetDateTime("dttime"));




                }
            }
            con.Close();
            //DataTable a = get_ekfgq_ttfsp();

            //List<DataRow> t0 = a.AsEnumerable().Where(row=>row.Field<int>("idspec")==Convert.ToInt32(_docId)).ToList();

            //foreach( var c in t0)
            //{
            //    //temp.Add(c.Field<MySqlDateTime>("dttime").GetDateTime());
            //}

             return temp;
        }

        public static long ConvertToUnixTime(DateTime datetime)
        {
            DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return (long)(datetime - sTime).TotalSeconds;
        }
        public void addWorkDays(string idSpec, string idUser, bool recetion, bool published, DateTime dttime, string hrtime, string mntime, string ordering, string checked_out)
        {

            MySqlConnectionStringBuilder mysqlCSB;
            mysqlCSB = new MySqlConnectionStringBuilder();
            mysqlCSB.Server = server;
            mysqlCSB.Database = database;
            mysqlCSB.UserID = UserID;
            mysqlCSB.Password = Password;
            string Id = null;
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = mysqlCSB.ConnectionString;
            MySqlCommand cmd = new MySqlCommand();

            con.Open();
            cmd.CommandText = "INSERT INTO enx4w_ttfsp(id, idspec,iduser,reception, published, dttime,hrtime,mntime,ordering,checked_out,ttime)" +
                " VALUES(@ID,@idSpec,@idUser,@reception,@published,@dttime,@hrtime,@mntime,@ordering,@checked_out,@ttime)";
            var ttime = ConvertToUnixTime(dttime);
            cmd.Parameters.AddWithValue("@ID", Id);
            cmd.Parameters.AddWithValue("@idSpec", idSpec);
            cmd.Parameters.AddWithValue("@idUser", idUser);
            cmd.Parameters.AddWithValue("@reception", recetion);
            cmd.Parameters.AddWithValue("@published", published);
            cmd.Parameters.AddWithValue("@dttime", dttime);
            cmd.Parameters.AddWithValue("@hrtime", hrtime);
            cmd.Parameters.AddWithValue("@mntime", mntime);
            cmd.Parameters.AddWithValue("@ordering", ordering);
            cmd.Parameters.AddWithValue("@checked_out", checked_out);
            cmd.Parameters.AddWithValue("@ttime", ttime);
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();

            //synhronyze.SynhronyzeTable("ekfgq_ttfsp", 2);
        }

        public void remWorkDays(string idSpec, DateTime dttime)
        {

            MySqlConnectionStringBuilder mysqlCSB;
            mysqlCSB = new MySqlConnectionStringBuilder();
            mysqlCSB.Server = server;
            mysqlCSB.Database = database;
            mysqlCSB.UserID = UserID;
            mysqlCSB.Password = Password;
            string Id = null;
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = mysqlCSB.ConnectionString;
            MySqlCommand cmd = new MySqlCommand();

            con.Open();
            cmd.CommandText = "DELETE FROM enx4w_ttfsp WHERE idspec =@idSpec AND dttime=@dttime";
            cmd.Parameters.AddWithValue("@idSpec", idSpec);
            cmd.Parameters.AddWithValue("@dttime", dttime);
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();

            //synhronyze.SynhronyzeTable("ekfgq_ttfsp", 2);
        }
        public void RemTimeInWorkDay(string idSpec, DateTime dttime, string _hrtime, string _mntime)
        {

            MySqlConnectionStringBuilder mysqlCSB;
            mysqlCSB = new MySqlConnectionStringBuilder();
            mysqlCSB.Server = server;
            mysqlCSB.Database = database;
            mysqlCSB.UserID = UserID;
            mysqlCSB.Password = Password;
            string Id = null;
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = mysqlCSB.ConnectionString;
            MySqlCommand cmd = new MySqlCommand();

            con.Open();
            cmd.CommandText = "DELETE FROM enx4w_ttfsp WHERE idspec =@idSpec AND dttime=@dttime AND hrtime=@hrtime AND mntime=@mntime";
            cmd.Parameters.AddWithValue("@idSpec", idSpec);
            cmd.Parameters.AddWithValue("@dttime", dttime);
            cmd.Parameters.AddWithValue("@hrtime", _hrtime);
            cmd.Parameters.AddWithValue("@mntime", _mntime);
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();

            //synhronyze.SynhronyzeTable("ekfgq_ttfsp", 2);
        }



        #region INSERT IN TO BASE

        /// <summary>
        /// Запись в таблицу ekfgq_ttfsp_dop для отображения записаного талончика.
        ///  Всё что с перфиксом "_" на прямую идет в команд параметр остальные переменные переприсваиваються внутри
        /// </summary>
        /// <param name="Iduser"></param>
        /// <param name="FIOuser"></param>
        /// <param name="Userphone"></param>
        /// <param name="UserMail"></param>
        /// <param name="_date"></param>
        /// <param name="_hours"></param>
        /// <param name="_minutes"></param>
        public void INsertTheApointment(string Iduser, int _id_specialist, string FIOuser, string Userphone, string UserMail, string _specializations_name, string _specialist_name, string _specialist_email, DateTime _date, string _hours, string _minutes, string _number_cabinet = "0")
        {
            MySqlConnectionStringBuilder mysqlCSB;
            mysqlCSB = new MySqlConnectionStringBuilder();
            mysqlCSB.Server = server;
            mysqlCSB.Database = database;
            mysqlCSB.UserID = UserID;
            mysqlCSB.Password = Password;
            mysqlCSB.ConvertZeroDateTime = true;

            string tempOrder = GetNumberOrder();
            string[] tempArray = tempOrder.Split(new char[] { '-' });
            tempOrder = tempArray[0];
            #region private variables for SQLparametrs
            // int idrec; где его взять и что єто вообще
            int _iduser = Convert.ToInt32(Iduser);

            int _ordering = GetOrdering();
            string _rfio = FIOuser;
            string _rphone = Userphone;
            string _info = _date.ToShortDateString() + " " + _hours + ":" + _minutes + " <br /><u>ФИО: </u> " + _rfio + " <br /><u>Телефон: </u>" + _rphone;
            string _ipuser = "111.111.111.111";//рандомно взятый с базы
            string _rmail = UserMail;
            string _number_order = (Convert.ToInt32(tempOrder) + 1).ToString() + "-REG";
            int _cdate = 1485730860;//константа с базы
            string _office_name = "Поликлиника №4";
            //   string _specializations_name = "Терапевт";// с клача врача
            //  string _specialist_name = "витащить с класа врача";
            //   string _specialist_email = "витащить с класа врача";
            string _order_password = "V9EFJP";// скопировано с  базы возможно нужно генерировать
            string _office_address = "м.Шостка, вул. Щедрина, 1 Телефони:\r+ 38(05449) 3 - 28 - 95,\r+38(05449) 3 - 23 - 52";
            //   int _number_cabinet = 4;
            #endregion


            UpdateInToBase(_iduser, _rfio, _rphone, _info, _ipuser, _rmail, _id_specialist, _date, _hours, _minutes);
            using (MySqlConnection con = new MySqlConnection(mysqlCSB.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = con;

                    con.Open();


                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText =
                    "INSERT INTO enx4w_ttfsp_dop(iduser, id_specialist, ordering, rfio, rphone, info, ipuser, rmail, number_order, cdate, date, hours, minutes, office_name, specializations_name, specialist_name, specialist_email, order_password, office_address, number_cabinet) " +
                    "VALUES(@iduser, @id_specialist, @ordering, @rfio, @rphone, @info, @ipuser, @rmail, @number_order, @cdate, @date, @hours, @minutes, @office_name, @specializations_name, @specialist_name, @specialist_email, @order_password, @office_address, @number_cabinet)";

                    #region Command Parametrs

                    cmd.Parameters.AddWithValue("@iduser", _iduser);
                    cmd.Parameters.AddWithValue("@id_specialist", _id_specialist);
                    cmd.Parameters.AddWithValue("@ordering", _ordering);
                    cmd.Parameters.AddWithValue("@rfio", _rfio);
                    cmd.Parameters.AddWithValue("@rphone", _rphone);
                    cmd.Parameters.AddWithValue("@info", _info);
                    cmd.Parameters.AddWithValue("@ipuser", _ipuser);
                    cmd.Parameters.AddWithValue("@rmail", _rmail);
                    cmd.Parameters.AddWithValue("@number_order", _number_order);
                    cmd.Parameters.AddWithValue("@cdate", _cdate);
                    cmd.Parameters.AddWithValue("@date", _date);
                    cmd.Parameters.AddWithValue("@hours", _hours);
                    cmd.Parameters.AddWithValue("@minutes", _minutes);
                    cmd.Parameters.AddWithValue("@office_name", _office_name);
                    cmd.Parameters.AddWithValue("@specializations_name", _specializations_name);
                    cmd.Parameters.AddWithValue("@specialist_name", _specialist_name);
                    cmd.Parameters.AddWithValue("@specialist_email", _specialist_email);
                    cmd.Parameters.AddWithValue("@order_password", _order_password);
                    cmd.Parameters.AddWithValue("@office_address", _office_address);
                    cmd.Parameters.AddWithValue("@number_cabinet", _number_cabinet);

                    #endregion

                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    con.Close();

                }
            }
            // synhronyze.SynhronyzeTable("ekfgq_ttfsp_dop", 2);

        }

        /// <summary>
        /// Обновление  таблицы ekfgq_ttfsp для отобреженя занятого талона на сайте
        /// </summary>
        public void UpdateInToBase(int _iduser, string _rfio, string _rphone, string _info, string _ipuser, string _rmail, int _idspec, DateTime _dttime, string _hours, string _minutes)
        {

            MySqlConnectionStringBuilder mysqlCSB;
            mysqlCSB = new MySqlConnectionStringBuilder();
            mysqlCSB.Server = server;
            mysqlCSB.Database = database;
            mysqlCSB.UserID = UserID;
            mysqlCSB.Password = Password;
            mysqlCSB.ConvertZeroDateTime = true;




            using (MySqlConnection con = new MySqlConnection(mysqlCSB.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = con;

                    con.Open();


                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE enx4w_ttfsp SET iduser=@iduser, reception='1', rfio=@rfio, rphone=@rphone, info=@info, ipuser=@ipuser, rmail=@rmail" +
                        " WHERE idspec=@idspec AND dttime=@dttime AND hrtime=@hrtime AND mntime=@mntime";

                    #region Command Parametrs

                    cmd.Parameters.AddWithValue("@iduser", _iduser);
                    cmd.Parameters.AddWithValue("@rfio", _rfio);
                    cmd.Parameters.AddWithValue("@rphone", _rphone);
                    cmd.Parameters.AddWithValue("@info", _info);
                    cmd.Parameters.AddWithValue("@ipuser", _ipuser);
                    cmd.Parameters.AddWithValue("@rmail", _rmail);
                    cmd.Parameters.AddWithValue("@idspec", _idspec);
                    cmd.Parameters.AddWithValue("@dttime", _dttime);
                    cmd.Parameters.AddWithValue("@hrtime", _hours);
                    cmd.Parameters.AddWithValue("@mntime", _minutes);

                    #endregion

                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();



                }
            }


        }
        /// <summary>
        /// получение  максимального значнеия поредяка для записи в базу
        /// </summary>
        /// <returns></returns>
        private int GetOrdering()
        {
            MySqlConnectionStringBuilder mysqlCSB;
            mysqlCSB = new MySqlConnectionStringBuilder();
            mysqlCSB.Server = server;
            mysqlCSB.Database = database;
            mysqlCSB.UserID = UserID;
            mysqlCSB.Password = Password;
            mysqlCSB.ConvertZeroDateTime = true;

            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = mysqlCSB.ConnectionString;
            MySqlCommand cmd = new MySqlCommand();

            int i = 0;

            con.Open();
            cmd.CommandText = "SELECT ordering FROM enx4w_ttfsp_dop WHERE ordering = (SELECT MAX(ordering) FROM  enx4w_ttfsp_dop )";
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            using (MySqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    i = Convert.ToInt32(dr.GetString("ordering"));
                }
            }
            con.Close();

            return i + 1;
        }

        /// <summary>
        /// Получение  последнего "number order" для записи в базу
        /// </summary>
        private int _ILimit = 1;
        private string GetNumberOrder()
        {
            MySqlConnectionStringBuilder mysqlCSB;
            mysqlCSB = new MySqlConnectionStringBuilder();
            mysqlCSB.Server = server;
            mysqlCSB.Database = database;
            mysqlCSB.UserID = UserID;
            mysqlCSB.Password = Password;
            mysqlCSB.ConvertZeroDateTime = true;

            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = mysqlCSB.ConnectionString;
            MySqlCommand cmd = new MySqlCommand();

            string temp = null;
            con.Close();
            con.Open();
            cmd.CommandText = "SELECT number_order FROM enx4w_ttfsp_dop ORDER BY id DESC LIMIT @ILimit";
            cmd.Parameters.AddWithValue("@ILimit", _ILimit);
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            using (MySqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {

                    temp = dr.GetString("number_order");
                }
            }

            con.Close();
            //проверка если номер порядка пустой возьми на одну запись больше
            if (temp == "")
            {
                _ILimit = _ILimit + 1;

                temp = GetNumberOrder();
            }
            else
            {
                _ILimit = 1;
            }

            return temp;



        }
        /// <summary>
        /// Обновление поля в таблице которое отвечает за отображенье приема врача (По времени/По талонам)
        /// </summary>
        /// <param name="_docid"></param>
        /// <param name="_parametr"> Росписнаие : True - по талонам false - по времени</param>
        public void InsertTalonTime(int _docid, bool _parametr)
        {
            MySqlConnectionStringBuilder mysqlCSB;
            mysqlCSB = new MySqlConnectionStringBuilder();
            mysqlCSB.Server = server;
            mysqlCSB.Database = database;
            mysqlCSB.UserID = UserID;
            mysqlCSB.Password = Password;
            mysqlCSB.ConvertZeroDateTime = true;


            using (MySqlConnection con = new MySqlConnection(mysqlCSB.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = con;

                    con.Open();


                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE talon_time SET parametr=@parametr" +
                        " WHERE doctor_id=@doctor_id ";

                    #region Command Parametrs


                    cmd.Parameters.AddWithValue("@parametr", _parametr);
                    cmd.Parameters.AddWithValue("@doctor_id", _docid);

                    #endregion

                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();



                }
            }

            // synhronyze.SynhronyzeTable("talon_time", 2);
        }

        #endregion






    }
}
