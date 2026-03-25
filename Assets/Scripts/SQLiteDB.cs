using UnityEngine;
using SQLite;
using System.IO;
public class ElementDB
{
    public int id { get; set; }
    public string symbol { get; set; }
    public string name { get; set; }
}

public class SQLiteDB : MonoBehaviour
{
    private string dbPath;

    void Start()
    {
        dbPath = Path.Combine(Application.persistentDataPath, "chemistry.db");
        Debug.Log("DB Path: " + dbPath);

        var db = new SQLiteConnection(dbPath);

        db.Execute(@"
            CREATE TABLE IF NOT EXISTS elements (
                id INTEGER PRIMARY KEY,
                symbol TEXT NOT NULL,
                name TEXT NOT NULL
            );
        ");

        db.Execute("INSERT OR IGNORE INTO elements (id, symbol, name) VALUES (1, 'H', 'Hydrogen');");
        db.Execute("INSERT OR IGNORE INTO elements (id, symbol, name) VALUES (2, 'C', 'Carbon');");
        db.Execute("INSERT OR IGNORE INTO elements (id, symbol, name) VALUES (3, 'O', 'Oxygen');");
        db.Execute("INSERT OR IGNORE INTO elements (id, symbol, name) VALUES (4, 'N', 'Nitrogen');");
        db.Execute("INSERT OR IGNORE INTO elements (id, symbol, name) VALUES (5, 'S', 'Sulfur');");
        db.Execute("INSERT OR IGNORE INTO elements (id, symbol, name) VALUES (6, 'P', 'Phosphorus');");
        db.Execute("INSERT OR IGNORE INTO elements (id, symbol, name) VALUES (7, 'Na', 'Sodium');");
        db.Execute("INSERT OR IGNORE INTO elements (id, symbol, name) VALUES (8, 'Cl', 'Chlorine');");

        var elements = db.Table<ElementDB>().ToList();
        foreach (var e in elements)
        {
            Debug.Log(e.id + ": " + e.symbol + " - " + e.name);
        }

        db.Close();
    }
}