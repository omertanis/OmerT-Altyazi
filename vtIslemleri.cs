using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;

namespace bootstrapWeb
{
    public class vtIslemleri
    {
        private string connStr = "Data Source=Omer; User Id = omert; Password=omert;";
        private OracleDataAdapter da;
        private DataTable dt;
        private string query;
        private OracleCommand command;
        private OracleConnection connection;
        private OracleDataReader dataReader;
        private OracleDataAdapter adapter;

        /// <summary>
        /// Kullanıcıyı veritabanına ekler
        /// </summary>
        /// <param name="isim">isim</param>
        /// <param name="eposta">eposta</param>
        /// <param name="sifre">şifre</param>
        /// <returns>Eğer eklendi ise geriye true döndürür</returns>
        public bool kullaniciKayit(string isim, string eposta, string sifre)
        {
            connection = new OracleConnection(connStr);
            connection.Open();
            //conn = new SqlConnection(connStr);
            query = "Insert Into Users(UsersID,Name,EMail,Password,Roles) values(USERS_ID_AUTO.nextval,:ad,:posta,:pass,:role)";
            //cmd = new SqlCommand(query, conn);
            command = new OracleCommand(query, connection);
            try
            {
                //conn.Open();
                command.Parameters.Add(new OracleParameter(":ad", isim));
                command.Parameters.Add(new OracleParameter(":posta", eposta));
                command.Parameters.Add(new OracleParameter(":pass", sifre));
                command.Parameters.Add(new OracleParameter(":role", "User"));
                command.ExecuteNonQuery();
                return true;
            }
            catch (OracleException)
            {
                connection.Close();
                return false;
                throw new Exception("SQL Bağlantı hatası");
            }
            finally
            {
                connection.Close();
            }
        }


        /// <summary>
        /// Kullanıcı girişini yapar. 
        /// </summary>
        /// <param name="email">kullanıcı eposta</param>
        /// <param name="sifre">kullanıcı şifre</param>
        /// <returns>Giriş yapılırsa [0] id, [1] isim , [2] email, [3] rol</returns>
        public string[] kullaniciGiris(string email,string sifre)
        {
           query = "Select UsersID,Name,EMail,Roles from Users where EMail = :email and Password = :password";
           connection = new OracleConnection(connStr);
           string [] bilgiler = null;
           try
           {
               connection.Open();
                command = new OracleCommand(query, connection);
                command.Parameters.Add(new OracleParameter("@email", email));
                command.Parameters.Add(new OracleParameter("@password", sifre));
                dataReader = command.ExecuteReader();
                if (dataReader.Read())
               {
                   bilgiler = new string[4];
                   bilgiler[0] = dataReader["UsersID"].ToString();
                   bilgiler[1] = dataReader["name"].ToString();
                   bilgiler[2] = dataReader["EMail"].ToString();
                   bilgiler[3] = dataReader["Roles"].ToString();
                   return bilgiler;
               }
               else
               {
                   return bilgiler;
               }
           }
            catch(Exception)
           {
               throw new Exception("vtIslemler.kullaniciGiris te hata var");
           }
            finally
           {
               connection.Close();
           }
        }

