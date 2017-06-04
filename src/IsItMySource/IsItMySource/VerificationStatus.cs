namespace IKriv.IsItMySource
{
    enum VerificationStatus
    {
        Skipped,
        SameChecksum,
        DifferentChecksum,
        Missing,
        NoChecksum,
        UnknownChecksumType,
        CouldNotCalculateChecksum
    }
}
