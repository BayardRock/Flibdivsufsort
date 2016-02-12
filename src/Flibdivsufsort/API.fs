module API

open System.Runtime.InteropServices
open Microsoft.FSharp.NativeInterop

open Flibdivsufsort.Wrapper

type SuffixArray<'t>(input: 't[]) =
    // TODO: Use long arrays instead
    let TLen = sizeof<'t> * input.Length
    let SALen = sizeof<saidx64_t> * input.Length * 5
    let SA = Array.zeroCreate<saidx64_t>(SALen)

    let result = 
        let THandle = GCHandle.Alloc(input, GCHandleType.Pinned)
        let SAHandle = GCHandle.Alloc(SA, GCHandleType.Pinned)
        let res = divsufsort64(NativePtr.ofNativeInt <| THandle.AddrOfPinnedObject(), NativePtr.ofNativeInt <| SAHandle.AddrOfPinnedObject(), int64 TLen)
        do THandle.Free()
        do SAHandle.Free()
        res

    do if result <> 0 then
         failwithf "Could not build suffix array, error code: %i" result

    member t.Search (query: 't[]) = 
        let P = query
        let PLen = P.Length * sizeof<'t>
        let numResults, idx = 
            let PHandle = GCHandle.Alloc(P, GCHandleType.Pinned)
            let THandle = GCHandle.Alloc(input, GCHandleType.Pinned)
            let SAHandle = GCHandle.Alloc(SA, GCHandleType.Pinned)
            let idx = NativePtr.stackalloc<saidx64_t>(1)
            let res = sa_search64(NativePtr.ofNativeInt <| THandle.AddrOfPinnedObject(), int64 TLen, NativePtr.ofNativeInt <| PHandle.AddrOfPinnedObject(), int64 PLen, NativePtr.ofNativeInt <| SAHandle.AddrOfPinnedObject(), int64 SALen, idx)
            do PHandle.Free() 
            do THandle.Free()
            do SAHandle.Free()
            res, NativePtr.read idx
        [|
            for i = 0L to numResults - 1L do
                let arrayMatchOffset = SA.[int (int64 idx + i)]
                yield arrayMatchOffset
        |]
        