        /// <summary>
        /// Tüm kullanıcıları döndürür 
        /// </summary>
        /// <returns>datatable olarak veriler</returns>
        public DataTable kullaniciListesi()
        {
            try
            {
                connection = new OracleConnection(connStr);
                query = "Select * from Users";
                command = new OracleCommand(query);
                da = new OracleDataAdapter();
                dt = new DataTable();
                connection.Open();
                command.Connection = connection;
                da.SelectCommand = command;
                da.Fill(dt);
                return dt;
            }
            catch (OracleException)
            {
                connection.Close();
                throw new Exception("SQL Bağlantı Hatası");
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Email girilen kullanıcıları listeler
        /// </summary>
        /// <param name="email">kullanıcı eposta</param>
        /// <returns> datatable olarak bilgiler</returns>
        public DataTable kullaniciListesi(string email)
        {
            try
            {
                connection = new OracleConnection(connStr);
                da = new OracleDataAdapter();
                dt = new DataTable();
                connection = new OracleConnection(connStr);
                query = "Select * from Users" +
                        " where EMail like '%" + email + "%' " +
                        " order by EMail";
                command = new OracleCommand(query);
                da = new OracleDataAdapter();
                dt = new DataTable();
                connection.Open();
                command.Connection = connection;
                da.SelectCommand = command;
                da.Fill(dt);
                return dt;
            }
            catch (OracleException)
            {
                connection.Close();
                throw new Exception("SQL Bağlantı Hatası");
            }
            finally
            {
                connection.Close();
            }
        }
        /// <summary>
        ///  Film listesini almaya yarar
        /// </summary>
        /// <returns> Datatable olarak film listesi
        /// </returns>
        public DataTable filmListesi()
        {
            try
            {
                connection = new OracleConnection(connStr);
                query = "Select FilmsID , Name from Films order by Name";
                command = new OracleCommand(query);
                adapter = new OracleDataAdapter();
                dt = new DataTable();
                connection.Open();
                command.Connection = connection;
                adapter.SelectCommand = command;
                adapter.Fill(dt);
                return dt;
            }
            catch(OracleException)
            {
                connection.Close();
                throw new Exception("SQL Bağlantı Hatası");
            }
            finally
            {
                connection.Close();
            }
        }
        /// <summary>
        /// Film listesinde arama yazısı bulunanları getirir
        /// </summary>
        /// <param name="ad">Aranacak kelime</param>
        /// <returns>DTable olarak filmlistesi</returns>
        public DataTable filmListesi(string ad)
        {
            try
            {
                connection = new OracleConnection(connStr);
                query = "Select FilmsID , Name from Films";
                query += " where Name like '%" + ad + "%' ";
                query += " order by Name";
                command = new OracleCommand(query);
                adapter = new OracleDataAdapter();
                dt = new DataTable();
                connection.Open();
                command.Connection = connection;
                adapter.SelectCommand = command;
                adapter.Fill(dt);
                return dt;
            }
            catch (OracleException err)
            {
                connection.Close();
                throw new Exception("SQL Bağlantı Hatası");
            }
            finally
            {
                connection.Close();
            }
        }
        

        /// <summary>
        /// altyazıların tümünü listeler
        /// </summary>
        /// <returns>datatable olarak bilgiler</returns>
        public DataTable altyaziListesi()
        {
            try 
            {
                connection = new OracleConnection(connStr);
                query  = "Select s.SubtitlesID, s.Name  as sName , s.Directory "
                        + ", f.Name as fName , u.Name as uName "
                        + "from Subtitles s "
                        + "Inner Join Films f on f.FilmsID = s.FilmsID "
                        + "Inner Join Users u on s.UsersID = u.UsersID "
                        + "Order by sName";
                command = new OracleCommand(query);
                da = new OracleDataAdapter();
                dt = new DataTable();
                connection.Open();
                command.Connection = connection;
                da.SelectCommand = command;
                da.Fill(dt);
                return dt;
            }
            catch (OracleException err)
            {
                connection.Close();
                throw new Exception("SQL Bağlantı Hatası" + err.ToString());
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// altyazıları altyazı adına göre aratır
        /// </summary>
        /// <param name="ad">altyazı ad</param>
        /// <returns>datatable olarak bilgiler</returns>
        public DataTable altyaziListesi(string ad)
        {
            try
            {
                connection = new OracleConnection(connStr);
                query = "Select s.SubtitlesID, s.Name sName , s.Directory , f.Name as fName ," +
                            "u.Name as uName from Subtitles s " +
                            "Inner Join Films f on f.FilmsID = s.FilmsID " +
                            "Inner Join Users u on u.UsersID = s.UsersID " +
                            " where s.Name like '%" + ad + "%' "+
                            " order by sName";
                command = new OracleCommand(query);
                da = new OracleDataAdapter();
                dt = new DataTable();
                connection.Open();
                command.Connection = connection;
                da.SelectCommand = command;
                da.Fill(dt);
                return dt;
            }
            catch (OracleException err)
            {
                connection.Close();
                throw new Exception("SQL Bağlantı Hatası");
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Altyazıları listeler
        /// </summary>
        /// <param name="filmID">Filmin ID sine göre arama yapılır.</param>
        /// <returns>Geriye Datatable olarak döndürür</returns>
        public DataTable altyaziListesiFilm(string filmID)
        {
            try
            {
                /*"SELECT f.Name ""Film Adı"",s.Name as ""Altyazı Adı"" ,s.Directory ,u.Name as ""Yükleyenin Adı""                 +"FROM Subtitles s"
                + "Inner join Films f on f.FilmsID = s.FilmsID"
                + "inner join Users u on u.UsersID = s.UsersID"
                + "where f.FilmsID = 1*/

                connection = new OracleConnection(connStr);
                da = new OracleDataAdapter();
                dt = new DataTable();
                query = "SELECT f.Name film_Adi ,s.Name Altyazi_Adi ,s.Directory ,"
                    + "u.Name Yukleyenin_Adi FROM Subtitles s Inner join Films f on f.FilmsID=s.FilmsID "
                    + "inner join Users u on u.UsersID = s.UsersID where f.FilmsID = :1";
                command = new OracleCommand(query,connection);
                /*command.Parameters.Add(new OracleParameter("@filmAdi", "FilmAdı"));
                command.Parameters.Add(new OracleParameter("@AltyaziAdi", "AltyazıAdı"));
                command.Parameters.Add(new OracleParameter("@YukleyeninAdi", "YükleyeninAdı"));*/
                command.Parameters.Add(new OracleParameter("@1", filmID));
                connection.Open();
                command.Connection = connection;
                da.SelectCommand = command;
                da.Fill(dt);
                return dt;
            }
            catch (OracleException)
            {
                connection.Close();
                throw new Exception("SQL Bağlantı Hatası");
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Kullanıcıya ait altyazıları listeler
        /// </summary>
        /// <param name="userID">Kullanıcı ID si</param>
        /// <returns>datatable olarak döndürür</returns>
        public DataTable altyaziListesiKullanici(string userID)
        {
            try
            {
                connection = new OracleConnection(connStr);
                da = new OracleDataAdapter();
                dt = new DataTable();
                query = "Select s.SubtitlesID, s.Name  as sName , s.Directory , f.Name as fName , "
                    + "u.Name as uName from Subtitles s Inner Join Films f on f.FilmsID = s.FilmsID "
                    + "Inner Join Users u on s.UsersID = u.UsersID where u.UsersID = "+userID;
                command = new OracleCommand(query, connection);
                connection.Open();
                command.Connection = connection;
                da.SelectCommand = command;
                da.Fill(dt);
                return dt;
            }
            catch (OracleException ex)
            {
                connection.Close();
                throw new Exception("SQL Bağlantı Hatası" + ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }
        /// <summary>
        /// Kullanıcıya ait altyazıları arama kutucuğu ile 
        /// </summary>
        /// <param name="userID">Kullanıcı ID si</param>
        /// <param name="filmAd">Film Adı</param>
        /// <returns>datatable olarak döndürür</returns>
        public DataTable altyaziListesiKullanici(string userID,string filmAd)
        {

            try
            {
                connection = new OracleConnection(connStr);
                da = new OracleDataAdapter();
                dt = new DataTable();
                query = "Select s.SubtitlesID, s.Name sName , s.Directory , f.Name as fName ," +
                        "u.Name as uName from Subtitles s " +
                        "Inner Join Films f on f.FilmsID = s.FilmsID " +
                        "Inner Join Users u on u.UsersID = s.UsersID " +
                        " where s.Name like '%" + filmAd + "%' "
                        + "and u.UsersID = " + userID
                        + " order by sName";
                command = new OracleCommand(query, connection);
                connection.Open();
                command.Connection = connection;
                da.SelectCommand = command;
                da.Fill(dt);
                return dt;
            }
            catch (OracleException)
            {
                connection.Close();
                throw new Exception("SQL Bağlantı Hatası");
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Veritabanına film eklemek için yapılır
        /// </summary>
        /// <param name="filmAdi">filmin adı</param>
        /// <returns>eklenirse true döndürür</returns>
        public bool filmEkle(string filmAdi)
        {
            try
            {
                connection = new OracleConnection(connStr);
                query= "insert into Films(filmsid,Name) Values(FILMS_ID_AUTO.nextval,'" + filmAdi + "')";
                connection = new OracleConnection(connStr);
                command = new OracleCommand(query, connection);
                connection.Open();
                //"insert into altyaziDb.dbo.Films" + "(Name) Values('" + filmAdi + "')";
                command.ExecuteNonQuery();
                return true;
            }
            catch(OracleException)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// altyazı eklemeye yarar
        /// </summary>
        /// <param name="ad">film adı</param>
        /// <param name="kaynak">filmin kaydedildiği yer</param>
        /// <param name="filmid">film id si</param>
        /// <param name="userid">user id si</param>
        /// <returns>eklenirse true</returns>
        public bool altyaziEkle(string ad, string kaynak, string filmid, string userid)
        {
            try
            {
                connection = new OracleConnection(connStr);
                connection.Open();
                query = "Insert Into Subtitles (SubtitlesID,Name,Directory,FilmsID,UsersID) Values( SUBTITLES_ID_AUTO.nextval, :ad, :kaynak, :filmid, :userid)";
                command = new OracleCommand(query, connection);

                command.Parameters.Add(new OracleParameter("@ad", ad));
                command.Parameters.Add(new OracleParameter("@kaynak", kaynak));
                command.Parameters.Add(new OracleParameter("@filmid", filmid));
                command.Parameters.Add(new OracleParameter("@userid", userid));
                command.ExecuteNonQuery();
                return true;
             }
             catch(OracleException)
             {
                  connection.Close();
                  return false;
                  throw new Exception("SQL Bağlantı hatası");
             }
             finally
             {
                  connection.Close();
             }
                        
        }

        public bool bilgilerimiGuncelle(string ad,string sifre,string id)
        {
            try
            {
                connection = new OracleConnection(connStr);
                connection.Open();
                query = "update Users set Name = :ad , Password = :sifre where UsersID = :id";
                command = new OracleCommand(query, connection);
                command.Parameters.Add(new OracleParameter("@ad", ad));
                command.Parameters.Add(new OracleParameter("@sifre", sifre));
                command.Parameters.Add(new OracleParameter("@id", id));
                command.ExecuteNonQuery();
                return true;
            }
            catch (OracleException)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }


        /// <summary>
        /// Seçme işlemini sorgunun el ile girilmesi ile yapar
        /// </summary>
        /// <param name="sorgu">Select * from kisiler</param>
        /// <returns>datatable</returns>
        public DataTable sec(string sorgu)
        {
            try
            {
                connection = new OracleConnection(connStr);
                command = new OracleCommand(sorgu);
                da = new OracleDataAdapter();
                dt = new DataTable();
                connection.Open();
                command.Connection = connection;
                da.SelectCommand = command;
                da.Fill(dt);
                return dt;
            }
            catch (OracleException ex)
            {
                connection.Close();
                throw new Exception("SQL Bağlantı Hatası "+ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        //Bah bunlar lazım

        /// <summary>
        /// Sorgu el ile girilerek silme işlemi yapılır
        /// </summary>
        /// <param name="sorgu">"Delete from table where condition"</param>
        public bool sil(string sorgu)
        {
            try
            {
                connection = new OracleConnection(connStr);
                command = new OracleCommand(sorgu, connection);
                connection.Open();
                // string silmeSorgusu = "DELETE from musteriler where musterino=@musterino";
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
                connection.Close();
                return false;
            }
            finally
            {
                connection.Close();
            }
            
            
        }
        /// <summary>
        /// Sorgu el ile girilerek güncelleme yapar
        /// </summary>
        /// <param name="sorgu">"Update table set (column=value ,...) where condition</param>
        /// <returns>true if updated</returns>
        public bool guncelle(string sorgu)
        {

            try
            {
                connection = new OracleConnection(connStr);
                command = new OracleCommand(sorgu, connection);
                connection.Open();
                //UPDATE kisiler SET Ad=@ad,Soyad=@soyad,Yas=@yas,Tarih=@tarih,Onay=@onay WHERE ID=@id
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch
            {
                connection.Close();
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
        /// <summary>
        /// Sorguyu el ile girerek insert yapar
        /// </summary>
        /// <param name="sorgu">insert into table (columns[]) values(values[])</param>
        /// <returns>true if inserted</returns>
        public bool ekle(string sorgu)
        {
            try
            {
                connection = new OracleConnection(connStr);
                command = new OracleCommand(sorgu, connection);
                connection.Open();
                //"insert into altyaziDb.dbo.Films" + "(Name) Values('" + filmAdi + "')";
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch
            {
                connection.Close();
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}