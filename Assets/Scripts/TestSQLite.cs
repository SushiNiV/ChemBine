using UnityEngine;
using SQLite;
using System.IO;

public class SQLiteTest : MonoBehaviour
{
    void Start()
    {
        string dbPath = Path.Combine(
            Application.persistentDataPath,
            "test.db"
        );

        Debug.Log("DB Path: " + dbPath);

        var db = new SQLiteConnection(dbPath);

        db.Execute("CREATE TABLE IF NOT EXISTS Test (id INTEGER PRIMARY KEY, name TEXT)");

        db.Execute("INSERT INTO Test (name) VALUES ('SQLite Works!')");

        var result = db.ExecuteScalar<string>("SELECT name FROM Test LIMIT 1");

        Debug.Log("SQLite Result: " + result);
    }
}
