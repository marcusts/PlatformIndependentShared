// *********************************************************************************
// Copyright @2021 Marcus Technical Services, Inc.
// <copyright
// file=UIConst_PI.cs
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
   public class UIConst_PI
   {
      public const  int AFTER_BUTTON_PRESS_DELAY_MILLISECONDS           = 250;
      public const double ANDROID_ADJUSTMENT = 1.5;
      public const  double ANIMATION_BOUNCE_SCALE      = 0.9;
      public const string DEFAULT_IMAGE_SUFFIX = ".png";
      public const float HAPTIC_VIBRATION_MILLISECONDS = 250;
      public const string HEIGHT_PROPERTY_NAME = "Height";
      public const double MODERATE_OPACITY = 1.0 / 2.0;
      public const double NEUTRAL_WIDTH_HEIGHT = -1;
      public const double NOT_VISIBLE_OPACITY = 0;
      public const string SPACE = " ";
      public const int STANDARD_KEYBOARD_NUMBER = 0;
      public const double VISIBLE_OPACITY = 1;
      public const string WIDTH_PROPERTY_NAME = "Width";

      public const string X_PROPERTY_NAME = "X";

      public const string Y_PROPERTY_NAME = "Y";

      public static readonly uint BUTTON_BOUNCE_MILLISECONDS = 125;

      public static readonly  double DEFAULT_CORNER_RADIUS_FACTOR = CalcCornerRadiusFactor( 0.075 );

      public static readonly double MEDIUM_CORNER_RADIUS_FACTOR = CalcCornerRadiusFactor( 0.125 );

      public static readonly double LARGE_CORNER_RADIUS_FACTOR = CalcCornerRadiusFactor( 0.175 );

      public static readonly double DEFAULT_ENTRY_HEIGHT =
         DeviceUtils_PI.IsIos() ? 35.0.AdjustForOsAndDevice() : 40.0.AdjustForOsAndDevice();

      public static readonly double DEFAULT_ENTRY_WIDTH                   = 275.00.AdjustForOsAndDevice();

      public static readonly double A_MARGIN_SPACING_SINGLE_FACTOR = 10.0.AdjustForOsAndDevice();

      public static readonly double DEFAULT_STACK_LAYOUT_SPACING = A_MARGIN_SPACING_SINGLE_FACTOR;

      public static readonly double MARGIN_SPACING_DOUBLE_FACTOR = A_MARGIN_SPACING_SINGLE_FACTOR * 2;

      public static readonly double MARGIN_SPACING_HALF_FACTOR = A_MARGIN_SPACING_SINGLE_FACTOR / 2;

      public static readonly double MARGIN_SPACING_MEDIUM_FACTOR = A_MARGIN_SPACING_SINGLE_FACTOR * 1.5;

      private static double CalcCornerRadiusFactor( double iosFactor )
      {
         return DeviceUtils_PI.IsIos() ? iosFactor : ( iosFactor * ANDROID_ADJUSTMENT );
      }
   }
}
