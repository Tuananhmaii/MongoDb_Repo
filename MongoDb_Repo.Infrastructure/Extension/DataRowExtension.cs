using System.Data;

namespace MongoDb_Repo.Infrastructure.Extension
{
    public static class DataRowExtension
    {
        public static bool HasValueOnColumn(this DataRow row, string columnName) 
            => !string.IsNullOrWhiteSpace(row[columnName].ToString());

        public static bool HasValueOnColumn(this DataRow row, int columnIndex)
            => !string.IsNullOrWhiteSpace(row[columnIndex].ToString());
    }
}
