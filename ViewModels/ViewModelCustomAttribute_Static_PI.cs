// *********************************************************************************
// Copyright @2021 Marcus Technical Services, Inc.
// <copyright
// file=ViewModelCustomAttribute_Static_PI.cs
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

namespace Com.MarcusTS.PlatformIndependentShared.ViewModels
{
   using Com.MarcusTS.SharedUtils.Utils;

   public static class ViewModelCustomAttribute_Static_PI
   {
      public const string POUND_SYMBOL = "#";

      public const int    FALSE_BOOL   = 0;
      public const int    TRUE_BOOL    = 1;
      public const int    UNSET_BOOL   = -1;
      public const double UNSET_DOUBLE = -100.0d;
      public const int    UNSET_INT    = -100;

      public const string UNSET_STRING =
         "32r08932fuhvoiwgfpo'iwgpo34[098tr32 9io3g4gop43megfwv-09k=j31T2Q-03FmpkcEQWPLmFvew poiBVWERNI'JRewbpogfvEWPOKFjewp[o";

      public const string COLOR_HEX_SUFFIX        = "ColorHex";
      public const string TEXT_ALIGNMENT_SUFFIX   = "_TextAlignment";
      public const string LINE_BREAK_MODE_SUFFIX  = "_LineBreakMode";
      public const string BINDING_MODE_SUFFIX     = "_BindingMode";
      public const string FONT_ATTRIBUTES_SUFFIX  = "_FontAttributes";
      public const string OPERATING_SYSTEM_SUFFIX = "_OS";

      public static double GetUnsetValueOrDefault( this double d )
      {
         return d.IsUnset() ? default : d;
      }

      public static bool IsFalse( this int i )
      {
         return i == FALSE_BOOL;
      }

      public static int Reverse( this int i )
      {
         if ( i.IsUnset() )
         {
            return i;
         }

         // ELSE
         return i.IsTrue() ? FALSE_BOOL : TRUE_BOOL;
      }

      public static bool IsTrue( this int i )
      {
         return i == TRUE_BOOL;
      }

      public static bool IsUnset( this int i )
      {
         return i == UNSET_INT;
      }

      public static bool IsUnset( this double d )
      {
         return d.IsSameAs( UNSET_DOUBLE );
      }

      public static bool IsUnset( this string s )
      {
         return s.IsSameAs( UNSET_STRING );
      }
   }
}