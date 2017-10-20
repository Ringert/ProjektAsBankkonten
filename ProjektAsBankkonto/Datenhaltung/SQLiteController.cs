using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

using ProjektAsBankkonto.Interfaces.Datenhaltung;
using ProjektAsBankkonto.Datenhaltung.Model;


namespace ProjektAsBankkonto.Datenhaltung
{
    class SQLiteController : IDatenhaltung
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
            if (this.checkTableExists("kunde"))
                return;
        }
        private bool checkTableExists(string tableName)
        {
            string sql = "SELECT name FROM sqlite_master WHERE type='table' AND name='table_name';";
            SQLiteCommand command = new SQLiteCommand(sql, this.m_dbConnection);
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
        public bool insertKunde(ref Kunde kunde)
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
            kunde.KundenNr = this.getLastInsertedId();
            Kunde.Instances.Add(kunde);
            return (rowCount > 0);
        }
        public bool updateKunde(Kunde kunde)
        {
            return true;
        }
        public bool deleteKunde()
        {
            return true;
        }

        public Kunde fetchKunde(int kundeNr)
        {
            return new Kunde();
        }
        public Kunde[] fetchAllKunden()
        {
            return Kunde.Instances;
        }
        public int getAnzahlKunden()
        {
            return 0;
        }
        public int getGroessteKundenNr()
        {
            return 0;
        }

        /********************************* Filliale *********************************/
        public bool insertFiliale(Filiale filiale)
        {
            return true;
        }
        public bool updateFiliale(Filiale filiale)
        {
            return true;
        }
        public bool deleteFiliale(Filiale filiale)
        {
            return true;
        }
        public Filiale fetchFiliale(int filialeNr)
        {
            return new Filiale();
        }
        public Filiale[] fetchAllFilialen()
        {
            return Filiale.Instances;
        }
        public int getAnzahlFilialen()
        {
            return 0;
        }
        public int getGroessteFilialeNr()
        {
            return 0;
        }

        /********************************* Konto *********************************/
        public bool insertKonto(Konto konto)
        {
            return true;
        }
        public bool updateKonto(Konto konto)
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
            return new Konto();
        }
        public Konto[] fetchAllKonten()
        {
            return Konto.Instances;
        }
        public int getAnzahlKonten()
        {
            string sql = "SELECT COUNT(*) anz FROM konto";
            return 0;
        }
        public int getGroessteKontoNr()
        {
            return 0;
        }
    }
}
