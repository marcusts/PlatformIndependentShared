// *********************************************************************************
// Copyright @2022 Marcus Technical Services, Inc.
// <copyright
// file=ValidationUtils_PI.cs
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
   using System.Collections.Concurrent;
   using System.Collections.Generic;
   using System.Diagnostics;
   using System.Reflection;
   using System.Text;
   using System.Text.RegularExpressions;
   using System.Threading.Tasks;
   using Com.MarcusTS.ResponsiveTasks;
   using Com.MarcusTS.SharedUtils.Utils;

   public static class ValidationUtils_PI
   {
      public const  string MATCHES_PASSWORD = @"Matches the password";
      public const  string IS_LEGAL_EMAIL   = @"Is a legal e-mail address";
      private const char   X_CHAR           = 'X';
      public const  string IMPOSSIBLE_TEXT  = "WllNeverOccur!^%$^%(&*)_()_)*((*^&";

      public const int DEFAULT_MAX_CHARACTER_COUNT          = 15;
      public const int DEFAULT_MIN_CAPITAL_CHARACTER_COUNT  = 1;
      public const int DEFAULT_MIN_CHARACTER_COUNT          = 8;
      public const int DEFAULT_MIN_LOW_CASE_CHARACTER_COUNT = 1;
      public const int DEFAULT_MAX_REPEAT_CHARS             = 2;
      public const int DEFAULT_MIN_NUMERIC_CHARACTER_COUNT  = 0;
      public const int DEFAULT_MIN_SPECIAL_CHARACTER_COUNT  = 0;

      private const int MAX_PHONE_NUMBER_LENGTH = 10;

      /// <summary>The maximum expiration years</summary>
      private const int MAX_EXPIRATION_YEARS = 10;

      // Constant values are required for attributes
      public const string CHECK_BOX_INPUT_TYPE          = "CheckBoxInput";
      public const string DATE_TIME_INPUT_TYPE          = "DateTimeInput";
      public const string EXPIRATION_YEAR_INPUT_TYPE    = "ExpirationYearInput";
      public const string MONTH_INPUT_TYPE              = "MonthInput";
      public const string NULLABLE_DATE_TIME_INPUT_TYPE = "NullableDateTimeInput";
      public const string STATE_INPUT_TYPE              = "StateInput";
      public const string TEXT_INPUT_TYPE               = "TextInput";

      public const string DOUBLE_NUMERIC_TYPE          = "DoubleNumericType";
      public const string INT_NUMERIC_TYPE             = "IntNumericType";
      public const string LONG_NUMERIC_TYPE            = "LongNumericType";
      public const string NO_NUMERIC_TYPE              = "NoNumericType";
      public const string NULLABLE_DOUBLE_NUMERIC_TYPE = "NullableDoubleNumericType";
      public const string NULLABLE_INT_NUMERIC_TYPE    = "NullableIntNumericType";
      public const string NULLABLE_LONG_NUMERIC_TYPE   = "NullableLongNumericType";

      public static readonly string[] STATIC_MONTHS =
      {
         " ",
         "January",
         "February",
         "March",
         "April",
         "May",
         "June",
         "July",
         "August",
         "September",
         "October",
         "November",
         "December",
      };

      public static readonly string[] STATIC_STATES =
      {
         " ",
         "Alabama",
         "Alaska",
         "Arizona",
         "Arkansas",
         "California",
         "Colorado",
         "Connecticut",
         "Delaware",
         "Florida",
         "Georgia",
         "Hawaii",
         "Idaho",
         "Illinois",
         "Indiana",
         "Iowa",
         "Kansas",
         "Kentucky",
         "Louisiana",
         "Maine",
         "Maryland",
         "Massachusetts",
         "Michigan",
         "Minnesota",
         "Mississippi",
         "Missouri",
         "Montana",
         "Nebraska",
         "Nevada",
         "New Hampshire",
         "New Jersey",
         "New Mexico",
         "New York",
         "North Carolina",
         "North Dakota",
         "Ohio",
         "Oklahoma",
         "Oregon",
         "Pennsylvania",
         "Rhode Island",
         "South Carolina",
         "South Dakota",
         "Tennessee",
         "Texas",
         "Utah",
         "Vermont",
         "Virginia",
         "Washington",
         "West Virginia",
         "Wisconsin",
         "Wyoming",
      };

      /*
      public const string CHECK_BOX_INPUT_TYPE = Enum.GetName(typeof(InputTypes), InputTypes.CheckBoxInput);
      public const string DATE_TIME_INPUT_TYPE = Enum.GetName(typeof(InputTypes), InputTypes.DateTimeInput);
      public const string EXPIRATION_YEAR_INPUT_TYPE = Enum.GetName(typeof(InputTypes), InputTypes.ExpirationYearInput);
      public const string MONTH_INPUT_TYPE = Enum.GetName(typeof(InputTypes), InputTypes.MonthInput);
      public const string NULLABLE_DATE_TIME_INPUT_TYPE = Enum.GetName(typeof(InputTypes), InputTypes.NullableDateTimeInput);
      public const string STATE_INPUT_TYPE = Enum.GetName(typeof(InputTypes), InputTypes.StateInput);
      public const string TEXT_INPUT_TYPE  = Enum.GetName(typeof(InputTypes), InputTypes.TextInput);

      public const string DOUBLE_NUMERIC_TYPE = Enum.GetName(typeof(NumericTypes), NumericTypes.DoubleNumericType);
      public const string INT_NUMERIC_TYPE = Enum.GetName(typeof(NumericTypes), NumericTypes.IntNumericType);
      public const string LONG_NUMERIC_TYPE = Enum.GetName(typeof(NumericTypes), NumericTypes.LongNumericType);
      public const string NO_NUMERIC_TYPE = Enum.GetName(typeof(NumericTypes), NumericTypes.NoNumericType);
      public const string NULLABLE_DOUBLE_NUMERIC_TYPE = Enum.GetName(typeof(NumericTypes), NumericTypes.NullableDoubleNumericType);
      public const string NULLABLE_INT_NUMERIC_TYPE = Enum.GetName(typeof(NumericTypes), NumericTypes.NullableIntNumericType);
      public const string NULLABLE_LONG_NUMERIC_TYPE = Enum.GetName(typeof(NumericTypes), NumericTypes.NullableLongNumericType);
      */

      public static int[] STATIC_EXPIRATION_YEARS
      {
         get
         {
            var yearsList = new List<int>();
            var thisYear  = DateTime.Now.Year;

            for (var yearIdx = thisYear; yearIdx < (thisYear + MAX_EXPIRATION_YEARS); yearIdx++)
            {
               yearsList.Add(yearIdx);
            }

            return yearsList.ToArray();
         }
      }

      public static async Task SetIsTrueOrFalse(this IThreadSafeAccessor isValidAccessor, bool isValid,
                                                IResponsiveTasks         IsValidChangedTask)
      {
         ErrorUtils.IssueArgumentErrorIfFalse(isValidAccessor.IsNotNullOrDefault(),
            nameof(SetIsTrueOrFalse) + ": Null thread safe accessor passed in");

         if (isValidAccessor.IsTrue() != isValid)
         {
            if (isValid)
            {
               isValidAccessor.SetTrue();
            }
            else
            {
               isValidAccessor.SetFalse();
            }

            await IsValidChangedTask.RunAllTasksUsingDefaults(isValidAccessor).WithoutChangingContext();
         }
      }

      public static string StripStringFormatCharacters(string entryText,
                                                       string stringFormat,
                                                       string numericType,
                                                       int    charsToRight = 0,
                                                       bool   firstFocused = false)
      {
         // Remove the numeric string formats (all except for numbers and dots)
         if (stringFormat.IsEmpty() || entryText.IsEmpty())
         {
            return entryText;
         }

         // Numbers are either "Whole" (no special symbols allowed) or "Decimal" (a single dot is allowed, plus a certain
         // number of characters after that).
         if (numericType.IsDecimal())
         {
            var retString = Regex.Replace(entryText, "[^0-9.]+", "");

            // Add the period and zeroes
            if (firstFocused)
            {
               var decimalPos = retString.PositionOfDecimal();
               if (decimalPos == -1)
               {
                  retString  += Extensions.DECIMAL;
                  decimalPos =  retString.PositionOfDecimal();
               }

               var trueEndPos = decimalPos + charsToRight;
               for (var charIdx = decimalPos + 1; charIdx <= trueEndPos; charIdx++)
               {
                  // char idx of 2 with ret string length of 2 means that retString[2] doesn't yet exist.
                  if (charIdx >= retString.Length)
                  {
                     retString += "0";
                  }
               }
            }

            return retString;
         }

         if (numericType.IsWhole())
         {
            return Regex.Replace(entryText, "[^0-9]+", "");
         }

         // ELSE
         // Illegal
         Debug.WriteLine(
            nameof(StripStringFormatCharacters)              +
            ": illegal numeric validation for entry text ->" +
            entryText                                        +
            "<- string format ->"                            +
            stringFormat                                     +
            "<- is decimal ? ->"                             +
            numericType.IsDecimal()                          +
            "<- is whole ? ->"                               +
            numericType.IsWhole()                            +
            "<-");

         return entryText;
      }

      public static bool IsANumberGreaterThanZero(this object o)
      {
         if (double.TryParse(o?.ToString(), out var testDouble))
         {
            return testDouble.IsGreaterThan(0);
         }

         return false;
      }

      public static bool IsAValidEmailAddress(this string str)
      {
         return str.IsNonNullRegexMatch
         (

            // Original does *not* work at all
            // @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@)) (?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$"
            // https://www.rhyous.com/2010/06/15/regular-expressions-in-cincluding-a-new-comprehensive-email-pattern/
            //@"^ (([^<> ()[\]\\.,;:\s@\""]+(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$"
            // https://msdn.microsoft.com/en-us/library/01escwtf(v=vs.110).aspx
            @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$"
         );
      }

      public static string StripMaskFromText_PI(this string text, string mask, IDictionary<int, char> maskPositions)
      {
         if (mask.IsEmpty() || maskPositions.IsEmpty() || text.IsEmpty())
         {
            return text;
         }

         // ELSE
         var strBuilder = new StringBuilder();

         for (var i = 0; i < text.Length; i++)
         {
            if (!maskPositions.ContainsKey(i))
            {
               strBuilder.Append(text[i]);
            }
         }

         return strBuilder.ToString();
      }

      public static Task<string> ConsiderMasking_PI(this string preparedNewText,   IDictionary<int, char> maskPositions,
                                                    string      lastValidatedText, int                    maxLength)
      {
         if (preparedNewText.IsNotEmpty() && maskPositions.IsNotEmpty() &&
            preparedNewText.IsDifferentThan(lastValidatedText))
         {
            foreach (var position in maskPositions)
            {
               if (preparedNewText.Length >= (position.Key + 1))
               {
                  var value = position.Value.ToString();
                  if (preparedNewText.Substring(position.Key, 1).IsDifferentThan(value))
                  {
                     preparedNewText = preparedNewText.Insert(position.Key, value);

                     // Insertion can potentially push a new character off the end of the mask and thereby exceed the max
                     // length
                     if ((maxLength > 0) && (preparedNewText.Length > maxLength))
                     {
                        preparedNewText = preparedNewText.Remove(preparedNewText.Length - 1);
                     }
                  }
               }

               // IMPORTANT 
               /* CAN'T BACK-SPACE THROUGH A MASKED CHARACTER IF THIS IS TRUE
   
               // This should allow an append as well as an insert The key is the position inside th string. The length
               // is zero-based. If the masked position is 1 (the second position), and the string has 1 character, the
               // top test will be: "if 1 >= 2" That will fail to append. This test checks to see if the dictionary
               // position is to the right of the current character by one character.
               else if (preparedNewText.Length == position.Key)
               {
                  // The preparedNewText does not contain the mask (yet), so add it
                  preparedNewText += position.Value;
   
                  // Nothing else to do
                  break;
               }
               */
               else
               {
                  // Nothing else to do
                  break;
               }
            }
         }

         return Task.FromResult(preparedNewText);
      }

      public static IDictionary<int, char> CreateMaskPositions_PI(this string mask)
      {
         var dict = new Dictionary<int, char>();

         if (mask.IsNotEmpty())
         {
            for (var i = 0; i < mask.Length; i++)
            {
               if (mask[i] != X_CHAR)
               {
                  dict.Add(i, mask[i]);
               }
            }
         }

         return dict;
      }

      public static bool IsDecimal(this string numericType)
      {
         return
            numericType.IsSameAs(DOUBLE_NUMERIC_TYPE) ||
            numericType.IsSameAs(NULLABLE_DOUBLE_NUMERIC_TYPE);
      }

      public static bool IsWhole(this string numericType)
      {
         return
            numericType.IsSameAs(INT_NUMERIC_TYPE) ||
            numericType.IsSameAs(LONG_NUMERIC_TYPE);
      }

      public static string PrepareTextForEditing_PI(this string entryText,
                                                    bool        firstFocused,
                                                    string      numericType,
                                                    string      stringFormat,
                                                    int         charsToRightOfDecimal)
      {
         var retStr =
            StripStringFormatCharacters
            (
               entryText,
               stringFormat,
               numericType,
               charsToRightOfDecimal,
               firstFocused
            );

         return retStr;
      }

      public static string AnalyzeChangedNumericText
      (
         string                 changedText,
         string                 originalText,
         string                 numericType,
         string                 mask,
         IDictionary<int, char> maskPositions,
         double                 minNumber,
         double                 maxNumber,
         int                    charsToRightOfDecimal,
         out bool               isLessThanMin,
         out bool               isGreaterThanMax,
         out bool               isValidNumber
      )
      {
         isLessThanMin    = false;
         isGreaterThanMax = false;
         isValidNumber    = false;


         // CRITICAL
         var strippedNewText = changedText.StripMaskFromText_PI(mask, maskPositions);
         var strippedOldText = originalText.StripMaskFromText_PI(mask, maskPositions);

         if (strippedNewText.IsEmpty() || strippedNewText.IsSameAs(strippedOldText))
         {
            return changedText;
         }

         // Spaces are illegal
         if (strippedNewText.IsNotEmpty() && strippedNewText.Contains(UIConst_PI.SPACE))
         {
            // FAIL
            {
               return originalText;
            }
         }

         // ELSE ALL OK

         // Numbers are either:
         // * "Whole" (no special symbols allowed) or
         // * "Decimal" (a single dot is allowed, plus a certain number of characters after that).
         if (numericType.IsDecimal())
         {
            if (double.TryParse(strippedNewText, out var testDouble))
            {
               isValidNumber = true;

               isLessThanMin    = testDouble.IsLessThan(minNumber);
               isGreaterThanMax = testDouble.IsGreaterThan(maxNumber);

               // ... else watch out for excessive decimal characters
               var decimalPos = strippedNewText.PositionOfDecimal();
               if (decimalPos > 0)
               {
                  var newTextCharsToRightOfDecimal = strippedNewText.Length - 1 - decimalPos;

                  if (newTextCharsToRightOfDecimal > charsToRightOfDecimal)
                  {
                     return originalText;
                  }
               }
            }

            // Return the changed text
            return changedText;
         }

         if (numericType.IsWhole())
         {
            if (long.TryParse(strippedNewText, out var testLong))
            {
               isValidNumber = true;

               isLessThanMin    = testLong < minNumber.ToRoundedInt();
               isGreaterThanMax = testLong > maxNumber.ToRoundedInt();

               // Return the changed text
               return changedText;
            }

            // FAIL: Return the original text
            return originalText;
         }

         // ELSE FAIL
         // This is a text field with numeric constraints
         // Illegal
         // ELSE
         // Illegal
         Debug.WriteLine
         (
            nameof(AnalyzeChangedNumericText)                +
            ": illegal numeric validation for entry text ->" +
            changedText                                      +
            "<- original text ->"                            +
            originalText                                     +
            "<- numeric type ->"                             +
            numericType                                      +
            "<- mask ->"                                     +
            mask                                             +
            "<- min number ->"                               +
            minNumber                                        +
            "<- max number ->"                               +
            maxNumber                                        +
            "<- is decimal ? ->"                             +
            numericType.IsDecimal()                          +
            "<- is whole ? ->"                               +
            numericType.IsWhole()                            +
            "<-"
         );

         return originalText;
      }

      public static string GetGenericNumberFormat(this int charsToRightOfDecimal)
      {
         return "d" + charsToRightOfDecimal;
      }

      public static bool DoesNotRepeatCharacters(this string str, int maxCharsAllowed)
      {
         if (str.IsEmpty())
         {
            return false;
         }

         for (var idx = maxCharsAllowed; idx < str.Length; idx++)
         {
            // Test the idx and characters directly to its left The min idx is the exact characters allowed:
            // Assuming 2 consecutive characters are allowed - 3 is a violation --

            // ABCDDD

            // The idx finally gets to the end of the string, so is at idx 5 We start at 4, one to the left (no need to
            // check the idx we are on). We end at 3 because that's all we need for a violation: The character "D" at
            // indexes 3, 4 and 5
            for (var subIdx = idx - maxCharsAllowed; subIdx <= idx; subIdx++)
            {
               // Not the same, so not illegal
               if (str[idx] != str[subIdx])
               {
                  break;
               }

               // All characters matched
               if (subIdx == idx)
               {
                  return false;
               }

               // Else continue checking
            }
         }

         return true;
      }

      /// <summary>Phones the number illegal character function.</summary>
      public static string PhoneNumberIllegalCharFunc(this string newText, out bool isOutsideOfRange)
      {
         isOutsideOfRange = false;

         // Overall: some complexity in what to allow, including spaces -- ??? or dots -- ?? But as the user types, we
         // have to allow partially accurate values so the user can complete their work.
         var retStr = string.Empty;

         foreach (var c in newText)
         {
            if (char.IsNumber(c) && (retStr.Length < MAX_PHONE_NUMBER_LENGTH))
            {
               retStr += c;
            }
         }

         return retStr;
      }

      public static IDictionary<PropertyInfo, AttributeT> CreateViewModelCustomAttributeDict<AttributeT>(
         this Type type)
         where AttributeT : Attribute
      {
         if (type.IsNullOrDefault())
         {
            return default;
         }

         var publicProperties = type.GetProperties();

         if (publicProperties.IsEmpty())
         {
            return default;
         }

         var retPropertiesDict = new ConcurrentDictionary<PropertyInfo, AttributeT>();

         // Reduce the public properties down to those with the custom attribute
         // ReSharper disable once PossibleNullReferenceException
         foreach (var propInfo in publicProperties)
         {
            var customAttribute = propInfo.GetCustomAttribute<AttributeT>();

            // Only add properties with a custom attribute
            if (customAttribute.IsNotNullOrDefault())
            {
               retPropertiesDict.AddOrUpdate(propInfo, customAttribute);
            }
         }

         return retPropertiesDict;
      }
   }
}