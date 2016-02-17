module Flibdivsufsort
#nowarn "9"

open System.Runtime.InteropServices
open Microsoft.FSharp.NativeInterop

open LibdivsufsortWrapper

type SuffixArray<'t>(input: 't[]) =
    // TODO: Use long arrays instead (there may still be a 32-bit max limit on size)
    let SALen = sizeof<saidx64_t> * input.Length * 5
    let SA = Array.zeroCreate<saidx64_t>(SALen)

    let result = 
        let THandle = GCHandle.Alloc(input, GCHandleType.Pinned)
        let SAHandle = GCHandle.Alloc(SA, GCHandleType.Pinned)
        let res = divsufsort64(NativePtr.ofNativeInt <| THandle.AddrOfPinnedObject(), NativePtr.ofNativeInt <| SAHandle.AddrOfPinnedObject(), input.LongLength)
        do THandle.Free()
        do SAHandle.Free()
        res

    do if result <> 0 then
         failwithf "Could not build suffix array, error code: %i" result

    member t.IsCorrect () = 
        let THandle = GCHandle.Alloc(input, GCHandleType.Pinned)
        let SAHandle = GCHandle.Alloc(SA, GCHandleType.Pinned)
        let res = sufcheck64(NativePtr.ofNativeInt <| THandle.AddrOfPinnedObject(), NativePtr.ofNativeInt <| SAHandle.AddrOfPinnedObject(), input.LongLength, 1)
        do THandle.Free()
        do SAHandle.Free()
        res = 0

    member t.Search (query: 't[]) : int64 [] = 
        let P = query
        let numResults, idx = 
            let PHandle = GCHandle.Alloc(P, GCHandleType.Pinned)
            let THandle = GCHandle.Alloc(input, GCHandleType.Pinned)
            let SAHandle = GCHandle.Alloc(SA, GCHandleType.Pinned)
            let idx = NativePtr.stackalloc<saidx64_t>(1)
            let res = sa_search64(NativePtr.ofNativeInt <| THandle.AddrOfPinnedObject(), input.LongLength, 
                                  NativePtr.ofNativeInt <| PHandle.AddrOfPinnedObject(), P.LongLength, 
                                  NativePtr.ofNativeInt <| SAHandle.AddrOfPinnedObject(), input.LongLength, idx)
            do PHandle.Free() 
            do THandle.Free()
            do SAHandle.Free()
            res, NativePtr.read idx
        [|
            for i = 0L to numResults - 1L do
                let arrayMatchOffset = SA.[int (int64 idx + i)]
                yield arrayMatchOffset
        |]
        