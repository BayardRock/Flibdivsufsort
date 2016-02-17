module internal LibdivsufsortWrapper
#nowarn "1182"

open System.Runtime.InteropServices
open Microsoft.FSharp.NativeInterop

//
///*
// * divsufsort64.h for libdivsufsort64
// * Copyright (c) 2003-2008 Yuta Mori All Rights Reserved.
// *
// * Permission is hereby granted, free of charge, to any person
// * obtaining a copy of this software and associated documentation
// * files (the "Software"), to deal in the Software without
// * restriction, including without limitation the rights to use,
// * copy, modify, merge, publish, distribute, sublicense, and/or sell
// * copies of the Software, and to permit persons to whom the
// * Software is furnished to do so, subject to the following
// * conditions:
// *
// * The above copyright notice and this permission notice shall be
// * included in all copies or substantial portions of the Software.
// *
// * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// * WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// * OTHER DEALINGS IN THE SOFTWARE.
// */
//
///*- Prototypes -*/
type saint_t = int32
type sauchar_t = byte
type saidx64_t = int64


///**
// * Constructs the suffix array of a given string.
// * @param T[0..n-1] The input string.
// * @param SA[0..n-1] The output array of suffixes.
// * @param n The length of the given string.
// * @return 0 if no error occurred, -1 or -2 otherwise.
// */
//DIVSUFSORT_API
//saint_t
//divsufsort64(const sauchar_t *T, saidx64_t *SA, saidx64_t n);
[<DllImport("divsufsort64.dll")>]
extern saint_t divsufsort64(sauchar_t* T, saidx64_t* SA, saidx64_t n);


///**
// * Constructs the burrows-wheeler transformed string of a given string.
// * @param T[0..n-1] The input string.
// * @param U[0..n-1] The output string. (can be T)
// * @param A[0..n-1] The temporary array. (can be NULL)
// * @param n The length of the given string.
// * @return The primary index if no error occurred, -1 or -2 otherwise.
// */
//DIVSUFSORT_API
//saidx64_t
//divbwt64(const sauchar_t *T, sauchar_t *U, saidx64_t *A, saidx64_t n);
[<DllImport("divsufsort64.dll")>]
extern saidx64_t divbwt64(sauchar_t *T, sauchar_t *U, saidx64_t *A, saidx64_t n);


///**
// * Returns the version of the divsufsort library.
// * @return The version number string.
// */
//DIVSUFSORT_API
//const char *
//divsufsort64_version(void);
[<DllImport("divsufsort64.dll")>]
extern char* divsufsort64_version();


///**
// * Constructs the burrows-wheeler transformed string of a given string and suffix array.
// * @param T[0..n-1] The input string.
// * @param U[0..n-1] The output string. (can be T)
// * @param SA[0..n-1] The suffix array. (can be NULL)
// * @param n The length of the given string.
// * @param idx The output primary index.
// * @return 0 if no error occurred, -1 or -2 otherwise.
// */
//DIVSUFSORT_API
//saint_t
//bw_transform64(const sauchar_t *T, sauchar_t *U,
//             saidx64_t *SA /* can NULL */,
//             saidx64_t n, saidx64_t *idx);
[<DllImport("divsufsort64.dll")>]
extern saint_t bw_transform64(sauchar_t *T, sauchar_t *U, saidx64_t *SA, saidx64_t n, saidx64_t *idx);


///**
// * Inverse BW-transforms a given BWTed string.
// * @param T[0..n-1] The input string.
// * @param U[0..n-1] The output string. (can be T)
// * @param A[0..n-1] The temporary array. (can be NULL)
// * @param n The length of the given string.
// * @param idx The primary index.
// * @return 0 if no error occurred, -1 or -2 otherwise.
// */
//DIVSUFSORT_API
//saint_t
//inverse_bw_transform64(const sauchar_t *T, sauchar_t *U,
//                     saidx64_t *A /* can NULL */,
//                     saidx64_t n, saidx64_t idx);
[<DllImport("divsufsort64.dll")>]
extern saint_t inverse_bw_transform64(sauchar_t *T, sauchar_t *U, saidx64_t *A, saidx64_t n, saidx64_t idx);


///**
// * Checks the correctness of a given suffix array.
// * @param T[0..n-1] The input string.
// * @param SA[0..n-1] The input suffix array.
// * @param n The length of the given string.
// * @param verbose The verbose mode.
// * @return 0 if no error occurred.
// */
//DIVSUFSORT_API
//saint_t
//sufcheck64(const sauchar_t *T, const saidx64_t *SA, saidx64_t n, saint_t verbose);
[<DllImport("divsufsort64.dll")>]
extern saint_t sufcheck64(sauchar_t *T, saidx64_t *SA, saidx64_t n, saint_t verbose);



///**
// * Search for the pattern P in the string T.
// * @param T[0..Tsize-1] The input string.
// * @param Tsize The length of the given string.
// * @param P[0..Psize-1] The input pattern string.
// * @param Psize The length of the given pattern string.
// * @param SA[0..SAsize-1] The input suffix array.
// * @param SAsize The length of the given suffix array.
// * @param idx The output index.
// * @return The count of matches if no error occurred, -1 otherwise.
// */
//DIVSUFSORT_API
//saidx64_t
//sa_search64(const sauchar_t *T, saidx64_t Tsize,
//          const sauchar_t *P, saidx64_t Psize,
//          const saidx64_t *SA, saidx64_t SAsize,
//          saidx64_t *left);
[<DllImport("divsufsort64.dll")>]
extern saidx64_t sa_search64(sauchar_t *T, saidx64_t Tsize, sauchar_t *P, saidx64_t Psize, saidx64_t *SA, saidx64_t SAsize, saidx64_t *left);


///**
// * Search for the character c in the string T.
// * @param T[0..Tsize-1] The input string.
// * @param Tsize The length of the given string.
// * @param SA[0..SAsize-1] The input suffix array.
// * @param SAsize The length of the given suffix array.
// * @param c The input character.
// * @param idx The output index.
// * @return The count of matches if no error occurred, -1 otherwise.
// */
//DIVSUFSORT_API
//saidx64_t
//sa_simplesearch64(const sauchar_t *T, saidx64_t Tsize,
//                const saidx64_t *SA, saidx64_t SAsize,
//                saint_t c, saidx64_t *left);
[<DllImport("divsufsort64.dll")>]
extern saidx64_t sa_simplesearch64(sauchar_t *T, saidx64_t Tsize, saidx64_t *SA, saidx64_t SAsize, saint_t c, saidx64_t *left);
