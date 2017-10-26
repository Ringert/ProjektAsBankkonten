using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

using ProjektAsBankkonto.Interfaces.Datenhaltung;
using ProjektAsBankkonto.Datenhaltung.Model;
using ProjektAsBankkonto.Datenhaltung.Enums;


namespace ProjektAsBankkonto.Datenhaltung
{
    public class SQLiteController : IDatenhaltung
    {
        private SQLiteConnection m_dbConnection;

        public SQLiteController()
        {
            this.m_dbConnection = new SQLiteConnection("Data Source=ProjektAsBankkonten.sqlite;Version=3;");
            this.m_dbConnection.Open();
            this.checkTables();
        }

        private void checkTables()
        {
            SQLiteCommand command;

            if (!this.checkTableExists("kunden")) {
                string sql = 
                    "CREATE TABLE kunden(" 
                    +   "   kunde_nr   INT             PRIMARY KEY NOT NULL," 
                    +   "   strasse    VARCHAR(100)    NOT NULL," 
                    +   "   plz        VARCHAR(10)     NOT NULL," 
                    +   "   ort        VARCHAR(100)    NOT NULL," 
                    +   "   land       INT             NOT NULL,"
                    +   "   geschlecht VARCHAR(1)      NOT NULL," 
                    +   "   vorname    VARCHAR(255)    NOT NULL," 
                    +   "   nachname   VARCHAR(255)    NOT NULL,"
                    +   "   geb_dat    DateTime        NOT NULL" 
                    +   ");";
                command = new SQLiteCommand(sql, this.m_dbConnection);
                command.ExecuteNonQuery();
            }
            if (!this.checkTableExists("konten"))
                return;
            if (!this.checkTableExists("filialen"))
                return;
       
        }
        private bool checkTableExists(string tableName)
        {
            string sql = "SELECT count(*) FROM sqlite_master WHERE name = @val1;";
            SQLiteCommand command = new SQLiteCommand(sql, this.m_dbConnection);
            command.Parameters.Add(new SQLiteParameter("@val1", System.Data.DbType.String) { Value = tableName });

            SQLiteDataReader reader = command.ExecuteReader();
            return (reader.FieldCount == 1);
        }

        private int getLastInsertedId()
        {
            string sql = "SELECT last_insert_rowid();";
            SQLiteCommand command = new SQLiteCommand(sql, this.m_dbConnection);
            object obj = command.ExecuteScalar();
            int id = (int)obj;
            return id;
        }
        /********************************* Kunde *********************************/
        public bool addKunde(ref Kunde kunde)
        {
            string sql = "INSERT INTO kunde (vorname, nachname, geb_dat, geschlecht, strasse, plz, ort, land) VALUES (@val1,@val2,@val3,@val4,@val5,@val6,@val7,@val8);";
            SQLiteCommand command = new SQLiteCommand(sql, this.m_dbConnection);
            command.Parameters.Add(new SQLiteParameter("@val1", System.Data.DbType.String) { Value = kunde.Vorname });
            command.Parameters.Add(new SQLiteParameter("@val2", System.Data.DbType.String) { Value = kunde.Nachname });
            command.Parameters.Add(new SQLiteParameter("@val3", System.Data.DbType.DateTime) { Value = kunde.Geburtsdatum });
            command.Parameters.Add(new SQLiteParameter("@val4", System.Data.DbType.String) { Value = kunde.Geschlecht });
            command.Parameters.Add(new SQLiteParameter("@val5", System.Data.DbType.String) { Value = kunde.Strasse });
            command.Parameters.Add(new SQLiteParameter("@val6", System.Data.DbType.String) { Value = kunde.Plz });
            command.Parameters.Add(new SQLiteParameter("@val7", System.Data.DbType.Int32) { Value = kunde.Ort });
            command.Parameters.Add(new SQLiteParameter("@val8", System.Data.DbType.String) { Value = kunde.Land });

            int rowCount = command.ExecuteNonQuery();
            kunde.KundeNr = this.getLastInsertedId();
            if(rowCount > 0)
            {
                Kunde.Instances[kunde.KundeNr] = kunde;
            }
            return (rowCount > 0);
        }
        public bool editKunde(Kunde kunde)
        {
            string sql = "UPDATE kunden SET vorname = @val1, nachname = @val2, geb_dat = @val3, geschlecht = @val4, strasse = @val5, plz = @val6, ort = @val7, land = @val8 WHERE kunde_nr = @val9;";
            SQLiteCommand command = new SQLiteCommand(sql, this.m_dbConnection);
            command.Parameters.Add(new SQLiteParameter("@val1", System.Data.DbType.String) { Value = kunde.Vorname });
            command.Parameters.Add(new SQLiteParameter("@val2", System.Data.DbType.String) { Value = kunde.Nachname });
            command.Parameters.Add(new SQLiteParameter("@val3", System.Data.DbType.DateTime) { Value = kunde.Geburtsdatum });
            command.Parameters.Add(new SQLiteParameter("@val4", System.Data.DbType.String) { Value = kunde.Geschlecht });
            command.Parameters.Add(new SQLiteParameter("@val5", System.Data.DbType.String) { Value = kunde.Strasse });
            command.Parameters.Add(new SQLiteParameter("@val6", System.Data.DbType.String) { Value = kunde.Plz });
            command.Parameters.Add(new SQLiteParameter("@val7", System.Data.DbType.Int32) { Value = kunde.Ort });
            command.Parameters.Add(new SQLiteParameter("@val8", System.Data.DbType.String) { Value = kunde.Land });
            command.Parameters.Add(new SQLiteParameter("@val9", System.Data.DbType.Int32) { Value = kunde.KundeNr });

            int rowCount = command.ExecuteNonQuery();
            kunde.KundeNr = this.getLastInsertedId();
            if (rowCount > 0)
            {
                Kunde.Instances[kunde.KundeNr] = kunde;
            }
            return (rowCount > 0);
        }
        public bool deleteKunde(Kunde kunde)
        {
            string sql = "DELETE FROM kunden WHERE kunde_nr = @val1;";
            SQLiteCommand command = new SQLiteCommand(sql, this.m_dbConnection);
            command.Parameters.Add(new SQLiteParameter("@val1", System.Data.DbType.Int32) { Value = kunde.KundeNr });

            int rowCount = command.ExecuteNonQuery();
            kunde.KundeNr = this.getLastInsertedId();
            if (rowCount > 0)
            {
                Kunde.Instances.Remove(kunde.KundeNr);
            }
            return (rowCount > 0);
        }
        private Kunde __createKundeFromReader(SQLiteDataReader reader)
        {
            return new Kunde()
            {
                KundeNr         = (int)             reader["kunde_nr"],
                Vorname         = (string)          reader["vorname"],
                Nachname        = (string)          reader["nachname"],
                Geburtsdatum    = DateTime.Parse(
                                  (string)          reader["geb_dat"]
                                ),
                Geschlecht      = (Geschlechter)    reader["geschlecht"],
                Strasse         = (string)          reader["strasse"],
                Plz             = (string)          reader["plz"],
                Ort             = (string)          reader["ort"],
                Land            = (Laender)         reader["land"]
            };
        }

