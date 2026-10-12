namespace SRS.Application.Models;

public sealed record BiometricSyncResult(int InsertedRecords, int UpdatedRecords, long SourceRecords, long DestinationRecords);