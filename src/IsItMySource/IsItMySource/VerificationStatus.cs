namespace IKriv.IsItMySource
{
    enum VerificationStatus
    {
        SameChecksum,
        DifferentChecksum,
        Missing,
        NoChecksum,
        UnknownChecksumType,
        CouldNotCalculateChecksum,
        Skipped
    }
}
