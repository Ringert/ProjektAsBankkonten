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
                    +   "   kunde_nr    INT             PRIMARY KEY NOT NULL," 
                    +   "   strasse     VARCHAR(100)    NOT NULL," 
                    +   "   plz         VARCHAR(10)     NOT NULL," 
                    +   "   ort         VARCHAR(100)    NOT NULL," 
                    +   "   land        INT             NOT NULL,"
                    +   "   geschlecht  CHARACTER(1)    NOT NULL," 
                    +   "   vorname     VARCHAR(255)    NOT NULL," 
                    +   "   nachname    VARCHAR(255)    NOT NULL,"
                    +   "   geb_dat     DateTime        NOT NULL" 
                    +   ");";
                command = new SQLiteCommand(sql, this.m_dbConnection);
                command.ExecuteNonQuery();
            }
            if (!this.checkTableExists("filialen"))
            {
                string sql =
                    "CREATE TABLE filialen("
                    + "   filiale_nr    INT             PRIMARY KEY NOT NULL,"
                    + "   blz           CHARACTER(8)    NOT NULL,"
                    + "   strasse       VARCHAR(100)    NOT NULL,"
                    + "   plz           VARCHAR(10)     NOT NULL,"
                    + "   ort           VARCHAR(100)    NOT NULL,"
                    + "   land          INT             NOT NULL,"
                    + ");";
                command = new SQLiteCommand(sql, this.m_dbConnection);
                command.ExecuteNonQuery();
            }
            if (!this.checkTableExists("konten"))
            {
                string sql =
                    "CREATE TABLE konten("
                    + "   konto_nr      CHARACTER(8)    PRIMARY KEY NOT NULL,"
                    + "   filiale_nr    INT             NOT NULL,"
                    + "   kunde_nr      INT             NOT NULL,"
                    + "   FOREIGN KEY   (filiale_nr)    REFERENCES filialen(filiale_nr),"
                    + "   FOREIGN KEY   (kunde_nr)      REFERENCES kunden(kunde_nr),"
                    + ");"; 
                command = new SQLiteCommand(sql, this.m_dbConnection);
                command.ExecuteNonQuery();
            }

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
        public bool addKunde(Kunde kunde)
        {
            string sql = "INSERT INTO kunden (vorname, nachname, geb_dat, geschlecht, strasse, plz, ort, land) VALUES (@val1,@val2,@val3,@val4,@val5,@val6,@val7,@val8);";
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
            return (rowCount > 0);
        }
        public bool deleteKunde(Kunde kunde)
        {
            string sql = "DELETE FROM kunden WHERE kunde_nr = @val1;";
            SQLiteCommand command = new SQLiteCommand(sql, this.m_dbConnection);
            command.Parameters.Add(new SQLiteParameter("@val1", System.Data.DbType.Int32) { Value = kunde.KundeNr });

            int rowCount = command.ExecuteNonQuery();
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
            string sql = "SELECT * FROM kunden WHERE kunde_nr = @val1;";
            SQLiteCommand command = new SQLiteCommand(sql, this.m_dbConnection);
            command.Parameters.Add(new SQLiteParameter("@val1", System.Data.DbType.Int32) { Value = kundeNr });
        
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();
            kunde = this.__createKundeFromReader(reader);
          
            return kunde;
        }
        public Dictionary<int, Kunde> fetchAllKunden()
        {
            Dictionary<int, Kunde> kunden = new Dictionary<int, Kunde>();
            Kunde kunde;

            string sql = "SELECT * FROM kunden;";
            SQLiteCommand command = new SQLiteCommand(sql, this.m_dbConnection);

            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read()) {
                kunde = this.__createKundeFromReader(reader);
                kunden[kunde.KundeNr] = kunde;
            }

            return kunden;
        }
        public Dictionary<int, Kunde> fetchRangeOfKunden(int nr, int offset)
        {
            Dictionary<int, Kunde> kunden = new Dictionary<int, Kunde>();
            Kunde kunde;

            string sql = "SELECT * FROM kunden LIMIT @val1 OFFSET @val2;";
            SQLiteCommand command = new SQLiteCommand(sql, this.m_dbConnection);
            command.Parameters.Add(new SQLiteParameter("@val1", System.Data.DbType.Int32) { Value = nr });

            command.Parameters.Add(new SQLiteParameter("@val1", System.Data.DbType.Int32) { Value = offset });

            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                kunde = this.__createKundeFromReader(reader);
                kunden[kunde.KundeNr] = kunde;
            }

            return kunden;
        }
        public int getCountKunden()
        {
            int count = 0;
            string sql = "SELECT COUNT(*) AS count FROM kunden;";
            SQLiteCommand command = new SQLiteCommand(sql, this.m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                count = (int)reader["count"];
            }
            return count;
        }
        /********************************* Filliale *********************************/
        public bool addFiliale(Filiale filiale)
        {
            string sql = "INSERT INTO filialen (blz, strasse, plz, ort, land) VALUES (@val1,@val2,@val3,@val4,@val5);";
            SQLiteCommand command = new SQLiteCommand(sql, this.m_dbConnection);
            command.Parameters.Add(new SQLiteParameter("@val1", System.Data.DbType.String) { Value = filiale.Blz });
            command.Parameters.Add(new SQLiteParameter("@val2", System.Data.DbType.String) { Value = filiale.Strasse });
            command.Parameters.Add(new SQLiteParameter("@val3", System.Data.DbType.String) { Value = filiale.Plz });
            command.Parameters.Add(new SQLiteParameter("@val4", System.Data.DbType.String) { Value = filiale.Ort });
            command.Parameters.Add(new SQLiteParameter("@val5", System.Data.DbType.Int32) { Value = filiale.Land });

            int rowCount = command.ExecuteNonQuery();
            filiale.FilialeNr = this.getLastInsertedId();
            if (rowCount > 0)
            {
                Filiale.Instances[filiale.FilialeNr] = filiale;
            }
            return (rowCount > 0);
        }
        public bool editFiliale(Filiale filiale)
        {

            string sql = "UPDATE filialen SET blz = @val1, strasse = @val2, plz = @val3, ort = @val4, land = @val5 WHERE filiale_nr = @val6;";
            SQLiteCommand command = new SQLiteCommand(sql, this.m_dbConnection);
            command.Parameters.Add(new SQLiteParameter("@val1", System.Data.DbType.String) { Value = filiale.Blz });
            command.Parameters.Add(new SQLiteParameter("@val2", System.Data.DbType.String) { Value = filiale.Strasse });
            command.Parameters.Add(new SQLiteParameter("@val3", System.Data.DbType.String) { Value = filiale.Plz });
            command.Parameters.Add(new SQLiteParameter("@val4", System.Data.DbType.String) { Value = filiale.Ort });
            command.Parameters.Add(new SQLiteParameter("@val5", System.Data.DbType.Int32) { Value = filiale.Land });
            command.Parameters.Add(new SQLiteParameter("@val6", System.Data.DbType.Int32) { Value = filiale.FilialeNr });

            int rowCount = command.ExecuteNonQuery();
            if (rowCount > 0)
            {
                Filiale.Instances[filiale.FilialeNr] = filiale;
            }
            return (rowCount > 0);
        }
        public bool deleteFiliale(Filiale filiale)
        {
            string sql = "DELETE FROM filialen WHERE kunde_nr = @val1;";
            SQLiteCommand command = new SQLiteCommand(sql, this.m_dbConnection);
            command.Parameters.Add(new SQLiteParameter("@val1", System.Data.DbType.Int32) { Value = filiale.FilialeNr });

            int rowCount = command.ExecuteNonQuery();
            if (rowCount > 0)
            {
                Filiale.Instances.Remove(filiale.FilialeNr);
            }
            return (rowCount > 0);
        }
        private Filiale __createFilialeFromReader(SQLiteDataReader reader)
        {
            return new Filiale()
            {
                FilialeNr = (int)reader["filiale_nr"],
                Strasse = (string)reader["strasse"],
                Plz = (string)reader["plz"],
                Ort = (string)reader["ort"],
                Land = (Laender)reader["land"]
            };
        }
        public Filiale fetchFiliale(int filialeNr)
        {
            Filiale filiale;
            if (Filiale.Instances.TryGetValue(filialeNr, out filiale))
            {
                return filiale;
            }

            string sql = "SELECT * FROM filialen WHERE filiale_nr = @val1;";
            SQLiteCommand command = new SQLiteCommand(sql, this.m_dbConnection);
            command.Parameters.Add(new SQLiteParameter("@val1", System.Data.DbType.Int32) { Value = filialeNr });

            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();
            filiale = this.__createFilialeFromReader(reader);
            
            return filiale;
        }
        public Dictionary<int, Filiale> fetchAllFilialen()
        {
            Filiale filiale;

            string sql = "SELECT * FROM filialen;";
            SQLiteCommand command = new SQLiteCommand(sql, this.m_dbConnection);

            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                filiale = this.__createFilialeFromReader(reader);
                Filiale.Instances[filiale.FilialeNr] = filiale;
            }
       
            return Filiale.Instances;
        }
        public Dictionary<int, Filiale> fetchRangeOfFilialen(int nr, int offset)
        {
            Dictionary<int, Filiale> filialen = new Dictionary<int, Filiale>();
            Filiale filiale;

            string sql = "SELECT * FROM filialen LIMIT @val1 OFFSET @val2;";
            SQLiteCommand command = new SQLiteCommand(sql, this.m_dbConnection);
            command.Parameters.Add(new SQLiteParameter("@val1", System.Data.DbType.Int32) { Value = nr });
            command.Parameters.Add(new SQLiteParameter("@val1", System.Data.DbType.Int32) { Value = offset });

            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                filiale = this.__createFilialeFromReader(reader);
                filialen[filiale.FilialeNr] = filiale;
                Filiale.Instances[filiale.FilialeNr] = filiale;
            }

            return filialen;
        }
        public int getCountFilialen()
        {
            int count = 0;
            string sql = "SELECT COUNT(*) AS count FROM filialen;";
            SQLiteCommand command = new SQLiteCommand(sql, this.m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                count = (int)reader["count"];
            }
            return count;
        }

        /********************************* Konto *********************************/
        public bool addKonto(Konto konto)
        {
            string sql = "INSERT INTO konten (konto_nr, kunde_nr, filiale_nr) VALUES (@val1,@val2,@val3);";
            SQLiteCommand command = new SQLiteCommand(sql, this.m_dbConnection);
            command.Parameters.Add(new SQLiteParameter("@val1", System.Data.DbType.String) { Value = konto.KontoNr });
            command.Parameters.Add(new SQLiteParameter("@val2", System.Data.DbType.Int32) { Value = konto.Kunde.KundeNr });
            command.Parameters.Add(new SQLiteParameter("@val3", System.Data.DbType.Int32) { Value = konto.Filiale.FilialeNr });

            int rowCount = command.ExecuteNonQuery();
            return (rowCount > 0);
        }
        public bool editKonto(string kontoNr, Konto konto)
        {
            string sql = "UPDATE konten SET konto_nr = @val1, filiale_nr = @val2 WHERE konto_nr = @val3;";
            SQLiteCommand command = new SQLiteCommand(sql, this.m_dbConnection);
            command.Parameters.Add(new SQLiteParameter("@val1", System.Data.DbType.String) { Value = konto.KontoNr });
            command.Parameters.Add(new SQLiteParameter("@val2", System.Data.DbType.Int32) { Value = konto.Filiale.FilialeNr });
            command.Parameters.Add(new SQLiteParameter("@val3", System.Data.DbType.String) { Value = kontoNr });

            int rowCount = command.ExecuteNonQuery();
            return (rowCount > 0);
        }
        public bool deleteKonto(Konto konto)
        {
            string sql = "DELETE FROM kontonen WHERE konto_nr = @val1;";
            SQLiteCommand command = new SQLiteCommand(sql, this.m_dbConnection);
            command.Parameters.Add(new SQLiteParameter("@val1", System.Data.DbType.String) { Value = konto.KontoNr });

            int rowCount = command.ExecuteNonQuery();
            return (rowCount > 0);
        }
        public bool checkKontoNrExists(string kontoNr)
        {
            int count = 0;
            string sql = "SELECT COUNT(*) AS count FROM konten WHERE konto_nr = @val1;";
            SQLiteCommand command = new SQLiteCommand(sql, this.m_dbConnection);
            command.Parameters.Add(new SQLiteParameter("@val1", System.Data.DbType.String) { Value = kontoNr });

            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                count = (int)reader["count"];
            }
            return (count == 1);
        }
        private Konto __createKontoFromReader(SQLiteDataReader reader)
        {
            Filiale filiale = this.fetchFiliale((int)reader["filiale_nr"]);
            return new Konto()
            {
                KontoNr     = (string)      reader["konto_nr"],
                Filiale     =               filiale
            };
        }
        public Dictionary<string, Konto> fetchAllKonten(Kunde kunde)
        {
            Konto konto;

            string sql = "SELECT * FROM konto WHERE kunde_nr = @val1;";
            SQLiteCommand command = new SQLiteCommand(sql, this.m_dbConnection);
            command.Parameters.Add(new SQLiteParameter("@val1", System.Data.DbType.Int32) { Value = kunde.KundeNr });

            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                konto = this.__createKontoFromReader(reader);
                konto.Kunde = kunde;
                kunde.Konten[konto.KontoNr] = konto;
            }

            return kunde.Konten;
        }
        public int getCountKonten(Kunde kunde)
        {
            int count = 0;
            string sql = "SELECT COUNT(*) AS count FROM konten;";
            SQLiteCommand command = new SQLiteCommand(sql, this.m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                count = (int)reader["count"];
            }
            return count;
        }
    }
}
