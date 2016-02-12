namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("Flibdivsufsort")>]
[<assembly: AssemblyProductAttribute("Flibdivsufsort")>]
[<assembly: AssemblyDescriptionAttribute("An F# Wrapper around the LibDibSufSort suffix array library")>]
[<assembly: AssemblyVersionAttribute("1.0")>]
[<assembly: AssemblyFileVersionAttribute("1.0")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "1.0"
