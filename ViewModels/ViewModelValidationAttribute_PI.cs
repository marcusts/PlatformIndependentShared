// *********************************************************************************
// Copyright @2021 Marcus Technical Services, Inc.
// <copyright
// file=ViewModelValidationAttribute_PI.cs
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
   using Com.MarcusTS.PlatformIndependentShared.Common.Interfaces;
   using Com.MarcusTS.PlatformIndependentShared.Common.Utils;
   using System;
   using System.Runtime.CompilerServices;

   public interface IViewModelValidationAttribute_PI :
      IViewModelCustomAttributeRoot_PI,
      IEntryValidationBehaviorProperties_PI,
      IViewValidationBehaviorProperties_PI,
      IMinAndMaxNumberProperties_PI,
      IPasswordProperties_PI
   {
      string InputTypeStr { get; set; }
      int IsInitialFocus { get; set; }
      int IsPassword { get; set; }
      string KeyboardName { get; set; }
      Type ValidatorType { get; set; }
   }

   [AttributeUsage( AttributeTargets.Property )]
   public class ViewModelValidationAttribute_PI :
      Attribute, IViewModelValidationAttribute_PI
   {
      public ViewModelValidationAttribute_PI
      (

         // REQUIRED
         int displayOrder,

         // OPTIONAL
         int    canUnmaskPassword                    = ViewModelCustomAttribute_Static_PI.UNSET_INT,
         int    charsToRightOfDecimal                = ViewModelCustomAttribute_Static_PI.UNSET_INT,
         int    doNotForceMaskInitially              = ViewModelCustomAttribute_Static_PI.UNSET_INT,
         string excludedChars                        = ViewModelCustomAttribute_Static_PI.UNSET_STRING,
         string inputTypeStr                         = ViewModelCustomAttribute_Static_PI.UNSET_STRING,
         string instructionsText                     = ViewModelCustomAttribute_Static_PI.UNSET_STRING,
         int    isInitialFocus                       = ViewModelCustomAttribute_Static_PI.UNSET_BOOL,
         int    isPassword                           = ViewModelCustomAttribute_Static_PI.UNSET_BOOL,
         string keyboardName                         = ViewModelCustomAttribute_Static_PI.UNSET_STRING,
         string labelVerticalTextAlignment           = ViewModelCustomAttribute_Static_PI.UNSET_STRING,
         string labelTextWrapMode                    = ViewModelCustomAttribute_Static_PI.UNSET_STRING,
         string mask                                 = ViewModelCustomAttribute_Static_PI.UNSET_STRING,
         int    maxCharacterCount                    = ViewModelCustomAttribute_Static_PI.UNSET_INT,
         int    maxLength                            = ViewModelCustomAttribute_Static_PI.UNSET_INT,
         double maxNumber                            = ViewModelCustomAttribute_Static_PI.UNSET_DOUBLE,
         int    maxRepeatChars                       = ViewModelCustomAttribute_Static_PI.UNSET_INT,
         int    minCapitalCharacterCount             = ViewModelCustomAttribute_Static_PI.UNSET_INT,
         int    minCharacterCount                    = ViewModelCustomAttribute_Static_PI.UNSET_INT,
         int    minLength                            = ViewModelCustomAttribute_Static_PI.UNSET_INT,
         int    minLowCaseCharacterCount             = ViewModelCustomAttribute_Static_PI.UNSET_INT,
         double minNumber                            = ViewModelCustomAttribute_Static_PI.UNSET_DOUBLE,
         int    minNumericCharacterCount             = ViewModelCustomAttribute_Static_PI.UNSET_INT,
         int    minSpecialCharacterCount             = ViewModelCustomAttribute_Static_PI.UNSET_INT,
         string numericType                          = ViewModelCustomAttribute_Static_PI.UNSET_STRING,
         string placeholderText                      = ViewModelCustomAttribute_Static_PI.UNSET_STRING,
         int    showInstructions                     = ViewModelCustomAttribute_Static_PI.UNSET_INT,
         int    showValidationErrors                 = ViewModelCustomAttribute_Static_PI.UNSET_INT,
         int    showValidationErrorsWithInstructions = ViewModelCustomAttribute_Static_PI.UNSET_INT,
         string stringFormat                         = ViewModelCustomAttribute_Static_PI.UNSET_STRING,
         int    textMustChange                       = ViewModelCustomAttribute_Static_PI.UNSET_INT,
         Type   validatorType                        = default,

         // AUTO SET
         [CallerMemberName] string viewModelPropertyName = ""
      )
      {
         CanUnmaskPassword = canUnmaskPassword;
         CharsToRightOfDecimal = charsToRightOfDecimal;
         DisplayOrder = displayOrder;
         DoNotForceMaskInitially = doNotForceMaskInitially;
         ExcludedChars = excludedChars;
         InputTypeStr = inputTypeStr.IsUnset() ? ValidationUtils_PI.TEXT_INPUT_TYPE : inputTypeStr;
         InstructionsText = instructionsText;
         IsInitialFocus = isInitialFocus;
         IsPassword = isPassword;
         KeyboardName = keyboardName;
         LabelTextWrapMode_LineBreakMode = labelTextWrapMode;
         LabelVerticalTextAlignment_TextAlignment = labelVerticalTextAlignment;
         Mask = mask;
         MaxCharacterCount = maxCharacterCount;
         MaxDecimalNumber = maxNumber;
         MaxLength = maxLength;
         MaxRepeatChars = maxRepeatChars;
         MinCapitalCharacterCount = minCapitalCharacterCount;
         MinCharacterCount = minCharacterCount;
         MinDecimalNumber = minNumber;
         MinLength = minLength;
         MinLowCaseCharacterCount = minLowCaseCharacterCount;
         MinNumericCharacterCount = minNumericCharacterCount;
         MinSpecialCharacterCount = minSpecialCharacterCount;
         NumericType = numericType.IsUnset() ? ValidationUtils_PI.NO_NUMERIC_TYPE : numericType;
         PlaceholderText = placeholderText;
         ShowInstructions = showInstructions;
         ShowValidationErrors = showValidationErrors;
         ShowValidationErrorsWithInstructions = showValidationErrorsWithInstructions;
         StringFormat = stringFormat;
         TextMustChange = textMustChange;
         ValidatorType = validatorType;
         ViewModelPropertyName = viewModelPropertyName;
      }

      public int CanUnmaskPassword { get; set; }
      public int CharsToRightOfDecimal { get; set; }
      public int DisplayOrder { get; set; }
      public int DoNotForceMaskInitially { get; set; }
      public string ExcludedChars { get; set; }
      public string InputTypeStr { get; set; }
      public string InstructionsText { get; set; }
      public int IsInitialFocus { get; set; }
      public int IsPassword { get; set; }
      public string KeyboardName { get; set; }
      public string LabelTextWrapMode_LineBreakMode { get; set; }
      public string LabelVerticalTextAlignment_TextAlignment { get; set; }
      public string Mask { get; set; }
      public int MaxCharacterCount { get; set; }
      public double MaxDecimalNumber { get; set; }
      public int MaxLength { get; set; }
      public int MaxRepeatChars { get; set; }
      public int MinCapitalCharacterCount { get; set; }
      public int MinCharacterCount { get; set; }
      public double MinDecimalNumber { get; set; }
      public int MinLength { get; set; }
      public int MinLowCaseCharacterCount { get; set; }
      public int MinNumericCharacterCount { get; set; }
      public int MinSpecialCharacterCount { get; set; }
      public string NumericType { get; set; }
      public string PlaceholderText { get; set; }
      public int ShowInstructions { get; set; }
      public int ShowValidationErrors { get; set; }
      public int ShowValidationErrorsWithInstructions { get; set; }
      public string StringFormat { get; set; }
      public int TextMustChange { get; set; }
      public Type ValidatorType { get; set; }
      public string ViewModelPropertyName { get; set; }
   }
}
