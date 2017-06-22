# IsItMySource

### What it does
IsItMySource is a program that can
   * Print list of source files stored in the debug information of EXE or PDB.
   * Check whether a set of source files matches a given EXE or PDB.

### Why you would need it
I wrote this tool so I could verify whether given executable was compiled from given source code. Let's say we need to modify a library or a tool X that is distributed in binary form, but was allegedly compiled from source revision Y. If it was really compiled from Y, then we can safely make necessary modifications and recompile, but if it was not compiled from Y, we may lose functionality, or introduce bugs. 

In the ideal world each executable should contain an unambiguous reference to the location and revision of the source code, but in practice it is not always the case.

### Managed vs Native Debug Information

Unfortunately, format of debug information is not well documented and tends to change over time. _IsItMySource_ is known to work with programs compiled by Visual Studio 2013, 2015, and 2017, but it may fail with older programs, e.g. those compiled with Visual Studio 6.

There are at least two different types of debug information: managed (.NET) and unmanaged (native). .NET executables contain both, but only the managed portion has the checksums. Native executables contain only native debug information.

_IsItMySource_ does not parse executable files by haand. Managed debug information is accessed via [Diagnostics Symbols Store Interface](https://docs.microsoft.com/en-us/dotnet/framework/unmanaged-api/diagnostics/) that is shipped with .NET Framework. Unmanaged debug information is read via [Debug Interface Access SDK](https://msdn.microsoft.com/en-us/library/x93ctkx8.aspx?f=255&MSPPError=-2147217396) that ships with Visual Studio. You must have Visual Studio and DIA SDK on the machine to read native debug information.

_IsItMySource_ works with  managed debug information by default. Use `--native` switch to read native debug information.

### Usage 

    IsItMySource [options] exe_or_pdb_file [folder]

If folder is specified, checks whether source files in the folder match those specified in the PDB or EXE file. If folder is not specified, shows list of source files of EXE or PDB file.

Loading PDB files directly is only supported for unmanaged debug info. Managed debug info reader starts with an EXE file and searches for PDB.
   
#### OPTIONS
`--allfiles`  
Include system files that are ignored by default.

`--ignore` pattern1[;pattern2...]  
                Ignore files that match specified wildcard patterns. Allowed
                wildcard characters are:   
   -  `**\`  empty string or any character sequence followed by backslash  
   -  `**`   any character sequence
   -  `*`    any character sequence not containing backslash
   -  `?`    any single character except backslash

`--managed`  
Read managed debug info via DIASymReader. This is the default. Supports only EXE files. PDB file must be next to the EXE file or in the path specified by `--search`.

`--native`  
Read native debug info via DIA SDK. This won't return source file checksums for managed executables. Supports EXE and PDB.

`--nosummary`  
Do not show summary statistics when verifying sources.

`--pdbdir` dir1[;dir2...]  
Use this semicolon-separated path to look for PDB file if EXE is specified.

`--root` _dir_  
Root path for source files inside the PDB/EXE file. File **{root}/relative.path** referenced by EXE/PDB will be matched to **{folder}/relative.path** on the local disk. Source files outside of the root will be ignored. E.g. if **proj.exe** refers to **c:\mysources\proj\foo\bar.cs**, and 
`IsItMySource --root d:\projects\proj proj.exe` is run, then the program will expect local file **d:\projects\proj\foo\bar.cs** to match 
                **c:\mysources\proj\foo\bar.cs** referenced **proj.exe**.

`--use` name  
Load debug info engine from assembly **{name}**. The assembly must have `[assembly:DebugInfoEngine(typeof(T))]` attribute where T implements `IDebugInfoReader` interface.

`--unmanaged`  
Same as --native

### Comments and Suggestions
Feel free to send your comments via the [Feedback page](http://www.ikriv.com/feedback.php?subj=About%20IsItMySource).