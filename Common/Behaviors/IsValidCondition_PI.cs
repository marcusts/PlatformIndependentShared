// *********************************************************************************
// Copyright @2022 Marcus Technical Services, Inc.
// <copyright
// file=IsValidCondition_PI.cs
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

namespace Com.MarcusTS.PlatformIndependentShared.Common.Behaviors
{
   using System;
   using System.Threading.Tasks;
   using Com.MarcusTS.ResponsiveTasks;
   using Com.MarcusTS.SharedUtils.Utils;
   using Com.MarcusTS.PlatformIndependentShared.Common.Interfaces;
   using Com.MarcusTS.PlatformIndependentShared.Common.Utils;
   using Com.MarcusTS.PlatformIndependentShared.ViewModels;

   public interface IIsValidCondition_PI : ICanBeValid_PI
   {
      Func<object, object, bool> IsValidFunc     { get; set; }
      string                     RuleDescription { get; set; }

      Task RevalidateSingleCondition(object targetObj, object compareObj);
   }

   public class IsValidCondition_PI : PropertyChangedBase_PI, IIsValidCondition_PI
   {
      public IThreadSafeAccessor        IsValid            { get; }      = new ThreadSafeAccessor(0);
      public IResponsiveTasks           IsValidChangedTask { get; set; } = new ResponsiveTasks(1);
      public Func<object, object, bool> IsValidFunc        { get; set; }

      public string RuleDescription { get; set; }

      public async Task RevalidateSingleCondition(object targetObj, object compareObj)
      {
         if (IsValidFunc.IsNotNullOrDefault())
         {
            await IsValid.SetIsTrueOrFalse(IsValidFunc.Invoke(targetObj, compareObj), IsValidChangedTask)
                         .WithoutChangingContext();
         }
      }

      public Task SetIsValid(bool isValid)
      {
         return IsValid.SetIsTrueOrFalse(isValid, IsValidChangedTask);
      }
   }
}