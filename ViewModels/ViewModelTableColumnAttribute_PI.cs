// *********************************************************************************
// Copyright @2021 Marcus Technical Services, Inc.
// <copyright
// file=ViewModelTableColumnAttribute_PI.cs
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
   using System;
   using System.Runtime.CompilerServices;
   using Com.MarcusTS.PlatformIndependentShared.Common.Interfaces;

   public interface IViewModelTableColumnAttribute_PI : IViewModelCustomAttributeRoot_PI, ICanSort
   {
      string BoundMode_BindingMode                     { get; set; }
      int    CanSort                                   { get; set; }
      string CellFontAttributes_FontAttributes         { get; set; }
      string CellFontFamily                            { get; set; }
      double CellFontSize_OS                           { get; set; }
      string CellHorizontalTextAlignment_TextAlignment { get; set; }
      string CellLineBreakMode_LineBreakMode           { get; set; }
      string CellStringFormat                          { get; set; }
      string CellTextColor_ColorHex                    { get; set; }
      double CellUniformMargin_OS                      { get; set; }
      double CellUniformPadding_OS                     { get; set; }
      string CellVerticalTextAlignment_TextAlignment   { get; set; }
      double ColumnWidth_OS                            { get; set; }
      string DisplayKind                                 { get; set; }
      string HeaderBackColor_ColorHex                    { get; set; }
      string HeaderBorderColor_ColorHex                  { get; set; }
      double HeaderBorderThickness_OS                    { get; set; }
      string HeaderFontAttributes_FontAttributes         { get; set; }
      string HeaderFontFamily                            { get; set; }
      double HeaderHeight_OS                             { get; set; }
      double HeaderFontSize_OS                           { get; set; }
      string HeaderHorizontalTextAlignment_TextAlignment { get; set; }
      string HeaderLineBreakMode_LineBreakMode           { get; set; }
      string HeaderName                                  { get; set; }
      string HeaderStringFormat                          { get; set; }
      string HeaderTextColor_ColorHex                    { get; set; }
      double HeaderUniformMargin_OS                      { get; set; }
      double HeaderUniformPadding_OS                     { get; set; }
      string HeaderVerticalTextAlignment_TextAlignment   { get; set; }
      int    IsFlexWidth                                 { get; set; }
   }

   [AttributeUsage(AttributeTargets.Property)]
   public class ViewModelTableColumnAttribute_PI :
      Attribute,
      IViewModelTableColumnAttribute_PI
   {
      public ViewModelTableColumnAttribute_PI
      (

         // REQUIRED
         int    displayOrder,
         string displayKind,
         string headerName,

         // OPTIONAL
         string boundMode                     = ViewModelCustomAttribute_Static_PI.UNSET_STRING,
         int    canSort                       = ViewModelCustomAttribute_Static_PI.UNSET_BOOL,
         string cellFontAttributes            = ViewModelCustomAttribute_Static_PI.UNSET_STRING,
         string cellFontFamily                = ViewModelCustomAttribute_Static_PI.UNSET_STRING,
         double cellFontSize                  = ViewModelCustomAttribute_Static_PI.UNSET_DOUBLE,
         string cellHorizontalTextAlignment   = ViewModelCustomAttribute_Static_PI.UNSET_STRING,
         string cellLineBreakMode             = ViewModelCustomAttribute_Static_PI.UNSET_STRING,
         string cellStringFormat              = ViewModelCustomAttribute_Static_PI.UNSET_STRING,
         string cellTextColor                 = ViewModelCustomAttribute_Static_PI.UNSET_STRING,
         double cellUniformMargin             = ViewModelCustomAttribute_Static_PI.UNSET_DOUBLE,
         double cellUniformPadding            = ViewModelCustomAttribute_Static_PI.UNSET_DOUBLE,
         string cellVerticalTextAlignment     = ViewModelCustomAttribute_Static_PI.UNSET_STRING,
         double columnWidth                   = ViewModelCustomAttribute_Static_PI.UNSET_DOUBLE,
         int    defaultSortOrder              = ViewModelCustomAttribute_Static_PI.UNSET_INT,

         // CRITICAL
         int    defaultSortDescending         = ViewModelCustomAttribute_Static_PI.FALSE_BOOL,

         string headerBackColor               = ViewModelCustomAttribute_Static_PI.UNSET_STRING,
         string headerBorderColor             = ViewModelCustomAttribute_Static_PI.UNSET_STRING,
         double headerBorderThickness         = ViewModelCustomAttribute_Static_PI.UNSET_DOUBLE,
         string headerFontAttributes          = ViewModelCustomAttribute_Static_PI.UNSET_STRING,
         string headerFontFamily              = ViewModelCustomAttribute_Static_PI.UNSET_STRING,
         double headerFontSize                = ViewModelCustomAttribute_Static_PI.UNSET_DOUBLE,
         double headerHeight                  = ViewModelCustomAttribute_Static_PI.UNSET_DOUBLE,
         string headerHorizontalTextAlignment = ViewModelCustomAttribute_Static_PI.UNSET_STRING,
         string headerLineBreakMode           = ViewModelCustomAttribute_Static_PI.UNSET_STRING,
         string headerStringFormat            = ViewModelCustomAttribute_Static_PI.UNSET_STRING,
         string headerTextColor               = ViewModelCustomAttribute_Static_PI.UNSET_STRING,
         double headerUniformMargin           = ViewModelCustomAttribute_Static_PI.UNSET_DOUBLE,
         double headerUniformPadding          = ViewModelCustomAttribute_Static_PI.UNSET_DOUBLE,
         string headerVerticalTextAlignment   = ViewModelCustomAttribute_Static_PI.UNSET_STRING,
         int    isFlexWidth                   = ViewModelCustomAttribute_Static_PI.UNSET_BOOL,

         // AUTO SET
         [CallerMemberName] string viewModelPropertyName = ""
      )
      {
         BoundMode_BindingMode                       = boundMode;
         CanSort                                     = canSort;
         CellFontAttributes_FontAttributes           = cellFontAttributes;
         CellFontFamily                              = cellFontFamily;
         CellFontSize_OS                             = cellFontSize;
         CellHorizontalTextAlignment_TextAlignment   = cellHorizontalTextAlignment;
         CellLineBreakMode_LineBreakMode             = cellLineBreakMode;
         CellStringFormat                            = cellStringFormat;
         CellTextColor_ColorHex                      = cellTextColor;
         CellUniformMargin_OS                        = cellUniformMargin;
         CellUniformPadding_OS                       = cellUniformPadding;
         CellVerticalTextAlignment_TextAlignment     = cellVerticalTextAlignment;
         ColumnWidth_OS                              = columnWidth;
         DefaultSortOrder                            = defaultSortOrder;
         DefaultSortDescending                       = defaultSortDescending;
         DisplayKind                                 = displayKind;
         DisplayOrder                                = displayOrder;
         HeaderBackColor_ColorHex                    = headerBackColor;
         HeaderBorderColor_ColorHex                  = headerBorderColor;
         HeaderBorderThickness_OS                    = headerBorderThickness;
         HeaderFontAttributes_FontAttributes         = headerFontAttributes;
         HeaderFontFamily                            = headerFontFamily;
         HeaderFontSize_OS                           = headerFontSize;
         HeaderHeight_OS                             = headerHeight;
         HeaderHorizontalTextAlignment_TextAlignment = headerHorizontalTextAlignment;
         HeaderLineBreakMode_LineBreakMode           = headerLineBreakMode;
         HeaderName                                  = headerName;
         HeaderStringFormat                          = headerStringFormat;
         HeaderTextColor_ColorHex                    = headerTextColor;
         HeaderUniformMargin_OS                      = headerUniformMargin;
         HeaderUniformPadding_OS                     = headerUniformPadding;
         HeaderVerticalTextAlignment_TextAlignment   = headerVerticalTextAlignment;
         IsFlexWidth                                 = isFlexWidth;
         ViewModelPropertyName                       = viewModelPropertyName;
      }

      public string BoundMode_BindingMode                       { get; set; }
      public int    CanSort                                     { get; set; }
      public string CellFontAttributes_FontAttributes           { get; set; }
      public string CellFontFamily                              { get; set; }
      public double CellFontSize_OS                             { get; set; }
      public string CellHorizontalTextAlignment_TextAlignment   { get; set; }
      public string CellLineBreakMode_LineBreakMode             { get; set; }
      public string CellStringFormat                            { get; set; }
      public string CellTextColor_ColorHex                      { get; set; }
      public double CellUniformMargin_OS                        { get; set; }
      public double CellUniformPadding_OS                       { get; set; }
      public string CellVerticalTextAlignment_TextAlignment     { get; set; }
      public double ColumnWidth_OS                              { get; set; }
      public int    DefaultSortOrder                            { get; set; }
      public int    DefaultSortDescending                       { get; set; }
      public string DisplayKind                                 { get; set; }
      public int    DisplayOrder                                { get; set; }
      public string HeaderBackColor_ColorHex                    { get; set; }
      public string HeaderBorderColor_ColorHex                  { get; set; }
      public double HeaderBorderThickness_OS                    { get; set; }
      public string HeaderFontAttributes_FontAttributes         { get; set; }
      public string HeaderFontFamily                            { get; set; }
      public double HeaderHeight_OS                             { get; set; }
      public double HeaderFontSize_OS                           { get; set; }
      public string HeaderHorizontalTextAlignment_TextAlignment { get; set; }
      public string HeaderLineBreakMode_LineBreakMode           { get; set; }
      public string HeaderName                                  { get; set; }
      public string HeaderStringFormat                          { get; set; }
      public string HeaderTextColor_ColorHex                    { get; set; }
      public double HeaderUniformMargin_OS                      { get; set; }
      public double HeaderUniformPadding_OS                     { get; set; }
      public string HeaderVerticalTextAlignment_TextAlignment   { get; set; }
      public int    IsFlexWidth                                 { get; set; }
      public string ViewModelPropertyName                       { get; set; }
   }
}
