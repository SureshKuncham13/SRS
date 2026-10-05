using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SRS.Application.DTOs.BiometricUserLog;
using SRS.Application.Interfaces.Repository;
using SRS.Infrastructure.Data;
using System.Data;

namespace SRS.Infrastructure.Repositories
{
    public class BiometricUserLogRepository : IBiometricUserLogRepository
    {
        private readonly SrsDbContext _srsDbContext;
        private readonly DevDbContext _devDbContext;

        public BiometricUserLogRepository(SrsDbContext srsDbContext, DevDbContext devDbContext)
        {
            _srsDbContext = srsDbContext;
            _devDbContext = devDbContext;
        }

        public async Task InsertIntoSrsAsync(BiometricUserLogCreateDto model, CancellationToken cancellationToken = default)
        {
            var connection = _srsDbContext.Database.GetDbConnection();

            if (connection.State != ConnectionState.Open)
                await connection.OpenAsync(cancellationToken);

            await using var command = connection.CreateCommand();

            command.CommandText = "dbo.SP_Insert_Biometric_User_Log";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(
                new SqlParameter("@MachineNumber", SqlDbType.Int)
                {
                    Value = model.MachineNumber
                });

            command.Parameters.Add(
                new SqlParameter("@IndRegID", SqlDbType.BigInt)
                {
                    Value = model.IndRegID
                });

            command.Parameters.Add(
                new SqlParameter("@DateTimeRecord", SqlDbType.DateTime)
                {
                    Value = model.DateTimeRecord
                });

            command.Parameters.Add(
                new SqlParameter("@DateOnlyRecord", SqlDbType.Date)
                {
                    Value = model.DateOnlyRecord.Date
                });

            command.Parameters.Add(
                new SqlParameter("@TimeOnlyRecord", SqlDbType.DateTime)
                {
                    Value = model.TimeOnlyRecord
                });

            command.Parameters.Add(
                new SqlParameter("@dwInOutMode", SqlDbType.Int)
                {
                    Value = model.dwInOutMode
                });

            command.Parameters.Add(
                new SqlParameter("@bio_hide", SqlDbType.Bit)
                {
                    Value = model.bio_hide
                });

            await command.ExecuteNonQueryAsync(cancellationToken);
        }

        public async Task InsertIntoDevAsync(BiometricUserLogCreateDto model, CancellationToken cancellationToken = default)
        {
            var connection = _devDbContext.Database.GetDbConnection();

            if (connection.State != ConnectionState.Open)
                await connection.OpenAsync(cancellationToken);

            await using var command = connection.CreateCommand();

            command.CommandText = "dbo.SP_Insert_Biometric_User_Log";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(
                new SqlParameter("@MachineNumber", SqlDbType.Int)
                {
                    Value = model.MachineNumber
                });

            command.Parameters.Add(
                new SqlParameter("@IndRegID", SqlDbType.BigInt)
                {
                    Value = model.IndRegID
                });

            command.Parameters.Add(
                new SqlParameter("@DateTimeRecord", SqlDbType.DateTime)
                {
                    Value = model.DateTimeRecord
                });

            command.Parameters.Add(
                new SqlParameter("@DateOnlyRecord", SqlDbType.Date)
                {
                    Value = model.DateOnlyRecord.Date
                });

            command.Parameters.Add(
                new SqlParameter("@TimeOnlyRecord", SqlDbType.DateTime)
                {
                    Value = model.TimeOnlyRecord
                });

            command.Parameters.Add(
                new SqlParameter("@dwInOutMode", SqlDbType.Int)
                {
                    Value = model.dwInOutMode
                });

            command.Parameters.Add(new SqlParameter("@bio_hide", SqlDbType.Bit)
                {
                    Value = model.bio_hide
                });

            await command.ExecuteNonQueryAsync(cancellationToken);
        }
    }
}