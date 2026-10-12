using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SRS.Application.Interfaces.Repository;
using SRS.Application.Models;

namespace SRS.Infrastructure.Repositories;

public sealed class BiometricUserLogSyncRepository : IBiometricUserLogSyncRepository
{
    private readonly string _connectionString;

    public BiometricUserLogSyncRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("BiometricSource")?? throw new InvalidOperationException("Connection string 'BiometricSource' is missing.");

        if (string.IsNullOrWhiteSpace(_connectionString))
        {
            throw new InvalidOperationException("Connection string 'BiometricSource' is empty.");
        }
    }

    public async Task<BiometricSyncResult?> ExecuteSyncProcedureAsync(CancellationToken cancellationToken = default)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        await using var command = new SqlCommand("dbo.Sync_Biometric_User_Logs3", connection)
        {
            CommandType = CommandType.StoredProcedure,
            CommandTimeout = 120
        };

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);

        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        return new BiometricSyncResult(
            InsertedRecords: Convert.ToInt32(reader["InsertedRecords"]),
            UpdatedRecords: Convert.ToInt32(reader["UpdatedRecords"]),
            SourceRecords: Convert.ToInt64(reader["SourceRecords"]),
            DestinationRecords: Convert.ToInt64(reader["DestinationRecords"]));
    }
}