        public Kunde fetchKunde(int kundeNr)
        {
            Kunde kunde;
            if(Kunde.Instances.TryGetValue(kundeNr, out kunde))
            {
                return kunde;
            }

            string sql = "SELECT * FROM kunden WHERE kunde_nr = @val1;";
            SQLiteCommand command = new SQLiteCommand(sql, this.m_dbConnection);
            command.Parameters.Add(new SQLiteParameter("@val1", System.Data.DbType.Int32) { Value = kunde.KundeNr });
        
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();
            kunde = this.__createKundeFromReader(reader);
          
            return kunde;
        }
        public Dictionary<int, Kunde> fetchAllKunden()
        {
            Kunde kunde;

            string sql = "SELECT * FROM kunden;";
            SQLiteCommand command = new SQLiteCommand(sql, this.m_dbConnection);

            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read()) {
                kunde = this.__createKundeFromReader(reader);
                Kunde.Instances[kunde.KundeNr] = kunde;
            }

            return Kunde.Instances;
        }
        public Dictionary<int, Kunde> fetchRangeOfKunden(int nr, int offset)
        {
            Dictionary<int, Kunde> kunden = new Dictionary<int, Kunde>();
            Kunde kunde;

            string sql = "SELECT * FROM kunden LIMIT @val1 OFFSET @val2;";
            SQLiteCommand command = new SQLiteCommand(sql, this.m_dbConnection);
            command.Parameters.Add(new SQLiteParameter("@val1", System.Data.DbType.Int32) { Value = nr });

            command.Parameters.Add(new SQLiteParameter("@val1", System.Data.DbType.Int32) { Value = nr });

            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                kunde = this.__createKundeFromReader(reader);
                Kunde.Instances[kunde.KundeNr] = kunde;
            }

            return Kunde.Instances;
        }
        public int getCountKunden()
        {
            return 0;
        }
        /********************************* Filliale *********************************/
        public bool addFiliale(ref Filiale filiale)
        {
            return true;
        }
        public bool editFiliale(Filiale filiale)
        {
            return true;
        }
        public bool deleteFiliale(Filiale filiale)
        {
            return true;
        }
        public Filiale fetchFiliale(int filialeNr)
        {
            Filiale filiale;
            if (Filiale.Instances.TryGetValue(filialeNr, out filiale))
            {
                return filiale;
            }
            return new Filiale();
        }
        public Dictionary<int, Filiale> fetchAllFilialen()
        {
            return Filiale.Instances;
        }
        public Dictionary<int, Filiale> fetchRangeOfFilialen(int nr, int begin)
        {
            return Filiale.Instances;
        }
        public int getCountFilialen()
        {
            return 0;
        }

        /********************************* Konto *********************************/
        public bool addKonto(ref Konto konto)
        {
            return true;
        }
        public bool editKonto(Konto konto)
        {

            return true;
        }
        public bool deleteKonto(Konto konto)
        {
            string sql = "DELETE FROM konto";
            return true;
        }
        public bool checkKontoNrExists(string kontoNr)
        {
            return true;
        }
        public Konto fetchKonto(string kontoNr)
        {
            Konto konto;
            if (Konto.Instances.TryGetValue(kontoNr, out konto))
            {
                return konto;
            }
            return new Konto();
        }
        public Dictionary<string, Konto> fetchAllKonten()
        {
            return Konto.Instances;
        }
        public Dictionary<string, Konto> fetchRangeOfKonten(int nr, int begin)
        {
            return Konto.Instances;
        }
        public int getCountKonten()
        {
            string sql = "SELECT COUNT(*) anz FROM konto";
            return 0;
        }
    }
}
