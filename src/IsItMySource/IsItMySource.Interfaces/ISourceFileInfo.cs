namespace IKriv.IsItMySource.Interfaces
{
    public enum ChecksumType
    {
        None,
        Md5,
        Sha1,
        Unknown
    }


    public interface ISourceFileInfo
    {
        string Path { get;  }
        ChecksumType ChecksumType { get; }
        string ChecksumTypeStr { get;  }
        byte[] Checksum { get; }
    }
}
