using Mono.Csv;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

public class ParseData : MonoBehaviour
{
    private string pathDataset = Path.Combine(Application.streamingAssetsPath, "dataset.csv");
    public static List<DataSetRow> rows;

    private void Awake()
    {
        rows = new List<DataSetRow>();
        List<string> row = new List<string>();
        using (var reader = new CsvFileReader(pathDataset))
        {
            while (reader.ReadRow(row))
            {
                DateTime date = DateTime.Parse(row[0], null, System.Globalization.DateTimeStyles.RoundtripKind);
                int tag = Int32.Parse(row[1]);

                float x_pos = float.Parse(row[2], CultureInfo.InvariantCulture);
                float y_pos = float.Parse(row[3], CultureInfo.InvariantCulture);

                float heading = float.Parse(row[4], CultureInfo.InvariantCulture);
                float direction = float.Parse(row[5], CultureInfo.InvariantCulture);

                float energy = float.Parse(row[6], CultureInfo.InvariantCulture);
                float speed = float.Parse(row[7], CultureInfo.InvariantCulture);
                float total_distance = float.Parse(row[8], CultureInfo.InvariantCulture);

                DataSetRow setRow = new DataSetRow(date, tag, x_pos, y_pos, heading, direction, energy, speed, total_distance);
                rows.Add(setRow);
            }
        }
    }
}
