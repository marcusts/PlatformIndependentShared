// #define USE_DELAYS

// *********************************************************************************
// Copyright @2022 Marcus Technical Services, Inc.
// <copyright
// file=LogUtils_PI.cs
// company="Marcus Technical Services, Inc.">
// </copyright>
// 
// MIT License
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// *********************************************************************************

namespace Com.MarcusTS.PlatformIndependentShared.Common.Utils
{
   using System;
   using System.Diagnostics;
   using System.Runtime.CompilerServices;
   using System.Threading.Tasks;
   using Com.MarcusTS.SharedUtils.Utils;

   public static class LogUtils_PI
   {
      private static readonly Random _rand          = new Random(Guid.NewGuid().GetHashCode());
      public const            int    MIN_TEST_DELAY = 250;
      public const            int    MAX_TEST_DELAY = 1250;
      public static readonly int NextDelay = _rand.Next(MIN_TEST_DELAY, MAX_TEST_DELAY);

      public static Task Log(string typeName, [CallerMemberName] string methodName = "", string suffix = "")
      {

         Debug.WriteLine(DateTime.Now.ToString("0:hh:mm:ss.fff") + ": Inside " + typeName + ": " + methodName + (suffix.IsEmpty() ? "" : ": " + suffix));

         return Task.CompletedTask;
      }
   }
}
