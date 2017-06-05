namespace IKriv.IsItMySource.Interfaces
{
    public enum ChecksumType
    {
        NoChecksum,
        Md5,
        Sha1,
        Unknown
    }
    
    public class SourceFileInfo
    {
        public string Path { get; set; }
        public ChecksumType ChecksumType { get; set; }
        public string ChecksumTypeStr { get; set; }
        public byte[] Checksum { get; set; }
    }
}
