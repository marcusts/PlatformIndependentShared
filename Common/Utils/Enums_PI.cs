// *********************************************************************************
// Copyright @2021 Marcus Technical Services, Inc.
// <copyright
// file=Enums.cs
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
   public enum Outcomes
   {
      Next,
      Cancel,
      TryAgain
   }

   public enum FlowableChildChanges
   {
      UNSET,
      CanvasWidthChange,
   }

   //public enum FlowableChildPositionStatuses
   //{
   //   UNSET,
   //   FilteredOut,
   //   OffScreen,
   //   OnScreen
   //}

   /// <summary>
   /// Enum HeaderFooterShowRules
   /// </summary>
   /// <summary>
   /// Enum HeaderFooterShowRules
   /// </summary>
   public enum HeaderFooterShowRules
   {
      /// <summary>
      /// The always
      /// </summary>
      /// <summary>
      /// The always
      /// </summary>
      Always,

      /// <summary>
      /// The only when at least one row is showing
      /// </summary>
      /// <summary>
      /// The only when at least one row is showing
      /// </summary>
      OnlyWhenAtLeastOneRowIsShowing,

      /// <summary>
      /// The never
      /// </summary>
      /// <summary>
      /// The never
      /// </summary>
      Never
   }

   /// <summary>
   /// Enum RowHeightCalculationMethods
   /// </summary>
   /// <summary>
   /// Enum RowHeightCalculationMethods
   /// </summary>
   public enum RowHeightCalculationMethods
   {
      /// <summary>
      /// The automatic calculate
      /// </summary>
      /// <summary>
      /// The automatic calculate
      /// </summary>
      AutoCalc,

      /// <summary>
      /// The parent decides
      /// </summary>
      /// <summary>
      /// The parent decides
      /// </summary>
      ParentDecides,

      /// <summary>
      /// The override locally
      /// </summary>
      /// <summary>
      /// The override locally
      /// </summary>
      OverrideLocally
   }

   /// <summary>
   /// Enum SelectionRules
   /// </summary>
   public enum SelectionRules
   {
      /// <summary>
      /// The no selection
      /// </summary>
      NoSelection,

      /// <summary>
      /// The single selection can be null
      /// </summary>
      SingleSelectionCanBeNull,

      /// <summary>
      /// The single selection at least one required
      /// </summary>
      SingleSelectionAtLeastOneRequired,

      /// <summary>
      /// The multi selection can be null
      /// </summary>
      MultiSelectionCanBeNull,

      /// <summary>
      /// The multi selection at least one required
      /// </summary>
      MultiSelectionAtLeastOneRequired
   }

   /// <summary>Enum SelectionStyles</summary>
   public enum ImageLabelButtonSelectionStyles
   {
      /// <summary>The no selection</summary>
      NoSelection,

      /// <summary>The selection but no toggle as first two styles</summary>
      SelectionButNoToggleAsFirstTwoStyles,

      /// <summary>Toggles selection between the first and second styles ONLY.</summary>
      ToggleSelectionAsFirstTwoStyles,

      /// <summary>Toggles selection through all styles.</summary>
      ToggleSelectionThroughAllStyles
   }

   public enum AvailableGrids
   {
      Stage,
      Keyboard,
      Gross
   }

   /// <summary></summary>
   public enum ButtonStates
   {
      // Important to have this first
      Deselected,

      Selected,
      Disabled,
      None
   }

   /*
   public enum InputTypes
   {
      TextInput,
      StateInput,
      MonthInput,
      ExpirationYearInput,
      DateTimeInput,
      NullableDateTimeInput,
      CheckBoxInput
   }

   public enum NumericTypes
   {
      NoNumericType,
      DoubleNumericType,
      NullableDoubleNumericType,
      IntNumericType,
      NullableIntNumericType,
      LongNumericType,
      NullableLongNumericType
   }
   */

   //public enum ValidationTypes
   //{
   //   AnyText,
   //   WholeNumber,
   //   DecimalNumber
   //}

}